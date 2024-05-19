using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public partial class TrainingManual : ObservableValidator
    {
        [ObservableProperty]
        private long _id;

        [MaxLength(100)]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string _name = string.Empty;

        [Url]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string? _resourceLink = string.Empty;

        [ObservableProperty]
        private DateOnly _dateOfPublication;

        public long DisciplineId {  get; set; }
    }
}
