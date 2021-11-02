using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofia.BLL.Model;
using Sofia.DAL.Repository;

namespace Sofia.BLL.Service.Filtres
{
    public class LikesFilter : IOfferFilter
    {
        IDAO datasource;
        public LikesFilter()
        {

        }
        public LikesFilter(IDAO ds)
        {
            this.datasource = ds;
        }
        virtual public TutoringOffer filterOut(IEnumerable<TutoringOffer> offers)
        {
            TutoringOffer bestOffer = null;
            int bestLikes = 0;
            int tutorLikes = 0;
            foreach (TutoringOffer o in offers)
            {
                tutorLikes = datasource.findMemberByTutoringOfferId(o.TutoringOfferId).Likes;
                if (tutorLikes > bestLikes)
                {
                    bestOffer = o;
                    bestLikes = tutorLikes;
                }
                

            }
            return bestOffer;
        }
    }
    
}

