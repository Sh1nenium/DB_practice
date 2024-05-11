using Model;
using Model.DataAccess.Repositories;

namespace ViewModel.UseCases
{
    using Model.DataAccess;
    using System.Threading.Tasks;

    public class GroupRepository : IGroupRepository
    {
        public async Task Add(Group group)
        {
            using Context context = new();
            await context.Groups.AddAsync(group);
            context.SaveChanges();
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

        public async Task Remove(long id)
        {
            using Context context = new();
            Group? findedGroup = await context.Groups.FindAsync(id);

            if (findedGroup == null) return;

            context.Groups.Remove(findedGroup);
            context.SaveChanges();
        }

        public void Update(Group group)
        {
            using Context context = new();
            context.Groups.Update(group);
            context.SaveChanges();
        }
    }
}
