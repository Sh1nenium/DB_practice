using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using Model.DataAccess.Repositories;
using System.Collections.ObjectModel;
using ViewModel.Abstrations;
using ViewModel.UseCases;

namespace ViewModel
{
    public partial class EmployeeInDisciplineViewModel : BaseViewModel
    {
        private IEmployeeRepository _employeeRepository = new EmployeeRepository();

        private IEmployeeDisciplineRepository _employeeDisciplineRepository = new EmployeeDisciplineRepository();

        [ObservableProperty]
        private ObservableCollection<Employee> _employeeInDiscipline;

        [ObservableProperty]
        private ObservableCollection<Employee> _employee;

        [ObservableProperty]
        private Employee? _currentEmployeeInList;

        partial void OnCurrentEmployeeInListChanged(Employee? value)
        {
            AddEmployeeInDisciplineCommand.NotifyCanExecuteChanged();
        }

        partial void OnCurrentEmployeeInDisciplineChanged(Employee? value)
        {
            DeleteEmployeeInDisciplineCommand.NotifyCanExecuteChanged();
        }

        [ObservableProperty]
        private Employee? _currentEmployeeInDiscipline;

        private bool CurrentEmployeeInListIsNotNull()
        {
            return CurrentEmployeeInList != null;
        }

        private bool CurrentEmployeeInDisciplineIsNotNull()
        {
            return CurrentEmployeeInDiscipline != null;
        }

        public Discipline Discipline { get; set; }


        [RelayCommand(CanExecute = nameof(CurrentEmployeeInListIsNotNull))]
        public void AddEmployeeInDiscipline()
        {
            EmployeeDiscipline employeeDiscipline = new()
            {
                DisciplineId = Discipline.Id,
                EmployeeId = CurrentEmployeeInList!.Id,
                StartDateOfTeaching = DateOnly.FromDateTime(DateTime.Now),
                EndDateOfTeaching = null
            };

            _employeeDisciplineRepository.Add(employeeDiscipline);

            EmployeeInDiscipline.Add(CurrentEmployeeInList!);
            Employee.Remove(CurrentEmployeeInList!);
        }

        [RelayCommand(CanExecute = nameof(CurrentEmployeeInDisciplineIsNotNull))]
        public void DeleteEmployeeInDiscipline()
        {
            _employeeDisciplineRepository.Remove(CurrentEmployeeInDiscipline!.Id, Discipline.Id);

            EmployeeInDiscipline.Remove(CurrentEmployeeInDiscipline!);
            Employee.Add(CurrentEmployeeInDiscipline!);
        }

        public EmployeeInDisciplineViewModel(Discipline discipline)
        {
            Discipline = discipline;

            EmployeeInDiscipline = new ObservableCollection<Employee>(_employeeRepository.GetAllByDiscipline(Discipline.Id));

            Employee = new ObservableCollection<Employee>(_employeeRepository.GetAll().Except(EmployeeInDiscipline, new EmployeeComparer()));
        }
    }

    class EmployeeComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee? x, Employee? y)
        {
            return x?.Id == y?.Id;
        }

        public int GetHashCode(Employee obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
