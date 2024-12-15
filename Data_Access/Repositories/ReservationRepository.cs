using Data_Access.Interfaces;
using Data_Access.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly IDatabaseAccess _databaseAccess;

        public ReservationRepository(IDatabaseAccess databaseAccess)
        {
            _databaseAccess = databaseAccess;
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            var sql = "SELECT * FROM Reservation";
            return await _databaseAccess.ExecuteQueryAsync<Reservation>(sql);
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByScreeningIdAsync(int Id)
        {
            var sql = "SELECT * FROM Reservation WHERE ScreeningId = @ScreeningId";
            var parameters = new { ScreeningId = Id };
            return await _databaseAccess.ExecuteQueryAsync<Reservation>(sql, parameters);
        }
        public async Task<Reservation> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Reservation WHERE Id = @Id";
            var parameters = new { Id = id };
            return (await _databaseAccess.ExecuteQueryAsync<Reservation>(sql, parameters)).FirstOrDefault();
        }

        public async Task AddAsync(Reservation reservation)
        {
            var sql = "INSERT INTO Reservation (UserId, ScreeningId, NumberOfSeats) VALUES (@UserId, @ScreeningId, @NumberOfSeats)";
            await _databaseAccess.ExecuteNonQueryAsync(sql, reservation);
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            var sql = "UPDATE Reservation SET UserId = @UserId, ScreeningId = @ScreeningId, NumberOfSeats = @NumberOfSeats WHERE Id = @Id";
            await _databaseAccess.ExecuteNonQueryAsync(sql, reservation);
        }

        public async Task DeleteAsync(int id)
        {
            var sql = "DELETE FROM Reservation WHERE Id = @Id";
            var parameters = new { Id = id };
            await _databaseAccess.ExecuteNonQueryAsync(sql, parameters);
        }
    }
}
