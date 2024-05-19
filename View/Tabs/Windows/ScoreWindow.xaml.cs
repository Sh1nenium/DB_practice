using Model;
using System.Windows;
using System.Windows.Controls;
using ViewModel;

namespace View.Tabs.Windows
{
    /// <summary>
    /// Логика взаимодействия для ScoreWindow.xaml
    /// </summary>
    public partial class ScoreWindow : Window
    {
        public ScoreWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (textBox.DataContext is Score score)
                {
                    ((ScoreViewModel)DataContext).ScoreNumberChangedCommand.Execute(score);
                }
            }
        }
    }
}
