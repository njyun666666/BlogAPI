﻿using BlogAPI.Models.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.DB.BlogDB.IBlogDB
{
	public interface IBlogDB_Settings
	{
		public Task<BlogSettingModel> GetBlogSetting(string uid);
		public Task<int> Edit(string uid, BlogSettingModel model, string editor);
		public Task<List<ArticleTypeModel>> ArticleTypeGet(string uid);
		public Task<int> ArticleTypeAdd(string uid, string name);
		public Task<int> ArticleTypeEdit(string uid, string name, Int64 id);
	}
}