using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN211PE_SU22_NguyenTienPhatRepo.Models
{
    public partial class BankAccountTypeContext : DbContext
    {
        public BankAccountTypeContext(string con)
        {
            this.Database.SetConnectionString(con);
        }
    }
}
