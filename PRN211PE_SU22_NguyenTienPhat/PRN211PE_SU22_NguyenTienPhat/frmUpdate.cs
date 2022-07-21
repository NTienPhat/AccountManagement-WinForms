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
using Microsoft.Extensions.Configuration;
using PRN211PE_SU22_NguyenTienPhatRepo.Models;
namespace PRN211PE_SU22_NguyenTienPhat
{
    public partial class frmUpdate : Form
    {
        BankAccount acc;
        public frmUpdate()
        {
            InitializeComponent();
        }

        public frmUpdate(string? id)
        {
            InitializeComponent();
            LoadID();
            string cs = GetConnectionString();
            using(var db = new BankAccountTypeContext(cs))
            {
                acc=db.BankAccounts.Find(id);
                txtAccountID.Text = acc.AccountId.ToString();
                txtAccountName.Text=acc.AccountName.ToString();
                txtBranch.Text=acc.BranchName.ToString();
                txtDate.Text=acc.OpenDate.ToString();
                cboID.SelectedValue=acc.TypeId.ToString();
            }
            
        }

        public void LoadID()
        {
            string cs = GetConnectionString();
            using (var db = new BankAccountTypeContext(cs))
            {
                cboID.DisplayMember = "TypeName";
                cboID.ValueMember = "TypeID";
                cboID.DataSource = db.AccountTypes.ToList();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(CheckValid())
            {
                string cs = GetConnectionString();
                using (var db = new BankAccountTypeContext(cs))
                {
                    var a = db.BankAccounts.Where(a => a.AccountId == acc.AccountId).FirstOrDefault();
                    a.AccountId = txtAccountID.Text;
                    a.BranchName = txtBranch.Text;
                    a.AccountName = txtAccountName.Text;
                    a.OpenDate = Convert.ToDateTime(txtDate.Text);
                    a.TypeId = cboID.SelectedValue.ToString();
                    db.SaveChanges();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Your input is invalid!");
            }
        }

        public bool CheckValid()
        {
            if (txtAccountID.Text == "" || txtAccountName.Text == "" || txtBranch.Text == "" || txtDate.Text == "")
                return false;
            return true;
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
