using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Task
    {
        public long Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(3000)]
        public string Description { get; set; } = string.Empty;
    }
}
