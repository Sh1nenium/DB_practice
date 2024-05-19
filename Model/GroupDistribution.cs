using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [PrimaryKey("GroupId", "DisciplineId")]
    public partial class GroupDistribution : ObservableObject
    {
        [ObservableProperty]
        private long _groupId;

        [ObservableProperty]
        private long _disciplineId;

        [ForeignKey("DisciplineId")]
        public Discipline? Discipline { get; set; }

        [ObservableProperty]
        private int _hoursPerAcademicYear;

        [ObservableProperty]
        private DateOnly _dateOfDistribution;
    }
}
