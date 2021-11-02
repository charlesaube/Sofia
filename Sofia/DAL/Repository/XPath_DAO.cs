
using Sofia.BLL.Model;
using Sofia.BLL.Service.bob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Sofia.DAL.Repository
{
    public class XPath_DAO
    {
        public static XPath_DAO Instance { get; } = new XPath_DAO();
        //Chemin d'acces du fichier XML a changer selon l'endroit ou est enregistrer le programme
        private string xmlfile = Preferences.__XML_FILE_PATH;
        private XPathNavigator xpathNavigator;
        private XPathDocument xmlDocument;
        private XPath_DAO()
        {
             xmlDocument = new XPathDocument(@xmlfile);
            xpathNavigator = xmlDocument.CreateNavigator();
        }

        public Concept findConceptById(int id)
        {
            
            string findConceptByIdXPath = "//topic/concept[@conceptId='" + id + "']";
            XPathNodeIterator iterator = xpathNavigator.Select(findConceptByIdXPath);
            iterator.MoveNext();

            Member m = SqlMemberDAO.Instance.findMemberById(Convert.ToInt32(iterator.Current.GetAttribute("creatorId", "")));
            string name = iterator.Current.GetAttribute("name", "");
            string desc = iterator.Current.Value;
            return new Concept(id, name, desc, m);

        }

        public Concept findConceptByName(string conceptName)
        {
            
            string findConceptByNameXPath = "//topic/concept[@name='" + conceptName + "']";
            XPathNodeIterator iterator = xpathNavigator.Select(findConceptByNameXPath);
            iterator.MoveNext();
            int id = Convert.ToInt32(iterator.Current.GetAttribute("conceptId", ""));
            Member m = SqlMemberDAO.Instance.findMemberById(Convert.ToInt32(iterator.Current.GetAttribute("creatorId", "")));
            string desc = iterator.Current.Value;
            return new Concept(id, conceptName, desc, m);
        }
        public IList<string> findAllTopic()
        {
            
            string findAllTopic = "//topic";
            XPathNodeIterator iterator = xpathNavigator.Select(findAllTopic);
            IList<string> topicsName = new List<string>();
            while (iterator.MoveNext())
                topicsName.Add(iterator.Current.GetAttribute("label", ""));

            return topicsName;
        }
        public IList<string> findAllConceptFromTopic(string topicName)
        {
            
            string findConceptsByTopicName = "//topic[@label='" + topicName + "']/concept";

            XPathNodeIterator iterator = xpathNavigator.Select(findConceptsByTopicName);
            IList<string> conceptsName = new List<string>();
            while (iterator.MoveNext())
            {
                conceptsName.Add(iterator.Current.GetAttribute("name", ""));
            }
                
            return conceptsName;

        }

    }
}
