#region License
/*---------------------------------------------------------------------------------*\

	Distributed under the terms of an MIT-style license:

	The MIT License

	Copyright (c) 2006-2009 Stephen M. McKamey

	Permission is hereby granted, free of charge, to any person obtaining a copy
	of this software and associated documentation files (the "Software"), to deal
	in the Software without restriction, including without limitation the rights
	to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
	copies of the Software, and to permit persons to whom the Software is
	furnished to do so, subject to the following conditions:

	The above copyright notice and this permission notice shall be included in
	all copies or substantial portions of the Software.

	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
	IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
	FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
	AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
	LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
	OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
	THE SOFTWARE.

\*---------------------------------------------------------------------------------*/
#endregion License
using System;
using System.Reflection;

#if WINDOWS_STORE
using TP = System.Reflection.TypeInfo;
#else
using TP = System.Type;
#endif

using TCU =  Trails.Serialization.TypeCoercionUtility;

namespace  Trails.Serialization
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
	public class BTPropertyAttribute : Attribute
	{
		public string PropertyName { get; set; }

		public BTPropertyAttribute()
		{
			PropertyName = null;
		}

		public BTPropertyAttribute(string propertyName)
		{
			PropertyName = propertyName;
		}

		/// <summary>
		/// Gets the name specified for use in serialization.
		/// </summary>
		/// <returns></returns>
		public static string GetPropertyName(object value)
		{
			if(value == null)
			{
				return null;
			}

			Type type = value.GetType();
			MemberInfo memberInfo = null;

			if(TCU.GetTypeInfo(type).IsEnum)
			{
				string name = Enum.GetName(type, value);
				if(String.IsNullOrEmpty(name))
				{
					return null;
				}
				memberInfo = TCU.GetTypeInfo(type).GetField(name);
			}
			else
			{
				memberInfo = value as MemberInfo;
			}

			if(MemberInfo.Equals(memberInfo, null))
			{
				throw new ArgumentException();
			}

#if WINDOWS_STORE
			BTPropertyAttribute attribute = memberInfo.GetCustomAttribute<BTPropertyAttribute>(true);
#else
			BTPropertyAttribute attribute = Attribute.GetCustomAttribute(memberInfo, typeof(BTPropertyAttribute)) as BTPropertyAttribute;
#endif
			return attribute != null ? attribute.PropertyName : null;
		}
	}
}