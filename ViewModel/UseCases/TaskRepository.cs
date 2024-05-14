using Model.DataAccess;
using Model;
using Task = System.Threading.Tasks.Task;
using Model.DataAccess.Repositories;

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
            return [.. context.Tasks];
        }

        public async Task<Model.Task?> GetById(long id)
        {
            using Context context = new();
            return await context.Tasks.FindAsync(id);
        }

        public List<Model.Task> GetByDiscipline(long disciplineId)
        {
            using Context context = new();
            return [.. context.Tasks.Where(x => x.DisciplineId == disciplineId)];
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
