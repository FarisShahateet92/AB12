using AB12.Domain.Base.Schema;

namespace AB12.Domain.Persistence
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            // Look for any products.
            if (context.Products.Any())
            {
                return;   // DB has been seeded
            }

            string randomImgUrl = "https://media.istockphoto.com/id/1443305526/photo/young-smiling-man-in-headphones-typing-on-laptop-keyboard.jpg?s=1024x1024&w=is&k=20&c=wcaAuEUMIScsLWVfI8bnuFx5zMSA7XzUs8Hcl07YFbo=";

            var products = new Product[]
            {
                // add one example for images to load for a random url to be stored in DB
                new Product{ Name="MacBook Pro", Price=1200.00m, Description="Apple's latest laptop", Quantity=10, 
                             ImageData= DownloadImageAsBytes(randomImgUrl).Result,
                             ImageMimeType = "image/jpeg"

                },

                new Product{ Name="iPhone", Price=800.00m, Description="Apple's latest smartphone", Quantity=30},
                new Product{ Name="iPad", Price=700.00m, Description="Apple's latest tablet", Quantity=40},
                new Product{ Name="iPod", Price=100.00m, Description="Apple's latest portable media player", Quantity=50},
                new Product{ Name="iMac", Price=1500.00m, Description="Apple's latest desktop computer", Quantity=20},
                new Product{ Name="Mac Mini", Price=800.00m, Description="Apple's latest desktop computer", Quantity=10},
                new Product{ Name="Mac Pro", Price=2000.00m, Description="Apple's latest desktop computer", Quantity=5},
                new Product{ Name="MacBook Air", Price=1200.00m, Description="Apple's latest laptop", Quantity=10},
            };

            context.Products.AddRange(products);
            
            context.SaveChanges();
        }

        private static async Task<byte[]?> DownloadImageAsBytes(string imageUrl) // For temp use
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(imageUrl);

                if (response.IsSuccessStatusCode)
                {
                    byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();
                    return imageBytes;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
