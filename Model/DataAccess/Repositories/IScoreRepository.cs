namespace Model.DataAccess.Repositories
{
    using System.Threading.Tasks;

    public interface IScoreRepository
    {
        public List<Score> GetAll();

        public Task<Score?> GetById(long numberOfRecordBook, long taskId);

        public List<Score> GetAllByDiscipline(long disciplineId);

        public List<Score> GetAllByStudent(long numberOfRecordBook);

        public Task Add(Score score);

        public void Update(Score score);

        public Task Remove(long id);
    }
}
