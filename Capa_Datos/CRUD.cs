using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
    public interface CRUD<T>
    {
        List<T> Listar();
        int Create(T item);
        bool Update(T item);
        void Delete(T item);
    }
}
