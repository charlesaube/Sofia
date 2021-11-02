using Sofia.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofia.DAL.Repository
{
    public interface IDAO
    {
         IList<Member> Members { get;  }
        IList<Topic> Topics { get; set; }

        Member findMemberByTutoringOfferId(int id);
        Member findMemberByRequestId(int id);
         Member findMemberById(int id);
        Concept findConceptByName(string name);
        Concept findConceptById(int id);
        Request findRequestById(int requestid);
        IEnumerable<Request> findAllRequest();
        TutoringOffer findTutoringOfferById(int id);
        IEnumerable<TutoringOffer> findAllTutoringOffers();
        void createTutoringOffer(TutoringOffer t, Member m);
        void createMember(Member m);
         TimeSlot findTimeSlotByTutoringOfferId(int offerId);
        void createRequest(Request r, Member m);
         void createRequestTimeSlot(TimeSlot t, int r);
         void createTutoringOfferTimeSlot(TimeSlot t, int o);
        void updateRequest(int requestId, int actualDuration, int tutorId);
    }
}
