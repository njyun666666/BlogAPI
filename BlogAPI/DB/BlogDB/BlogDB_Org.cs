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
	public class BlogDB_Org : IBlogDB_Org
	{
		private string str_conn;


		public BlogDB_Org(IDBConnection dBConnection)
		{
			str_conn = dBConnection.ConnectionBlogDB();
		}

		public async Task<OrgAccountInfoModel> AccountInfoGet(string googleID)
		{
			string sql = "select * from TB_Org_Account_Info where GID=@in_GID ";

			DynamicParameters _params = new DynamicParameters();
			_params.Add("@in_GID", googleID, DbType.String, size: 255);

			return await SystemDB.QueryFirstOrDefaultAsync<OrgAccountInfoModel>(str_conn, sql, _params);
		}

		public async Task<int> AccountInfoAdd(string uid, string gid, string name, string email, string account, Int16 status)
		{
			string sql = " INSERT INTO TB_Org_Account_Info (`UID`,`GID`,`Name`,`Email`,`Account`,`Status`,`CreateDate`)" +
						" VALUES ( @in_uid, @in_gid, @in_name, @in_email, @in_account, @in_status, now() );";

			DynamicParameters _params = new DynamicParameters();
			_params.Add("@in_uid", uid, DbType.String, size: 255);
			_params.Add("@in_gid", gid, DbType.String, size: 255);
			_params.Add("@in_name", name, DbType.String, size: 255);
			_params.Add("@in_email", email, DbType.String, size: 255);
			_params.Add("@in_account", account, DbType.String, size: 255);
			_params.Add("@in_status", status, DbType.String, size: 255);

			return await SystemDB.ExecuteAsync(str_conn, sql, _params);
		}
		public async Task<int> SetDefaultRole(string uid)
		{
			string sql = "INSERT INTO `TB_Org_Role_User` (`ID`,`UID`,`Status`,`UpdateDate`,`Editor`)" +
						" select `ID`, @in_uid, 1 ,now(), 'default' from `TB_Org_Role` where `Status`= 1 and `Default`= 1";

			DynamicParameters _params = new DynamicParameters();
			_params.Add("@in_uid", uid, DbType.String);

			return await SystemDB.ExecuteAsync(str_conn, sql, _params);
		}

	}
}
