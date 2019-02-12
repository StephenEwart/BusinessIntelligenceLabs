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

            lstBox.DataSource = dates;


        }
    }
}
