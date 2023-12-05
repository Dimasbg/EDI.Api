using EDI.Crud.Data.Dao;
using EDI.Crud.Data.Database.EDIDb;
using EDI.Crud.Domain.UserRepo.Models;
using EDI.Crud.Utilities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EDI.Crud.Domain.UserRepo.Impl
{
    public class UserRepoImpl : IUserRepo
    {
        private readonly EDIDbContext _db;

        public UserRepoImpl(EDIDbContext db)
        {
            _db = db;
        }

        public async Task<User> Create(User d, CancellationToken c)
        {

            var exist = await Read(d.UserId, c);
            if (exist != null)
                throw new DomainLayerException($"User with id '{d.UserId}' already exist!");

            exist = await _db.tbl_user.FirstOrDefaultAsync(b => b.UserName.ToUpper() == d.UserName.ToUpper(), c);
            if (exist != null)
                throw new DomainLayerException($"User with username '{d.UserName}' already exist!");

            await _db.tbl_user.AddAsync(d, c);
            await _db.SaveChangesAsync(c);
            return d;
        }

        public async Task<UserDTResponseDomainModel> DataTablePaging(UserDTParamDomainModel p, CancellationToken cancellationToken)
        {
            if (p.PageSize == 0)
                return new UserDTResponseDomainModel
                {
                    Data = new List<User>(),
                    Draw = p.Draw,
                    RecordsFiltered = 0,
                    RecordsTotal = await _db.tbl_user.CountAsync(cancellationToken)
                };

            IQueryable<User> raw = _db.tbl_user
                .Select(b => new User
                {
                    NamaLengkap = b.NamaLengkap,
                    Password = b.Password,
                    Status = b.Status,
                    UserId = b.UserId,
                    UserName = b.UserName
                });

            IEnumerable<User> sorted;

            int totalCount = await raw.CountAsync(cancellationToken);
            p.PageSize = p.PageSize < 0 ? totalCount : p.PageSize;

            if (string.IsNullOrWhiteSpace(p.ColumnIndex))
            {
                if (p.SortDirection == "desc")
                    sorted = await raw.OrderByDescending(b => b.NamaLengkap).Skip(p.Skip).Take(p.PageSize).ToListAsync(cancellationToken);
                else
                    sorted = await raw.OrderBy(b => b.NamaLengkap).Skip(p.Skip).Take(p.PageSize).ToListAsync(cancellationToken);
            }
            else
                sorted = await Sort(raw, p.SortDirection, p.ColumnIndex).Skip(p.Skip).Take(p.PageSize).ToListAsync(cancellationToken);

            return new UserDTResponseDomainModel
            {
                Data = sorted,
                Draw = p.Draw,
                RecordsFiltered = totalCount,
                RecordsTotal = await raw.CountAsync(cancellationToken)
            };
        }

        private IQueryable<User> Sort(IQueryable<User> raw, string sortDirection, string orderBy)
        {
            switch (orderBy.ToUpper())
            {
                case "NAMALENGKAP":
                    if (sortDirection == "desc")
                        raw = raw.OrderByDescending(b => b.NamaLengkap);
                    else
                        raw = raw.OrderBy(b => b.NamaLengkap);
                    break;
                case "PASSWORD":
                    if (sortDirection == "desc")
                        raw = raw.OrderByDescending(b => b.Password);
                    else
                        raw = raw.OrderBy(b => b.Password);
                    break;
                case "STATUS":
                    if (sortDirection == "desc")
                        raw = raw.OrderByDescending(b => b.Status);
                    else
                        raw = raw.OrderBy(b => b.Status);
                    break;
                case "USERID":
                    if (sortDirection == "desc")
                        raw = raw.OrderByDescending(b => b.UserId);
                    else
                        raw = raw.OrderBy(b => b.UserId);
                    break;
                case "USERNAME":
                    if (sortDirection == "desc")
                        raw = raw.OrderByDescending(b => b.UserName);
                    else
                        raw = raw.OrderBy(b => b.UserName);
                    break;
            }

            return raw;
        }

        public async Task Delete(int id, CancellationToken c)
        {
            User data = await Read(id, c) ??
                throw new DomainLayerException($"User with id '{id}' not found!");
            _db.tbl_user.Remove(data);
            await _db.SaveChangesAsync(c);
        }

        public async Task<User> Read(int id, CancellationToken c)
            => await _db.tbl_user.FirstOrDefaultAsync(b => b.UserId == id, c);

        public async Task<User> Update(User d, CancellationToken c)
        {
            User result = await Read(d.UserId, c);

            var exist = await _db.tbl_user.FirstOrDefaultAsync(b => b.UserId != d.UserId && b.UserName.ToUpper() == d.UserName.ToUpper(), c);

            if (exist != null)
                throw new DomainLayerException("User with username '{d.UserName}' already exist!");

            result.Update(d);

            _db.tbl_user.Update(result);

            await _db.SaveChangesAsync(c);

            return result;
        }
    }
}
