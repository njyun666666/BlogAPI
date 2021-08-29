using BlogAPI.Models.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.DB.BlogDB.IBlogDB
{
	public interface IBlogDB_Article
	{
		public Task<List<ArticleListModel>> GetArticleList(string uid, bool all);
		public Task<List<ArticleInfoListModel>> GetArticleInfoList(string uid, int self);
		public Task<Int64> AddArticle(string uid, ArticleListModel model);
	}
}
