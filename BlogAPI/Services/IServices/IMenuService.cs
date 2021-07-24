using BlogAPI.Models.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Services.IServices
{
	public interface IMenuService
	{
		public Task<List<MenuViewModel>> GetMenu(string uid, int type);
		public List<MenuViewModel> SetMenu(int parentID);
	}
}
