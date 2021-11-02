using Sofia.BLL.Service.bob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofia.BLL.Model
{
    public class Request
    {
        public int RequestId { get; set; }
        public String BackgroundLevel { get; set; }
        public String Scope { get; set; }
        public int ExpectedDuration { get; set; }
        public int ActualDuration { get; set; }
        public int Filter { get; set; }
        public Concept Concept { get; set; }
        public Member Tutor { get; set; }
        public IList<TimeSlot> Meetings { get; set; }

        public Request(int id, String backgroundLevel, String scope, int expectedDuration, Concept concept, int filter = Preferences.__DEFAULT_FILTER)
        {
            this.RequestId = id;
            this.BackgroundLevel = backgroundLevel;
            this.Scope = scope;
            this.ExpectedDuration = expectedDuration;
            this.ActualDuration = 0;
            this.Filter = filter;
            this.Concept = concept;
            Meetings = new List<TimeSlot>();
        }

        public override String ToString()
        {
            return "Request Id: "+RequestId+" Background level:"+BackgroundLevel + "  Scope:" + Scope + " Expected duration: " + ExpectedDuration + " Actual Duration: "+ ActualDuration + " Filter: " + Filter + " Concept: " +Concept.ToString() + " Tutor: " + Tutor.ToString() + " " ;

        }
    }
}
