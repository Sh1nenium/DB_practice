using Model.DataAccess;
using Model;
using Task = System.Threading.Tasks.Task;
using Model.DataAccess.Repositories;

namespace ViewModel.UseCases
{
    public partial class EmployeeDisciplineRepository : IEmployeeDisciplineRepository
    {
        public async Task Add(EmployeeDiscipline employeeDiscipline)
        {
            using Context context = new();
            await context.EmployeeDisciplines.AddAsync(employeeDiscipline);
            context.SaveChanges();
        }

        public async Task Remove(long employeeId, long disciplineId)
        {
            using Context context = new();

            EmployeeDiscipline? findedEmployeeDiscipline = await context
                .EmployeeDisciplines
                .FindAsync(employeeId, disciplineId);

            if (findedEmployeeDiscipline == null)
            {
                return;
            }

            context.EmployeeDisciplines.Remove(findedEmployeeDiscipline);
            await context.SaveChangesAsync();
        }

        public void Update(EmployeeDiscipline employeeDiscipline)
        {
            using Context context = new();
            context.EmployeeDisciplines.Update(employeeDiscipline);
            context.SaveChanges();
        }
    }
}
