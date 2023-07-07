using Logica.Herramientas;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static Logica.Proveedor;
using static Logica.Servicio;

namespace Logica
{
    public class Empresa
    {
        List<Servicio> Servicios;
        List<Proveedor> Proveedores;
        List<Pago> Pagos;


//CORRECCION: No compila
        public bool AltaNuevoServicio(string nombre, string descripcion, int idProveedor, double ?porcentajeImpuesto, ZonaOperacion zonaOperacion)
        {
            if(Proveedores.Any(x=>x.Id == idProveedor))
            {
                Servicios = new List<Servicio>();

                if (porcentajeImpuesto != null)
                {//Electrico
                    Electrico servicioE = new Electrico();
                    servicioE.PorcentajeImpuesto = porcentajeImpuesto.Value;
                    servicioE.IdProveedor = idProveedor;
                    servicioE.Descripcion = descripcion;
                    servicioE.Nombre = nombre;
                    servicioE.Id = Servicios.Count;
                    servicioE.Zona = zonaOperacion;

                    Servicios.Add(servicioE);
                    return true;
                }
                
                Comunicacion servicio = new Comunicacion();
                servicio.Nombre = nombre;
                servicio.IdProveedor = idProveedor;
                servicio.Descripcion = descripcion;
                servicio.Id = Servicios.Count;
                servicio.Zona = zonaOperacion;

                Servicios.Add(servicio);
                return true;
                
            }
            return false;
                
        }

        public Response NuevoPago(int dniPagador, int idServicio, double monto)
        {
            
            var servicio = Servicios.Find(x => x.Id == idServicio);
            if(servicio != null)
            {
                var prov = Proveedores.Find(c => c.Id == servicio.IdProveedor);
                if (prov != null)
                {
                    prov.Saldo += monto;
                }
                else
                {
                    return new Response { Fallo = true, Comentario = "Proveedor mal ingresado al momento de cargar el servicio." };
                }
                Pago pago = new Pago();
                Pagos = new List<Pago>();
                if (servicio is Electrico electrico)
                {
                    ////CORRECCION:no esta correctamente calculado el impuesto.
                    pago.Importe = monto + ((electrico.PorcentajeImpuesto/100)+1);
                }
                if (servicio is Comunicacion comu)
                {
                    pago.Importe = monto + comu.CalcularPorcentaje(servicio.Zona);
                }
                pago.DniPagador = dniPagador;
                pago.Id = Pagos.Count;
                pago.IdServicio = idServicio;
                
                
                Pagos.Add(pago);

                //CORRECCION: Mal asignada la propiedad Fallo
                return new Response { Fallo = false, Comentario = "Pago Realizado y saldo de Proveedor actualizado." };
            }
            return new Response { Fallo = false, Comentario = "Servicio no encontrado." };
        }

        public Response RetirosAProveedor(int idProveedor, double monto)
        {
            var prov = Proveedores.Find(x => x.Id == idProveedor);
            if(prov != null)
            {
                if(prov.Saldo > monto)
                {
                    prov.Saldo -= monto;

                    //CORRECCION: Mal asignado la propiedad fallo.
                    return new Response { Fallo = false, Comentario="Retiro de dinero Exitoso." };
                }
                return new Response {Fallo= false, Comentario="Saldo del proveedor insuficiente." };
                
            }
            return new Response { Fallo = false, Comentario = "Proveedor no encontrado por ese id ingresado." };
        }

        public List<string> Reporte(Opera pais)
        {
            
            var listado = Proveedores.Where(x=>x.Operacion == pais).ToList();
            if(listado.Any())
            {
                List<string> listadoFinal = new List<string>();
                foreach(var provSelectPais in listado)
                {
                    foreach(var serv in Servicios)
                    {
                        if( provSelectPais.Id == serv.IdProveedor)
                        {
                            var cant = Pagos.Count(e=>e.IdServicio== serv.IdProveedor);
                            //CORRECCION: Esto se debia resolver en la clase base.
                            listadoFinal.Add($"La entidad {serv.Nombre} esta gestionada por {provSelectPais.Nombre} y tuvo {cant} pagos realizados en total.");
                        }
                    }
                }
                return listadoFinal;
            }
            return null;
        }

    }
}
