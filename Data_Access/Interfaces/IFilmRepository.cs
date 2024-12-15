using Data_Access.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositories
{
    internal interface IFilmRepository : IRepository<Film>
    {
        Task<Film> GetByTitleAsync(string title);
    }
}
