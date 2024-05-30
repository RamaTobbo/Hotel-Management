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

namespace HotelManagementSystem_project
{
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
        }

       

        private void addempbtn_Click(object sender, EventArgs e)
        {
            
            string connectionstring = "Data Source=.;Initial Catalog=hotelmanagement;Integrated Security=True;";
            SqlConnection conn = new SqlConnection(connectionstring);
            string query = $"INSERT INTO Employee (FirstName, LastName, Position, Salary, DateOfBirth, Phone,HotelName,HireDate) VALUES ('{empfirstnametxt.Text}', '{emplastnametxt.Text}','{positioncombo.SelectedItem}','{salarytxt.Text}', '{dateofbirth.Value.ToString("yyyy-MM-dd")}', '{phonenbtxt.Text}', '{hotelnamecombo.SelectedItem}', '{hiredate.Value.ToString("yyyy-MM-dd")}')";

           
            SqlCommand cmd = new SqlCommand(query, conn);
          




          
            try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Added!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error 404: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }


                emplastnametxt.Clear();
                empfirstnametxt.Clear();
                phonenbtxt.Clear();
                positioncombo.SelectedIndex = 0;


            }

        private void positioncombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int basePrice = 0;

            if (positioncombo.SelectedIndex != -1)
            {

                switch (positioncombo.SelectedIndex)
                {
                    case 0:
                        basePrice = 5000;
                        break;
                    case 1:
                        basePrice = 4500;
                        break;
                    case 2:
                        basePrice = 1500;
                        break;
                    case 3:
                        basePrice = 1400;
                        break;
                    case 4:
                        basePrice = 2000;
                        break;
                    case 5:
                        basePrice = 950;
                        break;
                }
                salarytxt.Text = basePrice.ToString();

            }
        }

        private void Employee_Load(object sender, EventArgs e)
        {
            string connectionstring = "Data Source=.;Initial Catalog=hotelmanagement;Integrated Security=True;";
            SqlConnection conn = new SqlConnection(connectionstring);

            string query = "SELECT FirstName,LastName FROM Employee;";
            SqlCommand cmd = new SqlCommand(query, conn);




            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    empcombo.Items.Add(reader["FirstName"].ToString()+" "+ reader["LastName"].ToString());

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




            string query3 = "SELECT HotelName FROM Hotel;";
            string query4 = "SELECT FirstName,LastName FROM Employee;";
            SqlCommand cmd2 = new SqlCommand(query4, conn);
            SqlCommand cmd3 = new SqlCommand(query3, conn);
            addempbtn.Enabled = false;

         


            try
            {
                conn.Open();
                SqlDataReader reader = cmd3.ExecuteReader();
                while (reader.Read())
                {

                    hotelnamecombo.Items.Add(reader["HotelName"].ToString());

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

        private void Confirmbtn_Click(object sender, EventArgs e)
        {
            if(empfirstnametxt.Text.Trim()=="" || emplastnametxt.Text.Trim() == "" || hotelnamecombo.SelectedIndex == -1 || phonenbtxt.Text.Trim() == "" || positioncombo.SelectedIndex == -1)
            {
                MessageBox.Show("Fill the required Inforamtion");
                return;
            }
            else
            {
                addempbtn.Enabled = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void empcombo_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void Display_Click(object sender, EventArgs e)
        {

        }

        private void displaybtn_Click(object sender, EventArgs e)
        {
       
          
        }

        private void viewemployeesbtn_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
            string connectionstring = "Data Source=.;Initial Catalog=hotelmanagement;Integrated Security=True;";
            SqlConnection conn = new SqlConnection(connectionstring);
            string query1 = $"SELECT * From Employee  ";
            SqlCommand cmd1 = new SqlCommand(query1, conn);

            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                SqlDataReader reader = cmd1.ExecuteReader();

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


        private void deleteempbtn_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;

        }

        private void empcombo_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            string selectedName = empcombo.SelectedItem.ToString();
            string[] nameParts = selectedName.Split(' ');

           

            string firstName = nameParts[0];
           
            string lastName = nameParts[1];
           
              string connectionstring = "Data Source=.;Initial Catalog=hotelmanagement;Integrated Security=True;";
              SqlConnection conn = new SqlConnection(connectionstring);
            string query = $"Delete FROM Employee WHERE FirstName='{firstName}' AND LastName='{lastName}' ";

              SqlCommand cmd = new SqlCommand(query, conn);






              try
              {
                  conn.Open();
                  cmd.ExecuteNonQuery();
                  MessageBox.Show("Employee Deleted!");
              }
              catch (Exception ex)
              {
                  MessageBox.Show("error 404: " + ex.Message);
              }
              finally
              {
                  conn.Close();
              }


              emplastnametxt.Clear();
              empfirstnametxt.Clear();
              phonenbtxt.Clear();
              positioncombo.SelectedIndex = 0;
        }
    }
}
