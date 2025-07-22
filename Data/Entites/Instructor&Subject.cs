using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entites
{
    public class Instructor_Subject
    {
        [Key]

        public int InsId { get; set; }
        [Key]
        public int SubId { get; set; }
        [ForeignKey(nameof(InsId))]
        [InverseProperty("inst_Sub")]
        public Instructor? Instructor { get; set; }
        [ForeignKey(nameof(SubId))]
        [InverseProperty("Instr_Sub")]


        public Subject? subjects { get; set; }



    }

}
