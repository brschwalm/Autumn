using System;

namespace Autumn.Mvc.Infrastructure.Domain.Helpers
{
	public static class DateTimeHelper
	{
		static DateTimeHelper()
		{
			MinSqlDate = new DateTime(1900, 1, 1);
		}

		public static DateTime MinSqlDate { get; private set; }		//Minimum date for SQL Server's DateTime
	}
}
