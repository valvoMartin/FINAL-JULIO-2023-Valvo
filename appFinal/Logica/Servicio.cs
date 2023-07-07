using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public abstract class Servicio
    {
        public enum ZonaOperacion { Cuyo, Norte, Centro, Patagonia }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdProveedor { get; set; }
        public ZonaOperacion Zona { get; set; }

        //CORRECCION: Falta metodo para obtener la descripción
    }
}
