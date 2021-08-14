using BlogAPI.Models.Theme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Services.IServices
{
	public interface IThemeService
	{
		public Task<ThemeDataViewModel> GetThemeData(string uid, ThemeDataRequestModel model);
	}
}
