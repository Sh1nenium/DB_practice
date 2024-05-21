using Model.DataAccess;
using Model;
using Task = System.Threading.Tasks.Task;
using Model.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ViewModel.UseCases
{
    public class TaskRepository : ITaskRepository
    {
        public async Task Add(Model.Task trainingManual)
        {
            using Context context = new();
            await context.Tasks.AddAsync(trainingManual);
            context.SaveChanges();
        }

        public List<Model.Task> GetAll()
        {
            using Context context = new();
            return [.. context.Tasks.Include(x => x.Discipline)];
        }

        public List<Model.Task> GetAllByGroup(long groupId)
        {
            using Context context = new();
            
            var query = context.GroupDistributions
                .Where(x => x.GroupId == groupId)
                .Include(x => x.Discipline)
                .Join(context.Tasks,
                    (discipline) => discipline.DisciplineId,
                    (task) => task.DisciplineId,
                    (discipline, task) => new { Discipline = discipline, Task = task})
                .Select(x => x.Task)
                .Include(x => x.Discipline)
                .ToList();

            return query;
        }

        public async Task<Model.Task?> GetById(long id)
        {
            using Context context = new();
            return await context.Tasks.FindAsync(id);
        }

        public List<Model.Task> GetByDiscipline(long disciplineId)
        {
            using Context context = new();
            return [.. context.Tasks.Where(x => x.DisciplineId == disciplineId).Include(x => x.Discipline)];
        }

        public async Task Remove(long id)
        {
            using Context context = new();
            Model.Task? findedTask = await context.Tasks.FindAsync(id);

            if (findedTask == null) return;

            context.Tasks.Remove(findedTask);
            context.SaveChanges();
        }

        public void Update(Model.Task trainingManual)
        {
            using Context context = new();
            context.Tasks.Update(trainingManual);
            context.SaveChanges();
        }
    }
}
