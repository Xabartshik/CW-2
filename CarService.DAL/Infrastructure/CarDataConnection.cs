using CarService.DAL.Interface;
using CarService.DAL.Model;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider.PostgreSQL;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CarService.DAL.Infrastructure
{
    public class CarDataConnection : DataConnection, ICarDataConnection
    {
        public CarDataConnection(IConfiguration configuration)
    : base(PostgreSQLTools.GetDataProvider(PostgreSQLVersion.v95),
          configuration.GetConnectionString("DefaultConnection"))
        {
        }

        public ITable<CarModel> Cars => this.GetTable<CarModel>();
        public ITable<ServiceModel> Services => this.GetTable<ServiceModel>();
        public ITable<ServiceRecordModel> ServiceRecords => this.GetTable<ServiceRecordModel>();

        public async Task<int> DeleteAsync<T>(T entity) where T : class
        {
            return await DeleteAsync(entity);
        }

        public async Task<int> InsertAsync<T>(T entity) where T : class
        {
            return await InsertAsync(entity);
        }

        public async Task<int> UpdateAsync<T>(T entity) where T : class
        {
            return await UpdateAsync(entity);
        }
    }
}
