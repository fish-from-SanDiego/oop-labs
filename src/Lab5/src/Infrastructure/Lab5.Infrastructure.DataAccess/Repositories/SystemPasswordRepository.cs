using Itmo.Dev.Platform.Postgres.Models;
using Lab5.Application.Abstractions.Entities;
using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Abstractions.ResultTypes;
using Lab5.Infrastructure.DataAccess.Entities;
using Lab5.Infrastructure.DataAccess.Exceptions;
using Npgsql;

namespace Lab5.Infrastructure.DataAccess.Repositories;

public class SystemPasswordRepository : ISystemPasswordRepository
{
    private readonly NpgsqlDataSourceBuilder _dataSourceBuilder;

    public SystemPasswordRepository(PostgresConnectionConfiguration configuration)
    {
        RepositoryException.ThrowIfNull(configuration);

        _dataSourceBuilder = new NpgsqlDataSourceBuilder(configuration.ToConnectionString());
    }

    public IFetchedCollection<string> GetAllPasswords()
    {
        const string sql = """
                           select password
                           from system_passwords
                           """;

        using NpgsqlDataSource dataSource = _dataSourceBuilder.Build();
        using NpgsqlCommand command = dataSource.CreateCommand(sql);
        var resultCollection = Enumerable.Empty<string>().ToHashSet();

        NpgsqlDataReader reader;

        try
        {
            reader = command.ExecuteReader();
        }
        catch (NpgsqlException)
        {
            return new FetchedCollection<string>(
                resultCollection,
                new GetError("Failed to get the passwords (failed to connect database)"));
        }

        while (reader.Read())
        {
            resultCollection.Add(reader.GetString(0));
        }

        reader.Dispose();
        return new FetchedCollection<string>(resultCollection, new GetSuccess());
    }

    public RegisterResult<string> RegisterPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            return new RegisterError<string>("Password should be non-empty");
        }

        const string selectSql = """
                                 select password
                                 from system_passwords
                                 where password = :password
                                 """;
        const string insertSql = """
                                 insert into system_passwords
                                 (password) values (:password)
                                 """;

        using NpgsqlDataSource dataSource = _dataSourceBuilder.Build();

        using NpgsqlCommand selectCommand = dataSource.CreateCommand(selectSql);
        selectCommand.Parameters.Add(new NpgsqlParameter<string>("password", password));

        using NpgsqlCommand insertCommand = dataSource.CreateCommand(insertSql);
        insertCommand.Parameters.Add(new NpgsqlParameter<string>("password", password));

        try
        {
            using NpgsqlDataReader reader = selectCommand.ExecuteReader();

            if (reader.Read() is true)
                return new RegisterError<string>("Password already exists");
            reader.Close();

            insertCommand.ExecuteNonQuery();
        }
        catch (NpgsqlException)
        {
            return new RegisterError<string>("Failed to register new password (failed to connect database)");
        }

        return new RegisterSuccess<string>(password);
    }
}