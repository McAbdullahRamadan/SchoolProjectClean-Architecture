using Data.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entites
{
    public class Student : GenralLocalizableEntities
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int StudID { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }

        [StringLength(100)]
        public string? Adreess { get; set; }

        [StringLength(100)]
        public string? Phone { get; set; }

        public int? DID { get; set; }

        [ForeignKey("DID")]
        [InverseProperty("Students")]
        public virtual Department? Departments { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<StudentSubject>? studentSubjects { get; set; }

    }
}
