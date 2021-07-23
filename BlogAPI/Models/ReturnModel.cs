using BlogAPI.Common;
using BlogAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Models
{
	public class ReturnModel
	{
		public ReturnCodeEnum Code { get; set; }
		public string Message { get; set; }

		public ReturnModel()
		{
			
		}

		public ReturnModel(ReturnCodeEnum code, string message = null)
		{
			Code = code;
			Message = message;

			if (string.IsNullOrWhiteSpace(message))
			{
				Message = EnumExtenstions.GetEnumDescription(code);
			}
		}
	}

	public class OkReturn : ReturnModel
	{
		public OkReturn() : base(ReturnCodeEnum.success) { }
	}

	public class FailReturn : ReturnModel
	{
		public FailReturn() : base(ReturnCodeEnum.fail) { }
	}
	public class ParamErrorReturn : ReturnModel
	{
		public ParamErrorReturn() : base(ReturnCodeEnum.param_error) { }
		public ParamErrorReturn(string paramName) : base(ReturnCodeEnum.param_error)
		{
			this.Message = "缺少參數:" + paramName;
		}
	}
}
