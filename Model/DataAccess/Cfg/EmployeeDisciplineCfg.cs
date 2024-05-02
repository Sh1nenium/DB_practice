using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.DataAccess.Cfg
{
    public class EmployeeDisciplineCfg : IEntityTypeConfiguration<EmployeeDiscipline>
    {
        public void Configure(EntityTypeBuilder<EmployeeDiscipline> builder)
        {
            builder.HasOne<Discipline>()
                .WithMany()
                .HasForeignKey(x => x.DisciplineId);

            builder.HasOne<Employee>()
                .WithMany()
                .HasForeignKey(x => x.EmployeeId);
        }
    }
}
