using AutoMapper;
using EFCore.BulkExtensions;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using FoodTruck.Dto.FoodDtos;
using FoodTruck.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Npgsql;
using Stripe;
using System.Data;
using System.Data.OleDb;


namespace FoodTruck.WebApi.Repositories.FoodRepository
{
    public class FoodRepository : IFoodRepository
    {
        private readonly UserIdentityDbContext _context;
        private IDistributedCache _distributedCache;
        private IConfiguration _configuration;
        private IWebHostEnvironment _webHostEnvironment;
        private IMapper _mapper;

        public FoodRepository(UserIdentityDbContext context, IDistributedCache distributedCache, IConfiguration configuration, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _context = context;
            _distributedCache = distributedCache;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }
        public async Task<List<FoodDto>> GetFoodsAsync()
        {
            var foods = await _context.Foods.Select(x => new FoodDto
            {
                FoodId = x.FoodId,
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                CategoryId = x.CategoryId,
                CountryId = x.CountryId,
                Description = x.Description,
                Price = x.price
            }).ToListAsync();

            return foods;
        }
        public async Task<List<FoodDto>> GetFoodsWithPagingAsync(int page, int pageSize)
        {
            var foods = await _context.Foods.Select(x => new FoodDto
            {
                Description = x.Description,
                FoodId = x.FoodId,
                ImageLocalPath = x.ImageLocalPath,
                Name = x.Name,
                ImageUrl = x.ImageUrl,
            }).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return foods;
        }
        public async Task<List<Food>> GetFoodsWithCategoryAsync()
        {
            return await _context.Foods.Include(x => x.Category).ToListAsync();
        }
        public async Task<List<FoodWithAllDto>> GetFoodsWithAllAsync(CancellationToken cancellationToken)
        {
            string key = "food";

            string? cachedMember = await _distributedCache.GetStringAsync(key, cancellationToken);

            if (string.IsNullOrEmpty(cachedMember))
            {
                #region oldQuery
                //var foodData = await (from food in _context.Foods
                //                      join country in _context.Countries on food.CountryId equals country.CountryId
                //                      join category in _context.Categories on food.CategoryId equals category.CategoryId
                //                      join foodMood in _context.FoodMood on food.FoodId equals foodMood.FoodId into foodMoodGroup
                //                      from foodMood in foodMoodGroup.DefaultIfEmpty()
                //                      join mood in _context.Moods on foodMood.MoodId equals mood.MoodId into moodGroup
                //                      from mood in moodGroup.DefaultIfEmpty()
                //                      join foodTaste in _context.FoodTaste on food.FoodId equals foodTaste.FoodId into foodTasteGroup
                //                      from foodTaste in foodTasteGroup.DefaultIfEmpty()
                //                      join taste in _context.Tastes on foodTaste.TasteId equals taste.TasteId into tasteGroup
                //                      from taste in tasteGroup.DefaultIfEmpty()
                //                      join foodIngredient in _context.FoodIngredient on food.FoodId equals foodIngredient.FoodId into foodIngredientGroup
                //                      from foodIngredient in foodIngredientGroup.DefaultIfEmpty()
                //                      join ingredient in _context.Ingredients on foodIngredient.IngredientId equals ingredient.IngredientId into ingredientGroup
                //                      from ingredient in ingredientGroup.DefaultIfEmpty()
                //                      select new
                //                      {
                //                          Food = food,
                //                          Country = country,
                //                          Category = category,
                //                          FoodMood = foodMood,
                //                          Mood = mood,
                //                          FoodTaste = foodTaste,
                //                          Taste = taste,
                //                          FoodIngredient = foodIngredient,
                //                          Ingredient = ingredient // Collect individual ingredients
                //                      }).ToListAsync();

                //// Grouping in-memory to return all ingredients for each food
                //var foodWithIngredients = foodData
                //    .GroupBy(f => f.Food.FoodId) // Group by FoodId
                //    .Select(group => new FoodWithAllDto
                //    {
                //        Food = group.First().Food,
                //        Country = group.First().Country,
                //        Category = group.First().Category,
                //        FoodMood = group.First().FoodMood,
                //        Mood = group.First().Mood,
                //        FoodTaste = group.First().FoodTaste,
                //        Taste = group.First().Taste,
                //        FoodIngredient = group.First().FoodIngredient,
                //        Ingredients = group
                //                      .Where(f => f.Ingredient != null)
                //                      .Select(f => f.Ingredient) // Select only ingredients
                //                      .ToList() // Convert to List
                //    })
                //    .ToList();
                #endregion

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
                                   join foodIngredient in _context.FoodIngredient on food.FoodId equals foodIngredient.FoodId into foodIngredientGroup
                                   from foodIngredient in foodIngredientGroup.DefaultIfEmpty()
                                   join ingredient in _context.Ingredients on foodIngredient.IngredientId equals ingredient.IngredientId into ingredientGroup
                                   from ingredient in ingredientGroup.DefaultIfEmpty()
                                   select new
                                   {
                                       Food = food,
                                       Country = country,
                                       Category = category,
                                       FoodMood = foodMood,
                                       Mood = mood,
                                       FoodTaste = foodTaste,
                                       Taste = taste,
                                       FoodIngredient = foodIngredient,
                                       Ingredient = ingredient
                                   })
                   .GroupBy(f => f.Food.FoodId)
                   .Select(g => new FoodWithAllDto
                   {
                       Food = g.First().Food,
                       Country = g.First().Country,
                       Category = g.First().Category,
                       FoodMood = g.First().FoodMood,
                       FoodTaste = g.First().FoodTaste,
                       FoodIngredient = g.First().FoodIngredient,
                       Ingredients = g.Where(f => f.Ingredient != null).Select(f => f.Ingredient).Distinct().ToList(),
                       Moods = g.Where(f => f.Mood != null).Select(f => f.Mood).Distinct().ToList(),
                       Tastes = g.Where(f => f.Taste != null).Select(f => f.Taste).Distinct().ToList()
                   })
                   .ToListAsync();

                var option = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(50));
                option.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2);

