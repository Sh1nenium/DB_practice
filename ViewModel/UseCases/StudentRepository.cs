using Model;
using Model.DataAccess;
using Model.DataAccess.Repositories;

namespace ViewModel.UseCases
{
    using System.Threading.Tasks;

    public class StudentRepository : IStudentRepository
    {
        public Task Add(Student student)
        {
            using Context context = new();
            throw new NotImplementedException();
        }

        public List<Student> GetAll()
        {
            using Context context = new();
            return [.. context.Students];
        }

        public Task<Student> GetByNubmerOfRecordBook(int numberOfRecordBook)
        {
            throw new NotImplementedException();
        }

        public Task Remove(Student student)
        {
            throw new NotImplementedException();
        }

        public Task Update(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
