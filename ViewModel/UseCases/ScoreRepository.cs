using Model.DataAccess;
using Model.DataAccess.Repositories;
using Model;

namespace ViewModel.UseCases
{
    using System.Threading.Tasks;

    public class ScoreRepository : IScoreRepository
    {
        public async Task Add(Score score)
        {
            using Context context = new();
            await context.Scores.AddAsync(score);
            context.SaveChanges();
        }

        public List<Score> GetAll()
        {
            using Context context = new();
            return [.. context.Scores];
        }

        public async Task<Score?> GetById(long id)
        {
            using Context context = new();
            return await context.Scores.FindAsync(id);
        }

        public List<Score> GetAllByStudent(long numberOfRecordBook)
        {
            using Context context = new();
            return [.. context.Scores.Where(x => x.NumberOfRecordBook == numberOfRecordBook)];
        }

        public async Task Remove(long id)
        {
            using Context context = new();
            Score? findedScore = await context.Scores.FindAsync(id);

            if (findedScore == null) return;

            context.Scores.Remove(findedScore);
            context.SaveChanges();
        }

        public void Update(Score score)
        {
            using Context context = new();
            context.Scores.Update(score);
            context.SaveChanges();
        }
    }
}
