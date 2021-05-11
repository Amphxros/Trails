using System;
using System.IO;
using System.Text;
using Trails.Serialization;

#if WINDOWS_STORE
using TP = System.Reflection.TypeInfo;
#else
using TP = System.Type;
#endif

namespace  Trails.Serialization
{
	/// <summary>
	/// An <see cref="IDataWriter"/> adapter for <see cref="JsonWriter"/>
	/// </summary>
	public class JsonDataWriter : IDataWriter
	{
		#region Constants

		public const string JsonMimeType = JsonWriter.JsonMimeType;
		public const string JsonFileExtension = JsonWriter.JsonFileExtension;

		#endregion Constants

		#region Fields

		private readonly JsonWriterSettings Settings;

		#endregion Fields

		#region Init

		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="settings">JsonWriterSettings</param>
		public JsonDataWriter(JsonWriterSettings settings)
		{
			if (settings == null)
			{
				throw new ArgumentNullException("settings");
			}
			this.Settings = settings;
		}

		#endregion Init

		#region IDataSerializer Members

		/// <summary>
		/// Gets the content encoding
		/// </summary>
		public Encoding ContentEncoding
		{
			get { return Encoding.UTF8; }
		}

		/// <summary>
		/// Gets the content type
		/// </summary>
		public string ContentType
		{
			get { return JsonDataWriter.JsonMimeType; }
		}

		/// <summary>
		/// Gets the file extension
		/// </summary>
		public string FileExtension
		{
			get { return JsonDataWriter.JsonFileExtension; }
		}

		/// <summary>
		/// Serializes the data object to the output
		/// </summary>
		/// <param name="output"></param>
		/// <param name="data"></param>
		public void Serialize(TextWriter output, object data)
		{
			new JsonWriter(output, this.Settings).Write(data);
		}

		#endregion IDataSerializer Members

		#region Methods

		/// <summary>
		/// Builds a common settings objects
		/// </summary>
		/// <param name="prettyPrint"></param>
		/// <returns></returns>
		public static JsonWriterSettings CreateSettings(bool prettyPrint)
		{
			JsonWriterSettings settings = new JsonWriterSettings();

			settings.PrettyPrint = prettyPrint;

			return settings;
		}

		#endregion Methods
	}
}
