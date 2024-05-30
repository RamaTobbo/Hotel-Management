using Guna.UI2.WinForms.Suite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace HotelManagementSystem_project
{
    public partial class booking : Form
    {
        public booking()
        {
            InitializeComponent();


        }

        private void booking_Load(object sender, EventArgs e)
        {
            checkindate.Value = DateTime.Today;
            secondestepgrpbox.Enabled = false;
            thirdstepgroupbox.Enabled = false;
            for (int i = 1; i <= 5; i++)
            {
                bedcombobox.Items.Add(i);
            }
            bedcombobox.SelectedIndex = 0;
            string connectionstring = "Data Source=.;Initial Catalog=hotelmanagement;Integrated Security=True;";

            SqlConnection conn = new SqlConnection(connectionstring);
            string query = "SELECT RoomNB FROM Room;";
            SqlCommand cmd = new SqlCommand(query, conn);


            string query1 = $"SELECT Room.RoomNB AS NB, RoomType.TypeRoomName " +
                 $"FROM Room " +
                 $"JOIN RoomType ON Room.TypeRoomID = RoomType.TypeRoomID " +
                 $"WHERE Room.Availability='Available'";
            SqlCommand cmd1 = new SqlCommand(query1, conn);

            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                SqlDataReader reader = cmd1.ExecuteReader();
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



            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    roomnbcombobox.Items.Add(reader["RoomNB"].ToString());

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
            bedcombobox.Enabled = false;
            roomtypecombobox.SelectedIndex = 0;
            roomnbcombobox.SelectedIndex = 0;

        }

        private void allocateroombtn_Click(object sender, EventArgs e)
        {

            DateTime checkOutDate = checkindate.Value.AddDays(int.Parse(nbofbookeddays.Text));


            checkoutdate.Value = checkOutDate;

            string connectionString = "Data Source=.;Initial Catalog=hotelmanagement;Integrated Security=True;";

            SqlConnection conn = new SqlConnection(connectionString);
            
                try
                {
                    conn.Open();


                    string insertGuestQuery = $"INSERT INTO Guest (Name, DateOfBirth, Address, Phone, Nationality) " +
                                               $"VALUES ('{customernametxt.Text}', '{dateofbirth.Value.ToString("yyyy-MM-dd")}', " +
                                               $"'{Customeraddresstxt.Text}', '{customerphonenbtx.Text}', '{customernationalitytxt.Text}')";
                    SqlCommand insertGuestCmd = new SqlCommand(insertGuestQuery, conn);
                    insertGuestCmd.ExecuteNonQuery();


                    string roomIdQuery = $"SELECT RoomID FROM Room WHERE RoomNB='{roomnbcombobox.SelectedItem}'";
                    SqlCommand roomIdCmd = new SqlCommand(roomIdQuery, conn);
                    int roomId = (int)roomIdCmd.ExecuteScalar();


                    string guestIdQuery = $"SELECT GuestID FROM Guest WHERE Name='{customernametxt.Text}'";
                    SqlCommand guestIdCmd = new SqlCommand(guestIdQuery, conn);
                    int guestId = (int)guestIdCmd.ExecuteScalar();
                  int totalprice=int.Parse(pricetxt.Text)*int.Parse(nbofbookeddays.Text);

                    string insertBookingQuery = $"INSERT INTO Booking (CheckinDate, CheckoutDate, RoomID, GuestID,TotalPrice) " +
                                                $"VALUES ('{checkindate.Value.ToString("yyyy-MM-dd")}', '{checkOutDate.ToString("yyyy-MM-dd")}', " +
                                                $"'{roomId}', '{guestId}','{totalprice}')";
                    SqlCommand insertBookingCmd = new SqlCommand(insertBookingQuery, conn);
                    insertBookingCmd.ExecuteNonQuery();

                    MessageBox.Show("Room Allocated!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox10_Enter(object sender, EventArgs e)
        {


        }

        private void extraradiobtn_CheckedChanged(object sender, EventArgs e)
        {
            if (extraradiobtn.Checked)
            {
                bedcombobox.Enabled = true;
            }

        }

        private void confirmbtn_Click(object sender, EventArgs e)
        {
            thirdstepgroupbox.Enabled = true;
            int extrabedsPricePerBed = 20;
            int totalprice = 0;

            if (roomtypecombobox.SelectedIndex != -1)
            {
                int basePrice = 0;
                switch (roomtypecombobox.SelectedIndex)
                {
                    case 0:
                        basePrice = 100;
                        break;
                    case 1:
                        basePrice = 200;
                        break;
                    case 2:
                        basePrice = 500;
                        break;
                    case 3:
                        basePrice = 120;
                        break;
                    case 4:
                        basePrice = 200;
                        break;
                    case 5:
                        basePrice = 180;
                        break;
                }

                int numberOfExtraBeds = bedcombobox.SelectedIndex + 1;

                int totalPrice = basePrice + (numberOfExtraBeds * extrabedsPricePerBed);

                pricetxt.Text = totalPrice.ToString();
                bedcombobox.Enabled = false;
                pricetxt.Enabled = false;

                string connectionstring = "Data Source=.;Initial Catalog=hotelmanagement;Integrated Security=True;";
                SqlConnection conn = new SqlConnection(connectionstring);
                /////////////////////////////////////////////nnn
                string roomNumber = roomnbcombobox.SelectedItem.ToString();




                try
                {
                    conn.Open();


                    string queryUpdateAvailability = $"UPDATE Room SET Availability = 'not available' WHERE RoomNB = '{roomNumber}'";
                    SqlCommand cmdUpdateAvailability = new SqlCommand(queryUpdateAvailability, conn);

                    int rowsAffected = cmdUpdateAvailability.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Confirmed");
                    }
                    else
                    {
                        MessageBox.Show("No room found with the selected room number");
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

        }


        private void availabilitybtn_Click(object sender, EventArgs e)
        {

            /////////////////////////////////
            if (customernametxt.Text.Trim() == "" || customernationalitytxt.Text.Trim() == "" || customernationalitytxt.Text.Trim() == "" || customerphonenbtx.Text.Trim() == "" || nbofbookeddays.Text.Trim() == "")
            {
                MessageBox.Show("Please Enter all the required fields!");
                return;
            }

            string roomNumber = roomnbcombobox.SelectedItem.ToString();

            string connectionstring = "Data Source=.;Initial Catalog=hotelmanagement;Integrated Security=True;";
            SqlConnection conn = new SqlConnection(connectionstring);
            string queryCheckAvailability = $"SELECT Availability FROM Room WHERE RoomNB = '{roomNumber}'";
            SqlCommand cmdCheckAvailability = new SqlCommand(queryCheckAvailability, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = cmdCheckAvailability.ExecuteReader();
                if (reader.Read())
                {
                   
                      //  MessageBox.Show("This room is avaialbale press confirm to allocate this room");
                        secondestepgrpbox.Enabled = true;
                   
                   
                }
                reader.Close();
               


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }




            try
            {
                conn.Open();
              
                string query = $"SELECT MAX(CheckoutDate) AS EarliestCheckoutDate, Availability FROM Room " +
                $"JOIN Booking ON Room.RoomID = Booking.RoomID " +
                $"WHERE Room.RoomNB = {roomNumber} " +
                $"GROUP BY Availability";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string availability = reader["Availability"].ToString();
                    DateTime checkoutDate = (DateTime)reader["EarliestCheckoutDate"];

                    if (availability == "not available" && checkindate.Value < checkoutDate)
                    {
                        MessageBox.Show($"This room is already not available. It will be available again on {checkoutDate.ToString()}");
                    }
                    if (availability == "not available" && checkindate.Value >= checkoutDate)
                    {
                        MessageBox.Show("You can Book this room");
                    }
                 
                }

                reader.Close ();
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

        private void roomtypecombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (roomtypecombobox.SelectedItem == null)
            {
                return;
            }

            string selectedRoomType = roomtypecombobox.SelectedItem.ToString();
            string connectionstring = "Data Source=.;Initial Catalog=hotelmanagement;Integrated Security=True;";
            SqlConnection conn = new SqlConnection(connectionstring);

            string query = $"SELECT RoomNB FROM Room " +
                           $"JOIN RoomType ON Room.TypeRoomID = RoomType.TypeRoomID " +
                           $"WHERE RoomType.TypeRoomName = '{selectedRoomType}'";

            SqlCommand cmd = new SqlCommand(query, conn);
           

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                roomnbcombobox.Items.Clear(); 

                while (reader.Read())
                {
                    string roomNumber = reader["RoomNB"].ToString();
                    roomnbcombobox.Items.Add(roomNumber); 
                }

                if (roomnbcombobox.Items.Count > 0)
                {
                    roomnbcombobox.SelectedIndex = 0; 
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

        private void nbofbookeddays_TextChanged(object sender, EventArgs e)
        {
            if (int.Parse(nbofbookeddays.Text) <= 0)
            {
                MessageBox.Show("Please Enter A Valid Value");
            }
        }

        private void press(object sender, KeyPressEventArgs e)
        {
          
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' )
            {
                e.Handled = true;
            }
          
        }
    }
}
