using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class Comunicacion : Servicio
    {
        private double PorcentajeDescuento;
        
        public void CalcularPorcentaje(ZonaOperacion zonaOperacion)
        {
            if(ZonaOperacion.Cuyo == zonaOperacion)
            {
                PorcentajeDescuento = 15;
            }
            if (ZonaOperacion.Norte == zonaOperacion)
            {
                PorcentajeDescuento = 10;
            }
        }
    }
}
