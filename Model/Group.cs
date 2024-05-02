using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Group
    {
        public long Id { get; set; }

        [MaxLength(40)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(50)]
        public string SpecializationName { get; set; } = string.Empty;

        public DateOnly DateOfStudy { get; set; }
    }
}
