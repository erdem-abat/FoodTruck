namespace FoodTruck.Domain.Entities
{
    public class FoodIngredient
    {
        public int IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }
        public int FoodId { get; set; }
        public virtual Food Food { get; set; }
    }
}
