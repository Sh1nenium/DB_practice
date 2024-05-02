using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Model
{
    public class Student
    {
        [Key]
        public long NubmerOfRecordBook { get; set; }

        public byte[]? Photo { get; set; }

        public bool SabbaticalLeave { get; set; } = false;

        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(30)]
        public string Surname { get; set; } = string.Empty;

        [AllowNull]
        [MaxLength(30)]
        public string Patronymic { get; set; } = string.Empty;

        [MaxLength(50)]
        public string SpecializationName { get; set; } = string.Empty;

        [RegularExpression(@"\+? ?3?[ -]?8?[ -]?\(?(\d[ -]?){3}\)?[ -]?(\d[ -]?){7}")]
        public string PhoneNumber { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Street { get; set; } = string.Empty;

        public DateOnly DateOfStudy { get; set; }
    }
}
