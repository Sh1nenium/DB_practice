namespace Model.DataAccess.Repositories
{
    using System.Threading.Tasks;

    public interface IEmployeeRepository
    {
        public List<Employee> GetAll();

        public Task<Employee?> GetById(long id);

        public List<Employee> SearchAllByString(string searchString);

        public List<Employee> GetAllByDiscipline(long disciplineId);

        public Task Add(Employee employee);

        public void Update(Employee employee);

        public Task Remove(long id);
    }
}