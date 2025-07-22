using Data.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entites
{
    public class Instructor : GenralLocalizableEntities
    {
        public Instructor()
        {
            Instructors = new HashSet<Instructor>();
            inst_Sub = new HashSet<Instructor_Subject>();


        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int InsId { get; set; }
        public string? ENameAr { get; set; }
        public string? ENameEn { get; set; }
        public string? Address { get; set; }

        public string? Position { get; set; }
        public int? SupervisorId { get; set; }
        public decimal? Salary { get; set; }
        public int? DID { get; set; }

        [ForeignKey(nameof(DID))]
        [InverseProperty("Instructors")]
        public Department? department { get; set; }

        [InverseProperty("Instructor")]
        public Department? departmentManager { get; set; }


        [ForeignKey(nameof(SupervisorId))]
        [InverseProperty("Instructors")]
        public Instructor? Supervisor { get; set; }

        [InverseProperty("Supervisor")]
        public virtual ICollection<Instructor> Instructors { get; set; }

        [InverseProperty("Instructor")]
        public virtual ICollection<Instructor_Subject> inst_Sub { get; set; }









    }
}
