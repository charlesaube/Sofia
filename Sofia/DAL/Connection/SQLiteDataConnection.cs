using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofia.DAL.Connection
{
    public class SQLiteDataConnection : IDisposable
    {
        private SQLiteDataConnection()
        {

        }

        public static SQLiteDataConnection Instance { get; } = new SQLiteDataConnection();
        public SQLiteConnection Connection { get; private set; }

        public void Dispose()
        {
            this.Connection.Close();
        }

        public void OpenConnection()
        {

            Console.WriteLine("Connecting to database: Data Source = :memory:; Version = 3; New = true;" );

            this.Connection = new SQLiteConnection("Data Source = :memory:; Version = 3; New = true;");
            this.Connection.Open();
        }

    }
}
