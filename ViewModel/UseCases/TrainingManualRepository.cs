using Model;
using Model.DataAccess.Repositories;

namespace ViewModel.UseCases
{
    using Model.DataAccess;
    using System.Threading.Tasks;

    public class TrainingManualRepository : ITrainingManualRepository
    {
        public async Task Add(TrainingManual trainingManual)
        {
            using Context context = new();
            await context.TrainingManuals.AddAsync(trainingManual);
            context.SaveChanges();
        }

        public List<TrainingManual> GetAll()
        {
            using Context context = new();
            return [.. context.TrainingManuals];
        }

        public async Task<TrainingManual?> GetById(long id)
        {
            using Context context = new();
            return await context.TrainingManuals.FindAsync(id);
        }

        public List<TrainingManual> GetByDiscipline(long disciplineId)
        {
            using Context context = new();
            return [.. context.TrainingManuals.Where(x => x.DisciplineId == disciplineId)];
        }

        public async Task Remove(long id)
        {
            using Context context = new();
            TrainingManual? findedTrainingManual = await context.TrainingManuals.FindAsync(id);

            if (findedTrainingManual == null) return;

            context.TrainingManuals.Remove(findedTrainingManual);
            context.SaveChanges();
        }

        public void Update(TrainingManual trainingManual)
        {
            using Context context = new();
            context.TrainingManuals.Update(trainingManual);
            context.SaveChanges();
        }
    }
}
