using Sofia.BLL.Service.bob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Sofia.BLL.Model
{
    public class Concept
    {
        public int ConceptId { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public Member Creator { get; set; }

        public Concept(int id,String name, String description, Member creator)
        {
            this.ConceptId = id;
            this.Name = name;
            this.Description = description;
            this.Creator = creator;
        }

        public override String ToString()
        {

            return "Concept Id:"+ConceptId+" Concept name: "+Name + " Description: " + Description+ " Creator id: " + Creator.MemberId ;

        }
    }
}
