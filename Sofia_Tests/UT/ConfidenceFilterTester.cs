
using NSubstitute;
using Sofia.BLL.Model;
using Sofia.BLL.Service.Filtres;
using Sofia.BLL.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Sofia.DAL.Repository;

namespace Sofia_Tests.UT
{
    public class ConfidenceFilterTester
    {
        TutoringOffer fakeOffer1;
        TutoringOffer fakeOffer2;
        TutoringOffer fakeOffer3;
        Member fakeCreator;
        Concept fakeConcept;
        IList<TutoringOffer> fakeOffersList;

        [Fact]
        public void testConfidenceFilterOut()
        {
            //Arrange
            ConfidenceFilter fakeconfidenceFilter = Substitute.ForPartsOf<ConfidenceFilter>();
            fakeCreator = new Member(1,"048398", "514-435-3606", "jean.tremblay@colval.qc.ca", "ProgFun", "Jean", "Tremblay", "Titulaire en programmation");
            fakeConcept = new Concept(1,"Programmation c#", "programmation en c#", fakeCreator);
            fakeOffer1 = new TutoringOffer(1,10, 45, fakeConcept);
            fakeOffer2 = new TutoringOffer(2,5, 34, fakeConcept);
            fakeOffer3 = new TutoringOffer(3,7, 25, fakeConcept);
            fakeOffersList = new List<TutoringOffer>
            {
                fakeOffer1,
                fakeOffer2,
                fakeOffer3
            };

            

            //Act

            TutoringOffer actualOffer = fakeconfidenceFilter.filterOut(fakeOffersList);

            //Assert
            Assert.Equal(fakeOffer1.TutoringOfferId, actualOffer.TutoringOfferId);
        }
    }
}
