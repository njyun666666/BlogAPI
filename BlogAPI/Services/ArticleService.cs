using BlogAPI.DB.BlogDB.IBlogDB;
using BlogAPI.Models.Article;
using BlogAPI.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Services
{
	public class ArticleService : IArticleService
	{
		IBlogDB_Article db_Article;
		public ArticleService(IBlogDB_Article blogDB_Article)
		{
			db_Article = blogDB_Article;
		}

		public async Task<Int64> AddArticle(string uid, ArticleListModel model)
		{
			return await db_Article.AddArticle(uid, model);
		}

	}
}
