using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddAuthentication("CookieScheme")
                .AddCookie("CookieScheme", options =>
                {
                    options.Cookie.Name = "CookieName";
                    options.LoginPath = "/login";
                    options.AccessDeniedPath = "/loginDenied";
                });

builder.Services.AddAuthorization(options =>
                {
                    options.AddPolicy("HrManagerRole",
                        policy => policy.RequireClaim("Manager", "HRManager")
                                        .RequireClaim(ClaimTypes.Email, "HR@gmail.com"));
                });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
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


// EndPoints
app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
