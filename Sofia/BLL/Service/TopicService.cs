using Sofia.BLL.Model;
using Sofia.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofia.BLL.Service
{
    public class TopicService
    {

        public static void recordTopic(IDAO ds,String topicLabel,String description)
        {
            Topic t = new Topic(topicLabel, description);
            ds.Topics.Add(t);
        }
        
        public void recordConcept(IDAO ds,int id,Topic topic,String conceptName,String description, int creatorId)
        {
            Concept c = new Concept(id,conceptName, description,ds.findMemberById(creatorId));
            topic.Concepts.Add(c);
        }
    }
}

