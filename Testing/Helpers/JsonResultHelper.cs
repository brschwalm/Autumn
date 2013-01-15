using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Moq;

namespace Anythink.Mvc.Testing
{

	/// <summary>
	/// This class will help with JsonResult, which require some extra
	/// work.  Because a JsonResult is just an ActionResult that requires the
	/// ExecuteResult() method to get any output, this class will provide the necessary
	/// mocks and make for easier access to there result, either as a string,
	/// or deserialized to a specific type.
	/// See: http://www.heartysoft.com/ASPNET-MVC-Unit-Testing-JsonResult-Returning-Anonymous-Types
	/// for more information and alternatives to this approach.
	/// </summary>
	public class JsonResultHelper
	{
		private Mock<ControllerContext> _mockContext;
		private Mock<HttpResponseBase> _mockResponse;
		private StringBuilder _responseStringBuilder;

		/// <summary>
		/// Creates the helper and initializes it, unless specified otherwise
		/// </summary>
		/// <param name="manualInitialize"></param>
		public JsonResultHelper(bool manualInitialize = false)
		{
			if (!manualInitialize)
				this.Initialize();
		}

		/// <summary>
		/// Initialize the class and setup the mocks to handle the result
		/// </summary>
		public void Initialize()
		{
			_mockContext = new Mock<ControllerContext>();
			_mockResponse = new Mock<HttpResponseBase>();
			_responseStringBuilder = new StringBuilder();

			_mockResponse.Setup(x => x.Write(It.IsAny<string>())).Callback<string>(y => { _responseStringBuilder.Append(y); });
			_mockContext.Setup(x => x.HttpContext.Response).Returns(_mockResponse.Object);

			this.ControllerContext = _mockContext.Object;
		}

		/// <summary>
		/// Processes the result and gets the json result.
		/// </summary>
		/// <param name="result"></param>
		public void Process(JsonResult result)
		{
			result.ExecuteResult(this.ControllerContext);
		}

		/// <summary>
		/// The ControllerContext to use to Execute the result
		/// </summary>
		public ControllerContext ControllerContext { get; private set; }

		/// <summary>
		/// The results as a string.
		/// </summary>
		public string ResultString { get { return _responseStringBuilder.ToString(); } }

		/// <summary>
		/// Deserializes the JsonResult and returns the specified type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public T Deseralize<T>()
		{
			JavaScriptSerializer serializer = new JavaScriptSerializer();
			T result = serializer.Deserialize<T>(this.ResultString);
			return result;

			//Interesting... DataContractSerializer doesn't seem to work on SurgeFormResponse
			//DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
			//byte[] bytes = Encoding.UTF8.GetBytes(this.ResultString);
			//using (MemoryStream stream = new MemoryStream(bytes))
			//{
			//	return (T)serializer.ReadObject(stream);
			//}
		}
	}
}
