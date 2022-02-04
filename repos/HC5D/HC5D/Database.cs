using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Data;
namespace HC5D
{
    public class ResFormat
    {
        public string Message { get; set; }
        public bool Ok { get; set; }
    }
     class Database
    {
        public SQLiteConnection connection;

        public Database()
        {
            try
            {
            connection = new SQLiteConnection("Data Source=database.sqlite3");
            if (!File.Exists("./database.sqlite3"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                string sql = "create table messages (id INTEGER PRIMARY KEY AUTOINCREMENT, message TEXT NOT NULL)";
                string resultSql = "create table results (id INTEGER PRIMARY KEY AUTOINCREMENT, message TEXT NOT NULL)";

                connection.Open();

                ExecuteCommand(sql);
                Console.WriteLine("message table successfully created");

                ExecuteCommand(resultSql);
                Console.WriteLine("result table successfully created");

                Console.WriteLine("New db instance Connection is ready.");

                }
                else
                {
                    connection.Open();
                    Console.WriteLine("New db instance Connection is ready.");
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public ResFormat ExecuteCommand(string query)
        {
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.ExecuteNonQuery();


            return new ResFormat() { Message= "Command successfully Executed", Ok=true};

        }
        public ResFormat InsertResult(string result)
        {
          try{

                string query = "INSERT INTO results (message) VALUES ('" + result + "');";
                ResFormat res = ExecuteCommand(query);
                if (res.Ok)
                {
                    Console.WriteLine("Result inserted successfully");
                    return res;
                }
                else
                {
                    Console.WriteLine("Failed to insert");
                    return res;
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResFormat() { Message = "Exception occured while inserting =>" + ex.Message, Ok = false };
            }
            
        }

        public bool DeleteResult(int id)
        {
            try
            {
                string query = "DELETE FROM results WHERE id=" + id + ";";
                ResFormat status = ExecuteCommand(query);
                if (status.Ok)
                {
                    Console.WriteLine("result deleted from database");
                    return true;
                }
                else
                {
                    Console.WriteLine("Failed to delete result with id " + id);
                    return false;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Exception occured and Failed to delete result with id " + id);
                return false;
            }
        }


        public DataTable SelectAllResults()
        {
            try
            {
                string query = "SELECT id, message FROM results";
                SQLiteDataAdapter adp = new SQLiteDataAdapter(query, connection);
                DataTable tbl = new DataTable();
                adp.Fill(tbl);
                return tbl;
            }
            catch (Exception)
            {
                Console.WriteLine("Exception occured while fetching results");
                return null;
            }
        }
    }
}
