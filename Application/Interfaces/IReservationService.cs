using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access.Models;

namespace Application.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<Reservation>> GetReservationsByScreeningIdAsync(int screeningId);
        Task<IEnumerable<Reservation>> GetReservationsByUserIdAsync(int userId);
        Task<bool> MakeReservation(Reservation model);
    }
}
