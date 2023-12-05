using EDI.Crud.Domain.UserRepo;
using EDI.Crud.Domain.UserRepo.Impl;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDI.Crud.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void InjectDomainLayer(this IServiceCollection services)
        {
            services.AddScoped<IUserRepo, UserRepoImpl>();
        }
    }
}
