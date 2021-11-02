using Sofia.BLL.Service.bob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofia.BLL.Model
{
    public class TutoringOffer
    {
        public int TutoringOfferId { get; set; }
        public int ExpertiseLevel { get; set; }
        public double ServiceRate { get; set; }
        public int Duration { get; set; }
        public Concept Concept { get; set; }
        public IList<TimeSlot> TimeSlots { get; set; }

        public TutoringOffer(int id,int expertiseLevel, double serviceRate, Concept concept,int duration=0)
        {
            this.TutoringOfferId = id;
            this.ExpertiseLevel = expertiseLevel;
            this.ServiceRate = serviceRate;
            this.Duration = duration;
            this.Concept = concept;
            TimeSlots = new List<TimeSlot>();
        }
        public override String ToString()
        {
            return " Tutoring offer Id: "+TutoringOfferId+"Expertise level: " + ExpertiseLevel + " Service rate: " + ServiceRate + " Concept: "+ Concept.ToString()+" ";

        }

    }
}
