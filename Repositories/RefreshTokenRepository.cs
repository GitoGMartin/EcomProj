using ECommerce.API.Models;
using EcomProj.Interfaces;
using Npgsql;

namespace EcomProj.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {

        private readonly string _connectionString;
        public RefreshTokenRepository(IConfiguration configuration)
        {
            _connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING")
            ?? throw new InvalidOperationException("DATABASE_CONNECTION_STRING not found.");

        }

        private NpgsqlConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }

        public async Task Create(RefreshToken refreshToken)
        {
            await using var connection = CreateConnection();

            await connection.OpenAsync();

            const string sql = """
                INSERT INTO "RefreshTokens"
                (
                    "UserId",
                    "Token",
                    "ExpiresAt",
                    "CreatedAt"
                )
                VALUES
                (
                    @UserId,
                    @Token,
                    @ExpiresAt,
                    @CreatedAt
                );
                """;

            await using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.AddWithValue("@UserId", refreshToken.userId);
            command.Parameters.AddWithValue("@Token", refreshToken.token);
            command.Parameters.AddWithValue("@ExpiresAt", refreshToken.expiresOn);
            command.Parameters.AddWithValue("@CreatedAt", refreshToken.createDate);

            await command.ExecuteNonQueryAsync();
        }

        public async Task<RefreshToken?> GetByToken(string token)
        {
            await using var connection = CreateConnection();

            await connection.OpenAsync();

            const string sql = """
                SELECT
                    "RefreshTokenId",
                    "UserId",
                    "Token",
                    "ExpiresAt",
                    "CreatedAt",
                    "RevokedAt"
                FROM "RefreshTokens"
                WHERE "Token" = @Token;
                """;

            await using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.AddWithValue("@Token", token);

            await using var reader = await command.ExecuteReaderAsync();

            if (!await reader.ReadAsync())
            {
                return null;
            }

            return new RefreshToken
            {
                RefreshTokenId = reader.GetInt32(0),
                userId = reader.GetGuid(1),
                token = reader.GetString(2),
                expiresOn = reader.GetDateTime(3),
                createDate = reader.GetDateTime(4),
                revoked = reader.IsDBNull(5)
                    ? null
                    : reader.GetDateTime(5)
            };
        }

        public async Task Revoke(int refreshTokenId)
        {
            await using var connection = CreateConnection();

            await connection.OpenAsync();

            const string sql = """
                UPDATE "RefreshTokens"
                SET "RevokedAt" = CURRENT_TIMESTAMP
                WHERE "RefreshTokenId" = @RefreshTokenId;
                """;

            await using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.AddWithValue(
                "@RefreshTokenId",
                refreshTokenId
            );

            await command.ExecuteNonQueryAsync();
        }
    }
}
