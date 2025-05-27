namespace SimpleToDoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var Items = new List<Models.Item>
            {
                new Models.Item { Id = 1, Title = "Learn C#", IsCompleted = false },
                new Models.Item { Id = 2, Title = "Build a web app", IsCompleted = false },
                new Models.Item { Id = 3, Title = "Deploy to production", IsCompleted = false }
            };
            builder.Services.AddScoped<Repository.IItemRepository, Repository.ItemRepository>(provider =>
            {
                return new Repository.ItemRepository(Items);
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

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

           // app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Todo}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
