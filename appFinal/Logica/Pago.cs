using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class Pago
    {
        public int Id { get; set; }
        public DateTime FechaCobro { get; set; } = DateTime.Now; //CORRECCION: Incorrecta asignación
        public int DniPagador { get; set; }
        public int IdServicio { get; set; }
        public double Importe { get; set; }
    }
}
