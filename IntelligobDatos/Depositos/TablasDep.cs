using System;
using System.Collections.Generic;
using System.Linq;
using Intelligob.Datos.Contenedores;
using Intelligob.Datos.Referencia1;

namespace Intelligob.Datos.Depositos
{
    public class TablasDep: DepositoBase
    {
        public TablasDep() : this(DepositosControl.Instancia.Servicio) { }

        public TablasDep(IContenedor pConrtrol) : base(pConrtrol) { }

        public string CodigoSeparador
        {
            get { return Servicio.QTablaClaves.Where(t => t.Tabla == 2 && t.Clave == 6).FirstOrDefault().Denominacion.Trim(); }
        }

        public string CodigoPrefijo
        {
            get { return Servicio.QTablaClaves.Where(t => t.Tabla == 2 && t.Clave == 7).FirstOrDefault().Denominacion.Trim(); }
        }

        public string NombreEmpresa
        {
            get { return Servicio.QTablaClaves.Where(t => t.Tabla == 2 && t.Clave == 1).FirstOrDefault().Denominacion.Trim(); }
        }

        /* Traer tabla por ID 
        Integer -> Tabla
        Recibe el ID de una tabla y la retorna como resultado*/
        public Tabla TraerTablaPorId(int pId)
        {
            Tabla tabEncontrada = Servicio.QTablas.Where(t => t.Id == pId).FirstOrDefault();
            return tabEncontrada;
        }

        /* Traer clave por ID 
        Integer -> Clave
        Recibe el ID de una clave y la retorna como resultado*/
        public TablaClave TabTraerClavePorId(int pId)
        {
            TablaClave clvEncontrada = Servicio.QTablaClaves.Where(c => c.Id == pId).FirstOrDefault();
            return clvEncontrada;
        }

        /* Traer claves de tabla
        Integer -> List<Clave>
        Recibe el ID de una tabla y retorna sus claves*/
        public List<TablaClave> TabTraerClavesPorTabla(int pId)
        {
            return Servicio.QTablaClaves.Where(c => c.Estado == 0 && c.Tabla == pId).ToList();
        }

        /* Traer clave por tabla + clave
        Integer, Integer -> Clave
        Recibe el ID de la tabla y el indice de la clave y retorna como resultado la clave encontrada*/
        public List<TablaClave> TabTraerClavesPorTablaCve(int pTabla, int pClave)
        {
            return Servicio.QTablaClaves.Where(c => c.Estado == 0 && c.Tabla == pTabla && c.Clave == pClave).ToList();
        }

        /* Traer claves por tabla + superior
        Ineger, Integer -> Clave
        Recibe el ID de la tabla y el valor de Superior para traer claves por jerarquia */
        public List<TablaClave> TabTraerClavesPorJerarquia(int pTabla, int pSuperior)
        {
            return Servicio.QTablaClaves.Where(c => c.Estado == 0 && c.Tabla == pTabla && c.Superior == pSuperior).ToList();
        }

        /* Traer todas las tablas (filtro de visibilidad)
        Integer -> List<Tablas>
        Recibe un numero para filtrar la visibilidad de las tablas si el filtro es igual a 9 trae todas 
        caso contrario solo trae las coincidencias de el atributo PARTICULAR con el filtro recibido*/
        public List<Tabla> TabTraerTablasTodas(int pVisibilidad)
        {
            if (pVisibilidad == 9)
            {
                return Servicio.QTablas.OrderBy(t => t.Denominacion).ToList();
            }
            else
            {
                return Servicio.QTablas.Where(t => t.Particular == pVisibilidad).OrderBy(t => t.Denominacion).ToList();
            }

        }
    }
}
