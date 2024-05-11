using System.Windows.Controls;
using ViewModel;

namespace View.Tabs
{
    /// <summary>
    /// Логика взаимодействия для DisciplineTab.xaml
    /// </summary>
    public partial class DisciplineTab : UserControl
    {
        private readonly DisciplineViewModel _viewModel;

        public DisciplineTab()
        {
            InitializeComponent();

            _viewModel = new DisciplineViewModel();

            DataContext = _viewModel;
        }
    }
}
