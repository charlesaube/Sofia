using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofia.BLL.Model;
using Sofia.DAL.Connection;

namespace Sofia.DAL.Repository
{
    public class DatabaseDAO  : IDAO
    {
        SQLiteDataConnection connection;
        SqlMemberDAO memberDAO;
        SqlTutoringOfferDAO tutoringOfferDAO;
        SqlRequestDAO requestDAO;
        SqlTimeSlotDAO timeSlotDAO;
        XPath_DAO topicConcept_DAO;
        public static DatabaseDAO Instance { get; } = new DatabaseDAO();
        public DatabaseDAO()
        {
            connection = SQLiteDataConnection.Instance;
            connection.OpenConnection();
            memberDAO = SqlMemberDAO.Instance;
            tutoringOfferDAO = SqlTutoringOfferDAO.Instance;
            requestDAO = SqlRequestDAO.Instance;
            timeSlotDAO = SqlTimeSlotDAO.Instance;
            topicConcept_DAO = XPath_DAO.Instance;
        }

        public IList<Member> Members => throw new NotImplementedException();

        public IList<Topic> Topics { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }



        public void createMember(Member m)
        {
            memberDAO.InsertNewRowWithObject(m);
        }

        public void createRequest(Request r,Member m)
        {
            requestDAO.InsertNewRowWithObject(r,m);
        }

        public void createTutoringOffer(TutoringOffer o, Member m)
        {
            tutoringOfferDAO.InsertNewRowWithObject(o, m);
        }

        public void createTutoringOfferTimeSlot(TimeSlot t , int o)
        {
            timeSlotDAO.InsertNewTutoringOfferTimeSlot(t, o);
        }
        public void createRequestTimeSlot(TimeSlot t, int r)
        {
            timeSlotDAO.InsertNewRequestTimeSlot(t, r);
        }

        public IEnumerable<Request> findAllRequest()
        {
            return requestDAO.SelectAll();
        }

        public IEnumerable<TutoringOffer> findAllTutoringOffers()
        {
            return tutoringOfferDAO.SelectAll();
        }

        public Concept findConceptById(int id)
        {
            return topicConcept_DAO.findConceptById(id);
        }

        public Concept findConceptByName(string name)
        {
            return topicConcept_DAO.findConceptByName(name);
        }

        public Member findMemberById(int id)
        {
            return memberDAO.findMemberById(id);
        }

        public Member findMemberByTutoringOfferId(int id)
        {
            return tutoringOfferDAO.findMemberByOfferId(id);
        }
        public Member findMemberByRequestId(int id)
        {
            return requestDAO.findMemberByRequestId(id);
        }
        public Request findRequestById(int requestid)
        {
            return requestDAO.findRequestById(requestid);
        }

        public TutoringOffer findTutoringOfferById(int id)
        {
            return tutoringOfferDAO.findTutoringOfferById(id);
        }
        public TimeSlot findTimeSlotByTutoringOfferId(int offerId)
        {
            return timeSlotDAO.findTimeSlotByTutoringOfferId(offerId);
        }

        public void updateRequest(int requestId, int actualDuration, int tutorId)
        {
             requestDAO.updateRequest(requestId, actualDuration, tutorId);
        }
    }
}
