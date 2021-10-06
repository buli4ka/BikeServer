using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CarShop.Database;
using CarShop.Models;
using CarShop.Models.CarAttributes;
using CarShop.Repositories.Implementation;
using CarShop.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace CarShop
{
    public class Startup
    {
        private const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), Configuration["ImageFolder"]));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = TokenConfig.TokenConfig.ISSUER,
                        ValidateAudience = true,
                        ValidAudience = TokenConfig.TokenConfig.AUDIENCE,
                        ValidateLifetime = true,
                        IssuerSigningKey = TokenConfig.TokenConfig.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                });


            services.AddControllers().AddNewtonsoftJson(x =>
                x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "CarShop", Version = "v1"}); });
            services.AddDbContext<ShopContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder => { builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod(); });
            });

            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            services.AddTransient<IBaseRepository<Order>, BaseRepository<Order>>();
            services.AddTransient<IBaseRepository<Car>, BaseRepository<Car>>();
            services.AddTransient<IBaseRepository<Cart>, BaseRepository<Cart>>();
            services.AddTransient<IBaseRepository<Comfort>, BaseRepository<Comfort>>();
            services.AddTransient<IBaseRepository<Security>, BaseRepository<Security>>();
            services.AddTransient<IBaseRepository<Transmission>, BaseRepository<Transmission>>();
            services.AddTransient<IBaseRepository<WheelDrive>, BaseRepository<WheelDrive>>();
            services.AddTransient<IBaseRepository<Manufacturer>, BaseRepository<Manufacturer>>();
            services.AddTransient<IBaseRepository<Image>, BaseRepository<Image>>();
            services.AddTransient<IBaseRepository<Engine>, BaseRepository<Engine>>();
            services.AddTransient<ICarRepository, CarRepository>();
            services.AddTransient<IBaseRepository<User>, BaseRepository<User>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarShop v1"));
            }

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider =
                    new PhysicalFileProvider(
                        Path.Combine(Directory.GetCurrentDirectory(), Configuration["ImageFolder"])),
                RequestPath = new PathString("/" + Configuration["ImageFolder"])
            });
            app.UseCors(MyAllowSpecificOrigins);
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app
                .UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}