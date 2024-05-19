using CommunityToolkit.Mvvm.ComponentModel;
using Model;
using Model.DataAccess.Repositories;
using System.Collections.ObjectModel;
using ViewModel.Abstrations;
using ViewModel.UseCases;

namespace ViewModel
{
    public partial class StudentScoreViewModel : BaseViewModel
    {
        private IScoreRepository _scoreRepository = new ScoreRepository();

        [ObservableProperty]
        private ObservableCollection<Score> _scores;

        public Student Student { get; set; }

        public StudentScoreViewModel(Student student)
        {
            Student = student;

            Scores = new ObservableCollection<Score>(_scoreRepository.GetAllByStudent(Student.NumberOfRecordBook));
        }
    }
}
