using Data.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configruation
{
    public class Student_SubjectConfigurations : IEntityTypeConfiguration<StudentSubject>
    {
        public void Configure(EntityTypeBuilder<StudentSubject> builder)
        {
            builder.HasKey(x => new { x.SubID, x.StudID });

            builder.HasOne(ds => ds.Student)
                .WithMany(d => d.studentSubjects)
                .HasForeignKey(ds => ds.StudID);

            builder.HasOne(ds => ds.Subject)
                .WithMany(x => x.StudentSubjects)
                .HasForeignKey(ds => ds.SubID);

        }
    }
}
