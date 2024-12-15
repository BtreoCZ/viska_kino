using Data_Access.Interfaces;
using Data_Access.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositories
{
    public class ScreeningRepository : IScreeningRepository
    {
        private readonly IDatabaseAccess _databaseAccess;
        private readonly IFilmRepository _filmRepository;
        private readonly IScreeningRoomRepository _screeningroomRepository;
        public ScreeningRepository(IDatabaseAccess databaseAccess,IFilmRepository filmRepository,IScreeningRoomRepository screeningroomRepository)
        {
            _databaseAccess = databaseAccess;
            _filmRepository = filmRepository;
            _screeningroomRepository = screeningroomRepository;
        }

        public async Task<IEnumerable<Screening>> GetAllAsync()
        {
            var sql = "SELECT * FROM Screenings";
            return await _databaseAccess.ExecuteQueryAsync<Screening>(sql);
        }

        public async Task<Screening> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Screenings WHERE Id = @Id";
            var parameters = new { Id = id };
            var screening = (await _databaseAccess.ExecuteQueryAsync<Screening>(sql, parameters)).FirstOrDefault();

            screening.Film = await _filmRepository.GetByIdAsync(screening.FilmId);
            screening.ScreeningRoom = await _screeningroomRepository.GetByIdAsync(screening.ScreeningRoomId);

            return screening;
        }

        public async Task<Screening> GetScreeningByTimeAsync(DateTime start, DateTime end)
        {
            var sql = "SELECT * FROM Screenings WHERE ScreeningTime BETWEEN @Start AND @End";
            var parameters = new { Start = start, End = end };
            return (await _databaseAccess.ExecuteQueryAsync<Screening>(sql, parameters)).FirstOrDefault();
        }
        public async Task<IEnumerable<Screening>> GetScreeningsForDayAsync(DateTime day)
        {
            var sql = "SELECT * FROM Screenings WHERE DATE(ScreeningTime) = DATE(@Day)";
            var parameters = new { Day = day };
            var screenings = await _databaseAccess.ExecuteQueryAsync<Screening>(sql, parameters);

            foreach(Screening screening in screenings)
            {
                screening.Film = await _filmRepository.GetByIdAsync(screening.FilmId);
                screening.ScreeningRoom = await _screeningroomRepository.GetByIdAsync(screening.ScreeningRoomId);
            }

            return screenings;
        }
        public async Task AddAsync(Screening screening)
        {
            var sql = "INSERT INTO Screenings (ScreeningTime, FilmId, ScreeningRoomId) VALUES (@ScreeningTime, @FilmId, @ScreeningRoomId)";
            await _databaseAccess.ExecuteNonQueryAsync(sql, screening);
        }

        public async Task UpdateAsync(Screening screening)
        {
            var sql = "UPDATE Screenings SET ScreeningTime = @ScreeningTime, FilmId = @FilmId, ScreeningRoomId = @ScreeningRoomId WHERE Id = @Id";
            await _databaseAccess.ExecuteNonQueryAsync(sql, screening);
        }

        public async Task DeleteAsync(int id)
        {
            var sql = "DELETE FROM Screenings WHERE Id = @Id";
            var parameters = new { Id = id };
            await _databaseAccess.ExecuteNonQueryAsync(sql, parameters);
        }
    }
}
