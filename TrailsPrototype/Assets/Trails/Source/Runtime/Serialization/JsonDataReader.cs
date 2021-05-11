using System;
using System.IO;
using System.Text;

#if WINDOWS_STORE
using TP = System.Reflection.TypeInfo;
#else
using TP = System.Type;
#endif

namespace  Trails.Serialization
{
	/// <summary>
	/// An <see cref="IDataReader"/> adapter for <see cref="JsonDataReader"/>
	/// </summary>
	public class JsonDataReader : IDataReader
	{
		#region Constants

		public const string JsonMimeType = JsonWriter.JsonMimeType;
		public const string JsonFileExtension = JsonWriter.JsonFileExtension;

		#endregion Constants

		#region Fields

		private readonly JsonReaderSettings Settings;

		#endregion Fields

		#region Init

		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="settings">JsonWriterSettings</param>
		public JsonDataReader(JsonReaderSettings settings)
		{
			if (settings == null)
			{
				throw new ArgumentNullException("settings");
			}
			this.Settings = settings;
		}

		#endregion Init

		#region IDataReader Members

		/// <summary>
		/// Gets the content type
		/// </summary>
		public string ContentType
		{
			get { return JsonDataReader.JsonMimeType; }
		}

		/// <summary>
		/// Deserializes a data object of Type <param name="type">type</param> from the <param name="input">input</param>
		/// </summary>
		/// <param name="output"></param>
		/// <param name="data"></param>
		public object Deserialize(TextReader input, Type type)
		{
			return new JsonReader(input, this.Settings).Deserialize(type);
		}

		#endregion IDataReader Members

		#region Methods

		/// <summary>
		/// Builds a common settings objects
		/// </summary>
		/// <param name="allowNullValueTypes"></param>
		/// <returns></returns>
		public static JsonReaderSettings CreateSettings(bool allowNullValueTypes)
		{
			JsonReaderSettings settings = new JsonReaderSettings();

			settings.AllowNullValueTypes = allowNullValueTypes;

			return settings;
		}

		#endregion Methods
	}
}
