using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
    public class ClassUser
    {
        [Key]
        public int ClassId { set; get; }
        [ForeignKey("ClassId")]
        public Class classId { set; get; }
        [Key]
        public string UserId { set; get; }
        [ForeignKey("UserId")]
        public User User { set; get; }

    }
}
