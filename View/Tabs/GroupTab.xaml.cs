using System.Windows;
using System.Windows.Controls;
using View.Tabs.Windows;
using ViewModel;

namespace View.Tabs
{
    /// <summary>
    /// Логика взаимодействия для GroupTab.xaml
    /// </summary>
    public partial class GroupTab : UserControl
    {
        private readonly GroupViewModel _viewModel;

        public GroupTab()
        {
            InitializeComponent();

            _viewModel = new GroupViewModel();

            DataContext = _viewModel;
        }

        private void Open_New_Window_Student(object sender, RoutedEventArgs e)
        {
            var windowViewModel = _viewModel.CreateStudentsInGroupViewModel();

            var studentInGroupWindow = new StudentsInGroupWindow
            {
                DataContext = windowViewModel
            };

            studentInGroupWindow.Show();
        }

        private void Open_New_Window_Discipline(object sender, RoutedEventArgs e)
        {
            var windowViewModel = _viewModel.CreateGroupDistributionViewModel();

            var studentInGroupWindow = new GroupDistributionWindow
            {
                DataContext = windowViewModel
            };

            studentInGroupWindow.Show();
        }
    }
}
