namespace Model.DataAccess.Repositories
{
    using System.Threading.Tasks;

    public interface IDisciplineRepository
    {
        public List<Discipline> GetAll();

        public Task<Discipline?> GetById(long id);

        public Task Add(Discipline group);

        public void Update(Discipline group);

        public Task Remove(long id);
    }
}
