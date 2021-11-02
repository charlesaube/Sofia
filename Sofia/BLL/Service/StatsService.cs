using Sofia.BLL.Model;
using Sofia.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofia.BLL.Service
{
    public class StatsService
    {

       /* public static int computeMeetingUnits(IDAO ds,int learnerId)
        {
            int TotalTimeUnits = 0;
            foreach(Request request in ds.findAllRequest(learnerId))
            {
                TotalTimeUnits = TotalTimeUnits + request.ActualDuration;

            }

            return TotalTimeUnits;
        }
        */
        public static int computeProvidedTutoringUnits(IDAO ds, int tutorId)
        {
            int TotalProvidedTimeUnits = 0;
            foreach(Member tutor in ds.Members)
            {
                foreach(Request request in tutor.Requests)
                {
                    if(request.Tutor == ds.findMemberById(tutorId))
                    {
                        TotalProvidedTimeUnits += request.ActualDuration;
                    }
                }
            }

            return TotalProvidedTimeUnits;
        }
    }
}
