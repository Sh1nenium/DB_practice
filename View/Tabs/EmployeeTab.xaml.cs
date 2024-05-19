using System.Windows;
using System.Windows.Controls;
using View.Tabs.Windows;
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
        private void Open_New_Window_Discipline(object sender, RoutedEventArgs e)
        {
            var windowViewModel = _viewModel.CreateDisciplineEmployeeViewModel();

            var employeeDisciplineWindow = new DisciplineInEmployeeWindow
            {
                DataContext = windowViewModel
            };

            employeeDisciplineWindow.Show();
        }

    }
}
