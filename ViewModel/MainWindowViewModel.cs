namespace ViewModel
{
    public partial class MainWindowViewModel
    {
        public StudentViewModel StudentViewModel { get; } = new();

        public GroupViewModel GroupViewModel { get; } = new();
    }
}
