namespace Model.DataAccess.Repositories
{
    using System.Threading.Tasks;
    public interface IEmployeeDisciplineRepository
    {
        public Task Add(EmployeeDiscipline group);

        public void Update(EmployeeDiscipline group);

        public Task Remove(long employeeId, long disciplineId);
    }
}
    