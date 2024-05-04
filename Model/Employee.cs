using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Model
{
    public partial class Employee : ObservableValidator
    {
        [ObservableProperty]
        private long _id;

        [MaxLength(30)]
        [ObservableProperty]
        private string _name = string.Empty;

        [MaxLength(30)]
        [ObservableProperty]
        private string _surname = string.Empty;

        [AllowNull]
        [MaxLength(30)]
        [ObservableProperty]
        private string _patronymic = string.Empty;

        [MaxLength(50)]
        [ObservableProperty]
        private string _post = string.Empty;

        [RegularExpression(@"\+? ?3?[ -]?8?[ -]?\(?(\d[ -]?){3}\)?[ -]?(\d[ -]?){7}")]
        [ObservableProperty]
        private string _phoneNumber = string.Empty;

        [EmailAddress]
        [ObservableProperty]
        private string _email = string.Empty;

        [MaxLength(100)]
        [ObservableProperty]
        private string _street = string.Empty;

        [MaxLength(15)]
        [ObservableProperty]
        private string _workExperience = string.Empty;

        [AllowNull]
        [MaxLength(15)]
        [ObservableProperty]
        private string _academicDegree = string.Empty;
    }
}
