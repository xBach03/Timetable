using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
	public class TempTimetable
	{
		[Key]
		public int TempID { set; get; }
		public int Term { set; get; }
		public string School { set; get; }
		public int ClassId { set; get; }
		public int AClassId { set; get; }
		public string SubjectId { set; get; }
		public string SubjectName { set; get; }
		public string ESubjectName { set; get; }
		public string Difficulty { set; get; }
		public string Note { set; get; }
		public int DateCount { set; get; }
		public int Weekday { set; get; }
		public string Time { set; get; }
		public int Start { set; get; }
		public int End { set; get; }
		public string Shift { set; get; }
		public string Week { set; get; }
		public string ClassRoom { set; get; }
		public string Experiment { set; get; }
		public int? Enrolled { set; get; }
		public int? MaxNumber { set; get; }
		public string Status { set; get; }
		public string ClassType { set; get; }
		public string OpenStage { set; get; }
		public string EduProgram { set; get; }

	}
}
