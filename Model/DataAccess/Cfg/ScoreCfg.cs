using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.DataAccess.Cfg
{
    public class ScoreCfg : IEntityTypeConfiguration<Score>
    {
        public void Configure(EntityTypeBuilder<Score> builder)
        {
            builder.HasOne<Student>()
                .WithMany()
                .HasForeignKey(x => x.NumberOfRecordBook);

            builder.HasOne<Task>()
                .WithMany()
                .HasForeignKey(x => x.TaskId);
        }
    }
}
