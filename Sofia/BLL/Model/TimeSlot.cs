using Sofia.BLL.Service.bob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofia.BLL.Model
{
    public  class TimeSlot
    {
        public int TimeSlotId { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Adresse { get; set; }

        public TimeSlot(int id,DateTime date, string startTime, string endTime, string adresse = " ")
        {
            this.TimeSlotId = id;
            this.Date = date;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.Adresse = adresse;
        }
        public override String ToString()
        {
            return "Time slot ID: "+TimeSlotId+" Date: "+Date.ToLongDateString() + " Start time: " + StartTime + " End time: " + EndTime + " Adresse: " + Adresse;

        }
    }
}
