using System.Windows.Controls;
using ViewModel;

namespace View.Tabs
{
    /// <summary>
    /// Логика взаимодействия для EmployeeTab.xaml
    /// </summary>
    public partial class EmployeeTab : UserControl
    {
        private readonly EmployeeViewModel _viewModel;

        public EmployeeTab()
        {
            InitializeComponent();

            _viewModel = new EmployeeViewModel();

            DataContext = _viewModel;
        }
    }
}
