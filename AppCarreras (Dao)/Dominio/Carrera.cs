using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCarreras__Dao_.Dominio
{
    internal class Carrera
    {
        public int IdCarrera { get; set; }

        public string NombreTitulo { get; set; }

        public List<DetalleCarrera> LisDetalleCarrera { get; set; }

        public Carrera()
        {
            LisDetalleCarrera = new List<DetalleCarrera>();
        }

        public override string ToString()
        {
            return "Nombre de Carrera: " + NombreTitulo;
        }

        public void AgregarDetalle(DetalleCarrera detalle)
        {
            LisDetalleCarrera.Add(detalle);
        }

        public void EliminarDetalle(int id)
        {
            LisDetalleCarrera.RemoveAt(id);
        }
    }
}
