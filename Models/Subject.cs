using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
	[Table("Subject")]
	public class Subject
	{
		[Key]
		public string SubjectId { set; get; }
		public string? School { set; get; }
		public string SubjectName { set; get; }
		public string ESubjectName { set; get; }
		public string Difficulty { set; get; }
		public string EduProgram { set; get; }
		
	}
}
