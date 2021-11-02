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
    public class SqlMemberDAO : SQLiteDAO<Member>
    {

        private SqlMemberDAO()
        {
        }
        public static SqlMemberDAO Instance { get; } = new SqlMemberDAO();

        protected override void Initialize()
        {
            SqliteConnection = SQLiteDataConnection.Instance.Connection;
            SQLiteCommand command = GetCommand();
            command.CommandText = GetDropQuery();
            command.ExecuteNonQuery();

            command.CommandText = "create table Member ("
                + "MemberId int primary key not null, "
                + "Login varchar(50) not null, "
                + "Tel varchar(50) not null, "
                + "Email varchar(100) not null, "
                + "Password varchar(50) not null, "
                + "FirstName varchar(50) not null, "
                + "LastName varchar(50) not null, "
                + "Introduction varchar(150) not null, "
                + "Likes int  not null)";
            command.ExecuteNonQuery();
            //Tutor
            InsertNewRow(command, 1, "04839", "514-435-3606", "jean.tremblay@colval.qc.ca", "ProgFun", "Jean", "Tremblay", "Titulaire en programmation C#", 20);
            InsertNewRow(command, 2, "05667", "514-421-6709", "michel.cote@colval.qc.ca", "motdepas123", "Michel", "Côté", "Titulaire en programmation C#", 10);
            InsertNewRow(command, 3, "04835", "514-445-3436", "bob.tremblay@colval.qc.ca", "motdepas123", "Bob", "Tremblay", "Titulaire en programmation Java", 5);
            InsertNewRow(command, 4, "04875", "514-475-3436", "joelle.norcock@colval.qc.ca", "Pdlso", "Joelle", "Norcock", "Titulaire en programmation Java", 10);
            InsertNewRow(command, 5, "05835", "514-485-3436", "garette.tremblay@colval.qc.ca", "Pdlso", "Garette", "Tremblay", "Titulaire en bases de données SQL", 15);
            InsertNewRow(command, 6, "05335", "514-485-3436", "suzanne.brunon@colval.qc.ca", "Pdlso", "Suzanne", "Brunon", "Titulaire en bases de données SQL", 5);
            InsertNewRow(command, 7, "08335", "514-485-3436", "marc.dupuis@colval.qc.ca", "Pdlso", "Marc", "Dupuis", "Titulaire en bases de données XML", 5);
            InsertNewRow(command, 8, "06335", "514-485-3436", "mike.razow@colval.qc.ca", "Pdlso", "Mike", "Razowski", "Titulaire en bases de données XML", 20);
            //Learner
            InsertNewRow(command, 9, "1874673", "514-432-5679", "paul.houde@colval.qc.ca", "passwrd321", "Paul", "Houde", "Étudiant de deuxieme année", 0);
            InsertNewRow(command, 10, "1974573", "514-432-5679", "renaud.roun@colval.qc.ca", "passwrd321", "Renaud", "Roun", "Étudiant de premiere année", 0);
        }

        private void InsertNewRow(SQLiteCommand command, int id, string login, string tel, string email, string password, string firstName, string lastName, string intro, int likes)
        {
            command.CommandText = "insert into Member (MemberId,Login,Tel,Email,Password,FirstName,LastName,Introduction,Likes) values (?,?,?,?,?,?,?,?,?)";
            command.Parameters.AddWithValue("MemberId", id);
            command.Parameters.AddWithValue("Login", login);
            command.Parameters.AddWithValue("Tel", tel);
            command.Parameters.AddWithValue("Email", email);
            command.Parameters.AddWithValue("Password", password);
            command.Parameters.AddWithValue("FirstName", firstName);
            command.Parameters.AddWithValue("LastName", lastName);
            command.Parameters.AddWithValue("Introduction", intro);
            command.Parameters.AddWithValue("Likes", likes);
            command.ExecuteNonQuery();
        }

        public void InsertNewRowWithObject(Member m)
        {
            SQLiteCommand command = GetCommand();
            command.CommandText = "insert into Member (MemberId,Login,Tel,Email,Password,FirstName,LastName,Introduction,Likes) values (?,?,?,?,?,?,?,?,?)";
            command.Parameters.AddWithValue("MemberId", m.MemberId);
            command.Parameters.AddWithValue("Login", m.Login);
            command.Parameters.AddWithValue("Tel", m.Tel);
            command.Parameters.AddWithValue("Email", m.Email);
            command.Parameters.AddWithValue("Password", m.Password);
            command.Parameters.AddWithValue("FirstName", m.FirstName);
            command.Parameters.AddWithValue("LastName", m.LastName);
            command.Parameters.AddWithValue("Introduction", m.Introduction);
            command.Parameters.AddWithValue("Likes", m.Likes);
            command.ExecuteNonQuery();
        }
        
        protected override  Member ReturnObject(SQLiteDataReader reader)
        {
            int id = reader.GetInt32(0);
            string login = reader.GetString(1);
            string tel = reader.GetString(2);
            string email = reader.GetString(3);
            string password = reader.GetString(4);
            string firstName = reader.GetString(5);
            string lastName = reader.GetString(6);
            string introduction = reader.GetString(7);
            int likes = reader.GetInt32(8);


            return new Member(id,login, tel, email, password, firstName, lastName, introduction, likes);
        }

        public Member findMemberById(int id)
        {
            SQLiteCommand command = GetCommand();
            command.CommandText = GetSelectQuery("MemberId");
            command.Parameters.AddWithValue("MemberId", id);
            SQLiteDataReader reader = command.ExecuteReader();

            reader.Read();

            return ReturnObject(reader);
        }

     
    }
}
