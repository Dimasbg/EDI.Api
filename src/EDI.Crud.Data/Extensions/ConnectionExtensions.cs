using EDI.Crud.Data.Database.EDIDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDI.Crud.Data.Extensions
{
    public static class ConnectionExtensions
    {
        public static void ConfigureDatabaseConnection(this IServiceCollection services, IConfiguration configuration)
        {
            string connection = configuration.GetValue<string>("ConnectionStrings:EDIDb");

            services.AddDbContext<EDIDbContext>(options
                => options.UseNpgsql(connection));
        }
    }
}
