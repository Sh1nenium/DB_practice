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

    public partial class GroupViewModel : BaseViewModel
    {
        private IGroupRepository _groupRepository = new GroupRepository();

        private State _state = State.OnDefault;

        [ObservableProperty]
        private bool _isEnabledStudentInfo = false;

        [ObservableProperty]
        private bool _isEnabledDataGrid = true;

        [ObservableProperty]
        private ObservableCollection<Group> _groups;

        [ObservableProperty]
        private Group? _currentGroup = null;

        partial void OnCurrentGroupChanged(Group? value)
        {
            EditGroupCommand.NotifyCanExecuteChanged();
        }

        private bool GroupNotNull()
        {
            return CurrentGroup != null;
        }

        private bool GroupsIsExist()
        {
            return Groups.Count != 0;
        }

        private void SwapState(State state)
        {
            IsEnabledStudentInfo = !IsEnabledStudentInfo;
            IsEnabledDataGrid = !IsEnabledDataGrid;
            _state = state;
        }

        [RelayCommand]
        public void AddGroup()
        {
            SwapState(State.OnAdd);
            CurrentGroup = new()
            {
                Id = Groups.Count + 1
            };
            ApplyGroupCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand(CanExecute = nameof(GroupNotNull))]
        public void EditGroup()
        {
            SwapState(State.OnEdit);
        }

        [RelayCommand(CanExecute = nameof(GroupsIsExist))]
        public async Task DeleteGroup()
        {
            if (CurrentGroup == null)
            {
                await _groupRepository.Remove(Groups.Last().Id);
                Groups.Remove(Groups.Last());
                return;
            }
            await _groupRepository.Remove(CurrentGroup.Id);
            Groups.Remove(CurrentGroup);

            DeleteGroupCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand()]
        public async Task ApplyGroup()
        {
            if (CurrentGroup == null) return;

            if (_state == State.OnAdd)
            {
                await _groupRepository.Add(CurrentGroup);
                Groups.Add(CurrentGroup);
            }

            if (_state == State.OnEdit)
            {
                _groupRepository.Update(CurrentGroup);
            }

            SwapState(State.OnDefault);
            CurrentGroup = null;
        }

        [RelayCommand]
        public async Task CancelGroup()
        {
            if (_state == State.OnAdd)
            {
                CurrentGroup = null;
                SwapState(State.OnDefault);
                return;
            }

            if (_state == State.OnEdit)
            {
                if (CurrentGroup == null) return;

                Group? groupCopy = await _groupRepository.GetById(CurrentGroup.Id);

                int index = Groups.IndexOf(CurrentGroup);

                if (index == -1 || groupCopy == null) return;

                CurrentGroup = null;
                Groups[index] = groupCopy;
                SwapState(State.OnDefault);

                return;
            }
        }

        public GroupViewModel()
        {
            Groups = new ObservableCollection<Group>(_groupRepository.GetAll());
        }
    }
}
