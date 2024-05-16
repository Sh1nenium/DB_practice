using System.Windows;
using ViewModel;

namespace View.Tabs.Windows
{
    /// <summary>
    /// Логика взаимодействия для TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        public TaskWindow()
        {
            InitializeComponent();
        }

        private void Open_New_Window_Score(object sender, RoutedEventArgs e)
        {
            var windowViewModel = ((TaskViewModel)DataContext).CreateScoreViewModel();

            var scoreWindow = new ScoreWindow
            {
                DataContext = windowViewModel
            };

            scoreWindow.Show();
        }
    }
}
