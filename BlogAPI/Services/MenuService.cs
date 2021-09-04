using BlogAPI.DB.BlogDB.IBlogDB;
using BlogAPI.Models.Menu;
using BlogAPI.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Services
{
	public class MenuService : IMenuService
	{
		private IBlogDB_Menu db_Menu;

		private List<MenuModel> menus;
		

		public MenuService(IBlogDB_Menu blogDB_Menu)
		{
			db_Menu = blogDB_Menu;
		}

		public async Task<List<MenuViewModel>> GetMenu(string uid, int type)
		{
			menus = await db_Menu.GetMenu(uid, type);
			List<MenuViewModel> list = SetMenu(0);

			MenuViewModel myblog = list.Where(x => x.MenuID == 1).SingleOrDefault();
			if (myblog != null)
			{
				myblog.Url = "/" + await db_Menu.GetBlogAccount(uid);
			}

			return list;
		}

		public List<MenuViewModel> SetMenu(int parentID)
		{
			List<MenuViewModel> targetMenu = menus.Where(x => x.ParentID == parentID).OrderBy(x => x.Sort)
				.Select(x => new MenuViewModel
				{
					MenuID = x.MenuID,
					Title = x.Title,
					Url = x.Url,
					Icon = x.Icon
				}).ToList();

			targetMenu.ForEach(x =>
			{
				if (menus.Where(a => a.ParentID == x.MenuID).Any())
				{
					x.Children = SetMenu(x.MenuID);
				}
			});
			return targetMenu;
		}

	}
}
