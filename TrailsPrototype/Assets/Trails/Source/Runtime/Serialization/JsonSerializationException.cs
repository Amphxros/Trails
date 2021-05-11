using System;

#if WINDOWS_STORE
using TP = System.Reflection.TypeInfo;
#else
using TP = System.Type;
#endif

namespace Trails.Serialization
{
	public class JsonSerializationException : InvalidOperationException
	{
		#region Init

		public JsonSerializationException() : base() { }

		public JsonSerializationException(string message) : base(message) { }

		public JsonSerializationException(string message, Exception innerException) : base(message, innerException) { }

		#endregion Init
	}

	public class JsonDeserializationException : JsonSerializationException
	{
		#region Fields

		private int index = -1;

		#endregion Fields

		#region Init

		public JsonDeserializationException(string message, int index) : base(message)
		{
			this.index = index;
		}

		public JsonDeserializationException(string message, Exception innerException, int index)
			: base(message, innerException)
		{
			this.index = index;
		}

		#endregion Init

		#region Properties

		/// <summary>
		/// Gets the character position in the stream where the error occurred.
		/// </summary>
		public int Index
		{
			get { return this.index; }
		}

		#endregion Properties

		#region Methods

		/// <summary>
		/// Helper method which converts the index into Line and Column numbers
		/// </summary>
		/// <param name="source"></param>
		/// <param name="line"></param>
		/// <param name="col"></param>
		public void GetLineAndColumn(string source, out int line, out int col)
		{
			if (source == null)
			{
				throw new ArgumentNullException();
			}

			col = 1;
			line = 1;

			bool foundLF = false;
			int i = Math.Min(this.index, source.Length);
			for (; i>0; i--)
			{
				if (!foundLF)
				{
					col++;
				}
				if (source[i-1] == '\n')
				{
					line++;
					foundLF = true;
				}
			}
		}

		public override string ToString () {
			return "JsonDeserializationException: Index=" + Index +"\n"+base.ToString();
		}

		#endregion Methods
	}

	public class JsonTypeCoercionException : ArgumentException
	{
		#region Init

		public JsonTypeCoercionException() : base() { }

		public JsonTypeCoercionException(string message) : base(message) { }

		public JsonTypeCoercionException(string message, Exception innerException) : base(message, innerException) { }

		#endregion Init
	}
}
