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

	}
}
