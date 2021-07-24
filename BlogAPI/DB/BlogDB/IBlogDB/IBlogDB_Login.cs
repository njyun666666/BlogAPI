using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.DB.BlogDB.IBlogDB
{
	public interface IBlogDB_Login
	{
		public Task<int> LoginLogAdd(string key, string uid, string ip, Int16 status);
		public Task<bool> TokenKeyCheck(string key, string uid);
	}
}
