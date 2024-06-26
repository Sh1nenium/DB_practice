﻿using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [PrimaryKey("NumberOfRecordBook", "TaskId")]
    public partial class Score : ObservableValidator
    {
        [ObservableProperty]
        private long _numberOfRecordBook;

        [ForeignKey("NumberOfRecordBook")]
        public Student? Student { get; set; }

        [ObservableProperty]
        private long? _taskId;

        [ForeignKey("TaskId")]
        public Task? Task { get; set; }

        [Range(0, 5)]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private int _scoreNumber;

        [ObservableProperty]
        private DateOnly _dateOfIssue;
    }
}
