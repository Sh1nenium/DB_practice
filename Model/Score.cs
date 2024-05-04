using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    [PrimaryKey("NumberOfRecordBook", "TaskId")]
    public partial class Score : ObservableValidator
    {
        [ObservableProperty]
        private long _numberOfRecordBook;

        [ObservableProperty]
        private long _taskId;

        [Range(0, 20)]
        [ObservableProperty]
        private int _scoreNumber;
    }
}
