using Model;
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
using System.Windows.Shapes;
using ViewModel;

namespace View.Tabs.Windows
{
    /// <summary>
    /// Логика взаимодействия для StudentScoreWindow.xaml
    /// </summary>
    public partial class StudentScoreWindow : Window
    {
        public StudentScoreWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (textBox.DataContext is Score score)
                {
                    ((StudentScoreViewModel)DataContext).ScoreNumberChangedCommand.Execute(score);
                }
            }
        }
    }
}
