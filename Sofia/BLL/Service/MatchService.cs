
using Sofia.BLL.Model;
using Sofia.BLL.Service.bob;
using Sofia.BLL.Service.Filtres;
using Sofia.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofia.BLL.Service
{

    public class MatchService 
    {
        IDAO dataService;
        public MatchService(IDAO ds)
        {
            this.dataService = ds;
        }

        public TutoringOffer  findBestMatch(Request request)
        {
            //Step 1 chercher la liste des offres qui peuvent satisfaire le request
            IEnumerable<TutoringOffer> offers = findAllCompliantOffers(request);
            //Step 2: la meilleur offre selon le gout du demandeur
            if (offers.Count() >= 1)
                switch (request.Filter)
                {
                    case Preferences.__CONFIDENCE_FILTER:
                        IOfferFilter confidenceFilter = new ConfidenceFilter();
                        TutoringOffer  confidenceWinnerOffer = confidenceFilter.filterOut(offers);
                        return confidenceWinnerOffer;
                        

                    case Preferences.__RATE_FILTER:
                        IOfferFilter rateFilter = new RateFilter();
                        TutoringOffer rateWinnerOffer = rateFilter.filterOut(offers);
                        return rateWinnerOffer;
                        

                    case Preferences.__LIKE_FILTER:
                        IOfferFilter likeFilter = new LikesFilter(dataService);
                        TutoringOffer likeWinnerOffer = likeFilter.filterOut(offers);
                        return likeWinnerOffer;
                        
                    default :

                        break;


                }
            else { TutoringOffer winnerOffer = offers.FirstOrDefault(); }
            return null;
        }
        virtual public IEnumerable<TutoringOffer> findAllCompliantOffers(Request request)
        {
            IList<TutoringOffer> CompliantOffers = new List<TutoringOffer>();

            foreach (TutoringOffer offer in dataService.findAllTutoringOffers()) 
                {
                    if(offer.Concept.ConceptId == request.Concept.ConceptId)
                    {
                        CompliantOffers.Add(offer);
                    }
                }
            
            
            return CompliantOffers;
            
        }
    }
}
