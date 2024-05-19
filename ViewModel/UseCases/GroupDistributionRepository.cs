using Model;
using Model.DataAccess.Repositories;

namespace ViewModel.UseCases
{
    using Microsoft.EntityFrameworkCore;
    using Model.DataAccess;
    using System.Security.Cryptography;
    using System.Threading.Tasks;

    public class GroupDistributionRepository : IGroupDistributionRepository
    {
        public async Task Add(GroupDistribution groupDistribution)
        {
            using Context context = new();
            await context.GroupDistributions.AddAsync(groupDistribution);
            context.SaveChanges();
        }

        public List<GroupDistribution> GetAll()
        {
            using Context context = new();
            return [.. context.GroupDistributions];
        }

        public List<GroupDistribution> GetAllByGroup(long groupId)
        {
            using Context context = new();
            return [.. context.GroupDistributions.Where(x => x.GroupId == groupId).Include(x => x.Discipline)];
        }

        public async Task Remove(long groupId, long disciplineId)
        {
            using Context context = new();

            GroupDistribution? findedGroupDistributions = await context
                .GroupDistributions
                .FindAsync(groupId, disciplineId);

            if (findedGroupDistributions == null)
            {
                return;
            }

            context.GroupDistributions.Remove(findedGroupDistributions);
            await context.SaveChangesAsync();
        }

        public void Update(GroupDistribution groupDistribution)
        {
            using Context context = new();
            context.GroupDistributions.Update(groupDistribution);
            context.SaveChanges();
        }
    }
}
