using Data.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entites
{
    public class Subject : GenralLocalizableEntities
    {
        public Subject()
        {
            StudentSubjects = new HashSet<StudentSubject>();
            DepartmentSubjects = new HashSet<DepartmentSubject>();
            Instr_Sub = new HashSet<Instructor_Subject>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubID { get; set; }
        [StringLength(500)]
        public string? SubjectNameAr { get; set; }
        public string? SubjectNameEn { get; set; }

        public int? Period { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }
        [InverseProperty("Subjects")]

        public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; }
        [InverseProperty("subjects")]

        public virtual ICollection<Instructor_Subject> Instr_Sub { get; set; }

    }
}
