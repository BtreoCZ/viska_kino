using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access.Models;

namespace Application.Interfaces
{
    public interface IScreeningService
    {
        Task<IEnumerable<Screening>> GetScreeningsByDateAsync(DateTime date);
        Task<IEnumerable<Screening>> GetAllScreening();
        Task<Screening> GetScreeningByIdAsync(int screeningId);
        Task<bool> AddScreeningAsync(Screening model);
        Task<bool> UpdateScreeningAsync(Screening model);
        Task<bool> DeleteScreeningAsync(int screeningId);
    }
}
