using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encouragement_Board
{
    [Serializable()]
    class Encourager : Person
    {
        private int number_given;

        public Encourager() : base()
        {
            number_given = 0;
        }

        public Encourager(string name, int number_given, DateTime? date) : base(name)
        {
            this.number_given = number_given;
            Dates.Add((DateTime)date);
        }

        public int Number_Given
        {
            get { return number_given; }
            set { number_given = value;}
        }

        public void Add_ET(DateTime? date)
        {
            Dates.Add((DateTime)date);
            Number_Given += 1;
            SortDates();
        }

        public void Delete_ET(DateTime? date)
        {
            Dates.Remove((DateTime)date);
            Number_Given -= 1;
            if(Number_Given < 0 )
            {
                Number_Given = 0;
            }

            SortDates();
        }
    }
}
