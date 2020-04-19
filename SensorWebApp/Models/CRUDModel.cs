using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.IO.Ports;
using System.Threading;

namespace SensorWebApp.Models
{
    public class CRUDModel
    {
       
        public SQLiteConnection CreateConnection()
            {
                SQLiteConnection conn;
                //create a new database connection
                conn = new SQLiteConnection("Data Source = table.sqlite;");
                //open the connection
                try
                {
                    conn.Open();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                return conn;
            }

            public void createTable(SQLiteConnection conn)
            {
                SQLiteCommand SQLcmd = conn.CreateCommand();
                SQLcmd.CommandText = "DROP TABLE Data; CREATE TABLE Data(Distance INT, Time VarChar(30));";
            }
      
             public void InsertData(SQLiteConnection conn, String Data)
            {
                SQLiteCommand SQLcmd = conn.CreateCommand();
                Data = Data.Replace("\r\n" , "");
                String time = DateTime.Now.ToString("t");
                time = time.Replace(":", ".");
                SQLcmd.CommandText = "INSERT INTO Data(Distance, Time) VALUES(" + Data +","+ time +");";
                SQLcmd.ExecuteNonQuery();

            }

            public  DataTable ReadData(SQLiteConnection conn)
            {
                DataTable dt = new DataTable();
                SQLiteDataReader SQLread;
                SQLiteCommand SQLcmd = conn.CreateCommand();
                SQLcmd.CommandText = "SELECT * FROM Data ORDER BY TIME DESC limit 20;";
                SQLread = SQLcmd.ExecuteReader();
                dt.Load(SQLread);
                Console.WriteLine(dt);
                return dt;
            }
        }
    }