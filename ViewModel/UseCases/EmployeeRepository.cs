using Model.DataAccess;
using Model;
using Model.DataAccess.Repositories;

namespace ViewModel.UseCases
{
    using System.Threading.Tasks;

    public class EmployeeRepository : IEmployeeRepository
    {
        public async Task Add(Employee employee)
        {
            using Context context = new();
            await context.Employee.AddAsync(employee);
            context.SaveChanges();
        }

        public List<Employee> GetAll()
        {
            using Context context = new();
            return [.. context.Employee];
        }

        public async Task<Employee?> GetById(long numberOfRecordBook)
        {
            using Context context = new();
            return await context.Employee.FindAsync(numberOfRecordBook);
        }

        public List<Employee> GetAllByDiscipline(long disciplineId)
        {
            using Context context = new();
            var query = context.Employee
                .Join(context.EmployeeDisciplines,
                    employee => employee.Id,
                    employeeDiscipline => employeeDiscipline.EmployeeId,
                    (employee, employeeDiscipline) => new { Employee = employee, EmployeeDiscipline = employeeDiscipline })
                .Where(x => x.EmployeeDiscipline.DisciplineId == disciplineId)
                .Select(x => x.Employee)
                .ToList();

            return query;
        }

        public async Task Remove(long id)
        {
            using Context context = new();
            Employee? findedEmployee = await context.Employee.FindAsync(id);

            if (findedEmployee == null) return;

            context.Employee.Remove(findedEmployee);
            context.SaveChanges();
        }

        public void Update(Employee employee)
        {
            using Context context = new();
            context.Employee.Update(employee);
            context.SaveChanges();
        }
    }
}
