using ECommerce.API.Models;
using ECommerce.API.Repositories;
using EcomProj.Interfaces;
using Npgsql;

namespace EcomProj.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<UserRepository> _logger;
        public AddressRepository(ILogger<UserRepository> logger)
        {
            _connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING")
                ?? throw new InvalidOperationException("DATABASE_CONNECTION_STRING not found.");
            _logger = logger;
        }

        private NpgsqlConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }

        public async Task<Guid> CreateAsync(Address Addy)
        {
            Guid newId = Guid.Empty;
            const string sql = @"INSERT INTO ""addresses"" (user_id, address_line1, address_line2, city, province, postal_code, country, is_default)
VALUES ( @user_id, @address_line1, @address_line2, @city, @province, @postal_code, @country, @is_default)";

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


            AddParam("@user_id", Addy.userId);
            AddParam("@address_line1", Addy.addressLine1);
            AddParam("@address_line2", Addy.addressLine2);
            AddParam("@city", Addy.city);
            AddParam("@province", Addy.province);
            AddParam("@postal_code", Addy.postalCode);
            AddParam("@country", Addy.country);
            AddParam("@is_default", Addy.isDefault);

            var affected = await cmd.ExecuteNonQueryAsync();
            Address newAddress = await this.GetAddressByIdAsync(Addy.userId);
            newId = newAddress.addressId;
            return affected > 0 ? newId : Guid.Empty;
        }


        public async Task<bool> DeleteAsync(Guid id)
        {

            const string sql = @"DELETE FROM ""addresses"" WHERE ""addressId"" = @id";

            using var conn = CreateConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            var p = cmd.CreateParameter();
            p.ParameterName = "@id";
            p.Value = id;
            cmd.Parameters.Add(p);

            var affected = await cmd.ExecuteNonQueryAsync();
            return affected > 0;
        }

        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            var results = new List<Address>();

            const string sql = @"
        SELECT *
        FROM ""addresses""";

            await using var conn = CreateConnection();
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand(sql, conn);

            await using var rdr = await cmd.ExecuteReaderAsync();

            while (await rdr.ReadAsync())
            {
                results.Add(new Address
                {
                    addressId = (Guid)rdr["addressId"],
                    userId = (Guid)rdr["userId"],
                    addressLine1 = rdr["addressLine1"]?.ToString() ?? string.Empty,
                    addressLine2 = rdr["addressLine2"]?.ToString(),
                    city = rdr["city"]?.ToString() ?? string.Empty,
                    province = rdr["province"]?.ToString() ?? string.Empty,
                    postalCode = rdr["postalCode"]?.ToString() ?? string.Empty,
                    country = rdr["country"]?.ToString() ?? string.Empty,
                    isDefault = (bool)rdr["isDefault"]
                });
            }

            return results;
        }




        public async Task<bool> UpdateAsync(Guid id, Address Addy)
        {
            const string sql = @"UPDATE ""addresses"" SET userid = @userID, addressline1 = @addressLine1, addressline2 = @addressLine2, city = @city, province = @province, postalcode = @postalCode, country = @country, isdefault = @isDefault WHERE ""addressId"" = @id";

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

            AddParam("@id", id);
            AddParam("@userId", Addy.userId);
            AddParam("@addressLine1", Addy.addressLine1);
            AddParam("@addressLine2", Addy.addressLine2);
            AddParam("@city", Addy.city);
            AddParam("@province", Addy.province);
            AddParam("@postalCode", Addy.postalCode);
            AddParam("@country", Addy.country);
            AddParam("@isDefault", Addy.isDefault);

            var affected = await cmd.ExecuteNonQueryAsync();
            return affected > 0;
        }

        public async Task<Address?> GetAddressByIdAsync(Guid id)
        {
            var results = new Address();

            const string sql = @"
        SELECT *
        FROM ""addresses""
        WHERE ""addressId"" = @id";

            await using var conn = CreateConnection();
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            await using var rdr = await cmd.ExecuteReaderAsync();

            while (await rdr.ReadAsync())
            {
                results = new Address
                {
                    addressId = (Guid)rdr["addressId"],
                    userId = (Guid)rdr["userId"],
                    addressLine1 = rdr["addressLine1"]?.ToString() ?? string.Empty,
                    addressLine2 = rdr["addressLine2"]?.ToString(),
                    city = rdr["city"]?.ToString() ?? string.Empty,
                    province = rdr["province"]?.ToString() ?? string.Empty,
                    postalCode = rdr["postalCode"]?.ToString() ?? string.Empty,
                    country = rdr["country"]?.ToString() ?? string.Empty,
                    isDefault = (bool)rdr["isDefault"]
                };
            }

            return results;
        }
    }
}
