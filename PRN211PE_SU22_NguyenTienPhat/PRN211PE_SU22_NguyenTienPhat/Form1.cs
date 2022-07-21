using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PRN211PE_SU22_NguyenTienPhatRepo.Models;
namespace PRN211PE_SU22_NguyenTienPhat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtUserID.Text.Equals("")|| txtPassword.Text.Equals(""))
            {
                MessageBox.Show("Please input User ID and Password!");
            }
            else
            {
                string cs = GetConnectionString();
                using(var db = new BankAccountTypeContext(cs))
                {
                    var user = db.Users.Where(a => a.UserId == txtUserID.Text && a.Password == txtPassword.Text).FirstOrDefault();
                    if(user != null)
                    {
                        if(user.UserRole==1)
                        {
                            frmManagement formManagement = new frmManagement();
                            this.Hide();
                            formManagement.ShowDialog();
                            this.Show();
                        }
                        else
                        {
                            MessageBox.Show("You are not allowed to access this function!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid UserID or Password");
                    }
                }
            }
        }

        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            var strConn = config["ConnectionStrings:BankAccountTypeDB"];
            return strConn;
        }

    }
}
