using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calendar.Model
{
    class DayChange
    {
        public List<SportSelect> Selects;
        public DateTime Date;

        public DayChange(List<SportSelect> a, DateTime date)
        {
            Selects = a;
            Date = date;
        }
    }
}
