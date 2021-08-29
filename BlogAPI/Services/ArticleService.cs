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
		public ArticleService(IBlogDB_Article blogDB_Article, IBlogDB_Settings blogDB_Settings)
		{
			db_Article = blogDB_Article;
			db_Settings = blogDB_Settings;
		}

		public async Task<Int64> AddArticle(string uid, ArticleListModel model)
		{
			return await db_Article.AddArticle(uid, model);
		}
		public async Task<List<ArticleInfoListModel>> GetList(string uid, ArticleListRequestModel model)
		{
			int self = 0;

			BlogSettingModel settingModel;
			string targetUID;

			if (string.IsNullOrWhiteSpace(model.Account) || model.Account == "i")
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

			if (uid == targetUID)
			{
				self = 1;
			}

			return await db_Article.GetArticleInfoList(targetUID, self);
		}

	}
}
