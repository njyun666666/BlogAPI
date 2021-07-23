using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Enums
{
	public enum ReturnCodeEnum
	{
		[Description("Success")]
		success = 1,
		[Description("Fail")]
		fail = -1,
		[Description("參數錯誤")]
		param_error = -2,

	}
}
