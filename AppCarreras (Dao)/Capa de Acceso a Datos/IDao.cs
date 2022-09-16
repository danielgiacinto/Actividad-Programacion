using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCarreras__Dao_.Capa_de_Acceso_a_Datos
{
    internal interface IDao
    {
        void Create();
        void Update();
        void Delete();
        void Read();
    }
}
