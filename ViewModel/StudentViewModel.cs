using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using Model.DataAccess.Repositories;
using System.Collections.ObjectModel;
using ViewModel.Abstrations;
using ViewModel.UseCases;

namespace ViewModel
{
    using System.Threading.Tasks;

    public partial class StudentViewModel : BaseViewModel
    {
        private IStudentRepository studentRepository = new StudentRepository();

        private State _state = State.OnDefault;

        [ObservableProperty]
        private bool _isEnabledStudentInfo = false;

        [ObservableProperty]
        private bool _isEnabledDataGrid = true;

        [ObservableProperty]
        private ObservableCollection<Student> _students;

        [ObservableProperty]
        private Student? _currentStudent = null;

        partial void OnCurrentStudentChanged(Student? value)
        {
            AddCommand.NotifyCanExecuteChanged();
        }

        private bool StudentNotNull()
        {
            return CurrentStudent != null;
        }

        private bool StudentsIsExist()
        {
            return Students.Count != 0;
        }

        private void SwapState(State state)
        {
            IsEnabledStudentInfo = true;
            IsEnabledDataGrid = false;
            _state = state;
        }

        [RelayCommand]
        public void Add()
        {
            SwapState(State.OnAdd);
            CurrentStudent = null;

        }

        [RelayCommand(CanExecute = nameof(StudentNotNull))]
        public void Edit()
        {
            SwapState(State.OnEdit);
        }

        [RelayCommand(CanExecute = nameof(StudentsIsExist))]
        public async Task Delete(Student student)
        {
            if (student == null)
            {
                await studentRepository.Remove(Students.Last().NumberOfRecordBook);
                Students.Remove(Students.Last());
                return;
            }
            await studentRepository.Remove(student.NumberOfRecordBook);
            Students.Remove(student);

            DeleteCommand.NotifyCanExecuteChanged();
        }

        public StudentViewModel() 
        {
            Students = new ObservableCollection<Student>(studentRepository.GetAll());
        }
    }
}
