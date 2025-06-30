using CarService.DAL.Infrastructure;
using CarService.DAL.Interface;
using CarService.Domain;
using CarService.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.Data;

namespace CarService.DAL.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly ILogger<CarRepository> _logger;
        public CarRepository(IDbConnectionFactory factory, ILogger<CarRepository> logger) {
            _connectionFactory = factory ?? throw new ArgumentNullException(nameof(factory));
            _logger = logger;
        }

        public async Task<Car?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Поиск машины в БД по ID: {id}", id);
            try
            {
                using var connection = _connectionFactory.CreateConnection();
                await connection.OpenAsync();

                using var command = new NpgsqlCommand("Select brand, model, year, ownername, id from cars where id = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    return new Car
                    {
                        Id = reader.GetInt32("id"),
                        Brand = reader.GetString("brand"),
                        Model = reader.GetString("model"),
                        Year = reader.GetInt32("year"),
                        OwnerName = reader["ownername"] is null ? reader.GetString("ownername") : null
                    };
                }
                return null;
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Ошибка при получении машины по ID");
                throw;
            }

        }

        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            _logger.LogInformation("Поиск всех машин в БД");
            try
            {
                var cars = new List<Car>();
                using var connection = _connectionFactory.CreateConnection();
                await connection.OpenAsync();

                using var command = new NpgsqlCommand("Select brand, model, year, ownername, id from cars", connection);

                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    cars.Add(new Car()
                    {
                        Id = reader.GetInt32("id"),
                        Brand = reader.GetString("brand"),
                        Model = reader.GetString("model"),
                        Year = reader.GetInt32("year"),
                        OwnerName = reader["ownername"] is null ? reader.GetString("ownername") : null
                    });
                }
                return cars;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении всех машин");
                throw;
            }
        }

        public async Task AddAsync(Car car)
        {
            _logger.LogInformation("Добавление машины в БД");
            try
            {
                if (car is null)
                {
                    throw new ArgumentNullException(nameof(car));
                }


                using var connection = _connectionFactory.CreateConnection();
                await connection.OpenAsync();
                using var command = new NpgsqlCommand("Insert into cars (brand, model, year, ownername) values (@brand, @model, @year, @ownername)", connection);
                command.Parameters.AddWithValue("@brand", car.Brand);
                command.Parameters.AddWithValue("@model", car.Model);
                command.Parameters.AddWithValue("@year", car.Year);
                command.Parameters.AddWithValue("@ownername", car.OwnerName);

                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при добавлении машины в БД");
                throw;
            }
            
        }

        public async Task<bool> RemoveAsync(int id)
        {
            _logger.LogInformation("Удаление машины из БД");
            try
            {
                using var connection = _connectionFactory.CreateConnection();
                await connection.OpenAsync();

                using var command = new NpgsqlCommand("Delete from cars where id = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                return await command.ExecuteNonQueryAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при удалении машины из БД");
                throw;
            }

        }

        public async Task<bool> UpdateAsync(Car car)
        {
            _logger.LogInformation("Обновление машины в БД");
            try
            {
                using var connection = _connectionFactory.CreateConnection();
                await connection.OpenAsync();

                using var command = new NpgsqlCommand("Update cars set brand = @brand, model = @model, year = @year, ownername = @ownername where id = @id", connection);

                command.Parameters.AddWithValue("@id", car.Id);
                command.Parameters.AddWithValue("@brand", car.Brand);
                command.Parameters.AddWithValue("@model", car.Model);
                command.Parameters.AddWithValue("@year", car.Year);
                command.Parameters.AddWithValue("@ownername", car.OwnerName);

                return await command.ExecuteNonQueryAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении машины в БД");
                throw;
            }

        }
    }
}