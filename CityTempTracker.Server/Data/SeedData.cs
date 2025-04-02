namespace CityTempTracker.Server.Data
{
    namespace CityTempTracker.Server.Data
    {
        public static class SeedData
        {
            public static void Initialize(AppDbContext context)
            {
                if (context.Cities.Any())
                    return;

                var cities = new List<City>
            {
                new City { Name = "London", Country = "GB" },
                new City { Name = "Paris", Country = "FR" },
                new City { Name = "Stockholm", Country = "SE" }
            };

                context.Cities.AddRange(cities);
                context.SaveChanges();
            }
        }
    }

}