using System;

#if WINDOWS_STORE
using TP = System.Reflection.TypeInfo;
#else
using TP = System.Type;
#endif

namespace Trails.Serialization
{
	/// <summary>
	/// Allows classes to control their own JSON serialization
	/// </summary>
	public interface IJsonSerializable
	{
		void ReadJson(JsonReader reader);
		void WriteJson(JsonWriter writer);
	}
}
