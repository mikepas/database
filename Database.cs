using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DatabaseClass
{
    class Database
    {

        public static bool Login(string server, string uid, string pwd, string database, string query)
        {
            bool result = false;

            try
            {
                MySqlConnection conn = new MySqlConnection(string.Format("server={0};uid={1};pwd={2};database={3}", server, uid, pwd, database));
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    MySqlCommand mscCommand = new MySqlCommand(query, conn);
                    MySqlDataReader queryresult = mscCommand.ExecuteReader();
                    result = queryresult.Read();
                    conn.Close();
                }
            }
            catch
            {
                MessageBox.Show(@"Could not connect to the database!");
                return false;
            }

            if (!result)
            {
                MessageBox.Show(@"Incorrect username or password!");
                return false;
            }

            MessageBox.Show(@"You are logged in!");
            return true;
        }
    }
}
