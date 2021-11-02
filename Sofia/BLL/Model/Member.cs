using Sofia.BLL.Service.bob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofia.BLL.Model
{
    public class Member
    {
        public int MemberId { get; set; }
        public String Login { get; set; }
        public String Tel { get; set; }

        public String Email { get; set; }

        public String Password { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Introduction { get; set; }
        public int Likes { get; set; }

        public IList<TutoringOffer> TutoringOffers { get; set; }
        public IList<Request> Requests { get; set; }

        public Member(int id,String login ,String tel,String email,String password,String firstName ,String lastName ,String introduction  ,int likes = 0)
        {
            this.MemberId = id;
            this.Login = login;
            this.Tel = tel;
            this.Email = email;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Introduction = introduction;
            this.Likes = likes;
            TutoringOffers = new List<TutoringOffer>();
            Requests = new List<Request>();


        }

        public override String ToString()
        {
            return "Id: "+MemberId+" Username: "+Login + " Telephone: " + Tel + " Email: " + Email + " Password: " + Password + " Name: " + FirstName + " " + LastName + " Intro: " + Introduction + " Likes: " + Likes;
        }



    }
}
