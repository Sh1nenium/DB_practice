using CommunityToolkit.Mvvm.ComponentModel;
using Model;
using Model.DataAccess.Repositories;
using ViewModel.Abstrations;
using ViewModel.UseCases;

namespace ViewModel
{
    public partial class StudentScoreViewModel : BaseViewModel
    {
        private IScoreRepository _scoreRepository = new ScoreRepository();

        [ObservableProperty]
        private List<Score> _scores;

        public Student Student { get; set; } = new() { NumberOfRecordBook = 1 };

        public StudentScoreViewModel()
        {
            Scores = _scoreRepository.GetAllByStudent(Student.NumberOfRecordBook);
        }
    }
}
