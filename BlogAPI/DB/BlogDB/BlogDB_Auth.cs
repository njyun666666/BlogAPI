using BlogAPI.DB.BlogDB.IBlogDB;
using BlogAPI.DB.DBClass;
using BlogAPI.Models.Org;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.DB.BlogDB
{
	public class BlogDB_Auth : IBlogDB_Auth
	{
		private string str_conn;


		public BlogDB_Auth(IDBConnection dBConnection)
		{
			str_conn = dBConnection.ConnectionBlogDB();
		}

		public bool Check(string uid, string[] roles)
		{
			string sql = $"select exists( select 1 from TB_Org_Role_User where UID=@in_uid and Status=1 and ID in (@in_roles) )";

			DynamicParameters _params = new DynamicParameters();
			_params.Add("@in_uid", uid, DbType.String, size: 255);
			_params.Add("@in_roles", roles);

			return SystemDB.SingleQuery<bool>(str_conn, sql, _params);
		}

	}
}
