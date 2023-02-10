using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace CLI_SQlite_Demonstration
{
    class Program
    {
        static void Main(string[] args)
        {

            createDatabase("MYdatabase");

            SQLiteConnection connection;
            connection = CreateConnection();
            createTable(connection);
            insertData(connection);
            ReadDisplayData(connection);

            Console.ReadLine(); // To pause CLI 
        }

        static void createDatabase(String db_name)
        {    
            try
            {
                SQLiteConnection.CreateFile(db_name + ".db"); // this creates a zero-byte file
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR : creating Database");
                Console.WriteLine("ERROR DETAILS : " + ex.Message);
            }

        }

        static SQLiteConnection CreateConnection()
        {
            SQLiteConnection connection; // Create a new database connection           
            connection = new SQLiteConnection("Data Source = MYdatabase.db; Version = 3; New = True; Compress = True; ");
            
            try {
                connection.Open(); // Open the connection:
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR : connection to Database");
                Console.WriteLine("ERROR DETAILS : " + ex.Message);
            }
            return connection;
        }

        static void createTable(SQLiteConnection conn)
        {          
            try
            {
                SQLiteCommand command;
                string createsql = "CREATE TABLE Product(Product VARCHAR(20), Quantity INT)";
                command = conn.CreateCommand();
                command.CommandText = createsql;
                command.ExecuteNonQuery();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR : creating Table");
                Console.WriteLine("ERROR DETAILS : " + ex.Message);
            }
        }

        static void insertData(SQLiteConnection conn)
        {          
            try
            {
                SQLiteCommand command;
                command = conn.CreateCommand();
                command.CommandText = "INSERT INTO Product(Product, Quantity) VALUES('Toshiba G900', 230); ";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO Product(Product, Quantity) VALUES('Samsung A20', 500); ";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO Product(Product, Quantity) VALUES('Nokia C21', 150); ";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO Product(Product, Quantity) VALUES('Huawei nova 10', 0); ";
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR : Inserting Data");
                Console.WriteLine("ERROR DETAILS : " + ex.Message);
            }
        }

        static void ReadDisplayData(SQLiteConnection conn)
        {           
            try
            {
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine("\t\t DATABASE INFORMATION ");
                Console.WriteLine("---------------------------------------------------------");

                SQLiteDataReader datareader;
                SQLiteCommand command;
                command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM Product";

                Console.WriteLine("PRODUCT NAME \t\t || QUANTITY");
                Console.WriteLine("---------------------------------------------------------");

                datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    string reader_data = datareader.GetString(0) + "\t\t || " + datareader.GetInt32(1) ;
                    Console.WriteLine(reader_data);
                }
                conn.Close();
                Console.WriteLine("---------------------------------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR : Reading Data");
                Console.WriteLine("ERROR DETAILS : " + ex.Message);
            }
        }


    }
}
