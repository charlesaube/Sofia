
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
    public abstract class SQLiteDAO<T> 
    {
        protected  SQLiteConnection SqliteConnection;

        protected SQLiteDAO()
        {
            Initialize();
        }


        // Crée un objet de type T
        protected abstract T ReturnObject(SQLiteDataReader reader);


        // Crée et vide la table
        
         protected abstract void Initialize();

        // Sélectionne toutes les entités de la table
        public IList<T> SelectAll()
        {
            SQLiteCommand command = SqliteConnection.CreateCommand();
            command.CommandText = GetSelectQuery();
            SQLiteDataReader reader = command.ExecuteReader();

            IList<T> result = new List<T>();

            while (reader.Read()) result.Add(ReturnObject(reader));

            return result;
        }



        protected string GetDropQuery()
        {
            return $"drop table if exists {typeof(T).Name}";
        }



        protected string GetSelectQuery(string whereClause = null)
        {
            string query = $"select * from {typeof(T).Name}";

            // Ajoute la condition, si présente
            if (whereClause != null) query += $" where {whereClause} = ?";

            return query;
        }

        protected SQLiteCommand GetCommand()
        {
            return SqliteConnection.CreateCommand();
        }


    }
}
