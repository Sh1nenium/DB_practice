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

        private void Open_New_Window_Discipline(object sender, RoutedEventArgs e)
        {
            var windowViewModel = _viewModel.CreateTrainingManualViewModel();

            var studentInGroupWindow = new TrainingManualWindow
            {
                DataContext = windowViewModel
            };

            studentInGroupWindow.Show();
        }
    }
}
