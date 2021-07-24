using BlogAPI.Models.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Services.IServices
{
	public interface ISettingsService
	{
		public Task<BlogSettingModel> GetBlogSetting(string uid);
		public Task<int> Edit(string uid, BlogSettingModel model, string editor);
	}
}
