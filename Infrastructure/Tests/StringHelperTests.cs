using System;
using Autumn.Mvc.Infrastructure.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Autumn.Mvc.Infrastructure.Tests
{
	[TestClass]
	public class StringHelperTests
	{
		[TestMethod]
		public void StringHelperGuidSame()
		{
			//Arrange
			string input = "string to turn into guid";

			//Act
			Guid g1 = StringHelper.StringToGuid(input);
			Guid g2 = StringHelper.StringToGuid(input);

			//Assert
			Assert.AreEqual(g1, g2);
		}

		[TestMethod]
		public void StringHelperGuidDifferent()
		{
			//Arrange
			string input1 = "string to turn into guid";
			string input2 = "a different string";

			//Act
			Guid g1 = StringHelper.StringToGuid(input1);
			Guid g2 = StringHelper.StringToGuid(input2);

			//Assert
			Assert.AreNotEqual(g1, g2);
		}

		[TestMethod]
		public void StringHelperGuidLong()
		{
			//Arrange
			string input = "This is a very long string that we are going to test out and do some stuff with to see if it causes any problem in the string helper.  Hopefully, there won't be any issue with the length of the string, but we'll see what we get.";

			//Act
			Guid g1 = StringHelper.StringToGuid(input);

			//Assert
			//Essentially here, we're just making sure we got an actual guid, and it didn't throw an error
			Assert.AreNotEqual(Guid.Empty, g1);
		}

		[TestMethod]
		public void StringHelperGuidShort()
		{
			//Arrange
			string input = "a";

			//Act
			Guid g1 = StringHelper.StringToGuid(input);

			//Assert
			Assert.AreNotEqual(Guid.Empty, g1);
		}

		[TestMethod]
		public void StringHelperGuidEmptyNull()
		{
			//Arrange
			string input = string.Empty;
			string input2 = null;
			string input3 = " ";

			//Act
			Guid g1 = StringHelper.StringToGuid(input);
			Guid g2 = StringHelper.StringToGuid(input2);
			Guid g3 = StringHelper.StringToGuid(input3);
			Guid g4 = StringHelper.StringToGuid(input3);		//make sure it treats " " the same as null/whitespace

			//Assert
			Assert.AreNotEqual(Guid.Empty, g1);
			Assert.AreNotEqual(Guid.Empty, g2);
			Assert.AreNotEqual(Guid.Empty, g3);
			Assert.AreNotEqual(Guid.Empty, g4);
			Assert.AreNotEqual(g1, g2);				//it will generate a new guid, so make sure they're unique
			Assert.AreNotEqual(g3, g4);
		}

		[TestMethod]
		public void StringHelperRandomSame()
		{
			//Arrange - nothing to do

			//Act
			string value1 = StringHelper.GetRandomString();
			string value2 = StringHelper.GetRandomString();

			//Assert
			Assert.AreNotEqual(value1, value2);
			Assert.AreNotEqual(string.Empty, value1);
		}

		[TestMethod]
		public void StringHelperRandomLength()
		{
			//Arrange
			int min1 = 10, max1 = 100;
			int min2 = 1, max2 = 500;
			int min3 = 10, max3 = int.MaxValue;
			int min4 = 5, max4 = 5;

			//Act
			string value1 = StringHelper.GetRandomString(min1, max1);
			string value2 = StringHelper.GetRandomString(min2, max2);
			string value3 = StringHelper.GetRandomString(min3, max3);
			string value4 = StringHelper.GetRandomString(min4, max4);

			//Assert
			Assert.IsTrue(value1.Length >= min1 && value1.Length <= max1);
			Assert.IsTrue(value2.Length >= min2 && value2.Length <= max2);
			Assert.IsTrue(value3.Length >= min3 && value3.Length <= max3);
			Assert.IsTrue(value4.Length >= min4 && value4.Length <= max4);

		}

		[TestMethod]
		public void StringHelperRandomSameLength()
		{
			int min1 = 50, max1 = 50;
			string value = StringHelper.GetRandomString(min1, max1);
			Assert.AreEqual(min1, value.Length);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void StringHelperRandomNegativeMinLength()
		{
			int min1 = -1, max1 = 10;
			string value = StringHelper.GetRandomString(min1, max1);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void StringHelperRandomNegativeMaxLength()
		{
			int min1 = -1, max1 = 10;
			string value = StringHelper.GetRandomString(min1, max1);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void StringHelperRandomMaxGreaterThanMin()
		{
			int min1 = 10, max1 = 5;
			string value = StringHelper.GetRandomString(min1, max1);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void StringHelperRandomZeroMinLength()
		{
			int min4 = 0, max4 = 10;
			string value4 = StringHelper.GetRandomString(min4, max4);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void StringHelperRandomZeroMaxLength()
		{
			int min4 = 10, max4 = 0;
			string value4 = StringHelper.GetRandomString(min4, max4);
		}
	}
}
