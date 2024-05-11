using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Model
{
    public partial class TrainingManual : ObservableValidator
    {
        [ObservableProperty]
        private long _id;

        [MaxLength(100)]
        [ObservableProperty]
        private string _name = string.Empty;

        [Url]
        [ObservableProperty]
        private string? _resourceLink = string.Empty;

        [ObservableProperty]
        private DateOnly _dateOfPublication;
    }
}
