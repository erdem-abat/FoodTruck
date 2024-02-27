using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using FoodTruck.Dto.FoodDtos;
using FoodTruck.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Identity.Client;
using Newtonsoft.Json;

namespace FoodTruck.WebApi.Repositories.FoodRepository
{
    public class FoodRepository : IFoodRepository
    {
        private readonly FoodTruckContext _context;
        private IDistributedCache _distributedCache;
        public FoodRepository(FoodTruckContext context, IDistributedCache distributedCache)
        {
            _context = context;
            _distributedCache = distributedCache;
        }

        public async Task<List<Food>> GetFoodsWithCategory()
        {
            return await _context.Foods.Include(x => x.Category).ToListAsync();
        }
        public async Task<List<FoodWithAllDto>> GetFoodsWithAll()
        {
            //return await _context.Foods
            //    .Include(a => a.Country)
            //    .Include(b => b.Category)
            //    .Include(c => c.FoodMoods).ThenInclude(d => d.Mood)
            //    .Include(e => e.FoodTastes).ThenInclude(f => f.Taste)
            //    .ToListAsync();

            string key = "food";

            string? cachedMember = await _distributedCache.GetStringAsync(key);

            if (string.IsNullOrEmpty(cachedMember))
            {
                List<FoodWithAllDto> foods = await (from food in _context.Foods
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
                                                    select new FoodWithAllDto
                                                    {
                                                        Food = food,
                                                        Country = country,
                                                        Category = category,
                                                        FoodMood = foodMood,
                                                        FoodTaste = foodTaste,
                                                        Mood = mood,
                                                        Taste = taste
                                                    }).ToListAsync();

                var option = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(50));
                option.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2);

                await _distributedCache.SetStringAsync(
                    key,
                    JsonConvert.SerializeObject(foods, new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    }),
                    option
                    );

                return foods;
            }

            var cachedFoods = JsonConvert.DeserializeObject<List<FoodWithAllDto>>(cachedMember);

            if (cachedFoods != null)
            {
                _context.Foods.AttachRange(cachedFoods.Select(x => x.Food).ToList());
            }

            return cachedFoods.ToList();
        }

        public async Task<List<FoodWithAllDto>> GetFoodsByFilter(GetFoodsByFilterParameters getFoodsByFilterParameters)
        {

            var foods = (from food in _context.Foods
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
                         select new FoodWithAllDto
                         {
                             Food = food,
                             Country = country,
                             Category = category,
                             FoodMood = foodMood,
                             FoodTaste = foodTaste,
                             Mood = mood,
                             Taste = taste
                         }).AsNoTracking();

            if (getFoodsByFilterParameters.minPrice != null)
            {
                foods = foods.Where(x => x.Food.Price > getFoodsByFilterParameters.minPrice);
            }

            if (getFoodsByFilterParameters.maxPrice != null && getFoodsByFilterParameters.maxPrice > getFoodsByFilterParameters.minPrice)
            {
                foods = foods.Where(x => x.Food.Price < getFoodsByFilterParameters.maxPrice);
            }

            if (getFoodsByFilterParameters.categoryId != null)
            {
                foods = foods.Where(x => x.Category.CategoryId == getFoodsByFilterParameters.categoryId);
            }

            if (getFoodsByFilterParameters.countryId != null)
            {
                foods = foods.Where(x => x.Country.CountryId == getFoodsByFilterParameters.countryId);
            }

            if (getFoodsByFilterParameters.tasteId != null)
            {
                foods = foods.Where(x => x.Taste.TasteId == getFoodsByFilterParameters.tasteId);
            }

            if (getFoodsByFilterParameters.moodId != null)
            {
                foods = foods.Where(x => x.Mood.MoodId == getFoodsByFilterParameters.moodId);
            }

            List<FoodWithAllDto> responseList = foods.ToList();

            return responseList;
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