using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
	public class ClassTime
	{
		[Key]
		public int ClassId { set; get; }
		[ForeignKey("ClassId")]
		public Class classId { set; get; }
		[Key]
		public int DateCount { set; get; }
		public int Weekday { set; get; }
		public string Time { set; get; }
		public int Start { set; get; }
		public int End { set; get; }
		public string Week { set; get; }
		public string Shift { set; get; }
		public string ClassRoom { set; get; }
		
	}
}
