using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        [ObservableProperty]
        private DateOnly _dateOfStudy;

        public long? GroupId { get; set; }

        [ForeignKey("GroupId")]
        public Group? Group { get; set; }
    }
}
