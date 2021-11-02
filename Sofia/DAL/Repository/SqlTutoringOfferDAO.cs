
using Sofia.BLL.Model;
using Sofia.DAL.Connection;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofia.DAL.Repository
{
    class SqlTutoringOfferDAO : SQLiteDAO<TutoringOffer> 
    {
        public static SqlTutoringOfferDAO Instance { get; } = new SqlTutoringOfferDAO();

        private SqlTutoringOfferDAO()
        {
            
        }
        protected override void Initialize()
        {
            SqliteConnection = SQLiteDataConnection.Instance.Connection;

            SQLiteCommand command = GetCommand();
            command.CommandText = GetDropQuery();
            command.ExecuteNonQuery();

            command.CommandText = "create table TutoringOffer ("
                + "TutoringOfferId int primary key not null, "
                + "ExpertiseLevel int not null, "
                + "ServiceRate double not null, "
                + "Duration int not null, "
                + "ConceptId int not null, "
                + "TutorId int not null)";
            command.ExecuteNonQuery();
            //offre concept1:
            InsertNewRow(command, 1, 8, 20,45,1,1);
            InsertNewRow(command, 2,7,15,60,1,2);
            //offre concept 2
            InsertNewRow(command, 3,9,25,30,2,3);
            InsertNewRow(command, 4,8,20,30,2,4);
            //offre concept 3
            InsertNewRow(command, 5, 9, 30, 30, 3, 5);
            InsertNewRow(command, 6, 6, 20, 45, 3, 6);
            //offre concept 4
            InsertNewRow(command, 7, 8, 25, 30, 4, 7);
            InsertNewRow(command, 8, 9, 15, 60, 4, 8);

        }
        

        private void InsertNewRow(SQLiteCommand command, int id, int expertiseLevel, double serviceRate,int duration, int conceptId, int tutorId)
        {
            command.CommandText = "insert into TutoringOffer (TutoringOfferId,ExpertiseLevel,ServiceRate,Duration,ConceptId,TutorId) values (?,?,?,?,?,?)";
            command.Parameters.AddWithValue("TutoringOfferId", id);
            command.Parameters.AddWithValue("ExpertiseLevel", expertiseLevel);
            command.Parameters.AddWithValue("ServiceRate", serviceRate);
            command.Parameters.AddWithValue("Duration", duration);
            command.Parameters.AddWithValue("ConceptId", conceptId);
            command.Parameters.AddWithValue("TutorId", tutorId);
            command.ExecuteNonQuery();
        }

        public void InsertNewRowWithObject(TutoringOffer t, Member tutor)
        {
            SQLiteCommand command = GetCommand();
            command.CommandText = GetDropQuery();
            command.CommandText = "insert into TutoringOffer (TutoringOfferId,ExpertiseLevel,ServiceRate,Duration,ConceptId,TutorId) values (?,?,?,?,?,?)";
            command.Parameters.AddWithValue("TutoringOfferId", t.TutoringOfferId);
            command.Parameters.AddWithValue("ExpertiseLevel", t.ExpertiseLevel);
            command.Parameters.AddWithValue("ServiceRate", t.ServiceRate);
            command.Parameters.AddWithValue("Duration", t.Duration);
            command.Parameters.AddWithValue("ConceptId", t.Concept.ConceptId);
            command.Parameters.AddWithValue("TutorId", tutor.MemberId);
            command.ExecuteNonQuery();
        }
        protected override TutoringOffer ReturnObject(SQLiteDataReader reader)
        {
            int id = reader.GetInt32(0);
            int expertiseLevel = reader.GetInt32(1);
            double serviceRate = reader.GetDouble(2);
            int duration = reader.GetInt32(3);
            Concept concept = XPath_DAO.Instance.findConceptById(reader.GetInt32(4));
            return  new TutoringOffer(id,expertiseLevel, serviceRate, concept,duration);
          
        }


        public TutoringOffer findTutoringOfferById(int id)
        {
            SQLiteCommand command = GetCommand();
            command.CommandText = GetSelectQuery("TutoringOfferId");
            command.Parameters.AddWithValue("TutoringOfferId", id);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            return ReturnObject(reader);
        }

        public Member findMemberByOfferId(int id)
        {
            SQLiteCommand command = GetCommand();
            command.CommandText = GetSelectQuery("TutoringOfferId");
            command.Parameters.AddWithValue("TutoringOfferId", id);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();  
            return SqlMemberDAO.Instance.findMemberById(reader.GetInt32(5));                  
        }
    }
}
