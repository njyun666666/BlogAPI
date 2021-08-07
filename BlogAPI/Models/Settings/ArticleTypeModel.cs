using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlogAPI.Models.Settings
{
	public class ArticleTypeModel
	{
		public Int64 ID { get; set; }
		public string Name { get; set; }
		[JsonIgnore]
		public string UID { get; set; }
		[JsonIgnore]
		public Int16 Status { get; set; }
		[JsonIgnore]
		public int Sort { get; set; }
		[JsonIgnore]
		public DateTime CreateDate { get; set; }
		[JsonIgnore]
		public DateTime? UpdateDate { get; set; }
	}
	public class ArticleTypeAddModel
	{
		public Int64 ID { get; set; }
		public string Name { get; set; }
	}
}
