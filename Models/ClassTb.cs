using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
	public class ClassTb
	{
		[Key]
		public int ClassId { set; get; }
		[ForeignKey("ClassId")]
		public Class classId { set; get; }
		[Key]
		public int TimetableId { set; get; }
		[ForeignKey("TimetableId")]
		public PTimetable timetableId { set; get; }
	}
}
