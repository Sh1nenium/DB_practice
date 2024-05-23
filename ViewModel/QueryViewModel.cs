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
                    AverageScore = g.Join(
                        context.Scores, s => 
                            s.NumberOfRecordBook, 
                            sc => sc.NumberOfRecordBook, 
                            (s, sc) => sc.ScoreNumber)
                    .Average()
                })
                .OrderBy(r => r.GroupName)
                .ToList());
        }

        public QueryViewModel() 
        {
            _query = new();
        }
    }
}
