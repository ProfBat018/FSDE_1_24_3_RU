using System.Reflection;
using Ecommerce;
using Ecommerce.Areas.Admin.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Data;
using Ecommerce.Areas.Identity.Data;
using ProductRepository.Contexts;
var builder = WebApplication.CreateBuilder(args);


var authConnectionString = builder.Configuration.GetConnectionString("AuthContextConnection16") ?? throw new InvalidOperationException("Connection string 'AuthContextConnection' not found.");;
var ecommerceConnectionString = builder.Configuration.GetConnectionString("ProductsContextConnection16") ?? throw new InvalidOperationException("Connection string 'AuthContextConnection' not found.");;


builder.Services.AddRazorPages();


builder.Services.AddDbContext<AuthContext>(options => options.UseSqlServer(authConnectionString));
builder.Services.AddDbContext<ProductsContext>(options => options.UseSqlServer(ecommerceConnectionString));


builder.Services.AddIdentity<EcommerceUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AuthContext>()
    .AddDefaultTokenProviders();


builder.Services.AddAuthorization(ops =>
{
    ops.AddPolicy("AdminPolicy",
        policy =>
        {
            policy.RequireRole("AppSuperAdmin", "AppAdmin");
        });
    
    ops.AddPolicy("UserPolicy",
        policy =>
        {
            policy.RequireAuthenticatedUser();
        });
});

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<ProductService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Admin}/{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// using (var scope = app.Services.CreateScope())
// {
//     var db = scope.ServiceProvider.GetRequiredService<ProductsContext>();
//     await ProductsContextSeeder.SeedAsync(db);
// }

app.Run();
