using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofia.BLL.Model;

namespace Sofia.BLL.Service.Filtres
{
    public class RateFilter : IOfferFilter
    {
        virtual public TutoringOffer filterOut(IEnumerable<TutoringOffer> offers)
        {
            TutoringOffer bestOffer = null;
            double bestRate = 100000;
            foreach (TutoringOffer o in offers)
            {
                if (o.ServiceRate < bestRate)
                {
                    bestOffer = o;
                    bestRate = o.ServiceRate;
                }

            }
            return bestOffer;

        }
    }
}
