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

        private int GetDateId(string date)
        {
            string[] arrayDate = date.Split('/');

            int year = Convert.ToInt32(arrayDate[2]);
            int month = Convert.ToInt32(arrayDate[1]);
            int day = Convert.ToInt32(arrayDate[0]);


            string dataConnect = Properties.Settings.Default.DestinationDatabase_1_ConnectionString;

            using (SqlConnection connect = new SqlConnection(dataConnect))
            {
                connect.Open();

                SqlCommand command = new SqlCommand("SELECT id FROM Time WHERE date = @date", connect);
                command.Parameters.Add(new SqlParameter("date", date));

                using (SqlDataReader reader = command.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        return Convert.ToInt32(reader["id"]);
                    }
                }
                return 0;
            }
        }

        private void btnLookup_Click(object sender, EventArgs e)
        {
            List<string> dates = new List<string>();
            lstBox.DataSource = null;
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

            foreach(string date in datesFormatted)
            {
                splitDates(date);
            }

        }

        private void splitDates(string rawDate)
        {
            string[] arrayDate = rawDate.Split('/');

            int year = Convert.ToInt32(arrayDate[2]);
            int month = Convert.ToInt32(arrayDate[1]);
            int day = Convert.ToInt32(arrayDate[0]);

            DateTime myDate = new DateTime(year, month, day);

            string dayOfWeek = myDate.DayOfWeek.ToString();
            int dayOfYear = myDate.DayOfYear;
            string monthName = myDate.ToString("MMMM");
            int weekNumber = dayOfYear / 7;
            Boolean weekend = false;
            if (dayOfWeek == "Saturday" || dayOfWeek == "Sunday")
            {
                weekend = true;
            }

            string databaseDate = myDate.ToString("M/dd/yyyy");

            InsertTimeDimension(databaseDate, dayOfWeek, day, monthName, month, weekNumber, year, weekend, dayOfYear);
        }

        private void InsertTimeDimension(string date, string dayName, int dayNumber, string monthName, int monthNumber, int weekNumber, int year, bool weekend, int dayOfYear)
        {
            string dataConnect = Properties.Settings.Default.DestinationDatabase_1_ConnectionString;

            using (SqlConnection connect = new SqlConnection(dataConnect))
            {
                connect.Open();

                SqlCommand command = new SqlCommand("SELECT id FROM Time WHERE date = @date", connect);
                command.Parameters.Add(new SqlParameter("@date", date));

                Boolean exists = false;

                using (SqlDataReader reader = command.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        exists = true;
                    }
                }

                if (exists == false)
                {
                    SqlCommand insertCommand = new SqlCommand("INSERT INTO Time" +
                        "(dayName, dayNumber, monthName, monthNumber, weekNumber, year, weekend, date, dayOfYear) VALUES " +
                        "(@dayName, @dayNumber, @monthName, @monthNumber, @weekNumber, @year, @weekend, @date, @dayOfYear)", connect);

                    insertCommand.Parameters.Add(new SqlParameter("@dayName", dayName));
                    insertCommand.Parameters.Add(new SqlParameter("@dayNumber", dayNumber));
                    insertCommand.Parameters.Add(new SqlParameter("@monthName", monthName));
                    insertCommand.Parameters.Add(new SqlParameter("@monthNumber", monthNumber));
                    insertCommand.Parameters.Add(new SqlParameter("@weekNumber", weekNumber));
                    insertCommand.Parameters.Add(new SqlParameter("@year", year));
                    insertCommand.Parameters.Add(new SqlParameter("@weekend", weekend));
                    insertCommand.Parameters.Add(new SqlParameter("@date", date));
                    insertCommand.Parameters.Add(new SqlParameter("@dayOfYear", dayOfYear));
                    Console.WriteLine(dayNumber + "/" +  monthNumber + "/" + year);

                    int recordsAffected = insertCommand.ExecuteNonQuery();
                    Console.WriteLine("Records affected : " + recordsAffected);

                }


            }
        }

        private void btnGetDatesFromSource_Click(object sender, EventArgs e)
        {
            List<string> destinationDates = new List<string>();

            lstGetDatesFromSource.Items.Clear();

            string connectionDestination = Properties.Settings.Default.DestinationDatabase_1_ConnectionString;

            using (SqlConnection dataConnect = new SqlConnection(connectionDestination))
            {
                dataConnect.Open();

                SqlCommand command = new SqlCommand("SELECT id, dayName, dayNumber, monthName, monthNumber, weekNumber, year, weekend, date, dayOfYear FROM Time", dataConnect);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string id = reader["id"].ToString();
                            string dayName = reader["dayName"].ToString();
                            string dayNumber = reader["dayNumber"].ToString();
                            string monthName = reader["monthName"].ToString();
                            string monthNumber = reader["monthNumber"].ToString();
                            string weekNumber = reader["weekNumber"].ToString();
                            string year = reader["year"].ToString();
                            string weekend = reader["weekend"].ToString();
                            string date = reader["date"].ToString();
                            string dayOfYear = reader["dayOfYear"].ToString();

                            string text;

                            text = "Id = " + id + ", day name = " + dayName + ", day number = " + dayNumber + ", month name = " + monthName +
                                ", month number = " + monthNumber + ", week number = " + weekNumber + ", year = " + year + ", weekend = "
                                + weekend + ", date = " + date + ", day name = " + dayName;

                            destinationDates.Add(text);
                        }
                    }
                    else
                    {
                        destinationDates.Add("No data is present in Time dimension"); 
                    }
                }

            }

            lstGetDatesFromSource.DataSource = destinationDates;
        }
    }
}
