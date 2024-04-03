using Microsoft.AspNetCore.Authorization;
using SigninSignupDapper.Autherization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
{
	options.LoginPath = "/Account/Login";
	options.Cookie.Name = "MyCookieAuth";
    options.AccessDeniedPath = "/Home/Index";
   
});
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("hrpolicy", option => {
        option.RequireClaim("Department", "HR").
        Requirements.Add(new AutherizationRequirements(2));
    });
    option.AddPolicy("marketingpolicy", option =>
    {
        option.RequireClaim("Department", "marketing");
        
    });
    

});
builder.Services.AddSingleton<IAuthorizationHandler, AutherizationRequirementsHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
