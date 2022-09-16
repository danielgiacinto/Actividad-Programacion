using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCarreras__Dao_.Dominio
{
    internal class Asignatura
    {
        public int IdAsignatura { get; set; }
        public string NombreAsignatura { get; set; }

        public override string ToString()
        {
            return NombreAsignatura;
        }
    }
}
