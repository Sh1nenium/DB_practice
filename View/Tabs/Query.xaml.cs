using System.Windows.Controls;
using ViewModel;

namespace View.Tabs
{
    /// <summary>
    /// Логика взаимодействия для Query.xaml
    /// </summary>
    public partial class Query : UserControl
    {
        private readonly QueryViewModel _viewModel;

        public Query()
        {
            InitializeComponent();

            _viewModel = new QueryViewModel();

            DataContext = _viewModel;
        }
    }
}
