using Intelligob.Datos.Contenedores;
using Intelligob.Datos.Referencia1;
using System;
using System.Collections.Generic;
using System.Linq;
using Intelligob.Utiles;

namespace Intelligob.Datos.Depositos
{
    public class AguaDep : DepositoBase
    {
        public AguaDep() : this(DepositosControl.Instancia.Servicio) { }

        public AguaDep(IContenedor pServicio)
            : base(pServicio)
        { }

        /// <summary>
        /// Consultar cuentas de agua por estado
        /// </summary>
        /// <param name="pEstado">Filtro de estado</param>
        /// <returns></returns>
        public IEnumerable<AguaPotable> AguaCuentasPorEstado(int pEstado)
        {
            if (pEstado == 9)
                return this.Servicio.QAguaCuentas.AsEnumerable();
            else
                return this.Servicio.QAguaCuentas.Where(a => a.Estado == pEstado).AsEnumerable();
        }

        /// <summary>
        /// Traer las patentes registradas por un contribuyente
        /// </summary>
        /// <param name="pId">Id del contribuyente</param>
        /// <param name="pEstado">Estado para filtrar los registros</param>
        /// <returns>Lista de Cuentas de Agua</returns>
        public IEnumerable<AguaPotable> AguaCuentaPorContribuyente(int pId, int pEstado)
        {
            IEnumerable<AguaPotable> res;
            if (pEstado == 9)
            {
                res = Servicio.QAguaCuentasExpandido.Where(a => a.Contribuyente == pId).AsEnumerable();
            }
            else
            {
                res = Servicio.QAguaCuentasExpandido.Where(a => a.Contribuyente == pId && a.Estado == pEstado).AsEnumerable();
            }
            return res;
        }

        /// <summary>
        /// Traer las patentes cuyo codigo coinciden con la cadena recibida (Patron de codigo)
        /// Se puede usar comodines para traer solo las cuentas de cierto sector/manzana/poligono/etc
        /// </summary>
        /// <param name="pCodigo">Codigo a buscar</param>
        /// <param name="pEstado">Estado para filtrar los registros</param>
        /// <param name="pTipoBusqueda">Criterio de busqueda a aplicar</param>
        /// <returns>Lista de Cuentas de Agua</returns>
        public IEnumerable<AguaPotable> AguaCuentaPorCodigo(String pCodigo, int pEstado, int pTipoBusqueda)
        {
            IEnumerable<AguaPotable> res;
            switch (pTipoBusqueda)
            {
                case 1: // Conteniendo
                    {
                        if (pEstado == 9)
                        {
                            res = Servicio.QAguaCuentasExpandido.Where(a => a.Codigo.Contains(pCodigo)).AsEnumerable();
                        }
                        else
                        {
                            res = Servicio.QAguaCuentasExpandido.Where(a => a.Codigo.Contains(pCodigo) && a.Estado == pEstado).AsEnumerable();
                        }
                        break;
                    }
                case 2: // Expresion regular
                    {
                        if (pEstado == 9)
                        {
                            res = Servicio.QAguaCuentasExpandido.Where(a => a.Codigo.Contains(pCodigo)).ToList();
                        }
                        else
                        {
                            res = Servicio.QAguaCuentasExpandido.Where(a => a.Estado == a.Estado && a.Codigo.Contains(pCodigo)).AsEnumerable();
                        }
                        break;
                    }
                default: // Codigo exacto
                    {
                        if (pEstado == 9)
                        {
                            res = Servicio.QAguaCuentasExpandido.Where(a => a.Codigo == pCodigo).AsEnumerable();
                        }
                        else
                        {
                            res = Servicio.QAguaCuentasExpandido.Where(a => a.Codigo == pCodigo && a.Estado == pEstado).AsEnumerable();
                        }
                        break;
                    }
            }
            return res;
        }

        /// <summary>
        /// Traer una cuenta de Agua por su Id
        /// </summary>
        /// <param name="pId">Id de la cuenta</param>
        /// <returns>AguaPotable</returns>
        public AguaPotable AguaCuentaPorId(int pId)
        {
            return Servicio.QAguaCuentas.Where(a => a.Id == pId).FirstOrDefault();
        }

        /// <summary>
        /// Registrar nueva cuenta de Agua potable
        /// </summary>
        /// <param name="pCta">Registro a guardar</param>
        /// <returns>AguaPotable</returns>
        public int AguaCuentaCrear(AguaPotable pCta)
        {
            return Servicio.AguaCuentaCrear(pCta);
        }

        /// <summary>
        /// Modifca el estado de una cuenta de Agua para señalar si esta eliminado o activo
        /// </summary>
        /// <param name="pId">Id de la cuenta a modificar</param>
        /// <param name="pEstado">Estado al que se actualizara</param>
        public void AguaModificarEstado(AguaPotable cta, int pEstado)
        {
            try
            {
                cta.Estado = pEstado;
                if (pEstado == 1)
                {
                    TablaClave tes = null;
                    try
                    {
                        tes = Servicio.QTablaClaves.Where(t => t.Tabla == 22 && t.Clave == 2).FirstOrDefault();
                    }
                    catch
                    {
                        tes = null;
                    }
                    if (tes != null)
                    {
                        cta.ServicioEstado = tes.Id;
                    }
                }
                else
                {
                    if (pEstado == 0)
                    {
                        TablaClave tes = null;
                        try
                        {
                            tes = AguaCuentaServicioActivo();
                        }
                        catch
                        {
                            tes = null;
                        }
                        if (tes != null)
                        {
                            cta.ServicioEstado = tes.Id;
                        }
                    }
                }
                Servicio.ActualizarEntidad(cta);
                Servicio.Guardar();
            }
            catch (Exception ex)
            {
                CuadroMensajes.Alertar("Operacion incompleta", "Se ha producido el siguiente error:", ex.Message, "");
            }
        }

        /// <summary>
        /// Consulta si el codigo de cuenta ya esta registrado
        /// </summary>
        /// <param name="pCodigo">Codigo a validar</param>
        /// <param name="pId">Id de la cuenta que se guarda</param>
        /// <returns>Logico</returns>
        public Boolean AguaCuentaCodigoRegistrado(String pCodigo, int pId)
        {
            Boolean ret = false;
            List<AguaPotable> ctas = new List<AguaPotable>();
            ctas = Servicio.QAguaCuentas.Where(a => a.Estado == 0 && a.Codigo == pCodigo && a.Id != pId).ToList();
            if (ctas.Count > 0)
            {
                ret = true;
            }
            return ret;
        }

        public IEnumerable<AguaServicio> AguaServiciosPorCuenta(int pCuenta)
        {
            IEnumerable<AguaServicio> ss = Servicio.QAguaServiciosExpandido.Where(s => s.Cuenta == pCuenta);
            return ss.ToList();
        }

        public TablaClave AguaCuentaServicioActivo()
        {
            return Servicio.QTablaClaves.Where(t => t.Tabla == 22 && t.Clave == 0).FirstOrDefault();
        }

    }
}
