using BlogAPI.DB.BlogDB.IBlogDB;
using BlogAPI.DB.DBClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.DB.BlogDB
{
	public class BlogDB_Menu : IBlogDB_Menu
	{
		public string str_conn;


		public BlogDB_Menu(IDBConnection dBConnection)
		{
			str_conn = dBConnection.ConnectionBlogDB();
		}

		


	}
}
