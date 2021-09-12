using BlogAPI.DB.BlogDB.IBlogDB;
using BlogAPI.Enums;
using BlogAPI.Models;
using BlogAPI.Models.Settings;
using BlogAPI.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Services
{
	public class SettingsService : ISettingsService
	{
		private IBlogDB_Settings db_Settings;
		public SettingsService(IBlogDB_Settings blogDB_Settings)
		{
			db_Settings = blogDB_Settings;
		}

		public async Task<BlogSettingModel> GetBlogSetting(string uid)
		{
			return await db_Settings.GetBlogSetting(uid);
		}
		public async Task<int> Edit(string uid, BlogSettingModel model, string editor)
		{
			return await db_Settings.Edit(uid, model, editor);
		}
		public async Task<List<ArticleTypeModel>> ArticleTypeGet(string uid)
		{
			return await db_Settings.ArticleTypeGet(uid);
		}
		public async Task<int> ArticleTypeAdd(string uid, string name)
		{
			return await db_Settings.ArticleTypeAdd(uid, name);			
		}
		public async Task<int> ArticleTypeEdit(string uid, string name, Int64 id)
		{
			return await db_Settings.ArticleTypeEdit(uid, name, id);
		}
		public async Task<int> ArticleTypeDelete(string uid, Int64 id)
		{
			return await db_Settings.ArticleTypeDelete(uid, id);
		}
		public async Task<int> ArticleTypeSortEdit(string uid, List<int> ids)
		{
			return await db_Settings.ArticleTypeSortEdit(uid, ids);
		}
		public async Task<BlogSettingAccModel> GetSetting(string uid, string account)
		{
			BlogSettingAccModel result = new BlogSettingAccModel();

			if (string.IsNullOrWhiteSpace(account) || account == "i")
			{
				result.Setting = await db_Settings.GetIndexDefault();
			}
			else
			{
				result.Setting = await db_Settings.GetAccountSetting(account, uid);
			}

			if (result == null)
			{
				return null;
			}

			if (uid == result.Setting.UID)
			{
				result.Self = 1;
			}

			return result;
		}
	}
}
