using EDI.Crud.Data.Dao;
using EDI.Crud.Domain.UserRepo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EDI.Crud.Domain.UserRepo
{
    public interface IUserRepo
    {
        public Task<User> Create(User d, CancellationToken c);
        public Task<User> Read(int id, CancellationToken c);
        public Task<User> Update(User d, CancellationToken c);
        public Task Delete(int id, CancellationToken c);
        public Task<UserDTResponseDomainModel> DataTablePaging(UserDTParamDomainModel p, CancellationToken cancellationToken);
    }
}
