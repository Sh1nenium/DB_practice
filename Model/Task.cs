using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public partial class Task : ObservableValidator
    {
        [ObservableProperty]
        private long _id;

        [MaxLength(100)]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string _name = string.Empty;

        [MaxLength(3000)]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string _description = string.Empty;

        public long DisciplineId { get; set; }

        [ForeignKey("DisciplineId")]
        public Discipline? Discipline { get; set; }
    }
}
