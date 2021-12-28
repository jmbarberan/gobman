using Intelligob.Datos.Referencia1;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intelligob.Datos.Contenedores
{
    public partial class ContenedorDatos : IContenedor
    {
        public IQueryable<AguaPotable> QAguaCuentas
        {
            get { return this.ControlDatos.AguaPotables.Expand("CategoriaNav,ContribuyenteNav,EstadoServicioNav"); }
        }

        public IQueryable<AguaPotable> QAguaCuentasExpandido
        {
            get { return this.ControlDatos.AguaPotables.Expand("CategoriaNav,ContribuyenteNav,FormaPropiedadNav,DominioNav,EstadoServicioNav"); }
        }

        public IQueryable<AguaServicio> QAguaServiciosExpandido
        {
            get { return this.ControlDatos.AguaServicios.Expand("ConceptoNav,EstadoNav"); }
        }

        public IQueryable<AguaLectura> QAguaLecturas
        {
            get { return this.ControlDatos.AguaLecturas; }
        }

        public IQueryable<AguaLectura> QAguaLecturasExpandido
        {
            get { return this.ControlDatos.AguaLecturas.Expand("CuentaAguaNav"); }
        }

        public int AguaServicioCrear(AguaServicio pSrv)
        {
            ControlDatos.AddToAguaServicios(pSrv);
            ControlDatos.SaveChanges();
            return pSrv.Id;
        }

        public int AguaCuentaCrear(AguaPotable pNuevo)
        {
            ControlDatos.AddToAguaPotables(pNuevo);
            ControlDatos.SaveChanges();
            foreach (AguaServicio s in pNuevo.ServiciosNav)
            {
                s.Cuenta = pNuevo.Id;
                this.AguaServicioCrear(s);
            }
            ControlDatos.SaveChanges();
            return pNuevo.Id;
        }

        public void AguaCuentaModificar(AguaPotable pModificado, IEnumerable<AguaServicio> pSrvEliminados)
        {
            List<AguaServicio> srvs = pModificado.ServiciosNav.ToList();
            this.ActualizarEntidad(pModificado);
            this.Guardar();
            // Quitar eliminados
            if (pSrvEliminados.Count() > 0)
            {
                foreach (AguaServicio s in pSrvEliminados)
                {
                    this.EliminarEntidad(s);
                }
            }
            // Insertar y modificar            
            if (srvs.Count() > 0)
            {
                foreach (AguaServicio sm in srvs)
                {
                    if (sm.Id > 0)
                    {
                        this.ActualizarEntidad(sm);
                    }
                    else
                    {
                        sm.Cuenta = pModificado.Id;
                        this.AguaServicioCrear(sm);
                    }
                }
            }
            this.Guardar();
        }
    }
}
