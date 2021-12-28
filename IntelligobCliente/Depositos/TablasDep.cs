using System;
using System.Collections.Generic;
using System.Linq;
using Intelligob.Cliente.Referencia;

namespace Intelligob.Cliente.Depositos
{
    public class TablasDep : DepositoBase
    {
        public TablasDep() : base(DepositosControl.Instance.Servicio) { }
        public TablasDep(IEntidades servicio)
            : base(servicio)
        { }

        /// <summary>
        /// Traer todas las clave de la db
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TablaClaveDto> ClavesPorEstado(int pEstado)
        {
            if (pEstado == 9)
                return this.Servicio.ReadTablaClaves();
            else
                return this.Servicio.ReadTablaClaves().Where(t => t.Estado == pEstado);
        }

        /// <summary>
        /// Traer todas las tablas filtradas por estado
        /// </summary>
        /// <param name="pEstado">Estado a filtrar (9 = todas)</param>
        /// <returns></returns>
        public virtual IEnumerable<TablaDto> TablasPorEstado(int pEstado)
        {
            if (pEstado == 9)
                return this.Servicio.ReadTablas();
            else
                return this.Servicio.ReadTablas().Where(t => t.Estado == pEstado);
        }

        /// <summary>
        /// Traer el separador de codigo
        /// </summary>
        public virtual string CodigoSeparador
        {
            get { return Servicio.ReadTablaClaves().Where(t => t.Tabla == 2 && t.Clave == 6).FirstOrDefault().Denominacion.Trim(); }
        }

        /// <summary>
        /// Traer el prefijo del codigo
        /// </summary>
        public virtual string CodigoPrefijo
        {
            get { return Servicio.ReadTablaClaves().Where(t => t.Tabla == 2 && t.Clave == 7).FirstOrDefault().Denominacion.Trim(); }
        }

        /// <summary>
        ///  Traer el nombre de la empresa
        /// </summary>
        public virtual string NombreEmpresa
        {
            get { return Servicio.ReadTablaClaves().Where(t => t.Tabla == 2 && t.Clave == 1).FirstOrDefault().Denominacion.Trim(); }
        }

        public virtual string TesoreroNombres
        {
            get { return ModeloCache.Instance.McClaves.Where(t => t.Tabla == 2 && t.Clave == 9).FirstOrDefault().Denominacion.Trim(); }
        }

        /// <summary>
        /// Traer todas las claves de una tabla
        /// </summary>
        /// <param name="pId">Tabla a consultar</param>
        /// <returns></returns>
        public virtual IEnumerable<TablaClaveDto> ClavesPorTabla(int pId)
        {
            return Servicio.ReadTablaClaves().Where(c => c.Estado == 0 && c.Tabla == pId);
        }

        /// <summary>
        /// Traer clave por tabla + clave
        /// </summary>
        /// <param name="pTabla">Tabla a buscar</param>
        /// <param name="pClave">Clave a buscar</param>
        /// <returns></returns>
        public virtual IEnumerable<TablaClaveDto> ClavesPorTablaCve(int pTabla, int pClave)
        {
            return Servicio.ReadTablaClaves().Where(c => c.Estado == 0 && c.Tabla == pTabla && c.Clave == pClave);
        }

        /// <summary>
        /// Traer clave por su id
        /// </summary>
        /// <param name="pId">Id de la clave requerida</param>
        /// <returns></returns>
        public virtual TablaClaveDto ClavePorId(int pId)
        {
            return Servicio.ReadTablaClave(String.Format(FormatoClave, pId));
        }
       
        /// <summary>
        /// Traer claves por tabla + superior
        /// </summary>
        /// <param name="pTabla">Tabla a buscar</param>
        /// <param name="pSuperior">Clave superior</param>
        /// <returns></returns>
        public virtual IEnumerable<TablaClaveDto> ClavesPorJerarquia(int pTabla, int pSuperior)
        {
            return Servicio.ReadTablaClaves().Where(c => c.Estado == 0 && c.Tabla == pTabla && c.Superior == pSuperior);
        }

        public virtual void ModificarClave(TablaClaveDto ptClave)
        {
            this.Servicio.UpdateTablaClave(ptClave);
        }

        public virtual PredioFotoDto LogoX48()
        {
            return this.Servicio.LogoX48();
        }
    
        public virtual ModuloDto ModuloPorId(int? pId)
        {
            if (pId != null)
            {
                return Servicio.ReadModulos().Where(m => m.Id == pId).FirstOrDefault();
            }
            return null;
        }
    }
}
