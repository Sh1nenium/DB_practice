namespace Model.DataAccess.Repositories
{
    using System.Threading.Tasks;

    public interface IDisciplineRepository
    {
        public List<Discipline> GetAll();

        public Task<Discipline?> GetById(int id);

        public Task Add(Group group);

        public void Update(Group group);

        public Task Remove(Group group);
    }
}
