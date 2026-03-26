using System;
using System.Collections.Generic;
using ICelebrity.lib;
using Npgsql;

namespace ICelebrity.lib;

public class DbRepository : ICelebrity<Celebrity>
{
    private readonly string _connectionString;
    private int _scn = 0;

    private DbRepository(string connectionString)
    {
        this._connectionString = connectionString;
    }

    public static ICelebrity<Celebrity> Create(string connectionString)
    {
        return new DbRepository(connectionString);
    }

    private NpgsqlConnection CreateConnection()
    {
        return new NpgsqlConnection(this._connectionString);
    }

    public List<Celebrity> GetAll()
    {
        var result = new List<Celebrity>();

        using (var connection = this.CreateConnection())
        {
            connection.Open();
            using var cmd = new NpgsqlCommand(
                "SELECT id, firstname, surname, photopath FROM celebrities ORDER BY id",
                connection
            );

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var cel = new Celebrity(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3)
                );
                result.Add(cel);
            }
        }

        return result;
    }

    public Celebrity? GetById(int id)
    {
        using var connection = this.CreateConnection();
        connection.Open();

        using var cmd = new NpgsqlCommand(
            "SELECT id, firstname, surname, photopath FROM celebrities WHERE id = @id",
            connection
        );
        cmd.Parameters.AddWithValue("id", id);

        using var reader = cmd.ExecuteReader();
        if (!reader.Read())
            return null;

        return new Celebrity(
            reader.GetInt32(0),
            reader.GetString(1),
            reader.GetString(2),
            reader.GetString(3)
        );
    }

    public bool Add(Celebrity celebrity)
    {
        using var connection = this.CreateConnection();
        connection.Open();

        using var cmd = new NpgsqlCommand(
            @"INSERT INTO celebrities (firstname, surname, photopath)
              VALUES (@fn, @sn, @pp);",
            connection
        );
        cmd.Parameters.AddWithValue("fn", celebrity.FirstName);
        cmd.Parameters.AddWithValue("sn", celebrity.Surname);
        cmd.Parameters.AddWithValue("pp", celebrity.PhotoPath);

        var rows = cmd.ExecuteNonQuery();
        if (rows > 0)
        {
            this._scn++;
            return true;
        }

        return false;
    }

    public bool DelById(int id)
    {
        using var connection = this.CreateConnection();
        connection.Open();

        using var cmd = new NpgsqlCommand("DELETE FROM celebrities WHERE id = @id", connection);
        cmd.Parameters.AddWithValue("id", id);

        var rows = cmd.ExecuteNonQuery();
        if (rows > 0)
        {
            this._scn++;
            return true;
        }

        return false;
    }

    public bool UpdateById(int id, Celebrity celebrity)
    {
        using var connection = this.CreateConnection();
        connection.Open();

        using var cmd = new NpgsqlCommand(
            @"UPDATE celebrities
              SET firstname = @fn,
                  surname   = @sn,
                  photopath = @pp
              WHERE id = @id",
            connection
        );
        cmd.Parameters.AddWithValue("fn", celebrity.FirstName);
        cmd.Parameters.AddWithValue("sn", celebrity.Surname);
        cmd.Parameters.AddWithValue("pp", celebrity.PhotoPath);
        cmd.Parameters.AddWithValue("id", id);

        var rows = cmd.ExecuteNonQuery();
        if (rows > 0)
        {
            this._scn++;
            return true;
        }

        return false;
    }

    public int AddAndGetId(Celebrity celebrity)
    {
        using var connection = this.CreateConnection();
        connection.Open();

        using var cmd = new NpgsqlCommand(
            @"INSERT INTO celebrities (firstname, surname, photopath)
              VALUES (@fn, @sn, @pp)
              RETURNING id;",
            connection
        );
        cmd.Parameters.AddWithValue("fn", celebrity.FirstName);
        cmd.Parameters.AddWithValue("sn", celebrity.Surname);
        cmd.Parameters.AddWithValue("pp", celebrity.PhotoPath);

        var result = cmd.ExecuteScalar();
        var id = result is int v ? v : Convert.ToInt32(result);
        this._scn++;
        return id;
    }

    public int GetIdByName(string celebrityName)
    {
        using var connection = this.CreateConnection();
        connection.Open();

        using var cmd = new NpgsqlCommand(
            @"SELECT id
              FROM celebrities
              WHERE (firstname || '|' || surname) ILIKE '%' || @name || '%'
              ORDER BY id
              LIMIT 1;",
            connection
        );
        cmd.Parameters.AddWithValue("name", celebrityName);

        var result = cmd.ExecuteScalar();
        return result is int id ? id : -1;
    }

    public int SaveChanges()
    {
        this._scn = 0;
        return this._scn;
    }

    public void Dispose()
    {
        // no-op
    }
}
