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
    public partial class Checkout : Form
    {
        public Checkout()
        {
            InitializeComponent();
           
        }

        private void Checkout_Load(object sender, EventArgs e)
        {
            checkoutdate.Value = DateTime.Today;
            string connectionstring = "Data Source=.;Initial Catalog=hotelmanagement;Integrated Security=True;";
            SqlConnection conn = new SqlConnection(connectionstring);

            string query = "SELECT Name FROM Guest;";
            SqlCommand cmd = new SqlCommand(query, conn);




            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    guestcombo.Items.Add(reader["Name"].ToString());

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

        private void checkoutbtn_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            if (guestcombo.SelectedItem == null)
            {
                MessageBox.Show("Please select a guest from the combo box.");
                return;
            }

            string selectedGuest = guestcombo.SelectedItem.ToString();
            string connectionString = "Data Source=.;Initial Catalog=hotelmanagement;Integrated Security=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Display guest details
                    string guestQuery = "SELECT * FROM Guest WHERE Name = @Name";
                    using (SqlCommand guestCmd = new SqlCommand(guestQuery, conn))
                    {
                        guestCmd.Parameters.AddWithValue("@Name", selectedGuest);
                        using (SqlDataReader guestReader = guestCmd.ExecuteReader())
                        {
                            while (guestReader.Read())
                            {
                                ListViewItem item = new ListViewItem(guestReader["GuestID"].ToString());
                                item.SubItems.Add(guestReader["Name"].ToString());
                                item.SubItems.Add(guestReader["DateOfBirth"].ToString());
                                item.SubItems.Add(guestReader["Address"].ToString());
                                item.SubItems.Add(guestReader["Phone"].ToString());
                                item.SubItems.Add(guestReader["Nationality"].ToString());
                                listView1.Items.Add(item);
                            }
                        }
                    }


                    string roomNumber = null;
                    string bookingQuery = @"
            SELECT Room.RoomNB
            FROM Booking
            INNER JOIN Guest ON Booking.GuestID = Guest.GuestID
            INNER JOIN Room ON Booking.RoomID = Room.RoomID
            WHERE Guest.Name = @Name";
                    using (SqlCommand bookingCmd = new SqlCommand(bookingQuery, conn))
                    {
                        bookingCmd.Parameters.AddWithValue("@Name", selectedGuest);
                        using (SqlDataReader bookingReader = bookingCmd.ExecuteReader())
                        {
                            if (bookingReader.Read())
                            {
                                roomNumber = bookingReader["RoomNB"].ToString();
                                MessageBox.Show($"Guest '{selectedGuest}' has booked room number '{roomNumber}'.");
                            }
                            else
                            {
                                MessageBox.Show($"Guest '{selectedGuest}' has not booked any room.");
                                return;
                            }
                        }
                    }


                    if (!string.IsNullOrEmpty(roomNumber))
                    {
                        string updateQuery = $"UPDATE Room SET Availability = 'Available' WHERE RoomNB={roomNumber}";
                        SqlCommand updateCmd = new SqlCommand(updateQuery, conn);


                        int rowsAffected = updateCmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Room number '{roomNumber}' availability has been updated to 'Available'.");
                        }
                        else
                        {
                            MessageBox.Show("Failed to update the room availability.");
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}