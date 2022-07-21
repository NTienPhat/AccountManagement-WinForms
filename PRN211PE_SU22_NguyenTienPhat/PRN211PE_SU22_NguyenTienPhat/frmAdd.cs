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
    public partial class frmAdd : Form
    {
        public frmAdd()
        {
            InitializeComponent();
            LoadID();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (CheckValid())
            {
                string cs = GetConnectionString();
                using (var db = new BankAccountTypeContext(cs))
                {
                    BankAccount acc = new BankAccount()
                    {
                        AccountId = txtAccID.Text,
                        AccountName = txtAccName.Text,
                        BranchName = txtBranch.Text,
                        OpenDate = Convert.ToDateTime(txtDate.Text),
                        TypeId = cboID.SelectedValue.ToString(),
                    };
                    db.BankAccounts.Add(acc);
                    db.SaveChanges();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Your input is invalid");
            }
            
              
        }

        public bool CheckValid()
        {
            if(txtAccID.Text =="" || txtAccName.Text =="" || txtBranch.Text =="" || txtDate.Text =="")
                return false;
            return true;
        }

        public void LoadID()
        {
            string cs = GetConnectionString();
            using(var db = new BankAccountTypeContext(cs))
            {
                cboID.DisplayMember = "TypeName";
                cboID.ValueMember = "TypeID";
                cboID.DataSource = db.AccountTypes.ToList();
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
