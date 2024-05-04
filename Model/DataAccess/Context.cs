using Microsoft.EntityFrameworkCore;
using Model.DataAccess.Cfg;

namespace Model.DataAccess
{
    public class Context : DbContext
    {
        public DbSet<Discipline> Disciplines { get; set; }

        public DbSet<Employee> Employee { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<TrainingManual> TrainingManuals { get; set; }

        public DbSet<Score> Scores { get; set; }

        public DbSet<EmployeeDiscipline> EmployeeDisciplines { get; set; }

        public DbSet<GroupDistribution> GroupDistributions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "host=localhost;port=5000;Database=DBTest;Username=postgres;Password=Sh1nen";
            optionsBuilder.UseNpgsql(connectionString)
                .UseAllCheckConstraints();
        }

        public Context()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentCfg());
            modelBuilder.ApplyConfiguration(new EmployeeDisciplineCfg());
            modelBuilder.ApplyConfiguration(new GroupDistributionCfg());
            modelBuilder.ApplyConfiguration(new DisciplineCfg());
            modelBuilder.ApplyConfiguration(new ScoreCfg());
        }
    }
}
