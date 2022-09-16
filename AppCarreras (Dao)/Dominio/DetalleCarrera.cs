using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCarreras__Dao_.Dominio
{
    internal class DetalleCarrera
    {
        public DetalleCarrera(int anioCursado, int cuatrimestre, Asignatura asignatura)
        {
            AnioCursado = anioCursado;
            Cuatrimestre = cuatrimestre;
            Asignatura = asignatura;
        }

        public int AnioCursado { get; set; }

        public int Cuatrimestre { get; set; }

        public Asignatura Asignatura { get; set; }

        public override string ToString()
        {
            return "Año Cursado: " + AnioCursado + "Cuatrimestre: " + Cuatrimestre + "Asignatura: " + Asignatura.NombreAsignatura;
        }
    }
}
