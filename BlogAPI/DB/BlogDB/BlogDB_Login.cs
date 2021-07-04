using BlogAPI.DB.BlogDB.IBlogDB;
using BlogAPI.DB.DBClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.DB.BlogDB
{
	public class BlogDB_Login : IBlogDB_Login
	{
		public string str_conn;


		public BlogDB_Login(IDBConnection dBConnection)
		{
			str_conn = dBConnection.ConnectionBlogDB();
		}

		public void test()
		{
			SystemDB.testconnect(str_conn);
		}

	}
}
