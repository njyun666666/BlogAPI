using BlogAPI.Models.Org;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.DB.BlogDB.IBlogDB
{
	public interface IBlogDB_Org
	{
		public Task<OrgAccountInfoModel> AccountInfoGet(string googleID);
		public Task<int> AccountInfoAdd(string uid, string gid, string name, string email, string account, Int16 status);
	}
}
