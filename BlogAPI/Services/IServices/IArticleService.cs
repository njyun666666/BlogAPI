using BlogAPI.Models.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Services.IServices
{
	public interface IArticleService
	{
		public Task<Int64> AddArticle(string uid, ArticleListModel model);
	}
}
