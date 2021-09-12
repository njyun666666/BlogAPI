using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlogAPI.Models.Settings
{
	public class BlogSettingModel
	{
		[JsonIgnore]
		public string UID { get; set; }
		public string Title { get; set; }
		public Int16 Status { get; set; }
		[JsonIgnore]
		public DateTime? UpdateDate { get; set; }
		[JsonIgnore]
		public string Editor { get; set; }
		[JsonIgnore]
		public Int16 IndexDefault { get; set; }
	}
	public class BlogSettingAccModel
	{
		public BlogSettingModel Setting { get; set; }
		public int Self { get; set; }
	}
}
