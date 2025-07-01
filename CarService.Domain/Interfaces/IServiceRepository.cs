using CarService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceService.Domain.Interfaces
{
    public interface IServiceRepository
    {
        Task<Service?> GetByIdAsync(int id);
        Task<IEnumerable<Service>> GetAllAsync();
        Task AddAsync(Service service);
        Task<bool> RemoveAsync(int id);
        Task<bool> UpdateAsync(Service service);
    }
}
