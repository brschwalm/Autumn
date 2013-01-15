using System;
using System.IO;
using System.Net.Http.Formatting;
using System.Security.Cryptography;
using System.Text;

namespace Autumn.Mvc.Infrastructure.Helpers
{
	/// <summary>
	/// Provides some string-based helper functions.
	/// </summary>
	public class StringHelper
	{
		/// <summary>
		/// Converts a string value into a GUID
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static Guid StringToGuid(string value)
		{
			//If they don't provide a value, just create a new guid
			if (string.IsNullOrWhiteSpace(value))
				return Guid.NewGuid();

			// Create a new instance of the MD5CryptoServiceProvider object.
			MD5 md5Hasher = MD5.Create();
			// Convert the input string to a byte array and compute the hash.
			byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
			return new Guid(data);
		}

		/// <summary>
		/// Gets a random string of a particular length
		/// </summary>
		/// <param name="minLength">The minimum length of the string</param>
		/// <param name="maxLength">The maximum length of the string</param>
		/// <returns>A random string of characters</returns>
		public static string GetRandomString(int minLength = 6, int maxLength = 20)
		{
			if (minLength <= 0) throw new ArgumentException("Minimum Length must be greater than zero.");
			if (maxLength <= 0) throw new ArgumentException("Maximum Length must be greater than zero, and greater than Minimum Length.");
			if (maxLength < minLength) throw new ArgumentException("Maximum Length must be greater than Minimum Length.");

			int length = 0;
			string result = string.Empty;

			while (length < minLength || length > maxLength)
			{
				if (length > maxLength)
				{
					//Too long, need to trim it
					result = result.Substring(0, maxLength);
				}
				else
				{
					string random = Path.GetRandomFileName().Replace(".", "");
					result += random;
				}

				length = result.Length;
			}

			return result;
		}

		/// <summary>
		/// Serializes the provided object given a specific formatter to use
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="formatter"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string Serialize<T>(MediaTypeFormatter formatter, T value)
		{
			// Create a dummy HTTP Content.
			Stream stream = new MemoryStream();
			var content = new System.Net.Http.StreamContent(stream);
			/// Serialize the object.
			formatter.WriteToStreamAsync(typeof(T), value, stream, content, null).Wait();
			// Read the serialized string.
			stream.Position = 0;
			return content.ReadAsStringAsync().Result;
		}

		/// <summary>
		/// Deserializes a string given the spefici formatter
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="formatter"></param>
		/// <param name="str"></param>
		/// <returns></returns>
		public static T Deserialize<T>(MediaTypeFormatter formatter, string str) where T : class
		{
			// Write the serialized string to a memory stream.
			Stream stream = new MemoryStream();
			StreamWriter writer = new StreamWriter(stream);
			writer.Write(str);
			writer.Flush();
			stream.Position = 0;
			// Deserialize to an object of type T
			return formatter.ReadFromStreamAsync(typeof(T), stream, null, null).Result as T;
		}

		/// <summary>
		/// Serializes an object to JSON
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string SerializeToJson<T>(T value)
		{
			var json = new JsonMediaTypeFormatter();
			return StringHelper.Serialize(json, value);
		}

		/// <summary>
		/// Deserializes an object from JSON
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="str"></param>
		/// <returns></returns>
		public static T DeserializeFromJson<T>(string str) where T : class
		{
			var json = new JsonMediaTypeFormatter();
			return StringHelper.Deserialize<T>(json, str);
		}
	}
}
