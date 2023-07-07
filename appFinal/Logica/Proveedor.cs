using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class Proveedor
    {
        //CORRECCION:Los enums van en un archivo aparte
        public enum Opera{Brasil, Argentina, Mexico}
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Opera Operacion { get; set; }
        public double Saldo { get; set; }
    }
}
