using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete.EfCore
{
    public static class SeedData
    {
         public static void FillTestDatas(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();

            if (context != null)
            {
                if(context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                if(!context.Tags.Any())
                {
                    context.Tags.AddRange(
                        new Entity.Tag { Text = "Web Programlama" },
                        new Entity.Tag { Text = "Backend" },
                        new Entity.Tag { Text = "Frontend"},
                        new Entity.Tag { Text = "Full Stack" },
                        new Entity.Tag { Text = "PHP" }
                        );
                    context.SaveChanges();
                }

                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new Entity.User {  UserName="Ahmet Yılmaz" },
                        new Entity.User {  UserName = "Süphan Yakut" });
                    context.SaveChanges();
                }

                if (!context.Posts.Any())
                {
                    context.Posts.AddRange(
                        new Entity.Post { Title = "Asp.net Core", Content = "Asp.net Core dersleri", IsActive = true, PublishedOn = DateTime.Now.AddDays(-10), Tags = context.Tags.Take(3).ToList(), UserId = 1 },
                        new Entity.Post { Title = "PHP", Content = "PHP dersleri", IsActive = true, PublishedOn = DateTime.Now.AddDays(-20), Tags = context.Tags.Take(2).ToList(), UserId = 1 },
                        new Entity.Post { Title = "Django Dersleri", Content = "Tüm django dersleri ve içerikleri", IsActive = true, PublishedOn = DateTime.Now.AddDays(-5), Tags = context.Tags.Take(4).ToList(), UserId = 2 });

                    context.SaveChanges();
                }

            }

        }
    }
}
