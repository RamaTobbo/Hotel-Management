using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagementSystem_project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

       

        private void passwordtxt_Enter(object sender, EventArgs e)
        {
            if (passwordtxt.Text == "Password")
            {
                passwordtxt.Text = "";
                passwordtxt.PasswordChar = '*';
                passwordtxt.ForeColor = Color.Black;
            }
        }

        private void usernametxt_Leave(object sender, EventArgs e)
        {
            if(usernametxt.Text == "")
            {
                usernametxt.Text = "Username";
                passwordtxt.Text = "";
                usernametxt.ForeColor= Color.Silver;
            }
        }

        private void passwordtxt_Leave(object sender, EventArgs e)
        {

            if (passwordtxt.Text == "")
            {
               
                passwordtxt.Text = "Password";
                passwordtxt.ForeColor = Color.Silver;
            }
        }

        private void usernametxt_Enter(object sender, EventArgs e)
        {
            if (usernametxt.Text == "Username")
            {
                usernametxt.Text = "";
                usernametxt.ForeColor = Color.Black;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            usernametxt.Padding=new Padding(15,0,15,5);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

       

        private void button1_Click_1(object sender, EventArgs e)
        {
           
           
          if (passwordtxt.Text == "Password" && usernametxt.Text == "Username")
            {
                MessageBox.Show("Please Enter Your Password and Username");
            }
            else if (usernametxt.Text == "Username")
            {
                MessageBox.Show("Please Enter Your Username");
            }
            else if (passwordtxt.Text == "Password")
            {
                MessageBox.Show("Please Enter Your Password");
            }
            else if (usernametxt.Text == "Admin" && passwordtxt.Text == "123")
            {
               
                Dashboard dashboard = new Dashboard();
                dashboard.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invalid Password or Username");
                usernametxt.Text = "";
                passwordtxt.Text = "";
            }
          
        }

        private void passwordtxt_TextChanged(object sender, EventArgs e)
        {
             
        }

        private void clickonpassword(object sender, EventArgs e)
        {
           

        }

        private void presspass(object sender, KeyPressEventArgs e)
        {
            if (usernametxt.Text == "Username")
            {
               
                    e.Handled = true;

                

            }
        }
    }
}
