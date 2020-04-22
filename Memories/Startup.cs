using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memories.Data;
using Memories.Data.Repositories;
using Memories.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NSwag;

namespace Memories
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthorization();
            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("MemoryContext")));

            services.AddScoped<MemoryDataInitializer>();
            services.AddScoped<IMemoryRepository, MemoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            //authenticatie
            services.AddAuthentication(option => { option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; })
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                    };
                });


            //alles rond identity
            services.AddIdentity<IdentityUser, IdentityRole>(cfg => cfg.User.RequireUniqueEmail = true).AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                //passwoord settings
                options.Password.RequiredLength = 6;

                //lockout
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.AllowedForNewUsers = true;

                //user
                options.User.RequireUniqueEmail = true;
            });

            //extra info toevoegen op swagger
            services.AddOpenApiDocument(c =>
            {
                c.DocumentName = "Api docs";
                c.Title = "Memory API";
                c.Version = "v1";
                c.Description = "The Memory API documentation - Jiri Vanhouwe";

                c.AddSecurity("JWT", new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,//use API keys for authorization. An API key is a token that a client provides when making API calls.In = OpenApiSecurityApiKeyLocation.Header, //token is passedin theheaderName = "Authorization", //name of header tobeusedDescription = "Type into the textbox: Bearer {your JWT token}."//description above textfieldto enter bearer token});c.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT")); //adds the token when a request is send});90

                });
                // services.AddSwaggerDocument();

            }); 
            }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MemoryDataInitializer memoryDataInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseOpenApi();
            app.UseSwaggerUi3(); 
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            memoryDataInitializer.InitializeData().Wait();
        }
    }
}
