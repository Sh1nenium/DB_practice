namespace Model.DataAccess.Repositories
{
    using System.Threading.Tasks;

    public interface IEmployeeRepository
    {
        public List<Employee> GetAll();

        public Task<Employee> GetById(int id);

        public Task Add(Employee employee);

        public Task Update(Employee employee);

        public Task Remove(Employee employee);
    }
}