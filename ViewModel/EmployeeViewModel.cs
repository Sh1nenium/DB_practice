using CommunityToolkit.Mvvm.ComponentModel;
using Model.DataAccess.Repositories;
using Model;
using System.Collections.ObjectModel;
using ViewModel.Abstrations;
using ViewModel.UseCases;
using CommunityToolkit.Mvvm.Input;

namespace ViewModel
{
    using System.Threading.Tasks;

    public partial class EmployeeViewModel : BaseViewModel
    {
        private IEmployeeRepository _employeeRepository = new EmployeeRepository();

        private State _state = State.OnDefault;

        [ObservableProperty]
        private bool _isEnabledEmployeeInfo = false;

        [ObservableProperty]
        private bool _isEnabledDataGrid = true;

        [ObservableProperty]
        private ObservableCollection<Employee> _employee;

        [ObservableProperty]
        private Employee? _currentEmployee = null;

        partial void OnCurrentEmployeeChanged(Employee? value)
        {
            EditEmployeeCommand.NotifyCanExecuteChanged();
        }

        private bool EmployeeNotNull()
        {
            return CurrentEmployee != null;
        }

        private bool EmployeesIsExist()
        {
            return Employee.Count != 0;
        }

        private void SwapState(State state)
        {
            IsEnabledEmployeeInfo = !IsEnabledEmployeeInfo;
            IsEnabledDataGrid = !IsEnabledDataGrid;
            _state = state;
        }

        public DisciplineEmployeeViewModel CreateDisciplineEmployeeViewModel()
        {
            return new DisciplineEmployeeViewModel()
            {
                Employee = CurrentEmployee!
            };
        }

        [RelayCommand]
        public void AddEmployee()
        {
            SwapState(State.OnAdd);
            CurrentEmployee = new()
            {
                Id = Employee.Count + 1
            };
            ApplyEmployeeCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand(CanExecute = nameof(EmployeeNotNull))]
        public void EditEmployee()
        {
            SwapState(State.OnEdit);
        }

        [RelayCommand(CanExecute = nameof(EmployeesIsExist))]
        public async Task DeleteEmployee()
        {
            if (CurrentEmployee == null)
            {
                await _employeeRepository.Remove(Employee.Last().Id);
                Employee.Remove(Employee.Last());
                return;
            }
            await _employeeRepository.Remove(CurrentEmployee.Id);
            Employee.Remove(CurrentEmployee);

            DeleteEmployeeCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand()]
        public async Task ApplyEmployee()
        {
            if (CurrentEmployee == null) return;

            if (_state == State.OnAdd)
            {
                await _employeeRepository.Add(CurrentEmployee);
                Employee.Add(CurrentEmployee);
            }

            if (_state == State.OnEdit)
            {
                _employeeRepository.Update(CurrentEmployee);
            }

            SwapState(State.OnDefault);
            CurrentEmployee = null;
        }

        [RelayCommand]
        public async Task CancelEmployee()
        {
            if (_state == State.OnAdd)
            {
                CurrentEmployee = null;
                SwapState(State.OnDefault);
                return;
            }

            if (_state == State.OnEdit)
            {
                if (CurrentEmployee == null) return;

                Employee? employeeCopy = await _employeeRepository.GetById(CurrentEmployee.Id);

                int index = Employee.IndexOf(CurrentEmployee);

                if (index == -1 || employeeCopy == null) return;

                CurrentEmployee = null;
                Employee[index] = employeeCopy;
                SwapState(State.OnDefault);

                return;
            }
        }

        public EmployeeViewModel()
        {
            Employee = new ObservableCollection<Employee>(_employeeRepository.GetAll());
        }
    }
}
