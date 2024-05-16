using Model.DataAccess;
using Model.DataAccess.Repositories;
using Model;

namespace ViewModel.UseCases
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Internal;
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
            return [.. context.Scores.Include(x => x.Task).Include(x => x.Student)];
        }

        public async Task<Score?> GetById(long id)
        {
            using Context context = new();
            return await context.Scores.FindAsync(id);
        }

        public List<Score> GetAllByDiscipline(long disciplineId)
        {
            using Context context = new();

            var query = context.Groups
                // Получить группы
                .Join(context.GroupDistributions,
                    group => group.Id,
                    groupDistribution => groupDistribution.GroupId,
                    (group, groupDistribution) => new { Group = group, GroupDistribution = groupDistribution })
                .Where(x => x.GroupDistribution.DisciplineId == disciplineId)
                .Select(x => x.Group)
                // Получить студентов
                .Join(context.Students,
                    group => group.Id,
                    student => student.GroupId,
                    (group, student) => new { Group = group, Student = student })
                .Where(x => x.Student.GroupId == x.Group.Id)
                .Select(x => x.Student)
                .Include(x => x.Group)
                // Получить score
                .GroupJoin(context.Scores,
                    student => student.NumberOfRecordBook,
                    score => score.NumberOfRecordBook,
                    (student, scores) => new { Student = student, Scores = scores })
                .SelectMany(x => x.Scores.DefaultIfEmpty(),
                    (x, sc) => new Score()
                    {
                        NumberOfRecordBook = x.Student.NumberOfRecordBook,
                        TaskId = (sc == null ? null : sc.TaskId),
                        ScoreNumber = (sc == null ? 0 : sc.ScoreNumber),
                        Student = x.Student,
                    })
                .ToList();

            return query;
        }

        public List<Score> GetAllByStudent(long numberOfRecordBook)
        {
            using Context context = new();
            return [.. context.Scores
                .Where(x => x.NumberOfRecordBook == numberOfRecordBook)
                .Include(x => x.Task)
                .ThenInclude(x => x!.Discipline)];
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
