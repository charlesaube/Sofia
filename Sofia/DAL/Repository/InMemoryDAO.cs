using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofia.BLL.Model;

namespace Sofia.DAL.Repository
{
    public class InMemoryDAO : IDAO
    {
       public  IList<Member> Members { get; set; }
       public  IList<Topic> Topics { get; set; }

        public InMemoryDAO()
        {
            this.Members = new List<Member>();
            this.Topics = new List<Topic>();

            Topic t1 = new Topic( "Programmation", "Tout sur la création de programme/application");
            Member titulaire1 = new Member(1,"048398", "514-435-3606", "jean.tremblay@colval.qc.ca", "ProgFun", "Jean", "Tremblay", "Titulaire en programmation");
            titulaire1.Likes = 20;
            Concept c1 = new Concept(1, "C#", "Programmation en C#",titulaire1);
            t1.Concepts.Add(c1);
            Topic t2 = new Topic( "Bases de données", "Tout sur la création et gestion de bases de données");
            Member titulaire2 = new Member(2,"05667", "514-421-6709", "michel.cote@colval.qc.ca", "motdepas123", "Michel", "Côté", "Titulaire en base de données");
            titulaire2.Likes = 50;
            Concept c2 = new Concept(2, "SQL Server", "Bases de données SQL Server", titulaire2);
            t2.Concepts.Add(c2);

            Member etudiant = new Member(3,"1874673", "514-432-5679", "paul.houde@colval.qc.ca", "passwrd321", "Paul", "Houde", "Étudiant de deuxieme année");
            TutoringOffer offer1 = new TutoringOffer( 1,8, 20, c1);
            TutoringOffer offer2 = new TutoringOffer( 2,10, 35, c2);
            TutoringOffer offer3 = new TutoringOffer( 3,9, 27, c2);
            TutoringOffer offer4 = new TutoringOffer( 4,7, 19, c1);
            Request request1 = new Request(1, "Fair", "pratique", 8, c2,3);
            Request request2 = new Request(2, "Good", "pratique", 8, c1);
            etudiant.Requests.Add(request1);
            etudiant.Requests.Add(request2);
            titulaire1.TutoringOffers.Add(offer1);
            titulaire1.TutoringOffers.Add(offer2);
            titulaire2.TutoringOffers.Add(offer3);
            titulaire2.TutoringOffers.Add(offer4);

            Members.Add(etudiant);
            Members.Add(titulaire1);
            Members.Add(titulaire2);
            Topics.Add(t1);
            Topics.Add(t2);

        }
        virtual public Member findMemberByTutoringOfferId(int id)
        {
            foreach(Member tutor in Members)
            {
   
                    foreach(TutoringOffer offer in tutor.TutoringOffers)
                    {
                        if (offer.TutoringOfferId == id)
                            return tutor;

                    }
                
            }
            return null;
        }

        public Member findMemberById(int id)
        {
            foreach (Member tutor in Members)
            {                              
                        if (tutor.MemberId == id)
                            return tutor;         
            }
            return null;

        }


        public Concept findConceptByName(string name)
        {
            foreach(Topic topic in Topics)
            {
                foreach(Concept concept in topic.Concepts)
                {
                    if (concept.Name == name)
                        return concept;
                }
            }
            return null;
        }
        public Concept findConceptById(int id)
        {
            foreach (Topic topic in Topics)
            {
                foreach (Concept concept in topic.Concepts)
                {
                    if (concept.ConceptId == id)
                        return concept;
                }
            }
            return null;
        }


        public Request findRequestById(int requestid)
        {
            foreach(Member member in Members)
            {
                foreach(Request request in member.Requests)
                {
                    if (request.RequestId == requestid)
                        return request;
                }
            }
            return null;
        }

        public IEnumerable<Request> findAllRequest()
        {
            foreach (Member member in Members)
            {
                
                    return member.Requests;
                
            }
            return null;
        }
        public IEnumerable<TutoringOffer> findAllTutoringOffers(int tutorid)
        {
            foreach (Member member in Members)
            {
                if (member.MemberId == tutorid)
                {
                    return member.TutoringOffers;
                }
            }
            return null;
        }

        public TutoringOffer findTutoringOfferById(int id)
        {
            foreach (Member member in Members)
            {
                foreach (TutoringOffer offer in member.TutoringOffers)
                {
                    if (offer.TutoringOfferId == id)
                        return offer;
                }
            }
            return null;
        }

        public IEnumerable<TutoringOffer> findAllTutoringOffers()
        {
            throw new NotImplementedException();
        }

        public void createTutoringOffer(TutoringOffer t, Member m)
        {
            foreach (Member me in Members)
            {
                if (me.MemberId == m.MemberId)
                {
                    m.TutoringOffers.Add(t);
                }
            }
        }

        public void createMember(Member m)
        {
            Members.Add(m);
        }

        public void createRequest(Request r, Member m)
        {
            foreach (Member me in Members)
            {
                if (me.MemberId == m.MemberId)
                {
                    m.Requests.Add(r);
                }
            }
        }


        public void createTutoringOfferTimeSlot(TimeSlot t, TutoringOffer o)
        {
            foreach (Member m in Members)
            {
                foreach (TutoringOffer offer in m.TutoringOffers)
                    if (offer.TutoringOfferId == o.TutoringOfferId)
                    {
                        o.TimeSlots.Add(t);
                    }
            }
        }

        public void updateRequest(int requestId, int actualDuration, int tutorId)
        {
            findRequestById(requestId).ActualDuration = actualDuration;
            findRequestById(requestId).Tutor = findMemberById(tutorId);
        }

        public void createRequestTimeSlot(TimeSlot t, int r)
        {
            throw new NotImplementedException();
        }

        public void createTutoringOfferTimeSlot(TimeSlot t, int o)
        {
            throw new NotImplementedException();
        }

        public TimeSlot findTimeSlotByTutoringOfferId(int offerId)
        {
            throw new NotImplementedException();
        }

        public Member findMemberByRequestId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
