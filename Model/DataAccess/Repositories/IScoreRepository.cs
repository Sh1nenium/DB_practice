namespace Model.DataAccess.Repositories
{
    using System.Threading.Tasks;

    public interface IScoreRepository
    {
        public List<Score> GetAll();

        public Task<Score?> GetById(long numberOfRecordBook, long taskId);

        public List<Score>? GetAllByDisciplineAndTask(long disciplineId, long taskId);

        public List<Score> GetAllByStudent(long numberOfRecordBook);

        public Task Add(Score score);

        public void Update(Score score);

        public Task Remove(long numberOfRecordBook, long taskId);
    }
}
