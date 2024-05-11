using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public partial class Discipline : ObservableValidator
    {
        [ObservableProperty]
        private long _id;

        [MaxLength(50)]
        [ObservableProperty]
        private string _name = string.Empty;

        [MaxLength(3000)]
        [ObservableProperty]
        private string _description = string.Empty;
    }
}