using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access.Interfaces;
using Dapper;
using Microsoft.Data.Sqlite;
using Data_Access.Database;

namespace Data_Access.Database
{
    public class DatabaseAccess : IDatabaseAccess
    {
        private readonly string _connectionString;

        public DatabaseAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string sql, object parameters = null) where T : class
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            return await connection.QueryAsync<T>(sql, parameters);
        }

        public async Task<int> ExecuteNonQueryAsync(string sql, object parameters = null)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            return await connection.ExecuteAsync(sql, parameters);
        }
    }
}
