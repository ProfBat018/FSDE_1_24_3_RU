using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Data;
using Ecommerce.Areas.Identity.Data;
using ProductRepository.Contexts;
var builder = WebApplication.CreateBuilder(args);


var authConnectionString = builder.Configuration.GetConnectionString("AuthContextConnection") ?? throw new InvalidOperationException("Connection string 'AuthContextConnection' not found.");;
var ecommerceConnectionString = builder.Configuration.GetConnectionString("ProductsContextConnection") ?? throw new InvalidOperationException("Connection string 'AuthContextConnection' not found.");;


builder.Services.AddRazorPages();
builder.Services.AddDbContext<AuthContext>(options => options.UseSqlServer(authConnectionString));
builder.Services.AddDbContext<ProductsContext>(options => options.UseSqlServer(ecommerceConnectionString));

builder.Services.AddDefaultIdentity<EcommerceUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AuthContext>();

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
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
