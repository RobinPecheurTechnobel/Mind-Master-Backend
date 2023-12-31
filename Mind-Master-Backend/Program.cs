using BLL.Interfaces;
using BLL.Services;
using DAL.Data;
using DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Connections;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Mind_Master_Backend.Services;
using System.Data.Common;
using System.Text;

namespace Mind_Master_Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthorization();

            // Entity Framework
            builder.Services.AddDbContext<MindMasterContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("local")));

            //DAL
            builder.Services.AddScoped<ThinkerRepository>();
            builder.Services.AddScoped<GroupRepository>();

            // BLL
            builder.Services.AddScoped<Argon2Service>();
            builder.Services.AddScoped<ThinkerService>();
            builder.Services.AddScoped<GroupService>();

            // API
            builder.Services.AddTransient<TokenService>();


            // Add JWT config
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["JwtOptions:Issuer"],
                        ValidAudience = builder.Configuration["JwtOptions:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:Secret"]))
                    };
                }
            );

            // Add Cors config
            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("MindCors", config =>
                {
                    // Limiter l'origine de la requete
                    config.WithOrigins("http://localhost:4200")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                            

                    // Tout autoriser
                    //config.AllowAnyOrigin();
                    //config.AllowAnyHeader();
                    //config.AllowAnyMethod();

                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("MindCors");

            app.MapControllers();

            app.Run();
        }
    }
}