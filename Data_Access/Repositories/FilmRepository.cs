using Data_Access.Interfaces;
using Data_Access.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        private readonly IDatabaseAccess _databaseAccess;

        public FilmRepository(IDatabaseAccess databaseAccess)
        {
            _databaseAccess = databaseAccess;
        }

        public async Task<IEnumerable<Film>> GetAllAsync()
        {
            var sql = "SELECT * FROM Films";
            return await _databaseAccess.ExecuteQueryAsync<Film>(sql);
        }

        public async Task<Film> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Films WHERE Id = @Id";
            var parameters = new { Id = id };
            return (await _databaseAccess.ExecuteQueryAsync<Film>(sql, parameters)).FirstOrDefault();
        }

        public async Task<Film> GetByTitleAsync(string title)
        {
            var sql = "SELECT * FROM Films WHERE Title = @Title";
            var parameters = new { Title = title };
            return (await _databaseAccess.ExecuteQueryAsync<Film>(sql, parameters)).FirstOrDefault();
        }

        public async Task AddAsync(Film film)
        {
            var sql = "INSERT INTO Films (Title, Director, DurationMinutes, Genre) VALUES (@Title, @Director, @DurationMinutes, @Genre)";
            await _databaseAccess.ExecuteNonQueryAsync(sql, film);
        }

        public async Task UpdateAsync(Film film)
        {
            var sql = "UPDATE Films SET Title = @Title, Director = @Director, DurationMinutes = @DurationMinutes, Genre = @Genre WHERE Id = @Id";
            await _databaseAccess.ExecuteNonQueryAsync(sql, film);
        }

        public async Task DeleteAsync(int id)
        {
            var sql = "DELETE FROM Films WHERE Id = @Id";
            var parameters = new { Id = id };
            await _databaseAccess.ExecuteNonQueryAsync(sql, parameters);
        }
    }
}
