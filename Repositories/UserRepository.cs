using ECommerce.API.Interfaces;
using ECommerce.API.Models;
using EcomProj.DTOs;
using Npgsql;

namespace ECommerce.API.Repositories;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository()
    {
        _connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING")
            ?? throw new InvalidOperationException("DATABASE_CONNECTION_STRING not found.");
    }

    private NpgsqlConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }

    public async Task<IEnumerable<UserDTO>> GetAllAsync()
    {
        var results = new List<UserDTO>();

        const string sql = @"
        SELECT *
        FROM ""users""";

        await using var conn = CreateConnection();
        await conn.OpenAsync();

        await using var cmd = new NpgsqlCommand(sql, conn);

        await using var rdr = await cmd.ExecuteReaderAsync();

        while (await rdr.ReadAsync())
        {
            results.Add(new UserDTO
            {
                id = (Guid)rdr["UserId"],
                firstName = rdr["FirstName"]?.ToString() ?? string.Empty,
                LastName = rdr["LastName"]?.ToString() ?? string.Empty,
                Email = rdr["Email"]?.ToString() ?? string.Empty,
                PhoneNumber = rdr["PhoneNumber"]?.ToString() ?? string.Empty,
                IsActive = (bool)rdr["IsActive"],
                CreateDate = (DateTime)rdr["CreateDat"],
                UpdateDate = (DateTime)rdr["UpdateDat"]
            });
        }

        return results;
    }

    public async Task<UserDTO?> GetByIdAsync(Guid id)
    {
        const string sql = @"
        SELECT *
        FROM users
        WHERE userid = @Id";

        using var conn = CreateConnection();
        await conn.OpenAsync();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;

        var p = cmd.CreateParameter();
        p.ParameterName = "@Id";
        p.Value = id;

        cmd.Parameters.Add(p);

        using var rdr = await cmd.ExecuteReaderAsync();

        if (await rdr.ReadAsync())
        {
            return new UserDTO
            {
                id = rdr["userid"] is Guid uid
                    ? uid
                    : Guid.Empty,

                firstName = rdr["firstname"] as string
                    ?? string.Empty,

                LastName = rdr["lastname"] as string
                    ?? string.Empty,

                Email = rdr["email"] as string
                    ?? string.Empty,

                PhoneNumber = rdr["phonenumber"] as string
                    ?? string.Empty,

                IsActive = rdr["isactive"] is bool b && b,

                CreateDate = rdr["createdat"] is DateTime cd
                    ? cd
                    : DateTime.MinValue,

                UpdateDate = rdr["updatedat"] is DateTime ud
                    ? ud
                    : DateTime.MinValue
            };
        }

        return null;
    }


    public async Task<Guid> CreateAsync(User user)
    {
        var newId = Guid.NewGuid();
        const string sql = @"INSERT INTO ""users"" (userid, FirstName, LastName, Email, passwordHash, PhoneNumber, IsActive, Createdat, UpdateDat)
VALUES ( @UserId, @FirstName, @LastName, @Email, @Password, @PhoneNumber, @IsActive, @Createdat, @UpdateDat)";

        using var conn = CreateConnection();
        await conn.OpenAsync();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;

        void AddParam(string name, object val)
        {
            var pp = cmd.CreateParameter();
            pp.ParameterName = name;
            pp.Value = val ?? DBNull.Value;
            cmd.Parameters.Add(pp);
        }

        AddParam("@UserId", newId);
        AddParam("@FirstName", user.FirstName);
        AddParam("@LastName", user.LastName);
        AddParam("@Email", user.Email);
        AddParam("@Password", user.passwordHash);
        AddParam("@PhoneNumber", user.phoneNumber);
        AddParam("@IsActive", true);
        AddParam("@CreateDat", DateTime.Now);
        AddParam("@UpdateDat", DateTime.Now);

        var affected = await cmd.ExecuteNonQueryAsync();
        return affected > 0 ? newId : Guid.Empty;
    }

    public async Task<bool> UpdateAsync(Guid id, UserDTO user)
    {
        const string sql = @"UPDATE ""users"" SET FirstName = @FirstName, LastName = @LastName, Email = @Email, PhoneNumber = @PhoneNumber, IsActive = @IsActive, UpdateDat = @UpdateDate WHERE UserId = @UserId";

        using var conn = CreateConnection();
        await conn.OpenAsync();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;

        void AddParam(string name, object val)
        {
            var pp = cmd.CreateParameter();
            pp.ParameterName = name;
            pp.Value = val ?? DBNull.Value;
            cmd.Parameters.Add(pp);
        }

        AddParam("@UserId", id);
        AddParam("@FirstName", user.firstName);
        AddParam("@LastName", user.LastName);
        AddParam("@Email", user.Email);
        AddParam("@PhoneNumber", user.PhoneNumber);
        AddParam("@IsActive", user.IsActive);
        AddParam("@UpdateDate", user.UpdateDate);

        var affected = await cmd.ExecuteNonQueryAsync();
        return affected > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        const string sql = @"DELETE FROM ""users"" WHERE UserId = @UserId";

        using var conn = CreateConnection();
        await conn.OpenAsync();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        var p = cmd.CreateParameter();
        p.ParameterName = "@UserId";
        p.Value = id;
        cmd.Parameters.Add(p);

        var affected = await cmd.ExecuteNonQueryAsync();
        return affected > 0;
    }
    public async Task<User?> GetUserByEmail(string email)
    {
        const string sql = @"
        SELECT *
        FROM users
        WHERE email = @email";
        using var conn = CreateConnection();
        await conn.OpenAsync();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        var p = cmd.CreateParameter();
        p.ParameterName = "@email";
        p.Value = (email ?? string.Empty).Trim();
        cmd.Parameters.Add(p);
        using var rdr = await cmd.ExecuteReaderAsync();
        if (await rdr.ReadAsync())
        {
            return new User
            {
                UserId = rdr["UserId"] is Guid uid ? uid : Guid.Empty,
                FirstName = rdr["FirstName"] as string ?? string.Empty,
                passwordHash = rdr["PasswordHash"] as string ?? string.Empty,
                LastName = rdr["LastName"] as string ?? string.Empty,
                Email = rdr["Email"] as string ?? string.Empty,
                phoneNumber = rdr["PhoneNumber"] as string ?? string.Empty,
                isActive = rdr["IsActive"] is bool b && b,
                createDate = rdr["Createdat"] is DateTime cd ? cd : DateTime.MinValue,
                updateDate = rdr["UpdateDat"] is DateTime ud ? ud : DateTime.MinValue
            };
        }
        return null;
    }
}
