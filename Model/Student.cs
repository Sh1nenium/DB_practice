using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Model
{
    public partial class Student : ObservableValidator
    {
        [Key]
        [Required]
        [ObservableProperty]
        private long _numberOfRecordBook;

        [ObservableProperty]
        private byte[]? _photo;

        [Required]
        [ObservableProperty]
        private bool _sabbaticalLeave = false;

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
        private string _specializationName = string.Empty;

        [Required]
        [RegularExpression(@"\+? ?3?[ -]?8?[ -]?\(?(\d[ -]?){3}\)?[ -]?(\d[ -]?){8}")]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string _phoneNumber = string.Empty;

        [Required]
        [EmailAddress]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string _email = string.Empty;

        [Required]
        [MaxLength(100)]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string _street = string.Empty;

        [Required]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private DateOnly _dateOfStudy;
        
        private string _groupName = string.Empty;

        [NotMapped]
        public string GroupName 
        {
            get => _groupName;
            set => SetProperty(ref _groupName, value);
        }
    }
}
