using Npgsql;

namespace CarService.DAL.Interface
{
    public interface IDbConnectionFactory
    {
        NpgsqlConnection CreateConnection();
    }
}
