namespace Model.DataAccess.Repositories
{
    using System.Threading.Tasks;

    public interface IDisciplineRepository
    {
        public List<Discipline> GetAll();

        public Task<Discipline?> GetById(long id);

        public List<Discipline> SearchAllByString(string searchString);

        public List<Discipline> GetAllByEmployee(long employeeId);

        public Task Add(Discipline group);

        public void Update(Discipline group);

        public Task Remove(long id);
    }
}
