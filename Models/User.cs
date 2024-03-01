using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
	[Table("User")]
	public class User : IdentityUser
	{
		public string? Course { set; get; }
		public string? Major { set; get; }
	}
}