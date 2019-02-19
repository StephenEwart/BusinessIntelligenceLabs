using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.OleDb;
using System.Data.SqlClient;

namespace BusinessIntelligenceLabs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLookup_Click(object sender, EventArgs e)
        {
            List<string> dates = new List<string>();
            lstBox.Items.Clear();

            string connectionString = Properties.Settings.Default.Data_set_1_1_ConnectionString;

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                OleDbDataReader reader = null;
                OleDbCommand getDates = new OleDbCommand("SELECT [Order Date], [Ship Date] FROM Sheet1", connection);

                reader = getDates.ExecuteReader();
                while (reader.Read())
                {
                    dates.Add(reader[0].ToString());
                    dates.Add(reader[1].ToString());

                }
            }

            List<string> datesFormatted = new List<string>();

            foreach(string date in dates)
            {
                var dat = date.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                datesFormatted.Add(dat[0]);
            }

            lstBox.DataSource = datesFormatted;

            splitDates(datesFormatted[0]);
        }

        private void splitDates(string rawDate)
        {
            string[] arrayDate = rawDate.Split('/');

            int year = Convert.ToInt32(arrayDate[2]);
            int month = Convert.ToInt32(arrayDate[1]);
            int day = Convert.ToInt32(arrayDate[0]);

            DateTime myDate = new DateTime(year, month, day);
            Console.WriteLine("Day of Week: ", myDate.DayOfWeek);

            string dayOfWeek = myDate.DayOfWeek.ToString();
            int dayOfYear = myDate.DayOfYear;
            string monthName = myDate.ToString("MMMM");
            int weekNumber = dayOfYear / 7;
            Boolean weekend = false;
            if (dayOfWeek == "Saturday" || dayOfWeek == "Sunday")
            {
                weekend = true;
            }
        }

        private void InsertTimeDimension(string date)
        {
            string dataConnect = Properties.Settings.Default.DestinationDatabase_1_ConnectionString;

            using (SqlConnection connect = new SqlConnection(dataConnect))
            {
                connect.Open();

                SqlCommand command = new SqlCommand("SELECT id FROM Time WHERE date = @date", connect);
                command.Parameters.Add(new SqlParameter("date", date));

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Boolean exists = false;
                    if (reader.HasRows)
                    {
                        exists = true;
                    }
                }

                if (exists = false)
                {

                }


            }
        }

    }
}
