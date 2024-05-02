using Microsoft.EntityFrameworkCore;

namespace Model
{
    [PrimaryKey("EmployeeId", "DisciplineId")]
    public class EmployeeDiscipline
    {
        public long EmployeeId { get; set; }

        public long DisciplineId { get; set; }

        public DateOnly StartDateOfTeaching { get; set; }

        public DateOnly EndDateOfTeaching { get; set; }
    }
}
