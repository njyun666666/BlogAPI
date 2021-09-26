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

		public async Task<bool> Check(string uid, string[] roles)
		{
			string sql = $"select exists( select 1 from TB_Org_Role_User where UID=@in_uid and Status=1 and ID in (@in_roles) )";

			DynamicParameters _params = new DynamicParameters();
			_params.Add("@in_uid", uid, DbType.String, size: 255);
			_params.Add("@in_roles", roles);

			return await SystemDB.QueryFirstOrDefaultAsync<bool>(str_conn, sql, _params);
		}
		public async Task<bool> CheckBlogEnabled(string uid, string account)
		{
			string sql = $"select exists (" +
							" select 1 from TB_Org_Account_Info a join TB_Blog_Setting b on a.UID = b.UID join TB_Org_Role_User r on a.UID = r.UID" +
							" where a.Account = @in_account and a.Status = 1 and (b.UID = @in_uid or b.Status = 1)  and r.ID = 'Blogger' and r.Status = 1" +
						 " )";

			DynamicParameters _params = new DynamicParameters();
			_params.Add("@in_uid", uid, DbType.String);
			_params.Add("@in_account", account, DbType.String);

			return await SystemDB.QueryFirstOrDefaultAsync<bool>(str_conn, sql, _params);
		}
		public async Task<bool> CheckArticleEnabled(string uid, Int64 articleID, int self)
		{
			string sql = "select exists (" +
							" select 1 from TB_Article_List l " +
							" join TB_Org_Account_Info ai on l.UID = ai.UID" +
							" where l.ID = @in_articleID and ai.UID = @in_uid " +
							" and ai.Status = 1 and ( @in_self = 1 or l.Status = 1)" +
						 " )";

			DynamicParameters _params = new DynamicParameters();
			_params.Add("@in_uid", uid, DbType.String);
			_params.Add("@in_articleID", articleID, DbType.Int64);
			_params.Add("@in_self", self, DbType.Int32);

			return await SystemDB.QueryFirstOrDefaultAsync<bool>(str_conn, sql, _params);
		}
		public async Task<bool> ArticleEditAuth(string uid, Int64 articleID)
		{
			string sql = "select exists (" +
							" select 1 from TB_Article_List where ID = @in_articleID and UID = @in_uid "+
						 " )";

			DynamicParameters _params = new DynamicParameters();
			_params.Add("@in_uid", uid, DbType.String);
			_params.Add("@in_articleID", articleID, DbType.Int64);

			return await SystemDB.QueryFirstOrDefaultAsync<bool>(str_conn, sql, _params);
		}

	}
}
