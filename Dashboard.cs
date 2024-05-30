using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagementSystem_project
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void addroombtn_Click(object sender, EventArgs e)
        {
         Room room = new Room();
            room.ShowDialog();
        }

        private void customerregbtn_Click(object sender, EventArgs e)
        {
            booking b = new booking();
            b.ShowDialog();
        }

        private void checkoutbtn_Click(object sender, EventArgs e)
        {
            Checkout b = new Checkout();
            b.ShowDialog();
        }

        private void Customerdetailsbtn_Click(object sender, EventArgs e)
        {
            Customerdetails customerdetails = new Customerdetails();
            customerdetails.ShowDialog();
        }

        private void employeebtn_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee();
            employee.ShowDialog();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            string connectionstring = "Data Source=.;Initial Catalog=hotelmanagement;Integrated Security=True;";
            string query = @"UPDATE Room
                     SET Availability = 'Available'
                     FROM Room
                     JOIN Booking ON Room.RoomID = Booking.RoomID
                     WHERE GETDATE() >= Booking.CheckoutDate;";

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                try
                {
                    conn.Open(); 

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
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
}
