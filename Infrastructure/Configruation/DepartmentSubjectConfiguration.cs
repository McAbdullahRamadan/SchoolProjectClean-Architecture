using Data.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configruation
{
    public class DepartmentSubjectConfiguration : IEntityTypeConfiguration<DepartmentSubject>
    {
        public void Configure(EntityTypeBuilder<DepartmentSubject> builder)
        {

            builder.HasKey(x => new { x.SubID, x.DID });


            builder.HasOne(x => x.Departments)
                .WithMany(d => d.DepartmentSubjects)
                .HasForeignKey(ds => ds.DID);


            builder.HasOne(x => x.Subjects)
                .WithMany(d => d.DepartmentSubjects)
                .HasForeignKey(ds => ds.SubID);
        }
    }
}
