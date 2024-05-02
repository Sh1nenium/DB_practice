using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Model
{
    public class TrainingManual
    {
        public long Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Url]
        [AllowNull]
        public string ResourceLink { get; set; } = string.Empty;


        public DateOnly DateOfPublication { get; set; }
    }
}
