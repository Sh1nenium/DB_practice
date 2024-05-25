using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using Model.DataAccess;
using System.Collections.ObjectModel;

namespace ViewModel
{
    public partial class QueryViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Query> _query;

        [ObservableProperty]
        private string _startsWith = string.Empty;

        [ObservableProperty]
        private int _studentCount;

        [ObservableProperty]
        private int _totalStudents;

        [ObservableProperty]
        private double _overallAverageScore;

        [RelayCommand]
        public void ExecuteQuery()
        {
            using Context context = new();

            Query = new(context.Groups
                .Join(context.Students, g => g.Id, s => s.GroupId, (g, s) => new { Group = g, Student = s })
                .Where(gs => gs.Group.Name.StartsWith(StartsWith))
                .GroupBy(gs => gs.Group, gs => gs.Student)
                .Where(g => g.Count() > StudentCount)
                .Select(g => new Query()
                {
                    GroupName = g.Key.Name,
                    StudentsCount = g.Count(),
                    AverageScore = g.SelectMany(gs => context.Scores
                        .Where(
                            sc => sc.NumberOfRecordBook == gs.NumberOfRecordBook), 
                            (gs, sc) => sc.ScoreNumber)
                        .Average(),
                    TotalStudents = context.Students.Count(),
                    OverallAverageScore = context.Scores.Average(s => s.ScoreNumber)
                })
                .OrderBy(r => r.GroupName)
                .ToList());

            OverallAverageScore = Query[0].OverallAverageScore;
            TotalStudents = Query[0].TotalStudents;
        }

        public QueryViewModel() 
        {
            _query = [];
        }
    }
}
