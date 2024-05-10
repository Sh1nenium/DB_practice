using Model;
using Model.DataAccess.Repositories;

namespace ViewModel.UseCases
{
    using Model.DataAccess;
    using System.Threading.Tasks;

    public class GroupRepository : IGroupRepository
    {
        public Task Add(Group group)
        {
            throw new NotImplementedException();
        }

        public List<Group> GetAll()
        {
            using Context context = new();
            return [.. context.Groups];
        }

        public async Task<Group?> GetById(long id)
        {
            using Context context = new();
            return await context.Groups.FindAsync(id);
        }

        public Task Remove(long id)
        {
            throw new NotImplementedException();
        }

        public void Update(Group group)
        {
            throw new NotImplementedException();
        }
    }
}
