using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Authn_Blazor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddAuthentication("Cookies")
                    .AddCookie(options =>
                    {
                        options.Cookie.Name = "Cookies";
                        options.LoginPath = "/login";
                        options.Events = new CookieAuthenticationEvents()
                        {
                            OnSigningIn = async context =>
                            {
                                var principal = context.Principal;
                                if (principal.HasClaim(x => x.Type == ClaimTypes.NameIdentifier))
                                {
                                    if (principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value == "ina")
                                    {
                                        var claimsIdentity = (ClaimsIdentity)principal.Identity;
                                        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                                    }
                                }
                                await Task.CompletedTask;
                            },

                            OnSignedIn = async context =>
                            {
                                await Task.CompletedTask;
                            },

                            OnValidatePrincipal = async context =>
                            {
                                await Task.CompletedTask;
                            }
                        };
                    });
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
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
