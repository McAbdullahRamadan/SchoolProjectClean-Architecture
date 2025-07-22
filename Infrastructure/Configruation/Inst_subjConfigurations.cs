using Data.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configruation
{
    public class Inst_subjConfigurations : IEntityTypeConfiguration<Instructor_Subject>
    {
        public void Configure(EntityTypeBuilder<Instructor_Subject> builder)
        {
            builder.HasKey(x => new { x.SubId, x.InsId });

            builder.HasOne(ds => ds.Instructor)
                .WithMany(d => d.inst_Sub)
                .HasForeignKey(f => f.InsId);


            builder.HasOne(ds => ds.subjects)
                .WithMany(d => d.Instr_Sub)
                .HasForeignKey(ds => ds.SubId);

        }
    }
}
