using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilas_Colas
{
    internal class Pila : Interface
    {
        private int contador;
        private object[] array;

        public Pila(int cantidad)
        {
            contador = -1;
            array = new object[cantidad];
        }

        public bool agregar(object elemento)
        {
            bool ok = false;

            if (contador < array.Length)
            {
                array[++contador] = elemento;
                ok = true;
            }
            return true;
        }

        public bool estaVacia()
        {
            return contador == -1;
        }

        public object extraer()
        {
            object e = null;

            if (!estaVacia())
            {
                e = array[contador];
                array[contador] = null;
                contador--;
            }
            return e;
        }

        public object primero()
        {
            object e = null;
            if (!estaVacia())
            {
                e = array[0];
            }
            return e;
        }
    }
}
