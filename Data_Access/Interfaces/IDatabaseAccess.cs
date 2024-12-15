using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Interfaces
{
    public interface IDatabaseAccess
    {
        Task<IEnumerable<T>> ExecuteQueryAsync<T>(string sql, object parameters = null) where T : class;
        Task<int> ExecuteNonQueryAsync(string sql, object parameters = null);
    }
}
