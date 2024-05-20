using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using Model.DataAccess.Repositories;
using System.Collections.ObjectModel;
using ViewModel.Abstrations;
using ViewModel.UseCases;

namespace ViewModel
{
    public partial class GroupDistributionViewModel : BaseViewModel
    {
        private IGroupDistributionRepository _groupDistributionRepository = new GroupDistributionRepository();

        private IDisciplineRepository _disciplineRepository = new DisciplineRepository();

        [ObservableProperty]
        private ObservableCollection<GroupDistribution> _groupDistributions;

        [ObservableProperty]
        private ObservableCollection<Discipline> _disciplines;

        [ObservableProperty]
        private int _hourPerAcademicYearTextBox;

        partial void OnHourPerAcademicYearTextBoxChanged(int value)
        {
            AddDisciplineInGroupCommand.NotifyCanExecuteChanged();
        }

        [ObservableProperty]
        private Discipline _currentDiscipline;

        [ObservableProperty]
        private bool _isEnabled = false;

        partial void OnCurrentDisciplineChanged(Discipline value)
        {
            IsEnabled = true;
            AddDisciplineInGroupCommand.NotifyCanExecuteChanged();
        }

        [ObservableProperty]
        private GroupDistribution _currentGroupDistribution;

        partial void OnCurrentGroupDistributionChanged(GroupDistribution value)
        {
            IsEnabled = false;
            DeleteDisciplineInGroupCommand.NotifyCanExecuteChanged();
        }

        private bool CurrentDisciplineInNotNullAndHour()
        {
            return CurrentDiscipline != null && HourPerAcademicYearTextBox > 0;
        }

        private bool CurrentGroupDistributionIsNotNull()
        {
            return CurrentGroupDistribution != null;
        }

        public Group Group {  get; set; }

        [RelayCommand(CanExecute = nameof(CurrentDisciplineInNotNullAndHour))]
        public void AddDisciplineInGroup()
        {
            GroupDistribution groupDistribution = new()
            {
                GroupId = Group.Id,
                DisciplineId = CurrentDiscipline.Id,
                HoursPerAcademicYear = HourPerAcademicYearTextBox,
                DateOfDistribution = DateOnly.FromDateTime(DateTime.Now)
            };

            _groupDistributionRepository.Add(groupDistribution);

            groupDistribution.Discipline = CurrentDiscipline;

            GroupDistributions.Add(groupDistribution);
            Disciplines.Remove(CurrentDiscipline);

            IsEnabled = true;
            HourPerAcademicYearTextBox = 0;
            AddDisciplineInGroupCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand(CanExecute = nameof(CurrentGroupDistributionIsNotNull))]
        public void DeleteDisciplineInGroup()
        {
            _groupDistributionRepository.Remove(CurrentGroupDistribution.GroupId, CurrentGroupDistribution.DisciplineId);

            Disciplines.Add(CurrentGroupDistribution.Discipline!);
            GroupDistributions.Remove(CurrentGroupDistribution);
        }

        public GroupDistributionViewModel(Group group)
        {
            Group = group;

            GroupDistributions = new(_groupDistributionRepository.GetAllByGroup(Group.Id));
            Disciplines = new(_disciplineRepository.GetAll().Where(x => GroupDistributions.Any(y => y.DisciplineId != x.Id)));
        }

    }
}
