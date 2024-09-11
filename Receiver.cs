using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encouragement_Board
{
    [Serializable()]
    class Receiver : Person
    {
        private int number_received;
        private Encourager? last_encourager;

        public Receiver() : base()
        {
            number_received = 0;
            last_encourager = null;
        }

        public Receiver(string name, int number_received, DateTime? date, Encourager last_encourager) : base(name)
        {
            this.number_received = number_received;
            Dates.Add((DateTime)date);
            this.last_encourager = last_encourager;
        }

        public Receiver(string receiver_name, int number_received, DateTime? date, string encourager_name, int number_given = 0) : base(receiver_name)
        {
            this.number_received = number_received;
            Dates.Add((DateTime)date);
            last_encourager = new Encourager(encourager_name, number_given, date);
        }

        public int Number_Received
        {
            get { return number_received; }
            set { number_received = value; }
        }

        public Encourager? Last_Encourager
        {
            get { return last_encourager; }
            set { last_encourager = value;}
        }

        public void Add_ET(DateTime? date, Encourager encourager)
        {
            Dates.Add((DateTime)date);
            SortDates();
            if(this.Dates.Last() == (DateTime)date)
            {
                Last_Encourager = encourager;
            }

            Number_Received += 1;
        }

        public void Delete_ET(DateTime? date)
        {
            Dates.Remove((DateTime)date);
            Number_Received -= 1;
            if(Number_Received < 0)
            {
                Number_Received = 0;
            }

            if(Number_Received == 0)
            {

            }

            SortDates();
        }
    }
}
