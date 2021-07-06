using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Models.Org
{
	public class OrgAccountInfoModel
	{
		public string UID { get; set; }
		public string GID { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Account { get; set; }
		public Int16 Status { get; set; }
	}
}
