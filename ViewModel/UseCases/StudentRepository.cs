﻿using Model;
using Model.DataAccess;
using Model.DataAccess.Repositories;

namespace ViewModel.UseCases
{
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class StudentRepository : IStudentRepository
    {
        public async Task Add(Student student)
        {
            using Context context = new();
            await context.Students.AddAsync(student);
            context.SaveChanges();
        }

        public List<Student> GetAll()
        {
            using Context context = new();
            return [.. context.Students.Include(x => x.Group)];
        }

        public List<Student> GetAllByGroup(long groupId)
        {
            using Context context = new();
            return [.. context.Students.Where(x => x.GroupId == groupId).Include(x => x.Group)];
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

        public List<Student> SearchAllByString(string searchString)
        {
            using Context context = new();
            return [.. context.Students
                .Where(x => (x.Name + ' ' + x.Surname + ' ' + x.Patronymic + x.SpecializationName)
                .Contains(searchString))];
        }

        public void Update(Student student)
        {
            using Context context = new();
            context.Students.Update(student);
            context.SaveChanges();
        }
    }
}
