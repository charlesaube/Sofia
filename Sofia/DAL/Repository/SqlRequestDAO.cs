
using Sofia.BLL.Model;
using Sofia.BLL.Service.bob;
using Sofia.DAL.Connection;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofia.DAL.Repository
{
    class SqlRequestDAO : SQLiteDAO<Request>
    {
        private SqlRequestDAO()
        {

        }

        public static SqlRequestDAO Instance { get; } = new SqlRequestDAO();

        protected override void Initialize()
        {
            SqliteConnection = SQLiteDataConnection.Instance.Connection;

            SQLiteCommand command = GetCommand();
            command.CommandText = GetDropQuery();
            command.ExecuteNonQuery();
            //Creer la table
            command.CommandText = "create table Request ("
                + "RequestId int primary key not null, "
                + "BackgroundLevel varchar(50) not null, "
                + "Scope varchar(50) not null, "
                + "ExpectedDuration int not null, "
                + "ActualDuration int, "
                + "Filter int not null, "
                + "ConceptId int not null, "
                + "LearnerId int not null, "
                + "TutorId int)";
            command.ExecuteNonQuery();
            //request concept 1
            InsertNewRow(command, 1, "Beginner", "Practice", 30, 9,1,1);
            //request concept 2
            InsertNewRow(command, 2, "Intermediate", "Practice", 45, 10, 2, 2);
            //request concept 3
            InsertNewRow(command, 3, "Beginner", "Theorie and Practice", 30, 9, 3, 3);
            //request concept 4
            InsertNewRow(command, 4, "Beginner", "Theorie and Practice", 30, 10, 4, 1);
        }


        protected  void InsertNewRow(SQLiteCommand command, int id, string backgroundLevel, string scope, int expectedDuration, int learnerId,int conceptId, int filter, int actualDuration = 0, int tutorId = 0)
        {
            command.CommandText = "insert into Request (RequestId,BackgroundLevel,Scope,ExpectedDuration,ActualDuration,Filter,ConceptId,LearnerId,TutorId) values (?,?,?,?,?,?,?,?,?)";
            command.Parameters.AddWithValue("RequestId", id);
            command.Parameters.AddWithValue("BackgroundLevel", backgroundLevel);
            command.Parameters.AddWithValue("Scope", scope);
            command.Parameters.AddWithValue("ExpectedDuration", expectedDuration);
            command.Parameters.AddWithValue("ActualDuration", actualDuration);
            command.Parameters.AddWithValue("Filter", filter);
            command.Parameters.AddWithValue("ConceptId", conceptId);
            command.Parameters.AddWithValue("LearnerId", learnerId);
            command.Parameters.AddWithValue("TutorId", tutorId);
            command.ExecuteNonQuery();
        }

        public void InsertNewRowWithObject(Request m, Member requester)
        {
            SQLiteCommand command = GetCommand();
            command.CommandText = "insert into Request (RequestId,BackgroundLevel,Scope,ExpectedDuration,ActualDuration,Filter,ConceptId,LearnerId) values (?,?,?,?,?,?,?,?)";
            command.Parameters.AddWithValue("RequestId", m.RequestId);
            command.Parameters.AddWithValue("BackgroundLevel", m.BackgroundLevel);
            command.Parameters.AddWithValue("Scope", m.Scope);
            command.Parameters.AddWithValue("ExpectedDuration", m.ExpectedDuration);
            command.Parameters.AddWithValue("ActualDuration", m.ActualDuration);
            command.Parameters.AddWithValue("Filter", m.Filter);
            command.Parameters.AddWithValue("ConceptId", m.Concept.ConceptId);
            command.Parameters.AddWithValue("LearnerId", requester.MemberId);
            command.ExecuteNonQuery();
        }

        public void updateRequest(int requestId,int actualDuration, int tutorId)
        {
            SQLiteCommand command = GetCommand();
            command.CommandText = "UPDATE Request SET  ActualDuration = ? ,TutorId = ? WHERE RequestId = ?";
            command.Parameters.AddWithValue("ActualDuration", actualDuration);
            command.Parameters.AddWithValue("TutorId", tutorId);
            command.Parameters.AddWithValue("RequestId", requestId);
            command.ExecuteNonQuery();
        }
        protected override Request ReturnObject(SQLiteDataReader reader)
        {
            int id = reader.GetInt32(0);
            string backgroundLevel = reader.GetString(1);
            string scope = reader.GetString(2);
            int expectedDuration = reader.GetInt32(3);
            int actualDuration = reader.GetInt32(4);
            int filter = reader.GetInt32(5);          
            Concept concept = XPath_DAO.Instance.findConceptById(reader.GetInt32(6));

            Request r = new Request(id,backgroundLevel, scope, expectedDuration, concept, filter);

            return r;
        }


        public Request findRequestById(int id)
        {
            SQLiteCommand command = GetCommand();
            command.CommandText = "SELECT * FROM Request WHERE RequestId = ?";
            command.Parameters.AddWithValue("RequestId", id);
            SQLiteDataReader reader = command.ExecuteReader();

            reader.Read();

            return ReturnObject(reader);
        }

        public Member findMemberByRequestId(int id)
        {
            SQLiteCommand command = GetCommand();
            command.CommandText = "SELECT * FROM Request WHERE RequestId = ?";
            command.Parameters.AddWithValue("RequestId", id);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            return SqlMemberDAO.Instance.findMemberById(reader.GetInt32(7));
        }

    }
}
