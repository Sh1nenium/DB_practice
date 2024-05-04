using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace Model
{
    [PrimaryKey("GroupId", "DisciplineId")]
    public partial class GroupDistribution : ObservableObject
    {
        [ObservableProperty]
        private long _groupId;

        [ObservableProperty]
        private long _disciplineId;

        [ObservableProperty]
        private int _hoursPerAcademicYear;

        [ObservableProperty]
        private DateOnly _dateOfDistribution;
    }
}
