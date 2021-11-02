using Sofia.BLL.Model;
using Sofia.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofia.BLL.Service.util
{
    public class  Session
    {

        public Member currentUser;

        public Session(string userType)
        {
            IDAO dao = new DatabaseDAO();
            if (userType == "Learner")
                currentUser = dao.findMemberById(9);
            else
                currentUser = dao.findMemberById(1);

            
        } 
    }
}
