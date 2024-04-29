using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace calendar.Model
{
    public class SportSelect
    {
        public bool? Selected;
        public string Name;
        public string Image;

        public SportSelect(bool? a, string b, string c)
        {
            this.Selected = a;
            this.Name = b;
            this.Image = c;
        }
    }
}
