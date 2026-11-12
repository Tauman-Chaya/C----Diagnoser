using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BL.API
{
    public interface IcrudBL<T>
    {
        Task Create(T item);
        Task<List<T>> Read();
        Task<List<T>> Read(Func<T, bool> func);
        Task Update(T item);
        Task Delete(T item);
    }
}
