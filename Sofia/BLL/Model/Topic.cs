using Sofia.BLL.Service.bob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofia.BLL.Model
{
    public class Topic
    {
        public int TopicId { get; set; }
        public String Label { get; set; }
        public String Description { get; set; }
        public IList<Concept> Concepts { get; set; }
        public Topic(String label, String description)
        {
            this.TopicId = Preferences.__TOPIC_AUTO_ID++;
            this.Label = label;
            this.Description = description;
            Concepts = new List<Concept>();
        }

        public override String ToString()
        {
            return "TopicId: "+TopicId+" Label: "+ Label + " Description: " + Description + " " ;

        }
    }
}
