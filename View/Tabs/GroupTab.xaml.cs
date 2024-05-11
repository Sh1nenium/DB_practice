using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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

        private void Open_New_Window(object sender, RoutedEventArgs e)
        {
            var windowViewModel = _viewModel.CreateStudentsInGroupViewModel();

            var studentInGroupWindow = new StudentsInGroupWindow
            {
                DataContext = windowViewModel
            };

            studentInGroupWindow.Show();
        }
    }
}
