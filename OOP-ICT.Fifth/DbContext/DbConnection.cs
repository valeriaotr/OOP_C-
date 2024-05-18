using Npgsql;
using OOP_ICT.Fifth.LastTurnUI;

namespace OOP_ICT.Fifth.DbContext;

public class DbConnection
{
    private NpgsqlConnection _connection;

    public DbConnection()
    {
        _connection = new NpgsqlConnection(JSONGetter.GetConnectionString());
    }

    public NpgsqlConnection GetConnection()
    {
        return _connection;
    }
}
