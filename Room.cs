using Guna.UI2.WinForms.Suite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Guna.UI2.Native.WinApi;

namespace HotelManagementSystem_project
{
    public partial class Room : Form

    {
   
        public Room()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void bedcombobox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Room_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;

            bednumbetxt.Enabled = false;
            roomtypecombobox.SelectedIndex = 0;
            string query = "SELECT RoomNB FROM Room;";
            string connectionstring = "Data Source=.;Initial Catalog=hotelmanagement;Integrated Security=True;";

            SqlConnection conn = new SqlConnection(connectionstring);
            SqlCommand cmd1 = new SqlCommand(query, conn);

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



        }

      

      

       

        private void confirmbtn_Click_1(object sender, EventArgs e)
        {
            string connectionstring = "Data Source=.;Initial Catalog=hotelmanagement;Integrated Security=True;";
            SqlConnection conn = new SqlConnection(connectionstring);
            string query1 = $"SELECT RoomNB From Room WHERE RoomNB='{roomnbtxt.Text}' ";
            SqlCommand cmd1 = new SqlCommand(query1, conn);
            if (roomnbtxt.Text.Trim() == "")
            {
                MessageBox.Show("Please Enter a Room Number");
            }

            try
            {
                conn.Open();
                SqlDataReader reader = cmd1.ExecuteReader();

                if (reader.Read())
                {
                    MessageBox.Show("This room already exists.Add another room number.You can Check The Available Room Numbers");

                    button1.Enabled = false;
                }
                else if(!reader.Read()&& roomnbtxt.Text.Trim()!="")
                {
                    reader.Close();
                    MessageBox.Show("You can add this room");
                    button1.Enabled = true;

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



            ////////////////////////////////////////////

          

            int basePrice = 0;
            switch (roomtypecombobox.SelectedIndex)
            {
                case 0:
                    basePrice = 100;
                   // bednumbetxt.Text = "1";
                    break;
                case 1:
                    basePrice = 200;
                  //  bednumbetxt.Text = "2";
                    break;
                case 2:
                    basePrice = 500;
                  //  bednumbetxt.Text = "3";
                    break;
                case 3:
                    basePrice = 120;
                  //  bednumbetxt.Text = "1";
                    break;
                case 4:
                    basePrice = 200;
                   // bednumbetxt.Text = "2";
                    break;
                case 5:
                    basePrice = 180;
                 //   bednumbetxt.Text = "2";
                    break;
            }

            pricetxt.Text = basePrice.ToString();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ListViewItem x = new ListViewItem(roomnbtxt.Text);

            x.SubItems.Add(roomtypecombobox.Text);
            x.SubItems.Add(bednumbetxt.Text);
            x.SubItems.Add(pricetxt.Text);

            listView1.Items.Add(x);
            string connectionstring = "Data Source=.;Initial Catalog=hotelmanagement;Integrated Security=True;";
            SqlConnection conn = new SqlConnection(connectionstring);
            string queryRoomTypeId = $"SELECT TypeRoomID FROM RoomType WHERE TypeRoomName ='{roomtypecombobox.SelectedItem}'";
            SqlCommand cmdRoomTypeId = new SqlCommand(queryRoomTypeId, conn);

            try
            {
                conn.Open();
                int typeRoomId = (int)cmdRoomTypeId.ExecuteScalar();

                string queryInsertRoom = $"INSERT INTO Room (RoomNB, Availability, HotelID, TypeRoomID) VALUES ('{roomnbtxt.Text}','{availabilitytxt.Text}', '{null}', '{typeRoomId}')";
                SqlCommand cmdInsertRoom = new SqlCommand(queryInsertRoom, conn);

                cmdInsertRoom.ExecuteNonQuery();
                MessageBox.Show("Room Added!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("error 404" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            ////////////////////////////////////////////
            ///





            roomnbtxt.Clear();
            pricetxt.Clear();


            roomtypecombobox.SelectedIndex = 0;
            button1.Enabled = false;
        }

        private void roomtypecombobox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            switch (roomtypecombobox.SelectedIndex)
            {
                case 0:

                    bednumbetxt.Text = "1";
                    break;
                case 1:

                    bednumbetxt.Text = "2";
                    break;
                case 2:

                    bednumbetxt.Text = "3";
                    break;
                case 3:

                    bednumbetxt.Text = "2";
                    break;
                case 4:

                    bednumbetxt.Text = "6";
                    break;
                case 5:

                    bednumbetxt.Text = "5";
                    break;
            }
        }
    }
}
