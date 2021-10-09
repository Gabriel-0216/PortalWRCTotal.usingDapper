using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repositorio
{
    public interface IRepository<T>
    {
        int Add(T objeto);
        int Remove(int id);

        List<T> GetAll();

        T Get(int id);

        int Update(T objeto);
        
    }
}
