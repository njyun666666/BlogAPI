using BlogAPI.Models.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.DB.BlogDB.IBlogDB
{
	public interface IBlogDB_Menu
	{
		public Task<List<MenuModel>> GetMenu(string uid, int type);
		public Task<string> GetBlogAccount(string uid);
	}
}
