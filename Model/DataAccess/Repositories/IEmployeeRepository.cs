namespace Model.DataAccess.Repositories
{
    using System.Threading.Tasks;

    public interface IEmployeeRepository
    {
        public List<Employee> GetAll();

        public Task<Employee?> GetById(long id);

        public Task Add(Employee employee);

        public void Update(Employee employee);

        public Task Remove(long id);
    }
}