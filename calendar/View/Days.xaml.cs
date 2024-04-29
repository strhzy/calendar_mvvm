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
    /// Логика взаимодействия для Days.xaml
    /// </summary>
    public partial class Days : Page
    {
        Main main;
        public Days(Main _main)
        {
            main = _main;
            InitializeComponent();
            main.update_dates += (sender, args) => UpdateDates();
            UpdateDates();
        }

        public void UpdateDates() 
        {
            while (days_wrappanel.Children.Count > 0)
                days_wrappanel.Children.RemoveAt(0);
            foreach (var i in main.days)
            {
                days_wrappanel.Children.Add(i);
            }
        }
    }
}
