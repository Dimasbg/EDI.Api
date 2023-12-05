using EDI.Crud.Data.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDI.Crud.Domain.UserRepo.Models
{
    public class UserDTResponseDomainModel
    {

        public int Draw { get; set; }

        public int RecordsFiltered { get; set; }

        public int RecordsTotal { get; set; }

        public IEnumerable<User> Data { get; set; }
    }
}
