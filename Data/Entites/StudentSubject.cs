using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entites
{
    public class StudentSubject
    {
        [Key]

        public int StudSubID { get; set; }
        public int StudID { get; set; }
        public int SubID { get; set; }
        public decimal? grade { get; set; }

        [ForeignKey("StudID")]
        [InverseProperty("studentSubjects")]
        public virtual Student? Student { get; set; }
        [ForeignKey("SubID")]
        [InverseProperty("StudentSubjects")]
        public virtual Subject? Subject { get; set; }
    }
}
