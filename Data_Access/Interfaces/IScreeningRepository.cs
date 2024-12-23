﻿using Data_Access.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositories
{
    public interface IScreeningRepository : IRepository<Screening>
    {
        Task<Screening> GetScreeningByTimeAsync(DateTime start, DateTime end);
        Task<IEnumerable<Screening>> GetScreeningsForDayAsync(DateTime day);
    }
}
