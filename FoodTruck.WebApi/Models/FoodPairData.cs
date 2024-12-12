using Microsoft.ML.Data;

namespace FoodTruck.WebApi.Models
{
    public class FoodRating
    {
        [LoadColumn(0)]
        public float orderId;
        [LoadColumn(1)]
        public float foodId;
        [LoadColumn(2)]
        public float Label;
    }

    public class FoodRatingPrediction
    {
        public float Label;
        public float Score;
    }
}
