using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BlogApp.Data.Concrete.EfCore
{
    public static class SeedData
    {
        public static void FillTestDatas(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();

            if (context != null)
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                if (!context.Tags.Any())
                {
                    context.Tags.AddRange(
                        new Entity.Tag { Text = "Web Programlama" ,Url = "web-programlama"},
                        new Entity.Tag { Text = "Backend", Url = "backend" },
                        new Entity.Tag { Text = "Frontend", Url = "frontend" },
                        new Entity.Tag { Text = "Full Stack", Url = "fullstack" },
                        new Entity.Tag { Text = "PHP", Url = "php" }
                    );
                    context.SaveChanges();
                }

                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new Entity.User { UserName = "Ahmet Yılmaz" },
                        new Entity.User { UserName = "Süphan Yakut" }
                    );
                    context.SaveChanges();
                }

                if (!context.Posts.Any())
                {
                    context.Posts.AddRange(
                        new Entity.Post { Title = "Asp.net Core", Url = "aspnet-core",Content = "Asp.net Core dersleri", IsActive = true, PublishedOn = DateTime.Now.AddDays(-10), Tags = context.Tags.Take(3).ToList(), UserId = 1,Image="1.jpg"},
                        new Entity.Post { Title = "PHP", Content = "PHP dersleri", Url = "php", IsActive = true, PublishedOn = DateTime.Now.AddDays(-20), Tags = context.Tags.Take(2).ToList(), UserId = 1, Image = "2.jpg" },
                        new Entity.Post { Title = "Django Dersleri", Url = "django", Content = "Tüm django dersleri ve içerikleri", IsActive = true, PublishedOn = DateTime.Now.AddDays(-5), Tags = context.Tags.Take(4).ToList(), UserId = 2, Image = "3.jpg" },
                        new Entity.Post { Title = "React Dersleri", Url = "react-dersleri", Content = "Tüm react dersleri ve içerikleri", IsActive = true, PublishedOn = DateTime.Now.AddDays(-10), Tags = context.Tags.Take(4).ToList(), UserId = 2, Image = "3.jpg" },
                        new Entity.Post { Title = "Angular Dersleri", Url = "angular", Content = "Tüm Angular dersleri ve içerikleri", IsActive = true, PublishedOn = DateTime.Now.AddDays(-20), Tags = context.Tags.Take(4).ToList(), UserId = 2, Image = "3.jpg" },
                        new Entity.Post { Title = "Web Tasarım Dersleri", Url = "web-tasarım", Content = "Tüm web tasarımı dersleri ve içerikleri", IsActive = true, PublishedOn = DateTime.Now.AddDays(-30), Tags = context.Tags.Take(4).ToList(), UserId = 2, Image = "3.jpg" }


                    );
                    context.SaveChanges();
                }

              
            }
        }
    }
}
