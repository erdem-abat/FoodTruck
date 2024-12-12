using FoodTruck.WebApi.Models;
using Microsoft.ML;
using Microsoft.ML.Trainers;

namespace FoodTruck.WebApi.Repositories.FoodRepository
{
    public class RecommendationService
    {
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
            var trainingDataPath = Path.Combine(Environment.CurrentDirectory, "TrainDatas", "recommendation-ratings-train.csv");
            var testDataPath = Path.Combine(Environment.CurrentDirectory, "TrainDatas", "recommendation-ratings-test.csv");

            IDataView trainingDataView = mlContext.Data.LoadFromTextFile<FoodRating>(trainingDataPath, hasHeader: true, separatorChar: ',');
            IDataView testDataView = mlContext.Data.LoadFromTextFile<FoodRating>(testDataPath, hasHeader: true, separatorChar: ',');

            return (trainingDataView, testDataView);
        }

        ITransformer BuildAndTrainModel(MLContext mlContext, IDataView trainingDataView)
        {
            IEstimator<ITransformer> estimator = mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "orderIdEncoded", inputColumnName: "orderId")
                                                 .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "foodIdEncoded", inputColumnName: "foodId"));

            var options = new MatrixFactorizationTrainer.Options
            {
                MatrixColumnIndexColumnName = "orderIdEncoded",
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
            var predictionEngine = mlContext.Model.CreatePredictionEngine<FoodRating, FoodRatingPrediction>(model);
            var testInput = new FoodRating { orderId = 50, foodId = 7 };

            var foodRatingPrediction = predictionEngine.Predict(testInput);

            if (Math.Round(foodRatingPrediction.Score, 1) > 3.5)
            {
                Console.WriteLine("Food " + testInput.foodId + " is recommended for order " + testInput.orderId);
            }
            else
            {
                Console.WriteLine("Food " + testInput.foodId + " is not recommended for order " + testInput.orderId);
            }
        }
    }
}
