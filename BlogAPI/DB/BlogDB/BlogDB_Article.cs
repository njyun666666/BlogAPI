using BlogAPI.DB.BlogDB.IBlogDB;
using BlogAPI.DB.DBClass;
using BlogAPI.Models.Article;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.DB.BlogDB
{
	public class BlogDB_Article : IBlogDB_Article
	{
		private string str_conn;


		public BlogDB_Article(IDBConnection dBConnection)
		{
			str_conn = dBConnection.ConnectionBlogDB();
		}
		public async Task<List<ArticlesTypeMenuModel>> GetArticlesTypeMenu(string uid, int self)
		{
			string status = "1";

			if (self == 1)
			{
				status = "0,1";
			}

			string sql = $"select t.ID, t.Name, Count(l.ID) as `Count` from TB_Article_Type t left join TB_Article_List l on t.ID=l.TypeID and l.Status in ({status})" +
						" where t.UID = @in_uid and t.Status = 1" +
						" group by t.ID order by t.Sort";

			DynamicParameters _params = new DynamicParameters();
			_params.Add("@in_uid", uid, DbType.String, size: 255);

			return await SystemDB.QueryAsync<ArticlesTypeMenuModel>(str_conn, sql, _params);
		}
		public async Task<List<ArticleListModel>> GetArticleList(string uid, bool all)
		{
			string sql = $"select * from TB_Article_List where UID=@in_uid and Status=1 order by Title";

			if (all)
			{
				sql = $"select * from TB_Article_List where UID=@in_uid order by Title";
			}

			DynamicParameters _params = new DynamicParameters();
			_params.Add("@in_uid", uid, DbType.String, size: 255);

			return await SystemDB.QueryAsync<ArticleListModel>(str_conn, sql, _params);
		}
		public async Task<List<ArticleInfoListModel>> GetIndexList(string uid, int self)
		{
			string sql = "select l.ID, l.Title, l.Description, l.TypeID, l.Status, l.CreateDate, t.Name as TypeName, ai.Name as UserName from TB_Article_List l join TB_Article_Type t on l.TypeID=t.ID" +
						" join TB_Org_Account_Info ai on l.UID=ai.UID" +
						" where ai.UID=@in_uid " +
						" and ai.Status=1 and ( @in_self=1 or l.Status=1 ) " +
						" order by l.CreateDate desc";

			DynamicParameters _params = new DynamicParameters();
			_params.Add("@in_uid", uid, DbType.String);
			_params.Add("@in_self", self, DbType.Int32);

			return await SystemDB.QueryAsync<ArticleInfoListModel>(str_conn, sql, _params);

		}
		public async Task<Int64> AddArticle(string uid, ArticleListModel model)
		{
			string sql = " INSERT INTO `TB_Article_List`" +
						" (`Title`,`Content`,`Description`,`TypeID`,`UID`,`Status`,`CreateDate`)" +
						" VALUES" +
						" (@in_title, @in_content, @in_description, @in_typeID, @in_uid, @in_status, now());" +

						" SELECT LAST_INSERT_ID();";
			
			DynamicParameters _params = new DynamicParameters();
			_params.Add("@in_uid", uid, DbType.String);
			_params.Add("@in_title", model.Title, DbType.String);
			_params.Add("@in_content", model.Content, DbType.String);
			_params.Add("@in_description", model.Description, DbType.String);
			_params.Add("@in_typeID", model.TypeID, DbType.String);
			_params.Add("@in_status", model.Status, DbType.String);

			return await SystemDB.ExecuteScalarAsync<Int64>(str_conn, sql, _params);
		}
		public async Task<Int64> EditArticle(string uid, ArticleListModel model)
		{
			string sql = " UPDATE `TB_Article_List`" +
					     " SET" +
						 " `Title` = @in_title," +
						 " `Content` = @in_content," +
						 " `Description` = @in_description," +
						 " `TypeID` = @in_typeID," +
						 " `Status` = @in_status," +
						 " `UpdateDate` = now()" +
						 "  WHERE `ID` = @in_id and `UID`= @in_uid ;";


			DynamicParameters _params = new DynamicParameters();
			_params.Add("@in_id", model.ID, DbType.Int64);
			_params.Add("@in_uid", uid, DbType.String);
			_params.Add("@in_title", model.Title, DbType.String);
			_params.Add("@in_content", model.Content, DbType.String);
			_params.Add("@in_description", model.Description, DbType.String);
			_params.Add("@in_typeID", model.TypeID, DbType.String);
			_params.Add("@in_status", model.Status, DbType.String);

			await SystemDB.ExecuteScalarAsync<Int64>(str_conn, sql, _params);

			return model.ID.Value;
		}
		public async Task<ArticleInfoListModel> GetArticle(Int64 id)
		{
			string sql = "select l.ID, l.Title, l.Content,  l.Status, l.TypeID, l.UID, l.CreateDate, t.Name as TypeName, ai.Name as UserName"+
						" from TB_Article_List l join TB_Article_Type t on l.TypeID = t.ID" +
						" join TB_Org_Account_Info ai on l.UID = ai.UID" +
						" where l.ID = @in_id";

			DynamicParameters _params = new DynamicParameters();
			_params.Add("@in_id", id, DbType.Int64);

			return await SystemDB.QueryFirstOrDefaultAsync<ArticleInfoListModel>(str_conn, sql, _params);
		}
	}
}
