using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using View.Tabs.Windows;
using ViewModel;

namespace View.Tabs
{
    /// <summary>
    /// Логика взаимодействия для StudentTab.xaml
    /// </summary>
    public partial class StudentTab : UserControl
    {
        private readonly StudentViewModel _viewModel;

        public StudentTab()
        {
            InitializeComponent();

            _viewModel = new StudentViewModel();

            DataContext = _viewModel;
        }
        
        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                _viewModel.UploadImage(openFileDialog.FileName);
            }
        }

        private void Open_New_Window_Score(object sender, RoutedEventArgs e)
        {
            var windowViewModel = _viewModel.CreateStudentScoreViewModel();

            var studentInGroupWindow = new StudentsInGroupWindow
            {
                DataContext = windowViewModel
            };

            studentInGroupWindow.Show();
        }
    }
}
