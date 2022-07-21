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
    public partial class frmManagement : Form
    {
        public frmManagement()
        {
            InitializeComponent();
            LoadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(dgvManagement.SelectedRows.Count >0)
            {
                string cs = GetConnectionString();
                using (var db = new BankAccountTypeContext(cs))
                {
                    string id = dgvManagement.SelectedCells[0].OwningRow.Cells["AccountId"].Value.ToString();
                    BankAccount acc = db.BankAccounts.Find(id);
                    db.BankAccounts.Remove(acc);
                    db.SaveChanges();
                    MessageBox.Show("Delete Successful!");
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Please select Account to Delete");
            }
            
        }


        public void LoadData()
        {
            string cs = GetConnectionString();
            using (var db = new BankAccountTypeContext(cs))
            {
                var data = db.BankAccounts.ToList();
                dgvManagement.DataSource = data;
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

        private void button1_Click(object sender, EventArgs e)
        {
            string cs = GetConnectionString();
            using (var db = new BankAccountTypeContext(cs))
            {
                var account=db.BankAccounts.Where(acc => acc.BranchName.Equals(txtSearch.Text.Trim())).ToList();
                dgvManagement.DataSource=account;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAdd formAdd = new frmAdd();
            this.Hide();
            formAdd.ShowDialog();
            this.Show();
            LoadData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvManagement.SelectedRows.Count > 0)
            {
                    string id = dgvManagement.SelectedCells[0].OwningRow.Cells["AccountId"].Value.ToString();
                    frmUpdate formUpdate = new frmUpdate(id);
                    this.Hide();
                    formUpdate.ShowDialog();
                    this.Show();
                    LoadData();
                
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
