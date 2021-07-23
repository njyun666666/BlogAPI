using BlogAPI.DB.BlogDB.IBlogDB;
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

		public BlogSettingModel GetBlogSetting(string uid)
		{
			return db_Settings.GetBlogSetting(uid);
		}
		public int Edit(string uid, BlogSettingModel model, string editor)
		{
			return db_Settings.Edit(uid, model, editor);
		}
	}
}
