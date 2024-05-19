namespace Model.DataAccess.Repositories
{
    using System.Threading.Tasks;

    public interface IGroupDistributionRepository
    {
        public List<GroupDistribution> GetAll();

        public List<GroupDistribution> GetAllByGroup(long groupId);

        public Task Add(GroupDistribution groupDistribution);

        public void Update(GroupDistribution groupDistribution);

        public Task Remove(long groupId, long disciplineId);
    }
}
