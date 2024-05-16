using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model.DataAccess.Repositories;
using Model;
using System.Collections.ObjectModel;
using ViewModel.Abstrations;
using ViewModel.UseCases;
using Task = Model.Task;

namespace ViewModel
{
    public partial class TaskViewModel : BaseViewModel
    {
        private ITaskRepository _taskRepository = new TaskRepository();

        private State _state = State.OnDefault;

        [ObservableProperty]
        private Discipline _discipline;

        [ObservableProperty]
        private bool _isEnabledTaskInfo = false;

        [ObservableProperty]
        private bool _isEnabledDataGrid = true;

        [ObservableProperty]
        private ObservableCollection<Task> _tasks;

        [ObservableProperty]
        private Task? _currentTask = null;

        partial void OnCurrentTaskChanged(Task? value)
        {
            EditTaskCommand.NotifyCanExecuteChanged();
        }

        private bool TaskNotNull()
        {
            return CurrentTask != null;
        }

        private bool TasksIsExist()
        {
            return Tasks.Count != 0;
        }

        private void SwapState(State state)
        {
            IsEnabledTaskInfo = !IsEnabledTaskInfo;
            IsEnabledDataGrid = !IsEnabledDataGrid;
            _state = state;
        }

        public ScoreViewModel CreateScoreViewModel()
        {
            return new ScoreViewModel(CurrentTask!);
        }

        [RelayCommand]
        public void AddTask()
        {
            SwapState(State.OnAdd);
            CurrentTask = new()
            {
                Id = Tasks.Count + 1
            };
            ApplyTaskCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand(CanExecute = nameof(TaskNotNull))]
        public void EditTask()
        {
            SwapState(State.OnEdit);
        }

        [RelayCommand(CanExecute = nameof(TasksIsExist))]
        public async System.Threading.Tasks.Task DeleteTask()
        {
            if (CurrentTask == null)
            {
                await _taskRepository.Remove(Tasks.Last().Id);
                Tasks.Remove(Tasks.Last());
                return;
            }
            await _taskRepository.Remove(CurrentTask.Id);
            Tasks.Remove(CurrentTask);

            DeleteTaskCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand()]
        public async System.Threading.Tasks.Task ApplyTask()
        {
            if (CurrentTask == null) return;

            if (_state == State.OnAdd)
            {
                CurrentTask.DisciplineId = Discipline.Id;
                await _taskRepository.Add(CurrentTask);
                Tasks.Add(CurrentTask);
            }

            if (_state == State.OnEdit)
            {
                _taskRepository.Update(CurrentTask);
            }

            SwapState(State.OnDefault);
            CurrentTask = null;
        }

        [RelayCommand]
        public async System.Threading.Tasks.Task CancelTask()
        {
            if (_state == State.OnAdd)
            {
                CurrentTask = null;
                SwapState(State.OnDefault);
                return;
            }

            if (_state == State.OnEdit)
            {
                if (CurrentTask == null) return;

                Task? taskCopy = await _taskRepository.GetById(CurrentTask.Id);

                int index = Tasks.IndexOf(CurrentTask);

                if (index == -1 || taskCopy == null) return;

                CurrentTask = null;
                Tasks[index] = taskCopy;
                SwapState(State.OnDefault);

                return;
            }
        }

        public TaskViewModel(Discipline discipline)
        {
            Discipline = discipline;

            Tasks = new ObservableCollection<Task>(_taskRepository.GetByDiscipline(Discipline.Id));
        }
    }
}
