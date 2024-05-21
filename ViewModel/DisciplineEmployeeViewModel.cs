using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model.DataAccess.Repositories;
using Model;
using System.Collections.ObjectModel;
using ViewModel.Abstrations;
using ViewModel.UseCases;

namespace ViewModel
{
    public partial class DisciplineEmployeeViewModel : BaseViewModel
    {
        private IDisciplineRepository _disciplineRepository = new DisciplineRepository();

        private IEmployeeDisciplineRepository _employeeDisciplineRepository = new EmployeeDisciplineRepository();

        [ObservableProperty]
        private ObservableCollection<Discipline> _disciplinesInEmployee;

        [ObservableProperty]
        private ObservableCollection<Discipline> _disciplines;

        [ObservableProperty]
        private Discipline? _currentDisciplineInList;

        partial void OnCurrentDisciplineInListChanged(Discipline? value)
        {
            AddDisciplineInEmployeeCommand.NotifyCanExecuteChanged();
        }

        partial void OnCurrentDisciplineInEmployeeChanged(Discipline? value)
        {
            DeleteDisciplineInEmployeeCommand.NotifyCanExecuteChanged();
        }

        [ObservableProperty]
        private Discipline? _currentDisciplineInEmployee;

        private bool CurrentEmployeeInListIsNotNull()
        {
            return CurrentDisciplineInList != null;
        }

        private bool CurrentDisciplineInEmployeeIsNotNull()
        {
            return CurrentDisciplineInEmployee != null;
        }

        public Employee Employee { get; set; }


        [RelayCommand(CanExecute = nameof(CurrentEmployeeInListIsNotNull))]
        public void AddDisciplineInEmployee()
        {
            EmployeeDiscipline employeeDiscipline = new()
            {
                DisciplineId = CurrentDisciplineInList!.Id,
                EmployeeId = Employee.Id,
                StartDateOfTeaching = DateOnly.FromDateTime(DateTime.Now),
                EndDateOfTeaching = null
            };

            _employeeDisciplineRepository.Add(employeeDiscipline);

            DisciplinesInEmployee.Add(CurrentDisciplineInList!);
            Disciplines.Remove(CurrentDisciplineInList!);
        }

        [RelayCommand(CanExecute = nameof(CurrentDisciplineInEmployeeIsNotNull))]
        public void DeleteDisciplineInEmployee()
        {
            _employeeDisciplineRepository.Remove(Employee.Id, CurrentDisciplineInEmployee!.Id);

            Disciplines.Add(CurrentDisciplineInEmployee!);
            DisciplinesInEmployee.Remove(CurrentDisciplineInEmployee!);
        }

        public DisciplineEmployeeViewModel(Employee employee)
        {
            Employee = employee;

            DisciplinesInEmployee = new ObservableCollection<Discipline>(_disciplineRepository.GetAllByEmployee(Employee.Id));

            Disciplines = new ObservableCollection<Discipline>(_disciplineRepository
                .GetAll()
                .Except(DisciplinesInEmployee, new DisciplineComparer()));
        }
    }

    class DisciplineComparer : IEqualityComparer<Discipline>
    {
        public bool Equals(Discipline? x, Discipline? y)
        {
            return x?.Id == y?.Id;
        }

        public int GetHashCode(Discipline obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}