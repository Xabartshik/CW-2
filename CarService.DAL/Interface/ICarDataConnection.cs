using CarService.DAL.Model;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.DAL.Interface
{
    public interface ICarDataConnection
    {
        ITable<CarModel> Cars { get; }
        public ITable<ServiceModel> Services { get;  }
        public ITable<ServiceRecordModel> ServiceRecords { get; }
        Task<int> InsertAsync<T>(T entity) where T : class;
        Task<int> UpdateAsync<T>(T entity) where T : class;
        Task<int> DeleteAsync<T>(T entity) where T : class;


    }
}
