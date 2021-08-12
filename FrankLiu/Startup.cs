using FrankLiu.Authorization;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FrankLiu
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
            services.AddRazorPages();

            services.AddAuthentication("CookieScheme")
                    .AddCookie("CookieScheme", options =>
                    {
                        options.Cookie.Name = "CookieName";
                        options.LoginPath = "/Account/Login";
                        options.ExpireTimeSpan = TimeSpan.FromSeconds(30);
                    });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly",
                    policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));

                options.AddPolicy("MustBelongToHRDeparment",
                    policy => policy.RequireClaim("Deparment", "HR"));

                options.AddPolicy("HRManagerOnly",
                    policy => policy.RequireClaim("Deparment", "HR")
                                    .RequireClaim("Manager")
                                    .Requirements.Add(new HRManagerProbationRequirement(3)));
            });

            services.AddSingleton<IAuthorizationHandler, HRManagerProbationRequirementHandler>();

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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
