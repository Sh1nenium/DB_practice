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

    public partial class TrainingManualViewModel : BaseViewModel
    {
        private ITrainingManualRepository _trainingManualRepository = new TrainingManualRepository();

        private State _state = State.OnDefault;

        [ObservableProperty]
        private Discipline _discipline;

        [ObservableProperty]
        private bool _isEnabledTrainingManualInfo = false;

        [ObservableProperty]
        private bool _isEnabledDataGrid = true;

        [ObservableProperty]
        private ObservableCollection<TrainingManual> _trainingManuals;

        [ObservableProperty]
        private TrainingManual? _currentTrainingManual = null;

        partial void OnCurrentTrainingManualChanged(TrainingManual? value)
        {
            if (CurrentTrainingManual == null)
            {
                EditTrainingManualCommand.NotifyCanExecuteChanged();
                return;
            }

            EditTrainingManualCommand.NotifyCanExecuteChanged();
            CurrentTrainingManual.ErrorsChanged += CurrentTrainingManual_ErrorsChanged;
        }

        private void CurrentTrainingManual_ErrorsChanged(object? sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            ApplyTrainingManualCommand.NotifyCanExecuteChanged();
        }

        private bool TrainingManualNotNull()
        {
            return CurrentTrainingManual != null;
        }

        private bool TrainingManualsIsExist()
        {
            return TrainingManuals.Count != 0;
        }

        private void SwapState(State state)
        {
            IsEnabledTrainingManualInfo = !IsEnabledTrainingManualInfo;
            IsEnabledDataGrid = !IsEnabledDataGrid;
            _state = state;
        }

        [RelayCommand]
        public void AddTrainingManual()
        {
            SwapState(State.OnAdd);
            CurrentTrainingManual = new()
            {
                Id = _trainingManualRepository.GetAll().Last().Id + 1
            };
            ApplyTrainingManualCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand(CanExecute = nameof(TrainingManualNotNull))]
        public void EditTrainingManual()
        {
            SwapState(State.OnEdit);
        }

        [RelayCommand(CanExecute = nameof(TrainingManualsIsExist))]
        public async Task DeleteTrainingManual()
        {
            if (CurrentTrainingManual == null)
            {
                await _trainingManualRepository.Remove(TrainingManuals.Last().Id);
                TrainingManuals.Remove(TrainingManuals.Last());
                return;
            }
            await _trainingManualRepository.Remove(CurrentTrainingManual.Id);
            TrainingManuals.Remove(CurrentTrainingManual);

            DeleteTrainingManualCommand.NotifyCanExecuteChanged();
        }

        private bool ValidateCurrentTrainingManual()
        {
            if (CurrentTrainingManual == null) { return false; }

            return !CurrentTrainingManual.HasErrors;
        }

        [RelayCommand(CanExecute = nameof(ValidateCurrentTrainingManual))]
        public async Task ApplyTrainingManual()
        {
            if (CurrentTrainingManual == null) return;

            if (_state == State.OnAdd)
            {
                CurrentTrainingManual.DisciplineId = Discipline.Id;
                await _trainingManualRepository.Add(CurrentTrainingManual);
                TrainingManuals.Add(CurrentTrainingManual);
            }

            if (_state == State.OnEdit)
            {
                _trainingManualRepository.Update(CurrentTrainingManual);
            }

            SwapState(State.OnDefault);
            CurrentTrainingManual = null;
        }

        [RelayCommand]
        public async Task CancelTrainingManual()
        {
            if (_state == State.OnAdd)
            {
                CurrentTrainingManual = null;
                SwapState(State.OnDefault);
                return;
            }

            if (_state == State.OnEdit)
            {
                if (CurrentTrainingManual == null) return;

                TrainingManual? trainingManualCopy = await _trainingManualRepository.GetById(CurrentTrainingManual.Id);

                int index = TrainingManuals.IndexOf(CurrentTrainingManual);

                if (index == -1 || trainingManualCopy == null) return;

                CurrentTrainingManual = null;
                TrainingManuals[index] = trainingManualCopy;
                SwapState(State.OnDefault);

                return;
            }
        }

        public TrainingManualViewModel(Discipline discipline)
        {
            Discipline = discipline;

            TrainingManuals = new ObservableCollection<TrainingManual>(_trainingManualRepository.GetByDiscipline(Discipline.Id));
        }
    }
}
