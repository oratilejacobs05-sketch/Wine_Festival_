using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Wine_Festival_project
{
    public  class attendees : Festival ,IReport
    {
        public string TicketNumber { get; set; }
        public int Age { get; set; }
        public bool IsVIP { get; set; }
        public bool IsCheckedIn { get; set; }

       
        public attendees(string firstName, string surname, string ticket, int age, bool isVip) : base(firstName, surname)
        {
            TicketNumber = ticket;
            Age = age;
            IsVIP = isVip;
            IsCheckedIn = false;
        }
        
        public string GetReport() =>
          $"{Name} (Ticket: {TicketNumber}) | VIP: {IsVIP} | Checked In: {IsCheckedIn}";

        public override string GetStatusReport() =>
            $"{Name} | Age: {Age} | {(IsCheckedIn ? "Status : Inside Festival " : "Status : At Entrance")}";
    }


}

