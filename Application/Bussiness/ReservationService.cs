using Application.Interfaces;
using Data_Access.Models;
using Data_Access.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bussiness
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IScreeningRepository _screeningRepository;
        private readonly IUserRepository _userRepository;
        public ReservationService(
            IReservationRepository reservationRepository,
            IScreeningRepository screeningRepository,
            IUserRepository userRepository)
        {
            _reservationRepository = reservationRepository;
            _screeningRepository = screeningRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByScreeningIdAsync(int screeningId)
        {
            // Získání všech rezervací podle ID promítání
            return await _reservationRepository.GetReservationsByScreeningIdAsync(screeningId);
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByUserIdAsync(int userId)
        {
            return await _reservationRepository.GetReservationsByUserIdAsync(userId);
        }

        public async Task<bool> MakeReservation(Reservation model)
        {
            // Ověření, zda existuje promítání
            var screening = await _screeningRepository.GetByIdAsync(model.ScreeningId);
            if (screening == null)
            {
                return false; 
            }

            // Ověření, zda existuje uživatel
            var user = await _userRepository.GetByIdAsync(model.UserId);
            if (user == null)
            {
                return false; // Uživatel neexistuje
            }

            var reservations = await _reservationRepository.GetReservationsByScreeningIdAsync(model.ScreeningId);
            var reservedSeats = reservations.Sum(r => r.NumberOfSeats);
            if (reservedSeats + model.NumberOfSeats > screening.ScreeningRoom.Capacity)
            {
                return false; 
            }

            await _reservationRepository.AddAsync(model);
            screening.Capacity -= model.NumberOfSeats;
            await _screeningRepository.UpdateAsync(screening);
            return true;
        }
    }
}
