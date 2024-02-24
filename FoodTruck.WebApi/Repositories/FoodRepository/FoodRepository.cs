using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using FoodTruck.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace FoodTruck.WebApi.Repositories.FoodRepository
{
    public class FoodRepository : IFoodRepository
    {
        private readonly FoodTruckContext _context;

        public FoodRepository(FoodTruckContext context)
        {
            _context = context;
        }

        public async Task<List<Food>> GetFoodsWithCategory()
        {
            return await _context.Foods.Include(x => x.Category).ToListAsync();
        }
        public async Task<List<Food>> GetFoodsWithAll()
        {
            //return await _context.Foods
            //    .Include(a => a.Country)
            //    .Include(b => b.Category)
            //    .Include(c => c.FoodMoods).ThenInclude(d => d.Mood)
            //    .Include(e => e.FoodTastes).ThenInclude(f => f.Taste)
            //    .ToListAsync();

            var foods = await (from food in _context.Foods
                               join country in _context.Countries on food.CountryId equals country.CountryId 
                               join category in _context.Categories on food.CategoryId equals category.CategoryId
                               join foodMood in _context.FoodMood on food.FoodId equals foodMood.FoodId into foodMoodGroup
                               from foodMood in foodMoodGroup.DefaultIfEmpty()
                               join mood in _context.Moods on foodMood.MoodId equals mood.MoodId into moodGroup
                               from mood in moodGroup.DefaultIfEmpty()
                               join foodTaste in _context.FoodTaste on food.FoodId equals foodTaste.FoodId into foodTasteGroup
                               from foodTaste in foodTasteGroup.DefaultIfEmpty()
                               join taste in _context.Tastes on foodTaste.TasteId equals taste.TasteId into tasteGroup
                               from taste in tasteGroup.DefaultIfEmpty()
                               select new
                               {
                                   Food = food,
                                   Country = country,
                                   Category = category,
                                   FoodMood = foodMood,
                                   FoodTaste = foodTaste,
                                   Mood = mood,
                                   Taste = taste
                               }).ToListAsync();
            
            return foods.Select(x => x.Food).ToList();
        }
        public async Task<List<(string name, double price, string imageUrl)>> GetFoodMainPageInfo()
        {
            var values = await (from food in _context.Foods
                                select new
                                {
                                    food.Name,
                                    food.Price,
                                    food.ImageUrl
                                }).ToListAsync();

            return values.Select(x => (x.Name, x.Price, x.ImageUrl)).ToList();
        }
    }
}