
using System;
using System.Reflection;
#if !UNITY3D
using System.Xml.Serialization;
#endif

#if WINDOWS_STORE
using TP = System.Reflection.TypeInfo;
#else
using TP = System.Type;
#endif

using TCU = Trails.Serialization.TypeCoercionUtility;

namespace Trails.Serialization
{
	/// <summary>
	/// Designates a property or field to not be serialized.
	/// </summary>
	[AttributeUsage(AttributeTargets.All, AllowMultiple=false)]
	public sealed class BTIgnoreAttribute : Attribute
	{
		#region Methods

		/// <summary>
		/// Gets a value which indicates if should be ignored in Json serialization.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool IsJsonIgnore(object value)
		{
			if (value == null)
			{
				return false;
			}

			Type type = value.GetType();

			ICustomAttributeProvider provider = null;
			if (TCU.GetTypeInfo(type).IsEnum) {
				provider = TCU.GetTypeInfo(type).GetField(Enum.GetName(type, value));
			} else {
				provider = value as ICustomAttributeProvider;
			}

			if (provider == null) {
				throw new ArgumentException();
			}

			return provider.IsDefined(typeof(BTIgnoreAttribute), true);
		}

		/// <summary>
		/// Gets a value which indicates if should be ignored in Json serialization.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool IsXmlIgnore(object value)
		{
			if (value == null)
			{
				return false;
			}

			Type type = value.GetType();

			ICustomAttributeProvider provider = null;
			if (TCU.GetTypeInfo(type).IsEnum)
			{
				provider = TCU.GetTypeInfo(type).GetField(Enum.GetName(type, value));
			}
			else
			{
				provider = value as ICustomAttributeProvider;
			}

			if (provider == null)
			{
				throw new ArgumentException();
			}

#if !UNITY3D
			return provider.IsDefined(typeof(XmlIgnoreAttribute), true);
#else
			return false;
#endif
		}

		#endregion Methods
	}
}
