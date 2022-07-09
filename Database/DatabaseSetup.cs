namespace Avaliacao3BimLp3.Database;

using Microsoft.Data.Sqlite;

class DatabaseSetup
{
    private readonly DatabaseConfig _databaseConfig;

    public DatabaseSetup(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
        CreateStudentTable();
    }

    public void CreateStudentTable()
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Students(
                registration varchar(100) not null primary key,
                name varchar(250) not null,
                city varchar(250) not null, 
                former bit not null
            );
        ";

        command.ExecuteNonQuery();
        connection.Close();
    }
}

