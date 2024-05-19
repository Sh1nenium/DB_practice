using Model.DataAccess;
using Model;
using Model.DataAccess.Repositories;

namespace ViewModel.UseCases
{
    using System.Threading.Tasks;

    public class DisciplineRepository : IDisciplineRepository
    {
        public async Task Add(Discipline discipline)
        {
            using Context context = new();
            await context.Disciplines.AddAsync(discipline);
            context.SaveChanges();
        }

        public List<Discipline> GetAll()
        {
            using Context context = new();
            return [.. context.Disciplines];
        }

        public List<Discipline> SearchAllByString(string searchString)
        {
            using Context context = new();
            return [.. context.Disciplines
                .Where(x => (x.Name)
                .Contains(searchString))];
        }

        public List<Discipline> GetAllByEmployee(long employeeId)
        {
            using Context context = new();
            var query = context.Disciplines
                .Join(context.EmployeeDisciplines,
                    discipline => discipline.Id,
                    employeeDiscipline => employeeDiscipline.EmployeeId,
                    (discipline, employeeDiscipline) => new { Discipline = discipline, EmployeeDiscipline = employeeDiscipline })
                .Where(x => x.EmployeeDiscipline.DisciplineId == employeeId)
                .Select(x => x.Discipline)
                .ToList();

            return query;
        }

        public async Task<Discipline?> GetById(long numberOfRecordBook)
        {
            using Context context = new();
            return await context.Disciplines.FindAsync(numberOfRecordBook);
        }

        public async Task Remove(long id)
        {
            using Context context = new();
            Discipline? findedDiscipline = await context.Disciplines.FindAsync(id);

            if (findedDiscipline == null) return;

            context.Disciplines.Remove(findedDiscipline);
            context.SaveChanges();
        }

        public void Update(Discipline discipline)
        {
            using Context context = new();
            context.Disciplines.Update(discipline);
            context.SaveChanges();
        }
    }
}
