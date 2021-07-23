using BlogAPI.Models.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Services.IServices
{
	public interface ISettingsService
	{
		public BlogSettingModel GetBlogSetting(string uid);
		public int Edit(string uid, BlogSettingModel model, string editor);
	}
}
