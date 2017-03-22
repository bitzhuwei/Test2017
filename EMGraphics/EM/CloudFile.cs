using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EMGraphics
{
	/// <summary>
	/// information(vertex positions, triangles etc) laoded from a *.nas file.
	/// </summary>
	public class CloudFile
	{
		private static readonly string[] separator = new string[] { " " };

		public float MaxValue { get; private set; }

		public float MinValue { get; private set; }

		public List<float> PropertyValues { get; private set; }

		private CloudFile()
		{
			this.PropertyValues = new List<float>();
		}

		public static CloudFile Load(string filename)
		{
			CloudFile result = new CloudFile();

			using (var reader = new StreamReader(filename))
			{
				ReadMaxValue(result, reader);
				ReadMinValue(result, reader);
				ReadPropertyValues(result, reader);
			}

			return result;
		}

		private static void ReadPropertyValues(CloudFile result, StreamReader reader)
		{
			while (!reader.EndOfStream)
			{
				string line = reader.ReadLine();
				if (line == null || line == string.Empty) { break; }

				float value = float.Parse(line);
				result.PropertyValues.Add(value);
			}
		}

		private static void ReadMinValue(CloudFile result, StreamReader reader)
		{
			string line = reader.ReadLine();
			float value = float.Parse(line);
			result.MinValue = value;
		}

		private static void ReadMaxValue(CloudFile result, StreamReader reader)
		{
			string line = reader.ReadLine();
			float value = float.Parse(line);
			result.MaxValue = value;
		}
	}
}
