using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Model
{
    public partial class Student : ObservableValidator
    {
        [Key]
        [ObservableProperty]
        private long _numberOfRecordBook;

        [ObservableProperty]
        private byte[]? _photo;

        [ObservableProperty]
        private bool _sabbaticalLeave = false;

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
        private string _specializationName = string.Empty;

        [RegularExpression(@"\+? ?3?[ -]?8?[ -]?\(?(\d[ -]?){3}\)?[ -]?(\d[ -]?){7}")]
        [ObservableProperty]
        private string _phoneNumber = string.Empty;

        [EmailAddress]
        [ObservableProperty]
        private string _email = string.Empty;

        [MaxLength(100)]
        [ObservableProperty]
        private string _street = string.Empty;

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
