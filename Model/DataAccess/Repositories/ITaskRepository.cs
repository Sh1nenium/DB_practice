namespace Model.DataAccess.Repositories
{
    public interface ITaskRepository
    {
        public List<Task> GetAll();

        public Task<Task?> GetById(long id);

        public List<Task> GetByDiscipline(long disciplineId);

        public System.Threading.Tasks.Task Add(Task task);

        public void Update(Task task);

        public System.Threading.Tasks.Task Remove(long id);
    }
}
