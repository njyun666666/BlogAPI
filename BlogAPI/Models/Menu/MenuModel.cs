using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Models.Menu
{
	public class MenuModel
	{
        public int MenuID { set; get; }
        public string Title { set; get; }
        public int ParentID { set; get; }
        public string Url { set; get; }
        public string Icon { set; get; }
        public int Status { set; get; }
        public int Type { set; get; }
        public int Sort { set; get; }
        public DateTime? CreateDate { set; get; }
        public DateTime? UpdateDate { set; get; }
        public string Editor { set; get; }
		public Int16 AuthType { get; set; }
	}

    public class MenuViewModel
	{
        public int MenuID { set; get; }
        public string Title { set; get; }
        public string Url { set; get; }
        public string Icon { set; get; }
		public List<MenuViewModel> Children { get; set; }
	}

}
