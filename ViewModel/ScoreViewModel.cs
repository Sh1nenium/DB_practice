using CommunityToolkit.Mvvm.ComponentModel;
using Model;
using Model.DataAccess.Repositories;
using System.Collections.ObjectModel;
using ViewModel.Abstrations;
using ViewModel.UseCases;

namespace ViewModel
{
    public partial class ScoreViewModel : BaseViewModel
    {
        private IScoreRepository _scoreRepository = new ScoreRepository();

        [ObservableProperty]
        private ObservableCollection<Score> _scores;

        public Model.Task Task { get; set; }

        public ScoreViewModel(Model.Task task) 
        {
            Task = task;
            Scores = new ObservableCollection<Score>(_scoreRepository.GetAllByDiscipline(Task!.DisciplineId));
        }
    }
}
