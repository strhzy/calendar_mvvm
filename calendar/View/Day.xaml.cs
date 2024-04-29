using calendar.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace calendar.View
{
    /// <summary>
    /// Логика взаимодействия для Day.xaml
    /// </summary>
    public partial class Day : UserControl
    {
        Main main;
        public Day(Main _main)
        {
            InitializeComponent();
            main = _main;
            DataContext = main;
        }

        private void ButtonDay_Click(object sender, RoutedEventArgs e)
        {
            main.actual_day = Convert.ToInt32(this.DayNumber.Text);
            main.set_sports(Convert.ToInt32(this.DayNumber.Text));
            main.frame_to_sports();
        }

        private void ContextMenu1_Click(object sender, RoutedEventArgs e) 
        {
            main.actual_day = Convert.ToInt32(this.DayNumber.Text);
            main.set_sports(Convert.ToInt32(this.DayNumber.Text));
            main.frame_to_sports();
        }

        private void ContextMenu2_Click(object sender, RoutedEventArgs e)
        {
            main.actual_day = Convert.ToInt32(this.DayNumber.Text);
            main.clear_day();
        }
    }
}
