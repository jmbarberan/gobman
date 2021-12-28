using Intelligob.Datos.Referencia1;
using Intelligob.Utiles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;

namespace Intelligob.Datos.Contenedores
{
    public partial class ContenedorDatos : IContenedor
    {
        public IQueryable<PredioBase> QPredios
        {
            get { return this.ControlDatos.PredioBases; }
        }
        public IQueryable<PredioTerreno> QTerrenos
        {
            get { return this.ControlDatos.PredioTerrenos.Expand("CalidadSueloNav,RazanteNav,LocManzanaNav"); }
        }
        public IQueryable<PredioBase> QPrediosExtendido
        {
            get { return this.ControlDatos.PredioBases.Expand("ViaMaterialNav,DominioNav,ModoPropiedadNav,TipoPropiedadNav,PreAguaNav,PreAlcantarilladoNav"); }
        }
        public IQueryable<PredioBloque> QPredioBloques
        {
            get { return this.ControlDatos.PredioBloques; }
        }
        public IQueryable<PredioFrente> QPredioFrentes
        {
            get { return this.ControlDatos.PredioFrentes; }
        }
        public IQueryable<PredioPiso> QPredioPisos
        {
            get { return this.ControlDatos.PredioPisos.Expand("ConservacionNav"); }
        }
        public IQueryable<PredioConstruccion> QPredioConstrucciones
        {
            get { return this.ControlDatos.PredioConstrucciones.Expand("ConsElementoNav"); }
        }
        public IQueryable<PredioPropietario> QPredioPropietarios
        {
            get { return this.ControlDatos.PredioPropietarios.Expand("ContribuyenteNav"); }
        }
        public IQueryable<PredioFoto> QPredioFotos
        {
            get { return this.ControlDatos.PredioFotos; }
        }
        public int PredioCrear(PredioBase pre)
        {
            ControlDatos.AddToPredioBases(pre);
            ControlDatos.SaveChanges();
            return pre.Id;
        }        
        
        public void ModificarPredio(PredioBase predio)
        {
            ControlDatos.UpdateObject(predio);
            /*ControlDatos.BeginSaveChanges(
                c =>
                {
                      (c.AsyncState as Modelo).EndSaveChanges(c);
                }, ControlDatos);*/
            ControlDatos.SaveChanges();
        }
    
        public PredioBase PredioPorId(int id)
        {
            return ControlDatos.PredioBases.Where(p => p.Id == id).FirstOrDefault();
        }

        public IEnumerable<PredioBase> PrediosPorCodigo(String pCodigo, int pEstado, TipoBusquedaTexto pTipoBusqueda, int pTipoPredio)
        {
            List<PredioBase> res = new List<PredioBase>();
            if (pTipoPredio == 0)
            {
                switch (pTipoBusqueda)
                {
                    case TipoBusquedaTexto.tbComenzando:
                        {
                            if (pEstado == 9)
                            {
                                res = this.QPredios.Where(p => p.Codigo.Contains(pCodigo)).ToList();
                            }
                            else
                            {
                                res = this.QPredios.Where(p => p.Codigo.Contains(pCodigo) && p.Estado == pEstado).ToList();
                            }
                            break;
                        }
                    case TipoBusquedaTexto.tbConteniendo:
                        {
                            if (pEstado == 9)
                            {
                                res = this.QPredios.Where(p => p.Codigo.StartsWith(pCodigo)).ToList();
                            }
                            else
                            {
                                res = this.QPredios.Where(p => p.Estado == p.Estado && p.Codigo.StartsWith(pCodigo)).ToList();
                            }
                            break;
                        }
                    default: // Codigo exacto
                        {
                            if (pEstado == 9)
                            {
                                res = this.QPredios.Where(p => p.Codigo == pCodigo).ToList();
                            }
                            else
                            {
                                res = this.QPredios.Where(p => p.Codigo == pCodigo && p.Estado == pEstado).ToList();
                            }
                            break;
                        }
                }
            }
            else
            {
                switch (pTipoBusqueda)
                {
                    case TipoBusquedaTexto.tbComenzando:
                        {
                            if (pEstado == 9)
                            {
                                res = this.QPredios.Where(p => p.Codigo.Contains(pCodigo) && p.FormatoCodigo == pTipoPredio).ToList();
                            }
                            else
                            {
                                res = this.QPredios.Where(p => p.Codigo.Contains(pCodigo) && p.FormatoCodigo == pTipoPredio && p.Estado == pEstado).ToList();
                            }
                            break;
                        }
                    case TipoBusquedaTexto.tbConteniendo:
                        {
                            if (pEstado == 9)
                            {
                                res = this.QPredios.Where(p => p.Codigo.StartsWith(pCodigo) && p.FormatoCodigo == pTipoPredio).ToList();
                            }
                            else
                            {
                                res = this.QPredios.Where(p => p.Codigo.StartsWith(pCodigo) && p.FormatoCodigo == pTipoPredio && p.Estado == p.Estado).ToList();
                            }
                            break;
                        }
                    default: // Codigo exacto
                        {
                            if (pEstado == 9)
                            {
                                res = this.QPredios.Where(p => p.Codigo == pCodigo && p.FormatoCodigo == pTipoPredio).ToList();
                            }
                            else
                            {
                                res = this.QPredios.Where(p => p.Codigo == pCodigo && p.FormatoCodigo == pTipoPredio && p.Estado == pEstado).ToList();
                            }
                            break;
                        }
                }
            }
            List<PredioPropietario> lpro = new List<PredioPropietario>();
            foreach (PredioBase pre in res)
            {
                lpro.Clear();
                lpro = this.QPredioPropietarios.Where(p => p.Predio == pre.Id).ToList();
                foreach (PredioPropietario pro in lpro)
                {
                    pre.PropietariosNav.Add(pro);
                }
            }
            return res;
        }
    }
}
