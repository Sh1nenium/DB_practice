using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Model.DataAccess.Cfg;
using System.Resources;

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
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new StudentCfg());
            modelBuilder.ApplyConfiguration(new EmployeeDisciplineCfg());
            modelBuilder.ApplyConfiguration(new GroupDistributionCfg());
            modelBuilder.ApplyConfiguration(new DisciplineCfg());
            modelBuilder.ApplyConfiguration(new ScoreCfg());


            modelBuilder.Entity<Student>().HasData(new Student
            {
                NumberOfRecordBook = 1,
                Photo = File.ReadAllBytes("C:\\Users\\sabir\\source\\repos\\DB_practice\\View\\Resources\\1.jpg"),
                SabbaticalLeave = false,
                Name = "Дмитрий",
                Surname = "Сабиров",
                Patronymic = "Игоревич",
                SpecializationName = "Разработка ПО",
                PhoneNumber = "+7-983-231-95-09",
                Email = "sab@gmail.com",
                Street = "ул. Федор - Лыткино, 8",
                DateOfStudy = new DateOnly(2022, 02, 02)
            });

            modelBuilder.Entity<Group>().HasData(new Group
            {
                Id = 1,
                Name = "572-2",
                SpecializationName = "Разработка ПО",
                DateOfStudy = new DateOnly(2022, 02, 02)
            });

            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                Id = 1,
                Name = "Дмитрий",
                Surname = "Сабиров",
                Patronymic = "Игоревич",
                Post = "Преподаватель",
                PhoneNumber = "+7-983-231-95-09",
                Email = "sab@gmail.com",
                Street = "ул. Федор - Лыткино, 8",
                WorkExperience = "2 года",
                AcademicDegree = "Ученый",
            });

            modelBuilder.Entity<Discipline>().HasData(new Discipline
            {
                Id = 1,
                Name = "ОРБД",
                Description = "Описание"
            });
        }
    }
}
