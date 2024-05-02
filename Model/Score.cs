using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    [PrimaryKey("NumberOfRecordBook", "TaskId")]
    public class Score
    {
        public long NumberOfRecordBook { get; set; }

        public long TaskId { get; set; }

        [Range(0, 20)]
        public int ScoreNumber { get; set; }
    }
}
