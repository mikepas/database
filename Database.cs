using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DatabaseClass
{
    class Database
    {
        private MySqlConnection _conn;

        private void Connect(string server, string uid, string pwd, string database)
        {
            try
            {
                string connStr = string.Format("server={0};uid={1};pwd={2};database={3}", server, uid, pwd, database);
                _conn = new MySqlConnection(connStr);
                _conn.Open();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public bool Login(string server, string uid, string pwd, string database, string query)
        {
            Connect(server, uid, pwd, database);

            bool result = false;

            if (_conn.State == ConnectionState.Open)
            {
                MySqlCommand mscCommand = new MySqlCommand(query, _conn);
                MySqlDataReader queryresult = mscCommand.ExecuteReader();
                result = queryresult.Read();
                _conn.Close();
            }
            else
            {
                MessageBox.Show(@"Could not connect to the database");
                return false;
            }

            if (result)
            {
                MessageBox.Show(@"You are logged in");
                return true;
            }
            else
            {
                MessageBox.Show(@"Incorrect username or password");
                return false;
            }
        }
    }
}
