using BlogAPI.DB.BlogDB.IBlogDB;
using BlogAPI.DB.DBClass;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.DB.BlogDB
{
	public class BlogDB_Login : IBlogDB_Login
	{
		private string str_conn;


		public BlogDB_Login(IDBConnection dBConnection)
		{
			str_conn = dBConnection.ConnectionBlogDB();
		}


		public async Task<int> LoginLogAdd(string key, string uid, string ip, Int16 status)
		{
			string sql = " INSERT INTO `TB_Login_Log` (`TokenKey`,`UID`,`Date`,`IP`,`Status`)" +
						" VALUES (@in_key, @in_uid, now(), @in_ip, @in_status);";

			DynamicParameters _params = new DynamicParameters();
			_params.Add("@in_key", key, DbType.String, size: 255);
			_params.Add("@in_uid", uid, DbType.String, size: 255);
			_params.Add("@in_ip", ip, DbType.String, size: 255);
			_params.Add("@in_status", status, DbType.Int16);

			return await SystemDB.ExecuteAsync(str_conn, sql, _params, false);
		}

		public async Task<bool> TokenKeyCheck(string key, string uid)
		{
			string sql = "select exists ("+
						"  select 1 from TB_Login_Log where `TokenKey`= @in_key and UID = @in_uid and Status = 1" +
						" )";

			DynamicParameters _params = new DynamicParameters();
			_params.Add("@in_key", key, DbType.String, size: 255);
			_params.Add("@in_uid", uid, DbType.String, size: 255);

			return await SystemDB.QueryFirstOrDefaultAsync<bool>(str_conn, sql, _params);
		}

	}
}
