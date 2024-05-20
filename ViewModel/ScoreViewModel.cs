using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using Model.DataAccess.Repositories;
using System.Collections.ObjectModel;
using ViewModel.Abstrations;
using ViewModel.UseCases;

namespace ViewModel
{
    using System.Threading.Tasks;

    public partial class ScoreViewModel : BaseViewModel
    {
        private IScoreRepository _scoreRepository = new ScoreRepository();

        [ObservableProperty]
        private ObservableCollection<Score> _scores;

        public Model.Task Task { get; set; }

        [RelayCommand]
        public async Task ScoreNumberChanged(Score score)
        {
            if (await _scoreRepository.GetById(score.Student!.NumberOfRecordBook, Task.Id) == null)
            {
                await _scoreRepository.Add(new Score()
                {
                    NumberOfRecordBook = score.Student.NumberOfRecordBook,
                    TaskId = Task.Id,
                    ScoreNumber = score.ScoreNumber,
                });

                score.TaskId = Task.Id;

                return;
            }

             _scoreRepository.Update(score);
        }

        public ScoreViewModel(Model.Task task) 
        {
            Task = task;
            Scores = new ObservableCollection<Score>(_scoreRepository.GetAllByDisciplineAndTask(Task!.DisciplineId, Task.Id));
        }
    }
}
