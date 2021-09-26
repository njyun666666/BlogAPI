using BlogAPI.Models.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.DB.BlogDB.IBlogDB
{
	public interface IBlogDB_Article
	{
		public Task<List<ArticlesTypeMenuModel>> GetArticlesTypeMenu(string uid, int self);
		public Task<List<ArticleListModel>> GetArticleList(string uid, bool all);
		public Task<List<ArticleInfoListModel>> GetIndexList(string uid, int self);
		public Task<Int64> AddArticle(string uid, ArticleListModel model);
		public Task<Int64> EditArticle(string uid, ArticleListModel model);
		public Task<ArticleInfoListModel> GetArticle(Int64 id);
	}
}
