using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofia.BLL.Model;

namespace Sofia.BLL.Service.Filtres
{
    public class ConfidenceFilter : IOfferFilter
    {


        virtual public TutoringOffer filterOut(IEnumerable<TutoringOffer> offers)
        {
            TutoringOffer bestOffer=null ;
            int bestExpertise = 1;
            foreach (TutoringOffer o in offers)
            {
                if (o.ExpertiseLevel > bestExpertise)
                {
                    bestOffer = o;
                    bestExpertise = o.ExpertiseLevel;
                }

            }
            return bestOffer;
        }
    }
}
