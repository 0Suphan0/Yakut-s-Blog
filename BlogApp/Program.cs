using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BlogContext>(options =>
{
    var connString = builder.Configuration.GetConnectionString("mssql_connection");
    //options.UseSqlite(connString);
    options.UseSqlServer(connString);
});

builder.Services.AddScoped<IPostRepository, EfPostRepository>();
builder.Services.AddScoped<ITagRepository, EfTagRepository>();
builder.Services.AddScoped<ICommentRepository, EfCommentRepository>();



var app = builder.Build();

SeedData.FillTestDatas(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "post_details",
    pattern: "posts/details/{url}",
    defaults: new {controller="Posts", action="PostDetail"});

app.MapControllerRoute(
    name: "post_by_tag",
    pattern: "posts/tag/{tag}",
    defaults: new { controller = "Posts", action = "Index" });



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Posts}/{action=Index}/{id?}");

app.Run();
