
using NSubstitute;
using Sofia.BLL.Model;
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
    public class LikesFilterTester
    {
        TutoringOffer fakeOffer1;
        TutoringOffer fakeOffer2;
        TutoringOffer fakeOffer3;
        Member fakeTutor1;
        Member fakeTutor2;
        Member fakeTutor3;
        Concept fakeConcept;
        IList<TutoringOffer> fakeOffersList;
        [Fact]
        public void testLikesFilterOut()
        {
            //Arrange
            InMemoryDAO fakeDAO = Substitute.ForPartsOf<InMemoryDAO>();
            LikesFilter fakeLikesFilter = Substitute.ForPartsOf<LikesFilter>(fakeDAO);

            fakeTutor1 = new Member(1, "048398", "514-435-3606", "jean.tremblay@colval.qc.ca", "ProgFun", "Jean", "Tremblay", "Titulaire en programmation");
            fakeTutor1.Likes = 20;
            fakeConcept = new Concept(1,"Programmation c#", "programmation en c#", fakeTutor1);
            fakeOffer1 = new TutoringOffer(1,10, 45, fakeConcept);
            fakeOffer2 = new TutoringOffer(2,5, 34, fakeConcept);
            fakeOffer3 = new TutoringOffer(3,7, 25, fakeConcept);
            fakeTutor1.TutoringOffers.Add(fakeOffer1);
            fakeTutor2 = new Member(2,"05667", "514-421-6709", "michel.cote@colval.qc.ca", "motdepas123", "Michel", "Côté", "Titulaire en programmation");
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
            fakeDAO.Members.Add(fakeTutor1);
            fakeDAO.Members.Add(fakeTutor2);
            fakeDAO.Members.Add(fakeTutor3);


            //Act

            TutoringOffer actualOffer = fakeLikesFilter.filterOut(fakeOffersList);

            //Assert
            Assert.Equal(fakeOffer2.TutoringOfferId, actualOffer.TutoringOfferId);
        }
    }
}
