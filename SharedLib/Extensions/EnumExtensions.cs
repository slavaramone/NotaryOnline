using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace SharedLib.Extensions
{
	public static class EnumExtensions
	{
		public static string GetDescription(this Enum enumElement)
		{
			Type type = enumElement.GetType();

			MemberInfo[] memInfo = type.GetMember(enumElement.ToString());
			if (memInfo != null && memInfo.Length > 0)
			{
				object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
				if (attrs != null && attrs.Length > 0)
					return ((DescriptionAttribute)attrs[0]).Description;
			}

			return enumElement.ToString();
		}

		public static List<string> GetDescriptions(Type enumType)
		{
			var enumValues = Enum.GetValues(enumType);

			var enumDescs = new List<string>();
			foreach (var enumValue in enumValues)
			{
				Type type = enumValue.GetType();

				MemberInfo[] memInfo = type.GetMember(enumValue.ToString());
				if (memInfo != null && memInfo.Length > 0)
				{
					var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
					enumDescs.Add(((DescriptionAttribute)attrs[0]).Description);
				}
			};

			return enumDescs;
		}

		public static T GetValueFromDescription<T>(string description) where T : Enum
		{
			foreach (var field in typeof(T).GetFields())
			{
				if (Attribute.GetCustomAttribute(field,
				typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
				{
					if (attribute.Description == description)
						return (T)field.GetValue(null);
				}
				else
				{
					if (field.Name == description)
						return (T)field.GetValue(null);
				}
			}
			return default(T);
		}
	}
}
