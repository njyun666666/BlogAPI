using BlogAPI.Common;
using BlogAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlogAPI.Models
{
	public class ReturnModel
	{
		public int code { get; set; }
		public string message { get; set; }
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public object data { get; set; }

		public ReturnModel(int _code, string _message = null, object _data = null)
		{
			code = _code;
			message = _message;
			data = _data;

			if (string.IsNullOrWhiteSpace(_message))
			{
				message = EnumExtenstions.GetEnumDescription((ReturnCodeEnum)_code);
			}
		}

		public ReturnModel(ReturnCodeEnum _code, string _message = null, object _data = null)
		{
			code = (int)_code;
			message = _message;
			data = _data;

			if (string.IsNullOrWhiteSpace(_message))
			{
				message = EnumExtenstions.GetEnumDescription(_code);
			}
		}
	}

	public class OkReturn : ReturnModel
	{
		public OkReturn() : base(ReturnCodeEnum.success) { }
		public OkReturn(object data) : base(ReturnCodeEnum.success)
		{
			this.data = data;
		}
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
			this.message = "缺少參數:" + paramName;
		}
	}
	public class ExceptionReturn : ReturnModel
	{
		public ExceptionReturn() : base(ReturnCodeEnum.exception) { }
	}

}
