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

        public async Task<Student?> GetByNumberOfRecordBook(long numberOfRecordBook)
        {
            using Context context = new();
            return await context.Students.FindAsync(numberOfRecordBook);
        }

        public async Task Remove(long numberOfRecordBook)
        {
            using Context context = new();
            Student? findedStudent = await context.Students.FindAsync(numberOfRecordBook);

            if (findedStudent == null) return;

            context.Students.Remove(findedStudent);
            context.SaveChanges();
        }

        public async Task Update(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
