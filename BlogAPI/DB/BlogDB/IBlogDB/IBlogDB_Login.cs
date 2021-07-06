using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.DB.BlogDB.IBlogDB
{
	public interface IBlogDB_Login
	{
		public int LoginLogAdd(string token, string uid, string ip, Int16 status);
		public bool TokenKeyCheck(string key, string uid);
	}
}
