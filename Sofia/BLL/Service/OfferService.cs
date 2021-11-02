
using Sofia.BLL.Model;
using Sofia.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofia.BLL.Service
{
    public class OfferService
    {
          IDAO ds;
        
        public OfferService(IDAO ds)
        {
            this.ds = ds;
        }
        public  void postTutoringRequest(int id,String adresse, String level, String scope,int duration, int conceptId, int learnerId, int filter)
        {
            ds.createRequest(new Request(id, level, scope, duration, ds.findConceptById(conceptId), filter), ds.findMemberById(learnerId));  
        }

        public  void postTutoringOffer(int id,int tutorId,int conceptId, int tutorLevel, int serviceRate,int duration)
        {
            ds.createTutoringOffer(new TutoringOffer(id,tutorLevel, serviceRate, ds.findConceptById(conceptId),duration), ds.findMemberById(tutorId)); 
        }

        public  void postTutorTimeSlot(TimeSlot timeSlot ,int offerId)
        {
            ds.createTutoringOfferTimeSlot(timeSlot, offerId);

        }
        public  void postLearnerTimeSlot( TimeSlot timeSlot, int requestId)
        {
            ds.createRequestTimeSlot(timeSlot, requestId);

        }


        public  void postMeeting(int requestId,int actualDuration,int tutorId)
        {
            ds.updateRequest(requestId, actualDuration, tutorId);
        }
    }
}
