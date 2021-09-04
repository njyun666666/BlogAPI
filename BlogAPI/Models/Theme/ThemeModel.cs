using BlogAPI.Models.Article;
using BlogAPI.Models.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Models.Theme
{
	public class ThemeModel
	{
	}
	public class ThemeDataRequestModel
	{
		public string Account { get; set; }
		public int Self { get; set; }
	}
	public class ThemeDataViewModel
	{
		public string Title { get; set; }
		public List<ArticlesTypeMenuModel> Menu { get; set; }
	}
}
