using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using calendar.ViewModel;

namespace calendar.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Main main;
        Days days;
        public MainWindow()
        {
            InitializeComponent();
            main = new Main();
            days = new Days(main);
            this.DataContext = main;
            main.change_to_dates += (sender, args) => change_for_days();
            main.change_to_sports += (sender, args) => change_for_sports();
            frame_main.Content = days;
        }

        public void change_for_days()
        {
            fist_button.Content = "-";
            second_button.Content = "+";
            frame_main.Content = days;
        }

        public void change_for_sports() 
        {
            fist_button.Content = "<";
            second_button.Content = "📁";
            frame_main.Content = new Sports(main);
        }
    }
}