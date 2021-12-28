using Intelligob.Cliente.Depositos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intelligob.Cliente
{
    public class ModeloCache : IModeloCache
    {
        private static IModeloCache instance;

        public static IModeloCache Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ModeloCache();
                }
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        private IEnumerable<Referencia.TablaClaveDto> mcClaves;
        public IEnumerable<Referencia.TablaClaveDto> McClaves
        {
            get 
            {
                if (this.mcClaves == null)
                {
                    using (TablasDep dep = DepositosControl.Instance.TablasDepositoCrear())
                    {
                        this.mcClaves = dep.ClavesPorEstado(9);
                    }
                }
                return this.mcClaves;

            }
        }

        private IEnumerable<Referencia.TablaDto> mcTablas;
        public IEnumerable<Referencia.TablaDto> McTablas
        {
            get 
            {
                if (this.mcTablas == null)
                {
                    using (TablasDep dep = DepositosControl.Instance.TablasDepositoCrear())
                    {
                        this.mcTablas = dep.TablasPorEstado(9);
                    }
                }
                return this.mcTablas;
            }
        }

        private IEnumerable<Referencia.CoeficienteElementoDto> mcCoeficienteElementos;
        public IEnumerable<Referencia.CoeficienteElementoDto> McCoeficienteElementos
        {
            get 
            {
                if (this.mcCoeficienteElementos == null)
                {
                    using (CoeficientesDep dep = DepositosControl.Instance.CoeficientesDepositoCrear())
                    {
                        this.mcCoeficienteElementos = dep.CoeficienteElementosPorEstado(9);
                    }
                }
                return this.mcCoeficienteElementos;
            }            
        }        

        private IEnumerable<Referencia.CoeficienteDto> mcCoeficientes;
        public IEnumerable<Referencia.CoeficienteDto> McCoeficientes
        {
            get 
            {
                if (this.mcCoeficientes == null)
                {
                    using (CoeficientesDep dep = DepositosControl.Instance.CoeficientesDepositoCrear())
                    {
                        this.mcCoeficientes = dep.CoeficientesPorEstado(9);
                    }
                }
                return mcCoeficientes;
            }
        }

        private IEnumerable<Referencia.FuncionDto> mcFunciones;
        public IEnumerable<Referencia.FuncionDto> McFunciones
        {
            get
            {
                if (this.mcFunciones == null)
                {
                    using (SeguridadDep dep = DepositosControl.Instance.SeguridadDepositoCrear())
                    {
                        this.mcFunciones = dep.FuncionesPorEstado(0);
                        foreach (Referencia.FuncionDto f in this.mcFunciones)
                        {
                            f.ComandosNav = this.McComandos.Where(c => c.Funcion == f.Id).ToArray();
                        }
                    }
                }
                return this.mcFunciones;
            }
        }

        private IEnumerable<Referencia.ComandoDto> mcComandos;
        public IEnumerable<Referencia.ComandoDto> McComandos
        {
            get
            {
                if (this.mcComandos == null)
                {
                    using (SeguridadDep dep = DepositosControl.Instance.SeguridadDepositoCrear())
                    { this.mcComandos = dep.ComandosPorEstado(0); }
                }
                return this.mcComandos;
            }
        }

        private IEnumerable<Referencia.ConceptoDto> mcConceptos;
        public IEnumerable<Referencia.ConceptoDto> McConceptos
        {
            get
            {
                if (this.mcConceptos == null)
                {
                    using (ConceptosDep dep = DepositosControl.Instance.ConceptosDepositoCrear())
                    {
                        this.mcConceptos = dep.ConceptosPorEstado(9);
                    }
                }
                return mcConceptos;
            }
        }

        private IEnumerable<Referencia.RubroDto> mRubros;
        public IEnumerable<Referencia.RubroDto> McRubros
        {
            get
            {
                if (this.mRubros == null)
                {
                    using (EmisionesDep dep = new EmisionesDep() )
                    {
                        this.mRubros = dep.RubrosPorEstado(9);
                    }
                }
                return mRubros;
            }
        }

        private IEnumerable<Referencia.RebajaDto> mRebajas;
        public IEnumerable<Referencia.RebajaDto> McRebajas
        {
            get
            {
                if (this.mRebajas == null)
                {
                    using (RecaudacionesDep d = new RecaudacionesDep())
                    {
                        this.mRebajas = d.RebajasPorEstado(9);
                    }
                }
                return mRebajas;
            }
        }
        public void ResetTablaCache(String pTabla)
        {
            switch (pTabla)
            {
                case "mcCoeficienteElementos":
                    {
                        this.mcCoeficienteElementos = null;
                        break;
                    }
            }
        }        
    }
}
