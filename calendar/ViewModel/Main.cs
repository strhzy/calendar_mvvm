using calendar.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using calendar.ViewModel.Helpers;
using System.CodeDom;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using calendar.Model;
using Newtonsoft.Json;
using System.IO;

namespace calendar.ViewModel
{
    public class Main : BindingHelper
    {
        public List<Day> days = new List<Day>();

        private List<Sport> sports = new List<Sport>();

        public List<Sport> Sports
        {
            get { return sports; }
            set
            {
                sports = value;
                OnPropertyChanged();
            }
        }

        private int actualday;

        public int actual_day
        {
            get { return actualday; }
            set
            {
                actualday = value;
                OnPropertyChanged();
            }
        }


        private string month_year_text;

        public string MonthYearText
        {
            get { return month_year_text; }
            set
            {
                month_year_text = value;
                OnPropertyChanged();
            }
        }

        private DateTime date;

        int count;

        public event EventHandler update_dates;
        public event EventHandler change_to_dates;
        public event EventHandler change_to_sports;

        public Main()
        {
            date = DateTime.Now;
            while (date.Day != 1)
            {
                date = date.AddDays(-1);
            }
            count = count = DateTime.DaysInMonth(date.Year, date.Month);
            MonthYearText = DateTime.Now.ToString("MMMM") + " " + DateTime.Now.Year.ToString();
            set_dates();
            update_dates?.Invoke(this, EventArgs.Empty);
        }

        public void button1(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.ToString() == "+")
            {
                while (date.Day != 1)
                {
                    date = date.AddDays(-1);
                }
                date = date.AddMonths(1);
                MonthYearText = date.ToString("MMMM") + " " + date.Year.ToString();
                count = DateTime.DaysInMonth(date.Year, date.Month);
                set_dates();
                update_dates?.Invoke(this, EventArgs.Empty);
            }
            else if ((sender as Button).Content.ToString() == "📁")
            {
                set_dates();
                save();
            }

        }

        public void button2(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.ToString() == "-")
            {
                while (date.Day != 1)
                {
                    date = date.AddDays(-1);
                }
                date = date.AddMonths(-1);
                MonthYearText = date.ToString("MMMM") + " " + date.Year.ToString();
                count = DateTime.DaysInMonth(date.Year, date.Month);
                set_dates();
                update_dates?.Invoke(this, EventArgs.Empty);
            }
            else if ((sender as Button).Content.ToString() == "<")
            {
                sports.Clear();
                date = DateTime.Now;
                while (date.Day != 1)
                {
                    date = date.AddDays(-1);
                }
                MonthYearText = DateTime.Now.ToString("MMMM") + " " + DateTime.Now.Year.ToString();
                set_dates();
                frame_to_days();
                update_dates?.Invoke(this, EventArgs.Empty);
            }
        }

        private void save()
        {
            MessageBox.Show(date.Year.ToString() + " " + date.Month.ToString() + " " + actual_day.ToString());
            List<DayChange> x = SerDeser.Deserialization<DayChange>().Where(b => (b.Date.Month != date.Date.Month) || (b.Date.Year != date.Date.Year) || (Convert.ToInt64(b.Date.Day) != actual_day)).ToList();
            SerDeser.Serialization(x);
            MessageBox.Show(x.Count().ToString());
            List<SportSelect> sport_changes = new List<SportSelect>();
            foreach (var i in sports)
            {
                SportSelect sports = new SportSelect(i.SportCheckBox.IsChecked, i.NameOfSport.Text, i.SportImage.Source.ToString());
                sport_changes.Add(sports);
            }

            DayChange day_change = new DayChange(sport_changes, date.AddDays(actual_day - 1));
            SerDeser.Serialization(day_change);
        }

        public void frame_to_days()
        {
            change_to_dates?.Invoke(this, EventArgs.Empty);
        }

        public void frame_to_sports()
        {
            MonthYearText = date.ToString("dddd") + ", " + date.ToString("MMMM") + " " + actual_day + ", " + " " + date.Year.ToString();
            change_to_sports?.Invoke(this, EventArgs.Empty);
        }

        public void set_sports(int day)
        {
            var deser_days = SerDeser.Deserialization<DayChange>();
            DayChange? changed_day = null;

            string[] names_images = new string[7] { "basketball", "box", "fitness", "football", "hockey", "tennis", "volleyball" };
            string[] names_points = new string[7] { "Баскетбол", "Бокс", "Фитнес", "Футбол", "Хоккей", "Теннис", "Волейбол" };

            foreach (var i in deser_days)
            {
                if (Convert.ToInt32(i.Date.Day) == day && (i.Date.Month == date.Month) && (i.Date.Year == date.Year))
                {
                    changed_day = i;
                }
            }
            foreach (string i in names_images)
            {
                var path = System.IO.Path.GetFullPath(@$"..\..\..\View\images\{i}.png");
                var Path = new BitmapImage(new Uri(path));
                Sport sport = new Sport();
                sport.SportImage.Source = Path;
                sports.Add(sport);
            }
            for (int i = 0; i < 7; i++)
            {
                sports[i].NameOfSport.Text = names_points[i];
            }
            if (changed_day != null)
            {
                for (int i = 0; i < 7; i++)
                {
                    sports[i].SportCheckBox.IsChecked = changed_day.Selects[i].Selected;
                }
            }

        }

        private void set_dates()
        {
            days.Clear();
            var d = SerDeser.Deserialization<DayChange>();
            for (int i = 0; i < count; i++)
            {
                Day day = new Day(this);
                var path = System.IO.Path.GetFullPath(@"..\..\..\View\images\none.png");
                var Path = new BitmapImage(new Uri(path));
                day.ImageOfDay.Source = Path;
                day.DayNumber.Text = (i + 1).ToString();
                days.Add(day);
            }
            for (int i = 0; i < days.Count(); i++)
            {
                foreach (var j in d)
                {
                    if (Convert.ToInt32(days[i].DayNumber.Text) == j.Date.Day && j.Date.Year == date.Year && j.Date.Month == date.Month)
                    {
                        string? path = null;

                        foreach (var f in j.Selects)
                        {
                            if (f.Selected == true)
                            {
                                path = f.Image.ToString();
                            }
                        }
                        if (path != null)
                        {
                            var Path = new BitmapImage(new Uri(path));
                            days[i].ImageOfDay.Source = Path;
                        }
                    }
                }
            }
        }

        public void clear_day()
        {
            List<DayChange> x = SerDeser.Deserialization<DayChange>().Where(b => (b.Date.Month != date.Date.Month) || (b.Date.Year != date.Date.Year) || (Convert.ToInt64(b.Date.Day) != actual_day)).ToList();
            SerDeser.Serialization<DayChange>(x);
            set_dates();
            frame_to_days();
            update_dates?.Invoke(this, EventArgs.Empty);
        }
    }
}
