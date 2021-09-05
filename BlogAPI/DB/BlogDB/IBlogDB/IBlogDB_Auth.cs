using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.DB.BlogDB.IBlogDB
{
	public interface IBlogDB_Auth
	{
		public Task<bool> Check(string uid, string[] roles);
		public Task<bool> CheckBlogEnabled(string uid, string account);
	}
}
