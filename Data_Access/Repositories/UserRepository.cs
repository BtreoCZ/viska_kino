using Data_Access.Interfaces;
using Data_Access.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseAccess _databaseAccess;

        public UserRepository(IDatabaseAccess databaseAccess)
        {
            _databaseAccess = databaseAccess;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var sql = "SELECT * FROM Users";
            return await _databaseAccess.ExecuteQueryAsync<User>(sql);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Users WHERE Id = @Id";
            var parameters = new { Id = id };
            var users = await _databaseAccess.ExecuteQueryAsync<User>(sql, parameters);
            return users.FirstOrDefault();
        }

        public async Task<User> GetByNameAsync(string name)
        {
            var sql = "SELECT * FROM Users WHERE Name = @Name";
            var parameters = new { Name = name };
            var users = await _databaseAccess.ExecuteQueryAsync<User>(sql, parameters);
            return users.FirstOrDefault();
        }

        public async Task<User> GetByLoginAsync(string login)
        {
            var sql = "SELECT * FROM Users WHERE Login = @Login";
            var parameters = new { Login = login };
            var users = await _databaseAccess.ExecuteQueryAsync<User>(sql, parameters);
            return users.FirstOrDefault();
        }

        public async Task AddAsync(User user)
        {
            var sql = "INSERT INTO Users (Name, Email, PhoneNumber, Login, Password) VALUES (@Name, @Email, @PhoneNumber, @Login, @Password)";
            await _databaseAccess.ExecuteNonQueryAsync(sql, user);
        }

        public async Task UpdateAsync(User user)
        {
            var sql = "UPDATE Users SET Name = @Name, Email = @Email, PhoneNumber = @PhoneNumber, Login = @Login, Password = @Password WHERE Id = @Id";
            await _databaseAccess.ExecuteNonQueryAsync(sql, user);
        }

        public async Task DeleteAsync(int id)
        {
            var sql = "DELETE FROM Users WHERE Id = @Id";
            var parameters = new { Id = id };
            await _databaseAccess.ExecuteNonQueryAsync(sql, parameters);
        }
    }
}
