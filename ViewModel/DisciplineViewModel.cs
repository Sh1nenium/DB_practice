﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using Model.DataAccess.Repositories;
using System.Collections.ObjectModel;
using ViewModel.Abstrations;
using ViewModel.UseCases;

namespace ViewModel
{
    using System.Threading.Tasks;

    public partial class DisciplineViewModel : BaseViewModel
    {
        private IDisciplineRepository _disciplineRepository = new DisciplineRepository();

        private State _state = State.OnDefault;

        [ObservableProperty]
        private bool _isEnabledDisciplineInfo = false;

        [ObservableProperty]
        private bool _isEnabledDataGrid = true;

        [ObservableProperty]
        private ObservableCollection<Discipline> _disciplines;

        [ObservableProperty]
        private Discipline? _currentDiscipline = null;

        partial void OnCurrentDisciplineChanged(Discipline? value)
        {
            if (CurrentDiscipline == null)
            {
                EditDisciplineCommand.NotifyCanExecuteChanged();
                return;
            }

            EditDisciplineCommand.NotifyCanExecuteChanged();
            CurrentDiscipline.ErrorsChanged += CurrentDiscipline_ErrorsChanged;
        }

        private void CurrentDiscipline_ErrorsChanged(object? sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            ApplyDisciplineCommand.NotifyCanExecuteChanged();
        }

        private bool DisciplineNotNull()
        {
            return CurrentDiscipline != null;
        }

        private bool DisciplinesIsExist()
        {
            return Disciplines.Count != 0;
        }

        private void SwapState(State state)
        {
            IsEnabledDisciplineInfo = !IsEnabledDisciplineInfo;
            IsEnabledDataGrid = !IsEnabledDataGrid;
            _state = state;
        }

        [ObservableProperty]
        private string _searchString = string.Empty;

        partial void OnSearchStringChanged(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                Disciplines = new ObservableCollection<Discipline>(_disciplineRepository.GetAll());
                return;
            }

            Disciplines = new ObservableCollection<Discipline>(_disciplineRepository.SearchAllByString(value));
        }

        public TrainingManualViewModel CreateTrainingManualViewModel()
        {
            return new TrainingManualViewModel(CurrentDiscipline!);
        }

        public TaskViewModel CreateTaskViewModel()
        {
            return new TaskViewModel(CurrentDiscipline!);
        }

        public EmployeeInDisciplineViewModel CreateEmployeeDisciplineViewModel()
        {
            return new EmployeeInDisciplineViewModel(CurrentDiscipline!);
        }

        [RelayCommand]
        public void AddDiscipline()
        {
            SwapState(State.OnAdd);
            CurrentDiscipline = new()
            {
                Id = Disciplines.Last().Id + 1
            };
            ApplyDisciplineCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand(CanExecute = nameof(DisciplineNotNull))]
        public void EditDiscipline()
        {
            SwapState(State.OnEdit);
        }

        [RelayCommand(CanExecute = nameof(DisciplinesIsExist))]
        public async Task DeleteDiscipline()
        {
            if (CurrentDiscipline == null)
            {
                await _disciplineRepository.Remove(Disciplines.Last().Id);
                Disciplines.Remove(Disciplines.Last());
                return;
            }
            await _disciplineRepository.Remove(CurrentDiscipline.Id);
            Disciplines.Remove(CurrentDiscipline);

            DeleteDisciplineCommand.NotifyCanExecuteChanged();
        }

        private bool ValidateCurrentDiscipline()
        {
            if (CurrentDiscipline == null) { return false; }

            return !CurrentDiscipline.HasErrors;
        }

        [RelayCommand(CanExecute = nameof(ValidateCurrentDiscipline))]
        public async Task ApplyDiscipline()
        {
            if (CurrentDiscipline == null) return;

            if (_state == State.OnAdd)
            {
                await _disciplineRepository.Add(CurrentDiscipline);
                Disciplines.Add(CurrentDiscipline);
            }

            if (_state == State.OnEdit)
            {
                _disciplineRepository.Update(CurrentDiscipline);
            }

            SwapState(State.OnDefault);
            CurrentDiscipline = null;
        }

        [RelayCommand]
        public async Task CancelDiscipline()
        {
            if (_state == State.OnAdd)
            {
                CurrentDiscipline = null;
                SwapState(State.OnDefault);
                return;
            }

            if (_state == State.OnEdit)
            {
                if (CurrentDiscipline == null) return;

                Discipline? disciplineCopy = await _disciplineRepository.GetById(CurrentDiscipline.Id);

                int index = Disciplines.IndexOf(CurrentDiscipline);

                if (index == -1 || disciplineCopy == null) return;

                CurrentDiscipline = null;
                Disciplines[index] = disciplineCopy;
                SwapState(State.OnDefault);

                return;
            }
        }

        public DisciplineViewModel()
        {
            Disciplines = new ObservableCollection<Discipline>(_disciplineRepository.GetAll());
        }
    }
}
