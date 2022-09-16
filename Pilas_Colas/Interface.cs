using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilas_Colas
{
    internal interface Interface
    {
        bool estaVacia();

        object extraer();

        object primero();

        bool agregar(object elemento);
    }
}
