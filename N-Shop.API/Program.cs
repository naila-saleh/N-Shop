using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Web.Resource;
using N_Shop.API.Data;
using N_Shop.API.Services;
using Scalar.AspNetCore;

namespace N_Shop.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAdB2C"));

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddDbContext<ApplicationDbContext>(optoins => optoins.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<ICategoryService,CategoryService>();
        builder.Services.AddScoped<IBrandService,BrandService>();
        builder.Services.AddScoped<IProductService,ProductService>();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        // var context = new ApplicationDbContext();
        // try
        // {
        //     context.Database.CanConnect();
        //     Console.WriteLine("done");
        // }
        // catch (Exception e)
        // {
        //     Console.WriteLine("error");
        //     throw;
        // }
        //
        
        app.MapControllers();

        app.Run();
    }
}