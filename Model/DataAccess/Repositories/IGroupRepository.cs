namespace Model.DataAccess.Repositories
{
    using System.Threading.Tasks;

    public interface IGroupRepository
    {
        public List<Group> GetAll();

        public Task<Group?> GetById(long id);

        public List<Group> SearchAllByString(string searchString);

        public Task Add(Group group);

        public void Update(Group group);

        public Task Remove(long id);
    }
}
