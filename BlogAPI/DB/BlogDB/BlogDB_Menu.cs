using BlogAPI.DB.BlogDB.IBlogDB;
using BlogAPI.DB.DBClass;
using BlogAPI.Models.Menu;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.DB.BlogDB
{
	public class BlogDB_Menu : IBlogDB_Menu
	{
		private string str_conn;


		public BlogDB_Menu(IDBConnection dBConnection)
		{
			str_conn = dBConnection.ConnectionBlogDB();
		}

		public async Task<List<MenuModel>> GetMenu(string uid, int type)
		{
			string sql= " select * from TB_Menu where Type=@in_type and Status=1 and ( AuthType=1 or ( AuthType=2 and MenuID in (" +
						" 	select MenuID from TB_Auth " +
						" 	where ( ID in ( select ID from TB_Org_Role_User where UID=@in_uid and Status=1 ) and Type=1 and Status=1 )" +
						" 	or ( ID=@in_uid and Type=2 and Status=1 ))" +
						" ) )";

			DynamicParameters _params = new DynamicParameters();
			_params.Add("@in_uid", uid, DbType.String, size: 255);
			_params.Add("@in_type", type, DbType.Int32);

			return await SystemDB.QueryAsync<MenuModel>(str_conn, sql, _params);
		}
		public async Task<string> GetBlogAccount(string uid)
		{
			string sql= "select if(b.IndexDefault=1,'', a.Account) from TB_Org_Account_Info a join TB_Blog_Setting b on a.UID=b.UID"+
						" where a.UID = @in_uid ";
			
			DynamicParameters _params = new DynamicParameters();
			_params.Add("@in_uid", uid, DbType.String);

			return await SystemDB.QueryFirstOrDefaultAsync<string>(str_conn, sql, _params);
		}

	}
}
