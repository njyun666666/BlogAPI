using BlogAPI.DB.BlogDB.IBlogDB;
using BlogAPI.Models.Article;
using BlogAPI.Models.Menu;
using BlogAPI.Models.Settings;
using BlogAPI.Models.Theme;
using BlogAPI.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Services
{
	public class ThemeService : IThemeService
	{
		IBlogDB_Settings db_Settings;
		IBlogDB_Article db_Article;

		public ThemeService(IBlogDB_Settings blogDB_Settings, IBlogDB_Article blogDB_Article)
		{
			db_Settings = blogDB_Settings;
			db_Article = blogDB_Article;
		}

		public async Task<ThemeDataViewModel> GetThemeData(string uid, ThemeDataRequestModel model)
		{
			ThemeDataViewModel result = new ThemeDataViewModel();
			BlogSettingModel settingModel;
			string targetUID = string.Empty;


			if (model.Self == 1)
			{
				settingModel = await db_Settings.GetBlogSetting(uid);
			}
			else if (string.IsNullOrWhiteSpace(model.Account) || model.Account == "i")
			{
				settingModel = await db_Settings.GetIndexDefault();
			}
			else
			{
				settingModel = await db_Settings.GetAccountSetting(model.Account, uid);
			}

			if (settingModel == null)
			{
				return null;
			}

			if (settingModel.UID == uid)
			{
				model.Self = 1;
			}

			targetUID = settingModel.UID;
			result.Title = settingModel.Title;


			result.Menu = await db_Article.GetArticlesTypeMenu(targetUID, model.Self);


			return result;

		}

	}
}
