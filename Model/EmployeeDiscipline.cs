using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace Model
{
    [PrimaryKey("EmployeeId", "DisciplineId")]
    public partial class EmployeeDiscipline : ObservableObject
    {
        [ObservableProperty]
        private long _employeeId;

        [ObservableProperty]
        private long _disciplineId;

        [ObservableProperty]
        private DateOnly _startDateOfTeaching;

        [ObservableProperty]
        private DateOnly _endDateOfTeaching;
    }
}
