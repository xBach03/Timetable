using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Test.Models;
using Test.Controllers;
namespace Test
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Them vao ung dung cac dich vu
			builder.Services.AddControllersWithViews();
			builder.Services.AddDbContext<AppDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("connectString"));
			});

			builder.Services.AddDefaultIdentity<User>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

			builder.Services.AddRazorPages();
			builder.Services.AddDistributedMemoryCache();
			builder.Services.AddSession(options =>
			{

			});
			// Them vao dich vu Identity voi cau hinh mac dinh cho User
			// Them trien khai Entity Framework luu tru du lieu ve Indentity
			// Them token provider de phat sinh token (reset password, reset email...)
			//builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

			// Cau hinh cac tuy chon ve Identity
			builder.Services.Configure<IdentityOptions>(options => {
				// Thiet lap ve Password
				options.Password.RequireDigit = false; // khong bat buoc phai co so
				options.Password.RequireNonAlphanumeric = false; // khong bat buoc co ky tu dac biet
				options.Password.RequireUppercase = false; // khong bat buoc co chu in hoa
				options.Password.RequiredLength = 8; // so ky tu toi thieu cua password

				// Cau hinh Lockout - khoa tai khoan
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khoa 5 phut
				options.Lockout.MaxFailedAccessAttempts = 5; // Dang nhap that bai 5 lan thi khoa
				options.Lockout.AllowedForNewUsers = true;

				// Cau hinh ve User
				options.User.AllowedUserNameCharacters = // Cac ky tu co the duoc dat ten cho User
					"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
				options.User.RequireUniqueEmail = true;  // Email la duy nhat

				// Cau hinh dang nhap
				options.SignIn.RequireConfirmedEmail = false; // Cau hinh xac thuc email, khong bat buoc xac nhan email
				options.SignIn.RequireConfirmedPhoneNumber = false; // Cau hinh xac thuc sdt, khong bat buoc co sdt
			});
			// Cau hinh Cookie
			builder.Services.ConfigureApplicationCookie(options => {
				options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
				options.LoginPath = $"/login/"; // URL den trang dang nhap
				options.LogoutPath = $"/logout/";
				options.AccessDeniedPath = $"/Identity/Account/AccessDenied"; // Trang khi User bi cam truy cap  
			});
			builder.Services.Configure<SecurityStampValidatorOptions>(options =>
			{
				// Tren 5 giay truy cap lai se nap lai thong tin User
				// Nap lai thong tin Sercurity
				options.ValidationInterval = TimeSpan.FromSeconds(5);
			});

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
			app.UseSession();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
				endpoints.MapRazorPages();
			});
		 

			app.Run();
		}
	}
}