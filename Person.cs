using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encouragement_Board
{
    [Serializable()]
    class Person
    {
        private string full_name;
        private List<DateTime> dates;
        
        public Person()
        {
            full_name = string.Empty;
            dates = new List<DateTime>();
        }

        public Person(string name)
        {
            full_name = name;
            dates = new List<DateTime>();
        }

        public string Full_Name
        {
            get { return full_name; }
            set { full_name = value; }
        }

        public List<DateTime> Dates
        {
            get { return dates; }
            set { dates = value; }
        }

        public void SortDates()
        {
            Dates.Sort(DateTime.Compare);
        }

        public string Date
        {
            get
            {
                if (!Dates.Any()) //Resolves if Dates contains no elements
                {
                    return string.Empty;
                }
                else
                {
                    return Dates.Last().ToString("MM/dd/yyyy");
                }
            }
        }

        public override string ToString()
        {
            return Full_Name;
        }
    }
}
