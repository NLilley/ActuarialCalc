using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows;

namespace ActuarialCalc
{
    public class MySQLController
    {

        private MySqlConnection connection;
        private String server = "localhost";
        private String database = "actuarialdata";
        private String uid = "root";
        private String password = "";

        public MySQLController(String _server, String _user, String _password, String _database)
        {
            server = _server;
            uid = _user;
            password = _password;
            database = _database;


            Initialize();

        }

        public MySQLController()
        {
            Initialize();
        }

        private void Initialize()
        {
            //String connectionString = "SERVER=" + server + ";" + "UID=" + uid + "PASSWORD=" + password + ";";
            String connectionString = "SERVER=" + server + ";" + "UID=" + uid + ";" + "DATABASE=" + database;

            connection = new MySqlConnection(connectionString);

        }

        public Boolean openConnection()
        {
            try
            {
                connection.Open();
                Console.WriteLine("Connection has been opened!");
                return true;

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("You were unable to establish a connection to the MySQL Server");
                return false;
            }


        }

        public Boolean closeConnection()
        {
            try
            {
                connection.Close();
                Console.WriteLine("Connection has been closed!");
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public void createDB(String databaseName)
        {

            if (this.openConnection() == true)
            {
                try
                {

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = "CREATE DATABASE " + databaseName + ";";
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("The table " + databaseName + " has successfully been created!");
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    this.closeConnection();
                }

            }
            else
            {
                Console.WriteLine("Could not establish a connection to the server.  No database has been created)");
            }

        }

        //Very specific method of creating a table.
        public void createTable(String tableName)
        {

            if (this.openConnection() == true)
            {
                try
                {
                    String command = "CREATE TABLE " + tableName + "(Age int, Lives int)";
                    MySqlCommand cmd = new MySqlCommand(command, connection);
                    cmd.ExecuteNonQuery();


                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    this.closeConnection();
                }
            }
            else
            {
                Console.WriteLine("No connection to the server could be established.  No tables have been created");
            }


        }

        public List<List<String>> performQuery(String query)
        {
            List<List<String>> queryResults = new List<List<String>>();

            if (this.openConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    int numOfColumns = dataReader.FieldCount;

                    while (dataReader.Read())
                    {
                        List<String> rowData = new List<String>();

                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            rowData.Add(dataReader.GetString(i));
                        }
                        queryResults.Add(rowData);
                    }

                    dataReader.Close();

                    this.closeConnection();

                    return queryResults;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    this.closeConnection();
                }

            }
                return queryResults;
            

        }

        public void performNonQuery(String command)
        {

            if (this.openConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(command, connection);

                    cmd.ExecuteNonQuery();

                    this.closeConnection();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }


            }

        }

    }


}
