using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BlogAPI.Common
{
	public static class EnumExtenstions
	{
        public static string GetEnumDescription(this Enum value)
        {
            return value.GetType()
                .GetRuntimeField(value.ToString())
                .GetCustomAttributes<System.ComponentModel.DescriptionAttribute>()
                .FirstOrDefault()?.Description ?? string.Empty;
        }

    }
}
