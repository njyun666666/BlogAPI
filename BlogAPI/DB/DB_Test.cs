using BlogAPI.DB.DBClass;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.DB.BlogDB
{
	public interface IDB_Test
	{
		public int BlogDBTest();
	}
	public class DB_Test : IDB_Test
	{
		private string blogDB_conn;

		public DB_Test(IDBConnection dBConnection)
		{
			blogDB_conn = dBConnection.ConnectionBlogDB();
		}

		public int BlogDBTest()
		{
			int result = 0;

			try
			{
				using (MySqlConnection conn = new MySqlConnection(blogDB_conn))
				{
					if (conn.State == ConnectionState.Closed) conn.Open();
					result = 1;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return result;
		}
	}
}
