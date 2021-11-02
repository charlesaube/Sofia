
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
    public class SqlTimeSlotDAO : SQLiteDAO<TimeSlot>
    {
        public static SqlTimeSlotDAO Instance { get; } = new SqlTimeSlotDAO();

        private SqlTimeSlotDAO()
        {
        }

        protected override void Initialize()
        {
            SqliteConnection = SQLiteDataConnection.Instance.Connection;

            SQLiteCommand command = GetCommand();
            command.CommandText = GetDropQuery();
            command.ExecuteNonQuery();

            command.CommandText = "create table TimeSlot ("
                + "TimeSlotId int primary key not null, "
                + "Date date not null, "
                + "StartTime varchar(50) not null, "
                + "EndTime varchar(50) not null, "
                + "Adresse varchar(150), "
                + "TimeSlotType varchar(10) not null, "
                + "OfferId int not null)";
            command.ExecuteNonQuery();
            //time offer 1
            InsertNewRow(command, 1, DateTime.Now, "2pm", "4pm", "Collège de Valleyfield", "TutoringOffer", 1);
            //time offer 2
            InsertNewRow(command, 2, DateTime.Now, "1pm", "2pm", "Collège de Valleyfield", "TutoringOffer", 2);
            //time offer 3
            InsertNewRow(command, 3, DateTime.Now, "8am", "10am", "Collège de Valleyfield", "TutoringOffer", 3);
            //time offer 4
            InsertNewRow(command, 4, DateTime.Now, "2pm", "4pm", "Collège de Valleyfield", "TutoringOffer", 4);
            //time offer 5
            InsertNewRow(command, 5, DateTime.Now, "8am", "10am", "Collège de Valleyfield", "TutoringOffer", 5);
            //time offer 6
            InsertNewRow(command, 6, DateTime.Now, "1pm", "2pm", "Collège de Valleyfield", "TutoringOffer", 6);
            //time offer 7
            InsertNewRow(command, 7, DateTime.Now, "2pm", "4pm", "Collège de Valleyfield", "TutoringOffer", 7);
            //time offer 8
            InsertNewRow(command, 8, DateTime.Now, "1pm", "3pm", "Collège de Valleyfield", "TutoringOffer", 8);

            //time request 1
            InsertNewRow(command, 9, DateTime.Now, "2pm", "4pm", "Collège de Valleyfield", "Request", 1);
            //time request 2
            InsertNewRow(command, 10, DateTime.Now, "2pm", "4pm", "Collège de Valleyfield", "Request", 2);
            //time request 3
            InsertNewRow(command, 11, DateTime.Now, "2pm", "4pm", "Collège de Valleyfield", "Request", 3);
            //time request 4
            InsertNewRow(command, 12, DateTime.Now, "2pm", "4pm", "Collège de Valleyfield", "Request", 4);



        }
        private void InsertNewRow(SQLiteCommand command, int id, DateTime date, string startTime, string endTime, string adresse, string type, int offerId)
        {
            command.CommandText = "insert into TimeSlot (TimeSlotId,Date,StartTime,EndTime,Adresse,TimeSlotType,OfferId) values (?,?,?,?,?,?,?)";
            command.Parameters.AddWithValue("TimeSlotId", id);
            command.Parameters.AddWithValue("Date", date);
            command.Parameters.AddWithValue("StartTime", startTime);
            command.Parameters.AddWithValue("EndTime", endTime);
            command.Parameters.AddWithValue("Adresse", adresse);
            command.Parameters.AddWithValue("TimeSlotType", type);
            command.Parameters.AddWithValue("OfferId", offerId);
            command.ExecuteNonQuery();
        }

        public void InsertNewTutoringOfferTimeSlot (TimeSlot t, int offerId)
        {
            SQLiteCommand command = GetCommand();
            command.CommandText = "insert into TimeSlot (TimeSlotId,Date,StartTime,EndTime,Adresse,TimeSlotType,OfferId) values (?,?,?,?,?,?,?)";
            command.Parameters.AddWithValue("TimeSlotId", t.TimeSlotId);
            command.Parameters.AddWithValue("Date", t.Date);
            command.Parameters.AddWithValue("StartTime", t.StartTime);
            command.Parameters.AddWithValue("EndTime", t.EndTime);
            command.Parameters.AddWithValue("Adresse", t.Adresse);
            command.Parameters.AddWithValue("TimeSlotType", "TutoringOffer");
            command.Parameters.AddWithValue("OfferId", offerId);
            command.ExecuteNonQuery();
        }
        public void InsertNewRequestTimeSlot(TimeSlot t, int requestId)
        {
            SQLiteCommand command = GetCommand();
            command.CommandText = "insert into TimeSlot (TimeSlotId,Date,StartTime,EndTime,Adresse,TimeSlotType,OfferId) values (?,?,?,?,?,?,?)";
            command.Parameters.AddWithValue("TimeSlotId", t.TimeSlotId);
            command.Parameters.AddWithValue("Date", t.Date);
            command.Parameters.AddWithValue("StartTime", t.StartTime);
            command.Parameters.AddWithValue("EndTime", t.EndTime);
            command.Parameters.AddWithValue("Adresse", t.Adresse);
            command.Parameters.AddWithValue("TimeSlotType", "Request");
            command.Parameters.AddWithValue("OfferId", requestId);
            command.ExecuteNonQuery();
        }
        protected override  TimeSlot ReturnObject(SQLiteDataReader reader)
        {
            int id = reader.GetInt32(0);
            DateTime date = reader.GetDateTime(1);
            string startTime = reader.GetString(2);
            string endTime = reader.GetString(3);
            string adresse = reader.GetString(4);
            return new TimeSlot(id,date, startTime, endTime, adresse);
    
        }

        public TimeSlot findTimeSlotById(int id)
        {
            SQLiteCommand command = GetCommand();
            command.CommandText = GetSelectQuery("TimeSlotId");
            command.Parameters.AddWithValue("TimeSlotId", id);
            SQLiteDataReader reader = command.ExecuteReader();

            if(reader != null)
            {

                reader.Read();

                return ReturnObject(reader);
            }

            return null;
        }

        public TimeSlot findTimeSlotByTutoringOfferId(int offerId)
        {
            SQLiteCommand command = GetCommand();
            command.CommandText = "Select * from TimeSLot "+
                "Where TimeSlotType = ? and OfferId = ?";
            command.Parameters.AddWithValue("TimeSlotType", "TutoringOffer");
            command.Parameters.AddWithValue("OfferId", offerId);
            SQLiteDataReader reader = command.ExecuteReader();

            reader.Read();
            return ReturnObject(reader);
        }



    }
}
