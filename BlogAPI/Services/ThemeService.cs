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
			else if (string.IsNullOrWhiteSpace(model.Account))
			{
				settingModel = await db_Settings.GetIndexDefault();
			}
			else
			{
				settingModel = await db_Settings.GetAccountSetting(model.Account);
			}

			if (settingModel == null)
			{
				return null;
			}
			
			targetUID = settingModel.UID;
			result.Title = settingModel.Title;


			List<ArticleTypeModel> typeList = new List<ArticleTypeModel>();
			List<ArticleListModel> articleList = new List<ArticleListModel>();

			
			// 取得文章類型
			Task taskGetTypeList = Task.Run(async () =>
			{
				typeList = await db_Settings.ArticleTypeGet(targetUID);
			});

			// 取得文章清單
			Task taskGetArticleList = Task.Run(async () =>
			{
				articleList = await db_Article.GetArticleList(targetUID, false);
			});


			await Task.WhenAll(taskGetTypeList, taskGetArticleList);

			result.Menu = typeList.Select(x => new MenuViewModel()
			{
				MenuID = (int)x.ID,
				Title = x.Name,
				Children = articleList.Where(a => a.TypeID == x.ID).Select(a => new MenuViewModel() {
					Title = a.Title,
					Url = $"/article/{a.ID}"
				}).ToList()
			}).ToList();

			return result;

		}

	}
}