                await _distributedCache.SetStringAsync(
                    key,
                    JsonConvert.SerializeObject(foods, new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    }),
                    option,
                    cancellationToken
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

        public async Task<List<FoodWithAllDto>> GetFoodsByFilterAsync(GetFoodsByFilterParameters getFoodsByFilterParameters)
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
                         join foodIngredient in _context.FoodIngredient on food.FoodId equals foodIngredient.FoodId into foodIngredientGroup
                         from foodIngredient in foodIngredientGroup.DefaultIfEmpty()
                         join ingredient in _context.Ingredients on foodIngredient.IngredientId equals ingredient.IngredientId into ingredientGroup
                         from ingredient in ingredientGroup.DefaultIfEmpty()
                         select new
                         {
                             Food = food,
                             Country = country,
                             Category = category,
                             FoodMood = foodMood,
                             Mood = mood,
                             FoodTaste = foodTaste,
                             Taste = taste,
                             FoodIngredient = foodIngredient,
                             Ingredient = ingredient
                         })
                   .GroupBy(f => f.Food.FoodId)
                   .Select(g => new FoodWithAllDto
                   {
                       Food = g.First().Food,
                       Country = g.First().Country,
                       Category = g.First().Category,
                       FoodMood = g.First().FoodMood,
                       FoodTaste = g.First().FoodTaste,
                       FoodIngredient = g.First().FoodIngredient,
                       Ingredients = g.Where(f => f.Ingredient != null).Select(f => f.Ingredient).Distinct().ToList(),
                       Moods = g.Where(f => f.Mood != null).Select(f => f.Mood).Distinct().ToList(),
                       Tastes = g.Where(f => f.Taste != null).Select(f => f.Taste).Distinct().ToList()
                   })
                   .AsNoTracking();

            //if (getFoodsByFilterParameters.minPrice != null)
            //{
            //    foods = foods.Where(x => x.Food.Price > getFoodsByFilterParameters.minPrice);
            //}

            //if (getFoodsByFilterParameters.maxPrice != null && getFoodsByFilterParameters.maxPrice > getFoodsByFilterParameters.minPrice)
            //{
            //    foods = foods.Where(x => x.Food.Price < getFoodsByFilterParameters.maxPrice);
            //}

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
                foods = foods.Where(x => x.Tastes.Any(t => t.TasteId == getFoodsByFilterParameters.tasteId));
            }

            if (getFoodsByFilterParameters.moodId != null)
            {
                foods = foods.Where(f => f.Moods.Any(m => m.MoodId == getFoodsByFilterParameters.moodId));
            }

            List<FoodWithAllDto> responseList = foods.ToList();

            return responseList;
        }

        public async Task<List<(string name, string imageUrl)>> GetFoodMainPageInfoAsync()
        {
            var values = await (from food in _context.Foods
                                select new
                                {
                                    food.Name,
                                    food.ImageUrl
                                }).ToListAsync();

            return values.Select(x => (x.Name, x.ImageUrl)).ToList();
        }

        public async Task<string> CreateFoodToRestaurantAsync(Food food, List<int> foodMoodIds, List<int> foodTasteIds, List<int> foodIngredientIds, int restaurantId, string userId, double price)
        {
            try
            {
                RestaurantUser restaurantUserFromDb = _context.RestaurantUsers.First(x => x.UserId == userId && x.RestaurantId == restaurantId);

                if (restaurantUserFromDb != null)
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

                    foreach (var item in foodTasteIds)
                    {
                        var taste = new FoodTaste
                        {
                            TasteId = item,
                            FoodId = foodFromDb.FoodId
                        };

                        _context.FoodTaste.Add(taste);
                    }

                    foreach (var item in foodIngredientIds)
                    {
                        var ingredient = new FoodIngredient
                        {
                            IngredientId = item,
                            FoodId = foodFromDb.FoodId
                        };

                        _context.FoodIngredient.Add(ingredient);
                    }

                    await _context.SaveChangesAsync();

                    FoodRestaurant foodRestaurant = await _context.FoodRestaurant.FirstOrDefaultAsync(x => x.FoodId == foodFromDb.FoodId && x.RestaurantId == restaurantId);

                    if (foodRestaurant != null)
                    {
                        foodRestaurant.Price = price != 0 ? price : foodRestaurant.Price;
                        _context.FoodRestaurant.Update(foodRestaurant);

                        await _context.SaveChangesAsync();
                    }

                    else
                    {
                        _context.FoodRestaurant.Add(new FoodRestaurant
                        {
                            FoodId = foodFromDb.FoodId,
                            RestaurantId = restaurantId,
                            Price = price != 0 ? price : foodRestaurant.Price
                        });

                        await _context.SaveChangesAsync();
                    }

                    return "Food has been created!";
                }

                return "There is an error.";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Food> CreateFoodAsync(Food food, List<int> foodMoodIds, List<int> foodTasteIds, List<int> foodIngredientIds)
        {
            try
            {
                var ingredients = await _context.Ingredients
                                        .Where(i => foodIngredientIds.Contains(i.IngredientId))
                                        .ToListAsync();

                food.price = ingredients.Sum(x => x.price);

                _context.Foods.Add(food);

                if (food.Image != null)
                {
                    string fileName = food.Name +  Path.GetExtension(food.Image.FileName);
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

                var foodMoods = foodMoodIds.Select(moodId => new FoodMood
                {
                    MoodId = moodId,
                    Food = food
                }).ToList();

                var foodTastes = foodTasteIds.Select(tasteId => new FoodTaste
                {
                    TasteId = tasteId,
                    Food = food
                }).ToList();

                var foodIngredients = foodIngredientIds.Select(ingredientId => new FoodIngredient
                {
                    IngredientId = ingredientId,
                    Food = food
                }).ToList();

                _context.FoodMood.AddRange(foodMoods);
                _context.FoodTaste.AddRange(foodTastes);
                _context.FoodIngredient.AddRange(foodIngredients);

                await _context.SaveChangesAsync();

                return food;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> DocumentUploadAsync(IFormFile formFile)
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

        public async Task<DataTable> FoodDataTableAsync(string path)
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

        public async Task<bool> ImportFoodAsync(DataTable food)
        {
            var sqlconn = _configuration.GetConnectionString("AppDbConnectionString");

            #region SQL
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
            #endregion

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

        public async Task<bool> ImportDataFromExcelAsync(Dictionary<string, DataTable> excelData, string userId, int restaurantId)
        {
            var sqlconn = _configuration.GetConnectionString("AppDbConnectionString");

            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    // Dictionary to store Food IDs
                    Dictionary<string, int> foodIds = new Dictionary<string, int>();

                    var restaurant = _context.Restaurant.First(x => x.RestaurantId == restaurantId);
                    var userData = _context.RestaurantUsers.First(x => x.UserId == userId && x.RestaurantId == restaurant.RestaurantId);
                    var restaurantFoodIds = _context.FoodRestaurant.Where(x => x.RestaurantId == userData.RestaurantId).Select(x => x.FoodId).ToList();

                    // List to hold entities for bulk insert
                    List<Food> newFoods = new List<Food>();
                    List<FoodIngredient> newIngredients = new List<FoodIngredient>();
                    List<FoodTaste> newTastes = new List<FoodTaste>();

                    if (excelData.ContainsKey("Foods"))
                    {
                        foreach (DataRow row in excelData["Foods"].Rows)
                        {
                            // Step 1: Check if the food already exists in the database
                            var existingFood = _context.Foods.FirstOrDefault(f => f.Name == row["Name"].ToString());

                            if (existingFood != null && restaurantFoodIds.Contains(existingFood.FoodId))
                            {
                                foodIds.Add(row["Name"].ToString(), existingFood.FoodId);
                                continue; // Skip the insert as the food already exists
                            }

                            // Step 2: Prepare new food for bulk insert
                            var newFood = new Food
                            {
                                Name = row["Name"].ToString(),
                                Description = row["Description"].ToString(),
                                ImageUrl = row["ImageUrl"].ToString(),
                                CountryId = Convert.ToInt32(row["CountryId"]),
                                CategoryId = Convert.ToInt32(row["CategoryId"])
                            };

                            newFoods.Add(newFood);
                        }

                        // Perform bulk insert for new foods
                        if (newFoods.Count > 0)
                        {
                            await _context.BulkInsertAsync(newFoods);
                            await _context.SaveChangesAsync();

                            // Add the inserted food IDs to the dictionary for future use
                            foreach (var food in newFoods)
                            {
                                foodIds.Add(food.Name, food.FoodId);
                            }
                        }
                    }

                    // Bulk insert for Ingredients
                    if (excelData.ContainsKey("Ingredients"))
                    {
                        foreach (DataRow row in excelData["Ingredients"].Rows)
                        {
                            string foodName = row["FoodName"].ToString();
                            if (foodIds.TryGetValue(foodName, out int foodId))
                            {
                                var newIngredient = new FoodIngredient
                                {
                                    FoodId = foodId,
                                    IngredientId = Convert.ToInt32(row["IngredientId"])
                                };

                                newIngredients.Add(newIngredient);
                            }
                        }

                        if (newIngredients.Count > 0)
                        {
                            await _context.BulkInsertAsync(newIngredients);
                            await _context.SaveChangesAsync();
                        }
                    }

                    // Bulk insert for Tastes
                    if (excelData.ContainsKey("Tastes"))
                    {
                        foreach (DataRow row in excelData["Tastes"].Rows)
                        {
                            string foodName = row["FoodName"].ToString();
                            if (foodIds.TryGetValue(foodName, out int foodId))
                            {
                                var newTaste = new FoodTaste
                                {
                                    FoodId = foodId,
                                    TasteId = Convert.ToInt32(row["TasteId"])
                                };

                                newTastes.Add(newTaste);
                            }
                        }

                        if (newTastes.Count > 0)
                        {
                            await _context.BulkInsertAsync(newTastes);
                            await _context.SaveChangesAsync();
                        }
                    }

                    await transaction.CommitAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //// Step 1: Upload the Excel document
        //string path = await DocumentUpload(formFile);

        //// Step 2: Load data from the Excel file
        //var excelData = await LoadExcelData(path);

        //// Step 3: Import the data into the database
        //bool success = await ImportDataFromExcel(excelData);

        //public async Task<Dictionary<string, DataTable>> LoadExcelData(string path)
        //{
        //    var conStr = _configuration.GetConnectionString("ExcelConnectionString");
        //    conStr = string.Format(conStr, path);

        //    var tables = new Dictionary<string, DataTable>();

        //    using (OleDbConnection excelconn = new OleDbConnection(conStr))
        //    {
        //        excelconn.Open();

        //        // Get the schema information for all sheets in the Excel file
        //        DataTable excelSchema = excelconn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

        //        // Loop through all sheets and load each sheet into a separate DataTable
        //        foreach (DataRow sheet in excelSchema.Rows)
        //        {
        //            string sheetName = sheet["TABLE_NAME"].ToString();
        //            if (!sheetName.EndsWith("$")) continue; // Skip non-data sheets

        //            using (OleDbCommand cmd = new OleDbCommand($"SELECT * FROM [{sheetName}]", excelconn))
        //            {
        //                using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
        //                {
        //                    DataTable dataTable = new DataTable();
        //                    adapter.Fill(dataTable);
        //                    tables.Add(sheetName.TrimEnd('$'), dataTable); // Add each DataTable to dictionary, using the sheet name as the key
        //                }
        //            }
        //        }
        //        excelconn.Close();
        //    }

        //    return tables; // Dictionary containing each sheet's data
        //}

        public async Task<FoodWithAllDto> GetFoodByIdAsync(int foodId, CancellationToken cancellationToken)
        {
            return await (from food in _context.Foods
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
                    join foodIngredient in _context.FoodIngredient on food.FoodId equals foodIngredient.FoodId into foodIngredientGroup
                    from foodIngredient in foodIngredientGroup.DefaultIfEmpty()
                    join ingredient in _context.Ingredients on foodIngredient.IngredientId equals ingredient.IngredientId into ingredientGroup
                    from ingredient in ingredientGroup.DefaultIfEmpty()
                    where food.FoodId == foodId
                    select new
                    {
                        Food = food,
                        Country = country,
                        Category = category,
                        FoodMood = foodMood,
                        Mood = mood,
                        FoodTaste = foodTaste,
                        Taste = taste,
                        FoodIngredient = foodIngredient,
                        Ingredient = ingredient
                    })
                   .GroupBy(f => f.Food.FoodId)
                   .Select(g => new FoodWithAllDto
                   {
                       Food = g.First().Food,
                       Country = g.First().Country,
                       Category = g.First().Category,
                       FoodMood = g.First().FoodMood,
                       FoodTaste = g.First().FoodTaste,
                       FoodIngredient = g.First().FoodIngredient,
                       Ingredients = g.Where(f => f.Ingredient != null).Select(f => f.Ingredient).Distinct().ToList(),
                       Moods = g.Where(f => f.Mood != null).Select(f => f.Mood).Distinct().ToList(),
                       Tastes = g.Where(f => f.Taste != null).Select(f => f.Taste).Distinct().ToList()
                   })
                   .FirstOrDefaultAsync(cancellationToken) ?? new FoodWithAllDto();
        }
    }
}