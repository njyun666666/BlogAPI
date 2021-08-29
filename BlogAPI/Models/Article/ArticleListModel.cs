using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Models.Article
{
	public class ArticleListModel
	{
		public Int64 ID { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public Int64 TypeID { get; set; }
		public string UID { get; set; }
		public Int16 Status { get; set; }
		public DateTime? CreateDate { get; set; }
		public DateTime? UpdateDate { get; set; }
	}
	public class ArticleInfoListModel : ArticleListModel
	{
		public string TypeName { get; set; }
		public string UserName { get; set; }
	}
}
