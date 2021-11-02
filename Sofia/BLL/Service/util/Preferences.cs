using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofia.BLL.Service.bob
{
    public class Preferences
    {
        public const int __CONFIDENCE_FILTER = 1;
        public const int __RATE_FILTER = 2;
        public const int __LIKE_FILTER = 3;
        public const int __DEFAULT_FILTER = __CONFIDENCE_FILTER;
        public static int __CONCEPT_AUTO_ID = 1;
        public static int __MEMBER_AUTO_ID = 1;
        public static int __REQUEST_AUTO_ID = 5;
        public static int __TIMESLOT_AUTO_ID = 13;
        public static int __TOPIC_AUTO_ID = 1;
        public static int __TUTORINGOFFER_AUTO_ID = 9;
        public static string __XML_FILE_PATH = "D://CÉGEP//CEGEP Automne 2019//Développement d'applications graphiques//Sofia//Sofia//DAL//Repository//TopicConceptXML1.xml";
    }
}
