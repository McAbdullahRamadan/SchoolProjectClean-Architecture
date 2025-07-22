using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entites
{
    public class DepartmentSubject
    {
        [Key]

        public int DepSubID { get; set; }
        public int DID { get; set; }
        public int SubID { get; set; }
        [ForeignKey("DID")]
        [InverseProperty("DepartmentSubjects")]
        public virtual Department? Departments { get; set; }
        [ForeignKey("SubID")]
        [InverseProperty("DepartmentSubjects")]

        public virtual Subject? Subjects { get; set; }

    }
}
