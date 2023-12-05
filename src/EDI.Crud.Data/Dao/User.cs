using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDI.Crud.Data.Dao
{
    public class User
    {
        public int UserId { get; set; }
        public string NamaLengkap { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public char Status { get; set; }

        public void Update(User d)
        {
            NamaLengkap = d.NamaLengkap;
            UserName = d.UserName;
            Password = d.Password;
            Status = d.Status;
        }
    }
}
