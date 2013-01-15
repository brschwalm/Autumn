using System;
using System.ComponentModel.DataAnnotations;

namespace Autumn.Mvc.Infrastructure.Domain
{
	/// <summary>
	/// An entry into a Log table
	/// </summary>
	public class LogEntry
	{
		[Key]
		public int Id { get; set; }

		public DateTime Date { get; set; }
		
		public string User { get; set; }

		[MaxLength(50)]
		public string Level { get; set; }
	
		[MaxLength(255)]
		public string Logger { get; set; }
		
		[MaxLength(4000)]
		public string Message { get; set; }

		[MaxLength(2000)]
		public string Exception { get; set; }
	}
}
