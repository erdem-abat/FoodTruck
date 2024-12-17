using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML;

namespace FoodTruck.WebApi.Repositories.FoodRepository
{
    public class RecommendationService2
    {
        public class RatingInput
        {
            [LoadColumn(0)]
            public float userId { get; set; }
            [LoadColumn(1)]
            public float foodId { get; set; }
            [LoadColumn(2)]
            public float Label { get; set; }
        }

        public class Rating
        {
            public float Label { get; set; }
            public float Score { get; set; }
        }

        public void GetInfo()
        {
            MLContext mlContext = new MLContext();

            (IDataView trainingDataView, IDataView testDataView) = LoadData(mlContext);
            ITransformer model = BuildAndTrainModel(mlContext, trainingDataView);
            EvaluateModel(mlContext, testDataView, model);
            UseModelForSinglePrediction(mlContext, model);
        }

        (IDataView training, IDataView test) LoadData(MLContext mlContext)
        {
            var trainingDataPath = Path.Combine(Environment.CurrentDirectory, "TrainDatas/NewFolder", "ratings.csv");
            var testDataPath = Path.Combine(Environment.CurrentDirectory, "TrainDatas/NewFolder", "ratings-test.csv");

            IDataView trainingDataView = mlContext.Data.LoadFromTextFile<RatingInput>(trainingDataPath, hasHeader: true, separatorChar: ',');
            IDataView testDataView = mlContext.Data.LoadFromTextFile<RatingInput>(testDataPath, hasHeader: true, separatorChar: ',');

            return (trainingDataView, testDataView);
        }

        ITransformer BuildAndTrainModel(MLContext mlContext, IDataView trainingDataView)
        {
            IEstimator<ITransformer> estimator = mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "userIdEncoded", inputColumnName: "userId")
                                                 .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "foodIdEncoded", inputColumnName: "foodId"));

            var options = new MatrixFactorizationTrainer.Options
            {
                MatrixColumnIndexColumnName = "userIdEncoded",
                MatrixRowIndexColumnName = "foodIdEncoded",
                LabelColumnName = "Label",
                NumberOfIterations = 20,
                ApproximationRank = 100
            };

            var trainerEstimator = estimator.Append(mlContext.Recommendation().Trainers.MatrixFactorization(options));

            Console.WriteLine("=============== Training the model ===============");
            ITransformer model = trainerEstimator.Fit(trainingDataView);

            return model;
        }

        void EvaluateModel(MLContext mlContext, IDataView testDataView, ITransformer model)
        {
            Console.WriteLine("=============== Evaluating the model ===============");
            var prediction = model.Transform(testDataView);
            var metrics = mlContext.Regression.Evaluate(prediction, labelColumnName: "Label", scoreColumnName: "Score");
            Console.WriteLine("Root Mean Squared Error : " + metrics.RootMeanSquaredError.ToString());
            Console.WriteLine("RSquared: " + metrics.RSquared.ToString());
        }

        void UseModelForSinglePrediction(MLContext mlContext, ITransformer model)
        {
            Console.WriteLine("=============== Making a prediction ===============");
            var predictionEngine = mlContext.Model.CreatePredictionEngine<RatingInput, Rating>(model);
            var testInput = new RatingInput { userId = 39.3561390000000000f, foodId = 10 };

            var foodRatingPrediction = predictionEngine.Predict(testInput);

            if (Math.Round(foodRatingPrediction.Score, 1) > 3.5)
            {
                Console.WriteLine("Food " + testInput.foodId + " is recommended for user " + testInput.userId);
            }
            else
            {
                Console.WriteLine("Food " + testInput.foodId + " is not recommended for user " + testInput.userId);
            }
        }
    }
}