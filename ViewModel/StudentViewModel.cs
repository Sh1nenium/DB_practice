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
        private IStudentRepository _studentRepository = new StudentRepository();

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
            EditStudentCommand.NotifyCanExecuteChanged();
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
            IsEnabledStudentInfo = !IsEnabledStudentInfo;
            IsEnabledDataGrid = !IsEnabledDataGrid;
            _state = state;
        }

        public StudentScoreViewModel CreateStudentScoreViewModel()
        {
            return new StudentScoreViewModel(CurrentStudent!);
        }

        [RelayCommand]
        public void AddStudent()
        {
            SwapState(State.OnAdd);
            CurrentStudent = new()
            {
                NumberOfRecordBook = Students.Count + 1
            };
            ApplyStudentCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand(CanExecute = nameof(StudentNotNull))]
        public void EditStudent()
        {
            SwapState(State.OnEdit);
        }

        [RelayCommand(CanExecute = nameof(StudentsIsExist))]
        public async Task DeleteStudent()
        {
            if (CurrentStudent == null)
            {
                await _studentRepository.Remove(Students.Last().NumberOfRecordBook);
                Students.Remove(Students.Last());
                return;
            }
            await _studentRepository.Remove(CurrentStudent.NumberOfRecordBook);
            Students.Remove(CurrentStudent);

            DeleteStudentCommand.NotifyCanExecuteChanged();
        }

        public void UploadImage(string path)
        {
            if (CurrentStudent == null) return;

            CurrentStudent.Photo = File.ReadAllBytes(path);

            return;
        }

        [RelayCommand]
        public void DeleteImage(Student student)
        {
            if (CurrentStudent == null) return;

            CurrentStudent.Photo = null;
        }

        [RelayCommand()]
        public async Task ApplyStudent()
        {
            if (CurrentStudent == null) return;

            if (_state == State.OnAdd)
            {
                await _studentRepository.Add(CurrentStudent);
                Students.Add(CurrentStudent);
            }

            if (_state == State.OnEdit)
            {
                _studentRepository.Update(CurrentStudent);
            }

            SwapState(State.OnDefault);
            CurrentStudent = null;
        }

        [RelayCommand]
        public async Task CancelStudent()
        {
            if (_state == State.OnAdd)
            {
                CurrentStudent = null;
                SwapState(State.OnDefault);
                return;
            }

            if (_state == State.OnEdit)
            {
                if (CurrentStudent == null) return;

                Student? studentCopy = await _studentRepository.GetByNumberOfRecordBook(CurrentStudent.NumberOfRecordBook);

                int index = Students.IndexOf(CurrentStudent);

                if (index == -1 || studentCopy == null) return;

                CurrentStudent = null;
                Students[index] = studentCopy;
                SwapState(State.OnDefault);

                return;
            }
        }

        [RelayCommand]
        public void RefreshStudents()
        {
            Students = new ObservableCollection<Student>(_studentRepository.GetAll());
        }

        public StudentViewModel() 
        {
            Students = new ObservableCollection<Student>(_studentRepository.GetAll());
        }
    }
}
