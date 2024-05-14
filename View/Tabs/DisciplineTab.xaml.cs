using System.Windows;
using System.Windows.Controls;
using View.Tabs.Windows;
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

        private void Open_New_Window_TrainingManual(object sender, RoutedEventArgs e)
        {
            var windowViewModel = _viewModel.CreateTrainingManualViewModel();

            var trainingManualWindow = new TrainingManualWindow
            {
                DataContext = windowViewModel
            };

            trainingManualWindow.Show();
        }

        private void Open_New_Window_Task(object sender, RoutedEventArgs e)
        {
            var windowViewModel = _viewModel.CreateTaskViewModel();

            var taskWindow = new TaskWindow
            {
                DataContext = windowViewModel
            };

            taskWindow.Show();
        }

        private void Open_New_Window_Employee(object sender, RoutedEventArgs e)
        {
            var windowViewModel = _viewModel.CreateEmployeeDisciplineViewModel();

            var employeeDisciplineWindow = new EmployeeInDisciplineWindow
            {
                DataContext = windowViewModel
            };

            employeeDisciplineWindow.Show();
        }
    }
}
