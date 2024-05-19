namespace Model.DataAccess.Repositories
{
    using System.Threading.Tasks;

    public interface IStudentRepository
    {
        public List<Student> GetAll();

        public List<Student> GetAllByGroup(long groupId);

        public List<Student> SearchAllByString(string searchString);

        public Task<Student?> GetByNumberOfRecordBook(long numberOfRecordBook);

        public Task Add(Student student);

        public void Update(Student student);

        public Task Remove(long numberOfRecordBook);
    }
}
