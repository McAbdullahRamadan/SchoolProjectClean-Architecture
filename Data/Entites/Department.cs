using Data.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entites
{
    public class Department : GenralLocalizableEntities
    {
        public Department()
        {
            Students = new HashSet<Student>();
            DepartmentSubjects = new HashSet<DepartmentSubject>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DID { get; set; }

        [StringLength(100)]
        public string? DNameAr { get; set; }
        [StringLength(100)]
        public string? DNameEn { get; set; }
        public int? InsManager { get; set; }


        [InverseProperty("Departments")]
        public virtual ICollection<Student> Students { get; set; }
        [InverseProperty("Departments")]

        public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; }
        [InverseProperty("department")]
        public virtual ICollection<Instructor>? Instructors { get; set; }
        [ForeignKey("InsManager")]
        [InverseProperty("departmentManager")]
        public virtual Instructor? Instructor { get; set; }

    }
}
