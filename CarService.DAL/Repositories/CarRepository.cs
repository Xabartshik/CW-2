using CarService.DAL.Infrastructure;
using CarService.DAL.Interface;
using CarService.Domain;
using CarService.Domain.Interfaces;
using Npgsql;
using System.Data;

namespace CarService.DAL.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public CarRepository(IDbConnectionFactory factory) {
            _connectionFactory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        
        public async Task<Car?> GetByIdAsync(int id)
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
                    OwnerName = reader.GetString("ownername"),
                };
            }
            return null;
        }

        public async Task<IEnumerable<Car>> GetAllAsync()
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

        public async Task AddAsync(Car car)
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

        public async Task<bool> RemoveAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync();

            using var command = new NpgsqlCommand("Delete from cars where id = @id", connection);
            command.Parameters.AddWithValue("@id", id);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Car car)
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
    }
}