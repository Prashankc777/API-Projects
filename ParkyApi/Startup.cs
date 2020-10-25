using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ParkyApi.Data;
using ParkyApi.Mapper;
using ParkyApi.Repository;
using ParkyApi.Repository.CRepository;
using ParkyApi.Repository.IRepository;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ParkyApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();

            services.AddScoped<INationalRepository, NationalRepository>();
            services.AddScoped<IParkyRepository, TrailRepository>();
            services.AddScoped<IUSer, USerRepository>();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();
            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");

            services.AddAutoMapper(typeof(ParkyMappings));

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0); // default verison 1 banako
                options.ReportApiVersions = true;
            });
            var appSettingsSection = Configuration.GetSection("AppSettings");

            services.Configure<AppSettings>(appSettingsSection);

            var appSetting = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSetting.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });





            //services.AddSwaggerGen(options =>
            //{
            //    options.SwaggerDoc("parkyOpenAPISpec", new Microsoft.OpenApi.Models.OpenApiInfo()
            //    {
            //        Title = "Parky API",
            //        Version = "1",
            //        Description = "Udemy Parky API",
            //        Contact = new OpenApiContact()
            //        {
            //            Email = "Prashankc777@gmail.com",
            //            Name = "Prashan",
            //            Url = new Uri("https://www.facebook.com/")
            //        },
            //        License = new OpenApiLicense()
            //        {
            //            Name = "Prashan Raj KC",
            //            Url = new Uri("https://www.youtube.com/")
            //        }

            //    }); 
            //    //options.SwaggerDoc("parkyOpenAPISpecTrails", new Microsoft.OpenApi.Models.OpenApiInfo()
            //    //{
            //    //    Title = "Parky API",
            //    //    Version = "1",
            //    //    Description = "Udemy Parky API trails",
            //    //    Contact = new OpenApiContact()
            //    //    {
            //    //        Email = "Prashankc777@gmail.com",
            //    //        Name = "Prashan",
            //    //        Url = new Uri("https://www.facebook.com/")
            //    //    },
            //    //    License = new OpenApiLicense()
            //    //    {
            //    //        Name = "Prashan Raj KC",
            //    //        Url = new Uri("https://www.youtube.com/")
            //    //    }

            //    //});

            //    var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    var cmlCommentFile = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
            //    options.IncludeXmlComments(cmlCommentFile);
            //});


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var desc in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json",
                        desc.GroupName.ToUpperInvariant());
                    options.RoutePrefix = "";



                }

            });


            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()); 

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
