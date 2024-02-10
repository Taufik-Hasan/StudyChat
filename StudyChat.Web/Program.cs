using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudyChat.Core.Entities;
using StudyChat.DataAccess;
using StudyChat.DataAccess.Interface;
using StudyChat.DataAccess.Repository;
using StudyChat.Services;
using StudyChat.Services.Interface;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
    Options =>
    {
        Options.Password.RequiredUniqueChars = 0;
        Options.Password.RequireLowercase = false;
        Options.Password.RequireUppercase = false;
        Options.Password.RequireNonAlphanumeric = false;
        Options.Password.RequiredLength = 6;
    }
    ).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options => options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
builder.Services.AddScoped<IAnswerService, AnswerService>();


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

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
	  name: "area",
	  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
	);
	endpoints.MapControllerRoute(
	  name: "default",
	  pattern: "{controller=Home}/{action=Index}/{id?}"
	);
});

// Initialize default identity roles
using (var scope = app.Services.CreateScope())
{
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
	var roles = new[] { "Admin", "Teacher", "Student", "Moderator" };

	foreach (var role in roles)
	{
		if (!await roleManager.RoleExistsAsync(role))
		{
			await roleManager.CreateAsync(new IdentityRole(role));
		}
	}
}
// Initialize default admin user
using (var scope = app.Services.CreateScope())
{
	var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

	string adminEmail = "admin@admin.com";
	string adminPassword = "Admin123";

	if (await userManager.FindByEmailAsync(adminEmail) == null)
	{
		ApplicationUser admin = new ApplicationUser();
		admin.Email = adminEmail;
		admin.UserName = adminEmail;
		admin.Name = "Admin";

		var result = await userManager.CreateAsync(admin, adminPassword);

		await userManager.AddToRoleAsync(admin, "Admin");


	}

}
app.Run();
