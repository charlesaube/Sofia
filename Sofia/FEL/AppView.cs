using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofia.BLL;
using Sofia.BLL.Model;
using Sofia.BLL.Service;
using Sofia.DAL.Connection;
using Sofia.DAL.Repository;

namespace Sofia.FEL
{
    public class AppView
    {
        public static void run()
        {


            IDAO databaseDAO = new DatabaseDAO();

            MatchService matchService = new MatchService(databaseDAO);
            OfferService offerService = new OfferService(databaseDAO);
            /*
            Console.WriteLine(match.findBestMatch(databaseDAO.findRequestById(3)).ToString());
            
            foreach (String label in XPath_DAO.Instance.findAllConceptFromTopic("Programmation"))
                Console.WriteLine(label);

            Console.WriteLine(SqlRequestDAO.Instance.findHighestId());
            */
        }

    }
}
