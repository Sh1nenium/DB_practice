using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public partial class Discipline : ObservableValidator
    {
        [ObservableProperty]
        private long _id;

        [MaxLength(40)]
        [ObservableProperty]
        private string _name = string.Empty;

        [MaxLength(50)]
        [ObservableProperty]
        public string _specializationName = string.Empty;

        [ObservableProperty]
        private DateOnly _dateOfStudy;
    }
}