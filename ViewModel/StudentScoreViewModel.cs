using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using Model.DataAccess.Repositories;
using System.Collections.ObjectModel;
using ViewModel.Abstrations;
using ViewModel.UseCases;
using Task = System.Threading.Tasks.Task;

namespace ViewModel
{
    public partial class StudentScoreViewModel : BaseViewModel
    {
        private IScoreRepository _scoreRepository = new ScoreRepository();

        private ITaskRepository _taskRepository = new TaskRepository();

        [ObservableProperty]
        private bool _isEnabled;

        [ObservableProperty]
        private int _ScoreNumber;

        partial void OnScoreNumberChanged(int value)
        {
            AddScoreToStudentCommand.NotifyCanExecuteChanged();
        }

        [ObservableProperty]
        private ObservableCollection<Score> _scores;

        [ObservableProperty]
        private ObservableCollection<Model.Task> _tasks;

        [ObservableProperty]
        private Model.Task _currentTask;

        partial void OnCurrentTaskChanged(Model.Task value)
        {
            IsEnabled = true;
            AddScoreToStudentCommand.NotifyCanExecuteChanged();
        }

        [ObservableProperty]
        private Score _currentScore;

        partial void OnCurrentScoreChanged(Score value)
        {
            DeleteScoreToStudentCommand.NotifyCanExecuteChanged();
        }

        private bool Validate()
        {
            return CurrentTask != null && ScoreNumber >= 0 && ScoreNumber <= 5;
        }

        [RelayCommand(CanExecute = nameof(Validate))]
        public void AddScoreToStudent()
        {
            var score = new Score()
            {
                NumberOfRecordBook = Student.NumberOfRecordBook,
                TaskId = CurrentTask.Id,
                ScoreNumber = ScoreNumber,
                DateOfIssue = DateOnly.FromDateTime(DateTime.Now)
            };

            _scoreRepository.Add(score);

            score.Task = CurrentTask;

            Scores.Add(score);
            Tasks.Remove(CurrentTask);

            IsEnabled = false;
            ScoreNumber = 0;
        }

        private bool CurrentScoreNotNull()
        {
            return CurrentScore != null;
        }

        [RelayCommand(CanExecute = nameof(CurrentScoreNotNull))]
        public void DeleteScoreToStudent()
        {
            _scoreRepository.Add(CurrentScore);

            var task = new Model.Task()
            {
                Id = CurrentScore.TaskId ?? 0,
                Name = CurrentScore.Task!.Name,
                Description = CurrentScore.Task.Description,
                Discipline = CurrentScore.Task.Discipline,
            };

            _scoreRepository.Remove(Student.NumberOfRecordBook, CurrentScore.TaskId ?? 0);

            Tasks.Add(task);
            Scores.Remove(CurrentScore);

            DeleteScoreToStudentCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand]
        public async Task ScoreNumberChanged(Score score)
        {
            if (await _scoreRepository.GetById(Student.NumberOfRecordBook, score.TaskId ?? 0) == null)
            {
                await _scoreRepository.Add(new Score()
                {
                    NumberOfRecordBook = score.Student!.NumberOfRecordBook,
                    TaskId = score.TaskId ?? 0,
                    ScoreNumber = score.ScoreNumber,
                    DateOfIssue = DateOnly.FromDateTime(DateTime.Now),
                });

                score.TaskId ??= 0;

                return;
            }

            _scoreRepository.Update(score);
        }

        public Student Student { get; set; }

        public StudentScoreViewModel(Student student)
        {
            Student = student;

            Scores = new ObservableCollection<Score>(_scoreRepository.GetAllByStudent(Student.NumberOfRecordBook));

            Tasks = new(_taskRepository.GetAllByGroup(Student.GroupId ?? 0).Where(x => Scores.All(y => y.TaskId != x.Id)));
        }
    }
}
