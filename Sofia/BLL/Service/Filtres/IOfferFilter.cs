using Sofia.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofia.BLL.Service.Filtres
{
    interface IOfferFilter
    {
        TutoringOffer filterOut(IEnumerable<TutoringOffer> offers);
    }
}
