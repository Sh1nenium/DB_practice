using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using Model.DataAccess.Repositories;
using System.Collections.ObjectModel;
using ViewModel.Abstrations;
using ViewModel.UseCases;

namespace ViewModel
{
    public partial class StudentsInGroupViewModel : BaseViewModel
    {
        private IStudentRepository _studentRepository = new StudentRepository();

        [ObservableProperty]
        private ObservableCollection<Student> _studentsInGroup;

        [ObservableProperty]
        private ObservableCollection<Student> _students;

        [ObservableProperty]
        private Student? _currentStudentInList;

        partial void OnCurrentStudentInListChanged(Student? value)
        {
            AddStudentInGroupCommand.NotifyCanExecuteChanged();
        }

        partial void OnCurrentStudentInGroupChanged(Student? value)
        {
            DeleteStudentInGroupCommand.NotifyCanExecuteChanged();
        }

        [ObservableProperty]
        private Student? _currentStudentInGroup;

        private bool CurrentStudentInListIsNotNull()
        {
            return CurrentStudentInList != null;
        }

        private bool CurrentStudentInGroupIsNotNull()
        {
            return CurrentStudentInGroup != null;
        }

        public Group Group { get; set; } = new Group() { Id = 1 };


        [RelayCommand(CanExecute = nameof(CurrentStudentInListIsNotNull))]
        public void AddStudentInGroup()
        {
            CurrentStudentInList!.GroupId = Group.Id;
            _studentRepository.Update(CurrentStudentInList!);

            StudentsInGroup.Add(CurrentStudentInList);
            Students.Remove(CurrentStudentInList);
        }

        [RelayCommand(CanExecute = nameof(CurrentStudentInGroupIsNotNull))]
        public void DeleteStudentInGroup()
        {
            CurrentStudentInGroup!.GroupId = null;
            _studentRepository.Update(CurrentStudentInGroup!);

            Students.Add(CurrentStudentInGroup);
            StudentsInGroup.Remove(CurrentStudentInGroup);
        }

        public StudentsInGroupViewModel()
        {
            StudentsInGroup = new ObservableCollection<Student>(_studentRepository.GetAllByGroup(Group.Id));

            Students = new ObservableCollection<Student>(_studentRepository.GetAll().Except(StudentsInGroup, new StudentComparer()));
        }
    }

    class StudentComparer : IEqualityComparer<Student>
    {
        public bool Equals(Student? x, Student? y)
        {
            return x?.NumberOfRecordBook == y?.NumberOfRecordBook;
        }

        public int GetHashCode(Student obj)
        {
            return obj.NumberOfRecordBook.GetHashCode();
        }
    }
}
