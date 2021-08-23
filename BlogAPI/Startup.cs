using BlogAPI.Common;
using BlogAPI.DB.BlogDB;
using BlogAPI.DB.BlogDB.IBlogDB;
using BlogAPI.DB.DBClass;
using BlogAPI.Helper;
using BlogAPI.Middlewares;
using BlogAPI.Services;
using BlogAPI.Services.IServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI
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

			services.AddControllers();



			services.AddSingleton<IDBConnection, DBConnection>();
			services.AddSingleton<IDB_Test, DB_Test>();

			#region BlogDB
			services.AddSingleton<IBlogDB_Menu, BlogDB_Menu>();
			services.AddSingleton<IBlogDB_Org, BlogDB_Org>();
			services.AddSingleton<IBlogDB_Login, BlogDB_Login>();
			services.AddSingleton<IBlogDB_Auth, BlogDB_Auth>();
			services.AddSingleton<IBlogDB_Settings, BlogDB_Settings>();
			services.AddSingleton<IBlogDB_Article, BlogDB_Article>();			
			#endregion

			services.AddSingleton<IMyService, MyService>();

			#region Service
			services.AddSingleton<IMenuService, MenuService>();
			services.AddSingleton<ILoginService, LoginService>();
			services.AddSingleton<IGoogleLoginService, GoogleLoginService>();
			services.AddSingleton<IAuthService, AuthService>();
			services.AddSingleton<ISettingsService, SettingsService>();
			services.AddSingleton<IThemeService, ThemeService>();
			services.AddSingleton<IArticleService, ArticleService>();
			
			#endregion


			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "BlogAPI", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlogAPI v1"));
			}
			else
			{
				app.AddProductionExceptionHandling();
			}


			app.UseMiddleware<GoogleTokenMiddleware>();
			app.UseMiddleware<AuthMiddleware>();


			app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
