using EDI.Crud.Data.Dao;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDI.Crud.Data.Database.EDIDb
{
    public class EDIDbContext : DbContext
    {
        public EDIDbContext(DbContextOptions<EDIDbContext> options) : base(options)
        {

        }
        public DbSet<User> tbl_user { get; set; }
    }
}
