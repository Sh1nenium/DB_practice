using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public partial class Group : ObservableValidator
    {
        [ObservableProperty]
        private long _id;

        [Required]
        [MaxLength(40)]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string _name = string.Empty;

        [Required]
        [MaxLength(50)]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string _specializationName = string.Empty;

        [ObservableProperty]
        private DateOnly _dateOfStudy;
    }
}
