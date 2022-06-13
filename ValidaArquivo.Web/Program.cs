using Microsoft.EntityFrameworkCore;
using ValidaArquivo.Data;
using ValidaArquivo.Data.Repositories;
using ValidaArquivo.Domain.Interfaces.Repository;
using ValidaArquivo.Domain.Services;
using ValidaArquivo.Web.BackgroundServices;

namespace ValidaArquivo.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ValidaArquivoContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("ValidaArquivoContext"),
                    options => options.MigrationsAssembly("ValidaArquivo.Web")));

            ConfigureServices(builder.Services);

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IArquivoUploadRepository, ArquivoUploadRepository>();
            services.AddScoped<IVirusTotalRepository, VirusTotalRepository>();

            services.AddScoped<ArquivoUploadService>();

            services.AddHostedService<VirusTotalResultService>();
        }
    }
}