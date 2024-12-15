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
    public class ScreeningService : IScreeningService
    {
        private readonly IScreeningRepository _screeningRepository;

        public ScreeningService(IScreeningRepository screeningRepository)
        {
            _screeningRepository = screeningRepository;
        }

        public async Task<IEnumerable<Screening>> GetScreeningsByDateAsync(DateTime date)
        {
            return await _screeningRepository.GetScreeningsForDayAsync(date);
        }

        public async Task<IEnumerable<Screening>> GetAllScreening()
        {
            
            return await _screeningRepository.GetAllAsync();
        }

        public async Task<bool> AddScreeningAsync(Screening model)
        {
            
            try
            {
                await _screeningRepository.AddAsync(model);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateScreeningAsync(Screening model)
        {
            
            try
            {
                var existingScreening = await _screeningRepository.GetByIdAsync(model.Id);
                if (existingScreening == null)
                {
                    return false; 
                }

                await _screeningRepository.UpdateAsync(model);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteScreeningAsync(int screeningId)
        {
            
            try
            {
                var existingScreening = await _screeningRepository.GetByIdAsync(screeningId);
                if (existingScreening == null)
                {
                    return false; 
                }

                await _screeningRepository.DeleteAsync(screeningId);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<Screening> GetScreeningByIdAsync(int screeningId)
        {
            return await _screeningRepository.GetByIdAsync(screeningId);
        }   
    }
}
