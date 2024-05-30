using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagementSystem_project
{
    public partial class Customerdetails : Form
    {
        public Customerdetails()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Customerdetails_Load(object sender, EventArgs e)
        {
            string connectionstring = "Data Source=.;Initial Catalog=hotelmanagement;Integrated Security=True;";

            SqlConnection conn = new SqlConnection(connectionstring);

            string query = "SELECT Name FROM Guest";
            SqlCommand cmd = new SqlCommand(query, conn);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    searchbycombobox.Items.Add(reader["Name"].ToString());
                }




                if (searchbycombobox.SelectedItem != null)
                {
                    string query1 = $"SELECT Name, DateOfBirth, Address, Phone, Nationality FROM Guest WHERE Name='{searchbycombobox.SelectedItem.ToString()}'";
                    SqlCommand cmd1 = new SqlCommand(query, conn);


                    SqlDataReader reader1 = cmd1.ExecuteReader();

                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Display_Click(object sender, EventArgs e)
        {
            if (searchbycombobox.SelectedItem == null)
            {
                MessageBox.Show("Please select a name from the combo box.");
                return;
            }
            string connectionstring = "Data Source=.;Initial Catalog=hotelmanagement;Integrated Security=True;";

            SqlConnection conn = new SqlConnection(connectionstring);
            string query = $"SELECT G.Name, G.Phone, G.Nationality, R.RoomNB,B.CheckinDate,B.CheckoutDate " +
         $"FROM Guest G " +
         $"JOIN Booking B ON G.GuestID = B.GuestID " +
         $"JOIN Room R ON B.RoomID = R.RoomID WHERE G.Name='{searchbycombobox.SelectedItem}'";
           
            SqlCommand cmd = new SqlCommand(query, conn);

            DataTable dt = new DataTable();
            
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    dataGridView1.DataSource = null;
                    MessageBox.Show("No data found for the selected name.");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void displayallbtn_Click(object sender, EventArgs e)
        {
            string connectionstring = "Data Source=.;Initial Catalog=hotelmanagement;Integrated Security=True;";

            SqlConnection conn = new SqlConnection(connectionstring);

            string query = $"SELECT G.Name, G.Phone, G.Nationality, R.RoomNB,B.CheckinDate,B.CheckoutDate " +
             $"FROM Guest G " +
             $"JOIN Booking B ON G.GuestID = B.GuestID " +
             $"JOIN Room R ON B.RoomID = R.RoomID";

            SqlCommand cmd = new SqlCommand(query, conn);

                DataTable dt = new DataTable();

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            finally
            {
                conn.Close();
            }
            
        }

    }
}

    

