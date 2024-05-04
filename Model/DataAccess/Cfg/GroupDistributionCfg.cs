using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.DataAccess.Cfg
{
    public class GroupDistributionCfg : IEntityTypeConfiguration<GroupDistribution>
    {
        public void Configure(EntityTypeBuilder<GroupDistribution> builder)
        {
            builder.HasOne<Group>()
                .WithMany()
                .HasForeignKey(x => x.GroupId);

            builder.HasOne<Discipline>()
                .WithMany()
                .HasForeignKey(x => x.DisciplineId);
        }
    }
}
