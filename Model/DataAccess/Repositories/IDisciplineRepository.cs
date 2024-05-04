namespace Model.DataAccess.Repositories
{
    using System.Threading.Tasks;

    public interface IDisciplineRepository
    {
        public List<Group> GetAll();

        public Task<Group> GetById(int id);

        public Task Add(Group group);

        public Task Update(Group group);

        public Task Remove(Group group);
    }
}
