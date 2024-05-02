using Microsoft.EntityFrameworkCore;

namespace Model
{
    [PrimaryKey("GroupId", "DisciplineId")]
    public class GroupDistribution
    {
        public long GroupId { get; set; }

        public long DisciplineId { get; set; }

        public int HoursPerAcademicYear { get; set; }

        public DateOnly DateOfDistribution { get; set; }
    }
}
