using System;
using System.IO;
using System.Text;

#if WINDOWS_STORE
using TP = System.Reflection.TypeInfo;
#else
using TP = System.Type;
#endif

namespace Trails.Serialization
{
	/// <summary>
	/// A common interface for data serializers
	/// </summary>
	public interface IDataWriter
	{

		#region Properties

		/// <summary>
		/// Gets the content encoding for the serialized data
		/// </summary>
		Encoding ContentEncoding
		{
			get;
		}

		/// <summary>
		/// Gets the content type for the serialized data
		/// </summary>
		string ContentType
		{
			get;
		}

		/// <summary>
		/// Gets the file extension for the serialized data
		/// </summary>
		string FileExtension
		{
			get;
		}

		#endregion Properties

		#region Methods

		/// <summary>
		/// Serializes the data to the given output
		/// </summary>
		/// <param name="output"></param>
		/// <param name="data"></param>
		void Serialize(TextWriter output, object data);

		#endregion Methods
	}
}
