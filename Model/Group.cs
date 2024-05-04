using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public partial class Group : ObservableValidator
    {
        [ObservableProperty]
        private long _id;

        [MaxLength(40)]
        [ObservableProperty]
        private string _name = string.Empty;

        [MaxLength(50)]
        [ObservableProperty]
        private string _specializationName = string.Empty;

        [ObservableProperty]
        private DateOnly _dateOfStudy;
    }
}
