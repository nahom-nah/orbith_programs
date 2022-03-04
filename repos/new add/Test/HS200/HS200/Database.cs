using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Data;

namespace HS200
{
    class Database
    {
        public SQLiteConnection myConnection;
         
        public Database()
        {

            myConnection = new SQLiteConnection("Data Source=database.sqlite3");
            if (!File.Exists("./database.sqlite3"))
            {
                try
                {
                    SQLiteConnection.CreateFile("database.sqlite3");
                    Console.WriteLine("database file created!");
                    string sql = "create table messages (id INTEGER PRIMARY KEY AUTOINCREMENT, message TEXT NOT NULL)";
                    string resultSql = "create table results (id INTEGER PRIMARY KEY AUTOINCREMENT, message TEXT NOT NULL)";

                    myConnection.Open();
                    ExecuteCommand(sql);
                    Console.WriteLine("Message Table created!");
                    ExecuteCommand(resultSql);
                    Console.WriteLine("Result Table Created!");
                    Console.WriteLine("New db instance Connection is ready.");
                }
                catch (Exception exe)
                {
                    Console.WriteLine(exe.Message);
                }
            }
            else
            {
                myConnection.Open();
                Console.WriteLine("New db instance Connection is ready.");
            }
        }
        public ResFormat ExecuteCommand(string query)
        {
            try
            {
                SQLiteCommand command = new SQLiteCommand(query, myConnection);
                command.ExecuteNonQuery();
                return new ResFormat() { Message = "Message inserted successfully.", OK = true };
            }
            catch (Exception exe)
            {
                Console.WriteLine(exe.Message);
                return new ResFormat() { Message = "Exception occured => " + exe.Message, OK = false };
            }

        }
        public ResFormat InsertMessage(string message)
        {
            string log = "";
            try
            {
                string query = "INSERT INTO messages (message) VALUES ('" + message + "');";
                ResFormat res = ExecuteCommand(query);
                if (res.OK)
                {
                    Console.WriteLine("Message inserted.");
                    return res;
                }
                else
                {
                    Console.WriteLine("Failed to insert message.");
                    return res;
                }
            }
            catch (Exception exe)
            {
                Console.WriteLine("Exception occured and Failed to insert message");
                return new ResFormat() { Message = "Exception occured while inserting =>" + exe.Message, OK = false };
            }
        }

        public ResFormat InsertResult(string message)
        {
            string log = "";
            try
            {
                string query = "INSERT INTO results (message) VALUES ('" + message + "');";
                ResFormat res = ExecuteCommand(query);
                if (res.OK)
                {
                    Console.WriteLine("Result inserted.", ConsoleColor.Cyan);
                    return res;
                }
                else
                {
                    Console.WriteLine("Failed to insert result.", ConsoleColor.Cyan);
                    return res;
                }
            }
            catch (Exception exe)
            {
                Console.WriteLine("Exception occured and Failed to insert result", ConsoleColor.Cyan);
                return new ResFormat() { Message = "Exception occured while inserting result =>" + exe.Message, OK = false };
            }
        }

        public bool DeleteMessage(int id)
        {
            try
            {
                string query = "DELETE FROM messages WHERE id=" + id + ";";
                ResFormat status = ExecuteCommand(query);
                if (status.OK)
                {
                    Console.WriteLine("message deleted from database");
                    return true;
                }
                else
                {
                    Console.WriteLine("Failed to delete message with id " + id);
                    return false;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Exception occured and Failed to delete message with id " + id);
                return false;
            }
        }

        public bool DeleteResult(int id)
        {
            try
            {
                string query = "DELETE FROM results WHERE id=" + id + ";";
                ResFormat status = ExecuteCommand(query);
                if (status.OK)
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
        public DataTable SelectAllMessages()
        {
            try
            {
                string query = "SELECT id, message FROM messages";
                SQLiteDataAdapter adp = new SQLiteDataAdapter(query, myConnection);
                DataTable tbl = new DataTable();
                adp.Fill(tbl);
                return tbl;
            }
            catch (Exception)
            {
                Console.WriteLine("Exception occured while fetching messages");
                return null;
            }
        }

        public DataTable SelectAllResults()
        {
            try
            {
                string query = "SELECT id, message FROM results";
                SQLiteDataAdapter adp = new SQLiteDataAdapter(query, myConnection);
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
        public void CloseConnection()
        {
            myConnection.Close();
            Console.WriteLine("Connection closed");
        }
    }
}
