using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.App.API.Data;
using Library.App.API.Services;
using LibraryAppAPI.Handler;
using LibraryAppAPI.Helpers;
using LibraryAppAPI.Interfaces;
using LibraryAppAPI.Requirments;
using LibraryAppAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Library.App.API
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
            services.AddControllers();
            services.AddDbContext<LibraryDBContext>(_options => _options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<EFCoreBooksRepository>();
            /*services.AddMvc(_options =>
            {
                _options.OutputFormatters.Clear();
                _options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                _options.InputFormatters.Clear();
                //_options.InputFormatters.Add(new XmlSerializerInputFormatter());
            });*/
            services.AddMemoryCache();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer
                (_options =>
                {
                    _options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = TokenHelper.Issuer,
                        ValidAudience = TokenHelper.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey
                        (Convert.FromBase64String(TokenHelper.Secret))
                    };
                });
            services.AddAuthorization(_options =>
            {
                _options.AddPolicy("OnlyNonBlockedCustomer", policy =>
                {
                    policy.Requirements.Add(new CustomerBlockedStatusRequirment(false));
                });
            });
            services.AddSingleton<IAuthorizationHandler, CustomerBlockedStatusHandler>();

            services.AddScoped<ICustomerServices, CustomerServices>();
            services.AddScoped<IOrderService, OrderService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
