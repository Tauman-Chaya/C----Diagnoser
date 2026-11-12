using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Api
{
    public interface Icrud<T>
    {
        Task Create(T item);
        Task<List<T>> Read();
        Task<List<T>> Read(Func<T, bool> func);
        Task Update(T item);
        Task Delete(T item);
    }
}
