using AutoMapper;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using FoodTruck.Dto.FoodDtos;
using FoodTruck.WebApi.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Npgsql;
using System.Data;
using System.Data.OleDb;


namespace FoodTruck.WebApi.Repositories.FoodRepository
{
    public class FoodRepository : IFoodRepository
    {
        private readonly FoodTruckContext _context;
        private IDistributedCache _distributedCache;
        private IConfiguration _configuration;
        private IWebHostEnvironment _webHostEnvironment;
        private IMapper _mapper;

        public FoodRepository(FoodTruckContext context, IDistributedCache distributedCache, IConfiguration configuration, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _context = context;
            _distributedCache = distributedCache;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }
        public async Task<List<FoodDto>> GetFoods()
        {
            var foods = await _context.Foods.Select(x => new FoodDto
            {
                FoodId = x.FoodId,
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                Price = x.Price,
                CategoryId = x.CategoryId,
                CountryId = x.CountryId,
                Description = x.Description
            }).ToListAsync();

            return foods;
        }
        public async Task<List<FoodDto>> GetFoodsWithPaging(int page, int pageSize)
        {
            var foods = await _context.Foods.Select(x => new FoodDto
            {
                Description = x.Description,
                FoodId = x.FoodId,
                ImageLocalPath = x.ImageLocalPath,
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                Price = x.Price
            }).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return foods;
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

        public async Task<Food> CreateFood(Food food, List<int> foodMoodIds, List<int> foodTasteIds)
        {
            try
            {
                _context.Foods.Add(food);
                await _context.SaveChangesAsync();

                if (food.Image != null)
                {
                    string fileName = food.FoodId + Path.GetExtension(food.Image.FileName);
                    string filePath = @"wwwroot\FoodImages\" + fileName;
                    var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                    using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                    {
                        food.Image.CopyTo(fileStream);
                    }

                    food.ImageUrl = "/FoodImages/" + fileName;
                    food.ImageLocalPath = filePath;
                }
                else
                {
                    food.ImageUrl = "https://placehold.co/600x400";
                }
                _context.Foods.Update(food);
                await _context.SaveChangesAsync();

                Food foodFromDb = _context.Foods.First(x => x.Name.ToLower() == food.Name.ToLower());

                foreach (var item in foodMoodIds)
                {
                    var mood = new FoodMood
                    {
                        MoodId = item,
                        FoodId = foodFromDb.FoodId
                    };

                    _context.FoodMood.Add(mood);
                }

                await _context.SaveChangesAsync();

                foreach (var item in foodTasteIds)
                {
                    var taste = new FoodTaste
                    {
                        TasteId = item,
                        FoodId = foodFromDb.FoodId
                    };

                    _context.FoodTaste.Add(taste);
                }

                await _context.SaveChangesAsync();

                return food;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> DocumentUpload(IFormFile formFile)
        {
            string uploadPath = _webHostEnvironment.WebRootPath;
            string dest_path = Path.Combine(uploadPath, "uploaded_doc");
            if (!Directory.Exists(dest_path))
            {
                Directory.CreateDirectory(dest_path);
            }

            string sourceFile = Path.GetFileName(formFile.FileName);
            string path = Path.Combine(dest_path, sourceFile);

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                formFile.CopyTo(fileStream);
            }

            return path;
        }

        public async Task<DataTable> FoodDataTable(string path)
        {
            var conStr = _configuration.GetConnectionString("ExcelConnectionString");
            DataTable dataTable = new DataTable();
            conStr = string.Format(conStr, path);
            using (OleDbConnection excelconn = new OleDbConnection(conStr))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    using (OleDbDataAdapter adapterexcel = new OleDbDataAdapter())
                    {
                        excelconn.Open();
                        cmd.Connection = excelconn;
                        DataTable excelshema;
                        excelshema = excelconn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        var sheetname = excelshema.Rows[0]["Table_Name"].ToString();
                        excelconn.Close();

                        excelconn.Open();
                        cmd.CommandText = "Select * from [" + sheetname + "]";
                        adapterexcel.SelectCommand = cmd;
                        adapterexcel.Fill(dataTable);
                        excelconn.Close();

                    }
                }
            }
            return dataTable;
        }

        public async Task<bool> ImportFood(DataTable food)
        {
            var sqlconn = _configuration.GetConnectionString("AppDbConnectionString");

            //using (SqlConnection scon = new SqlConnection(sqlconn))
            //{
            //    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(scon))
            //    {
            //        sqlBulkCopy.DestinationTableName = "Foods";
            //        sqlBulkCopy.ColumnMappings.Add("Name", "Name");
            //        sqlBulkCopy.ColumnMappings.Add("Price", "Price");
            //        sqlBulkCopy.ColumnMappings.Add("Description", "Description");
            //        sqlBulkCopy.ColumnMappings.Add("CountryId", "CountryId");
            //        sqlBulkCopy.ColumnMappings.Add("CategoryId", "CategoryId");

            //        scon.Open();
            //        sqlBulkCopy.WriteToServer(food);
            //        scon.Close();
            //    }
            //}

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(sqlconn))
                {
                    conn.Open();

                    using (var writer = conn.BeginBinaryImport("COPY \"Foods\" (\"Name\", \"Price\", \"Description\", \"ImageUrl\", \"ImageLocalPath\", \"CountryId\", \"CategoryId\") FROM STDIN (FORMAT BINARY)"))
                    {
                        foreach (DataRow row in food.Rows)
                        {
                            writer.StartRow();

                            writer.Write(row["Name"], NpgsqlTypes.NpgsqlDbType.Text);
                            writer.Write(row["Price"], NpgsqlTypes.NpgsqlDbType.Double);
                            writer.Write(row["Description"], NpgsqlTypes.NpgsqlDbType.Text);
                            writer.Write(row["ImageUrl"], NpgsqlTypes.NpgsqlDbType.Text);
                            writer.Write(row["ImageLocalPath"], NpgsqlTypes.NpgsqlDbType.Text);
                            writer.Write(row["CountryId"], NpgsqlTypes.NpgsqlDbType.Bigint);
                            writer.Write(row["CategoryId"], NpgsqlTypes.NpgsqlDbType.Bigint);

                        }
                        writer.Complete();
                        conn.Close();

                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<FoodWithAllDto> GetFoodById(int foodId)
        {

            var data = (from food in _context.Foods
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
                        where food.FoodId == foodId
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

            return data.FirstOrDefault();
        }
    }
}