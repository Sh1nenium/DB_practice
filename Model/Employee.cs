using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Model
{
    public partial class Employee : ObservableValidator
    {
        [ObservableProperty]
        private long _id;

        [Required]
        [MaxLength(30)]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string _name = string.Empty;

        [Required]
        [MaxLength(30)]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string _surname = string.Empty;

        [AllowNull]
        [MaxLength(30)]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string _patronymic = string.Empty;

        [Required]
        [MaxLength(50)]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string _post = string.Empty;

        [Required]
        [RegularExpression(@"\+? ?3?[ -]?8?[ -]?\(?(\d[ -]?){3}\)?[ -]?(\d[ -]?){7}")]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string _phoneNumber = string.Empty;

        [Required]
        [EmailAddress]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string _email = string.Empty;

        [Required]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string _street = string.Empty;

        [MaxLength(15)]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string _workExperience = string.Empty;

        [AllowNull]
        [MaxLength(6)]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string _academicDegree = string.Empty;
    }
}
