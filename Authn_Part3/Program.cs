var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = "okta";
        //options.DefaultChallengeScheme = "google";
    })
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/denied";        
    })
    .AddOpenIdConnect("google", options =>
    {
        options.Authority = builder.Configuration["GoogleOpenId:Authority"];
        options.ClientId = builder.Configuration["GoogleOpenId:ClientId"];
        options.ClientSecret = builder.Configuration["GoogleOpenId:ClientSecret"];
        options.CallbackPath = builder.Configuration["GoogleOpenId:CallbackPath"];
        options.SignedOutCallbackPath = builder.Configuration["GoogleOpenId:SignedOutCallbackPath"];
        options.SaveTokens = false;
        options.Events = new OpenIdConnectEvents()
        {
            OnTokenValidated = async context =>
            {
                if (context.Principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value == "321321")
                {
                    var claim = new Claim(ClaimTypes.Role, "Admin");
                    var claimsIdentity = (ClaimsIdentity)context.Principal.Identity;
                    claimsIdentity.AddClaim(claim);
                }
            }
        };
    })
    .AddOpenIdConnect("okta", options =>
    {
        options.Authority = "https://dev-71421169.okta.com";
        options.ClientId = "0oa1igacvflbAddC75d7";
        options.ClientSecret = "jUiabSPLqH1AHVjvKDuXwlkFfvsYyXvLblqK1UDa";
        options.CallbackPath = "/okta-auth";
        options.ResponseType = "code";
    })
    ;



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
