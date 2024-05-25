using CommunityToolkit.Mvvm.ComponentModel;

namespace Model
{
    public partial class Query : ObservableObject
    {
        [ObservableProperty]
        private string _groupName = string.Empty;

        [ObservableProperty]
        private int studentsCount;

        [ObservableProperty]
        private double _averageScore;

        [ObservableProperty]
        private int _totalStudents;

        [ObservableProperty]
        private double _OverallAverageScore;
    }
}
