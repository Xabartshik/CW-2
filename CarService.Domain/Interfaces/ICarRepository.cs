namespace CarService.Domain.Interfaces
{
    public interface ICarRepository
    {
        Task<Car?> GetByIdAsync(int id);
        Task<IEnumerable<Car>> GetAllAsync();
        Task AddAsync(Car car);
        Task<bool> RemoveAsync(int id);
        Task<bool> UpdateAsync(Car car);
    }
}