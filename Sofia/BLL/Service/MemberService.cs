using Sofia.BLL.Model;
using Sofia.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofia.BLL.Service
{
    class MemberService
    {
        public MemberService()
        {
            
        }

        public static void singupMember(IDAO ds,int id,String login, String tel, String email, String password, String firstName, String lastName, String introduction)
        {
            Member m = new Member(id,login, tel, email, password, firstName, lastName, introduction);
            ds.createMember(m);
        }
    }
}
