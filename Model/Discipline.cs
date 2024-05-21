using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public partial class Discipline : ObservableValidator
    {
        [ObservableProperty]
        private long _id;

        [Required]
        [MaxLength(50)]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string _name = string.Empty;

        [Required]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string _description = string.Empty;
    }
}