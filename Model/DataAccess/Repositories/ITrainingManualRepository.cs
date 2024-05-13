namespace Model.DataAccess.Repositories
{
    using System.Threading.Tasks;

    public interface ITrainingManualRepository
    {
        public List<TrainingManual> GetAll();

        public Task<TrainingManual?> GetById(long id);

        public List<TrainingManual> GetByDiscipline(long disciplineId);

        public Task Add(TrainingManual trainingManual);

        public void Update(TrainingManual trainingManual);

        public Task Remove(long id);
    }
}
