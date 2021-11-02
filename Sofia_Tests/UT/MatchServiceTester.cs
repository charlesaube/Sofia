
using NSubstitute;
using Sofia.BLL.Model;
using Sofia.BLL.Service;
using Sofia.BLL.Service.Filtres;
using Sofia.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sofia_Tests.UT
{
    public class MatchServiceTester
    {
        TutoringOffer fakeOffer1;
        TutoringOffer fakeOffer2;
        TutoringOffer fakeOffer3;
        Member fakeTutor1;
        Member fakeTutor2;
        Member fakeTutor3;
        Request fakeRequest1;
        Request fakeRequest2;
        Request fakeRequest3;
        Concept fakeConcept;
        IList<TutoringOffer> fakeOffersList;

        [Fact]
        public void testFindBestMatch()
        {
            //Arrange
            fakeRequest1 = new Request(1,"Fair", "pratique", 8, fakeConcept);
            fakeRequest2 = new Request(2,"Good", "pratique", 8, fakeConcept,2);
            fakeRequest3 = new Request(3,"Good","fondamental",6,fakeConcept,3);
            fakeConcept = new Concept(1,"Programmation c#", "programmation en c#", fakeTutor1);
            fakeOffer1 = new TutoringOffer(1,10, 45, fakeConcept);
            fakeOffer2 = new TutoringOffer(2,5, 34, fakeConcept);
            fakeOffer3 = new TutoringOffer(3,7, 25, fakeConcept);
            fakeTutor1 = new Member(1,"048398", "514-435-3606", "jean.tremblay@colval.qc.ca", "ProgFun", "Jean", "Tremblay", "Titulaire en programmation");
            fakeTutor1.Likes = 20;
            fakeTutor1.TutoringOffers.Add(fakeOffer1);
            fakeTutor2 = new Member(2, "05667", "514-421-6709", "michel.cote@colval.qc.ca", "motdepas123", "Michel", "Côté", "Titulaire en programmation");
            fakeTutor2.Likes = 43;
            fakeTutor2.TutoringOffers.Add(fakeOffer2);
            fakeTutor3 = new Member(3,"05642", "514-432-6324", "bob.barette@colval.qc.ca", "Ouiad342", "Bob", "Barette", "Titulaire en programmation");
            fakeTutor3.Likes = 18;
            fakeTutor3.TutoringOffers.Add(fakeOffer3);
            fakeOffersList = new List<TutoringOffer>
            {
                fakeOffer1,
                fakeOffer2,
                fakeOffer3
            };
            InMemoryDAO fakeDAO = Substitute.For<InMemoryDAO>();
            fakeDAO.Members.Add(fakeTutor1);
            fakeDAO.Members.Add(fakeTutor2);
            fakeDAO.Members.Add(fakeTutor3);

            MatchService fakeMatchService = Substitute.ForPartsOf<MatchService>(fakeDAO);

            
            ConfidenceFilter fakeConfidenceFilter = Substitute.For<ConfidenceFilter>();
            fakeConfidenceFilter.filterOut(Arg.Any<IEnumerable<TutoringOffer>>()).Returns(fakeOffer1);
            
            RateFilter fakeRateFilter = Substitute.For<RateFilter>();
            fakeRateFilter.filterOut(Arg.Any<IEnumerable<TutoringOffer>>()).Returns(fakeOffer3);

            LikesFilter fakeLikesFilter = Substitute.For<LikesFilter>(fakeDAO);
            fakeLikesFilter.filterOut(Arg.Any<IEnumerable<TutoringOffer>>()).Returns(fakeOffer2);
            
            fakeMatchService.findAllCompliantOffers(Arg.Any<Request>()).ReturnsForAnyArgs(fakeOffersList);
            fakeDAO.findMemberByOfferId(fakeTutor1.MemberId).Returns(fakeTutor1);
            fakeDAO.findMemberByOfferId(fakeTutor2.MemberId).Returns(fakeTutor2);
            fakeDAO.findMemberByOfferId(fakeTutor3.MemberId).Returns(fakeTutor3);


            //Act

            Member actualTutor1 = fakeMatchService.findBestMatch(fakeRequest1);
            Member actualTutor2 = fakeMatchService.findBestMatch(fakeRequest2);
            Member actualTutor3 = fakeMatchService.findBestMatch(fakeRequest3);

            //Assert

            Assert.Equal(fakeTutor1.MemberId, actualTutor1.MemberId);
            Assert.Equal(fakeTutor3.MemberId, actualTutor2.MemberId);
            Assert.Equal(fakeTutor2.MemberId, actualTutor3.MemberId);
        }
    }
}
