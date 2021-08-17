using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Models.File
{
	public class FileUploadModel
	{
		[BindProperty(Name = "file[]")]
		public List<IFormFile> Files { get; set; }
	}


	/// <summary>
	/// vditor callback
	/// </summary>
	public class FileUploadViewModel
	{
		public string msg { get; set; }
		public int code { get; set; }
		public Data data { get; set; }
		public FileUploadViewModel()
		{
			data = new Data();
		}
	}

	public class Data
	{
		public List<string> errFiles { get; set; }
		public Dictionary<string, string> succMap { get; set; }
		public Data()
		{
			succMap = new Dictionary<string, string>();
			errFiles = new List<string>();
		}
	}


}
