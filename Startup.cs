using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using PIS.Data;
using PIS.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PIS
{
	public class Startup
	{
		public Startup( IConfiguration configuration )
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices( IServiceCollection services )
		{
			services.AddDbContext<ApplicationDbContext>( options =>
				 options.UseSqlServer(
					 Configuration.GetConnectionString( "DefaultConnection" ) ) );
			services.AddDatabaseDeveloperPageExceptionFilter();

			services.AddDefaultIdentity<IdentityUser>( options => options.SignIn.RequireConfirmedAccount = true )
				.AddEntityFrameworkStores<ApplicationDbContext>();
			services.AddControllersWithViews();

			services.AddSingleton<LanguageService>();

			services.AddDistributedMemoryCache();

			services.AddSession( options =>
			{
				options.IdleTimeout = TimeSpan.FromSeconds( 20 );
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			} );

			services.AddLocalization( options => options.ResourcesPath = "Properties" );

			services.AddMvc()
				.AddViewLocalization()
				.AddDataAnnotationsLocalization( options =>
				{
					options.DataAnnotationLocalizerProvider = ( type, factory ) =>
					{

						var assemblyName = new AssemblyName(typeof(ShareResource).GetTypeInfo().Assembly.FullName);

						return factory.Create( "ShareResource", assemblyName.Name );

					};

				} );

			services.Configure<RequestLocalizationOptions>(
				options =>
				{
					var supportedCultures = new List<CultureInfo>
						{
							new CultureInfo("en-US"),
							new CultureInfo("hu-HU"),
						};

					options.DefaultRequestCulture = new RequestCulture( culture: "hu-HU", uiCulture: "hu-HU" );

					options.SupportedCultures = supportedCultures;
					options.SupportedUICultures = supportedCultures;
					options.RequestCultureProviders.Insert( 0, new QueryStringRequestCultureProvider() );

				} );
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
		{
			if( env.IsDevelopment() )
			{
				app.UseDeveloperExceptionPage();
				app.UseMigrationsEndPoint();
			}
			else
			{
				app.UseExceptionHandler( "/Home/Error" );
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints( endpoints =>
			 {
				 endpoints.MapControllerRoute(
					 name: "default",
					 pattern: "{controller=Home}/{action=Index}/{id?}" );
				 endpoints.MapRazorPages();
			 } );

			var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
			app.UseRequestLocalization( locOptions.Value );

			app.UseSession();
		}
	}
}
