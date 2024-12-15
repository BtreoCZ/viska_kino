using Data_Access.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositories
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        Task<IEnumerable<Reservation>> GetReservationsByScreeningIdAsync(int Id);

        Task<IEnumerable<Reservation>> GetReservationsByUserIdAsync(int Id);
    }
}
