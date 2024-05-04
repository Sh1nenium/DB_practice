using CommunityToolkit.Mvvm.ComponentModel;
using Model;
using Model.DataAccess.Repositories;
using System.Collections.ObjectModel;
using ViewModel.Abstrations;
using ViewModel.UseCases;

namespace ViewModel
{
    public partial class StudentViewModel : BaseViewModel
    {
        private IStudentRepository studentRepository = new StudentRepository();

        [ObservableProperty]
        private ObservableCollection<Student> _students;

        
        private Student _currentStudent = new();

        public Student CurrentStudent
        {
            get => _currentStudent;
            set => SetProperty(ref _currentStudent, value);
        }

        public StudentViewModel() 
        {
            Students = new ObservableCollection<Student>(studentRepository.GetAll());
        }
    }
}
