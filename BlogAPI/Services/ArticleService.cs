using BlogAPI.DB.BlogDB.IBlogDB;
using BlogAPI.Models.Article;
using BlogAPI.Models.Settings;
using BlogAPI.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Services
{
	public class ArticleService : IArticleService
	{
		IBlogDB_Article db_Article;
		IBlogDB_Settings db_Settings;
		ISettingsService _settingsService;
		public ArticleService(IBlogDB_Article blogDB_Article, IBlogDB_Settings blogDB_Settings, ISettingsService settingsService)
		{
			db_Article = blogDB_Article;
			db_Settings = blogDB_Settings;
			_settingsService = settingsService;
		}

		public async Task<Int64> AddArticle(string uid, ArticleListModel model)
		{
			Int64 result = 0;

			if (!model.ID.HasValue)
			{
				result = await db_Article.AddArticle(uid, model);
			}
			else
			{
				result = await db_Article.EditArticle(uid, model);
			}

			return result;
		}
		public async Task<List<ArticleInfoListModel>> GetIndexList(string uid, ArticleListRequestModel model)
		{
			BlogSettingAccModel settingModel = await _settingsService.GetSetting(uid, model.Account);
			return await db_Article.GetIndexList(settingModel.Setting.UID, settingModel.Self);
		}
		public async Task<ArticleInfoListModel> GetArticle(string uid, ArticleRequestModel model)
		{
			ArticleInfoListModel article = await db_Article.GetArticle(model.ID);

			if (article.UID == uid)
			{
				article.isSelf = 1;
			}
			else
			{
				if (article.Status != 1)
				{
					article = null;
				}
			}

			BlogSettingModel settingModel = await _settingsService.GetBlogSetting(article.UID);
			if (settingModel.Status != 1)
			{
				article = null;
			}

			return article;
		}

	}
}
