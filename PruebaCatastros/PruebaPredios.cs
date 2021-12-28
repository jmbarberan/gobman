using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PruebaCatastros
{
    [TestClass]
    public class PruebaPredios
    {
        [TestMethod]
        public void ModificarPredio()
        {
            
            Intelligob.Cliente.Depositos.CatastrosDep dep = Intelligob.Cliente.Depositos.DepositosControl.Instance.CatastrosDepositoCrear();
            Intelligob.Cliente.Referencia.PredioBaseDto p = dep.PredioPorId(2);
            p.Observaciones = String.Format("Modificado en  {0}", DateTime.Now);
            p.Estado = 0;
            dep.PredioModificar(p);
            Assert.AreEqual(p.Estado, 0);
        }

        [TestMethod]
        public void TraerPredio()
        {
            long c = Convert.ToInt64("0912639069001");
            Intelligob.Cliente.Depositos.CatastrosDep dep = Intelligob.Cliente.Depositos.DepositosControl.Instance.CatastrosDepositoCrear();
            Intelligob.Cliente.Referencia.PredioBaseDto p = dep.PredioPorId(2);
            Assert.AreEqual(p.Id, 2);
        }

        [TestMethod]
        public void TraerFunciones()
        {
            IEnumerable<Intelligob.Cliente.Referencia.FuncionDto> fun = Intelligob.Cliente.ModeloCache.Instance.McFunciones;
            foreach(Intelligob.Cliente.Referencia.FuncionDto f in fun)
            {
                int i = f.Id;
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TraerModulosUsuario()
        {
            Intelligob.Cliente.Depositos.SeguridadDep dep = new Intelligob.Cliente.Depositos.SeguridadDep();
            IEnumerable<Intelligob.Cliente.Referencia.ModuloUsuarioDto> mods = dep.ModulosPorUsuario(2);
            int i = 0;
            foreach (Intelligob.Cliente.Referencia.ModuloUsuarioDto m in mods)
            {
                string s = m.Boton;
                i = i + 1;
            }
            Assert.IsTrue(i == 2);
        }

        [TestMethod]
        public void TraerDeudaContribuyente()
        {
            Intelligob.Cliente.Depositos.RecaudacionesDep dep = new Intelligob.Cliente.Depositos.RecaudacionesDep();
            IEnumerable<Intelligob.Cliente.Referencia.PlanillaDto> pls = dep.TraerDeudaContribuyente(166);
            int i = 0;
        }

        [TestMethod]
        public void TraerPlanillaCobroParcial()
        {
            Intelligob.Cliente.Depositos.RecaudacionesDep dep = new Intelligob.Cliente.Depositos.RecaudacionesDep();
            Intelligob.Cliente.Modelos.CobroPlanillaPagoParcial c = dep.CobroPlanillaParcial(74274);
            Assert.IsTrue(c.Planilla.Rebajas == 5);
        }
        
        [TestMethod]
        public void TraerInformes()
        {
            Intelligob.Reportes.ManejadorInformes repMan = new Intelligob.Reportes.ManejadorInformes();
            IEnumerable<Intelligob.Cliente.Modelos.Informe> infs = repMan.TraerInformesPorModulo(3);
            Assert.IsTrue(infs != null);
        }

        [TestMethod]
        public void TraerRecaudacionesPorFecha()
        {
            Intelligob.Cliente.Depositos.RepRecaudacionesDep dep = new Intelligob.Cliente.Depositos.RepRecaudacionesDep();
            IEnumerable<Intelligob.Cliente.Referencia.RepRecaudacionesFechaDto> res = dep.ReporteRecaudacionesFecha(new DateTime(2013, 7, 4), new DateTime(2013, 7, 4));
            Assert.IsTrue(res.Count() > 0);
        }

        [TestMethod]
        public void TraerBajasPorFecha()
        {
            Intelligob.Cliente.Depositos.EmisionesDep dep = new Intelligob.Cliente.Depositos.EmisionesDep();
            IEnumerable<Intelligob.Cliente.Referencia.PlanillaDto> ps = dep.PlanillasPorBajas(DateTime.Today, DateTime.Today);
            Assert.IsTrue(ps.Count() == 1);
        }

        [TestMethod]
        public void TraerCarpetaCatastral()
        {
            Intelligob.Cliente.Depositos.EmisionesDep dep = new Intelligob.Cliente.Depositos.EmisionesDep();
            //IEnumerable<Intelligob.Cliente.Referencia.CarpetaCatastralAnualDto> cat = dep.CarpetaCatastralPorAñoConcepto(2014, 1, "codigo");
            IEnumerable<Intelligob.Cliente.Referencia.CarpetaCatastralCorteItem> cat = dep.CarpetaCatastralPorAñoConceptoFecha(2014, 1, "codigo", DateTime.Today);
            Assert.IsTrue(cat.Count() > 0);
        }

        [TestMethod]
        public void ClalcularAvaluoDesdeCadena()
        {
            Double d = 0;
            Intelligob.Cliente.Depositos.CatastrosDep dep = new Intelligob.Cliente.Depositos.CatastrosDep();
            Type tipo = dep.GetType();
            System.Reflection.MethodInfo metodo = tipo.GetMethod("PredioUrbanoCalcularAvaluo");
            object[] par = new object[2];
            par[0] = 699;
            par[1] = false;
            d = Convert.ToDouble(metodo.Invoke(dep, par)); 
            Assert.IsTrue(d > 0);
        }

        [TestMethod]
        public void CalcularPorConcepto()
        {
            Intelligob.Cliente.Depositos.EmisionesDep dep = new Intelligob.Cliente.Depositos.EmisionesDep();
            //IEnumerable<Intelligob.Cliente.Referencia.RubroCalcularConcepto> rubs = dep.CalcularRubrosPorConcepto(1, "133");
            //Assert.IsTrue(rubs.Count() > 0);
        }

        [TestMethod]
        public void EmitirAgua()
        {
            Intelligob.Cliente.Depositos.EmisionesDep dep = new Intelligob.Cliente.Depositos.EmisionesDep();
            List<Intelligob.Cliente.Referencia.ConceptosEmisionDto> pars = new List<Intelligob.Cliente.Referencia.ConceptosEmisionDto>();

            Intelligob.Cliente.Referencia.ConceptosEmisionDto parCta = new Intelligob.Cliente.Referencia.ConceptosEmisionDto();
            parCta.Calcula = 1;
            parCta.Emite = 1;
            parCta.OrigenTipo = 1;
            parCta.TipoDato = 1;
            parCta.Identificador = 99;

            Intelligob.Cliente.Referencia.ConceptosEmisionDto parAño = new Intelligob.Cliente.Referencia.ConceptosEmisionDto();
            parAño.Calcula = 1;
            parAño.Emite = 1;
            parAño.OrigenTipo = 2;
            parAño.TipoDato = 1;
            parAño.Identificador = 2014;

            Intelligob.Cliente.Referencia.ConceptosEmisionDto parMes = new Intelligob.Cliente.Referencia.ConceptosEmisionDto();
            parMes.Calcula = 1;
            parMes.Emite = 1;
            parMes.OrigenTipo = 2;
            parMes.TipoDato = 1;
            parMes.Identificador = 1;

            pars.Add(parCta);
            pars.Add(parAño);
            pars.Add(parMes);

            int i = dep.EmitirTituloPorConcepto(6, pars);
            Assert.IsTrue(i == -1);
        }
        
        [TestMethod]
        public void CrearCodigoRelleno()
        {
            Intelligob.Cliente.Referencia.CarpetaCatastralCorteItem cat = new Intelligob.Cliente.Referencia.CarpetaCatastralCorteItem();
            cat.codigo = "1-2-15-1";
            Assert.IsTrue(cat.CodigoRelleno == "002015001");
        }

        [TestMethod]
        public void ConsultaAvaluos()
        {
            Intelligob.Cliente.Depositos.RepRecaudacionesDep r = new Intelligob.Cliente.Depositos.RepRecaudacionesDep();
            IEnumerable<Intelligob.Cliente.Referencia.BaseImponibleAño> res = r.BaseImponiblePorConceptoCodigo(1, "1-1-1-5");
            Assert.IsTrue(res.Count() > 0);
        }

        [TestMethod]
        public void ProbarReversionParcial()
        {
            Intelligob.Cliente.Depositos.RecaudacionesDep r = new Intelligob.Cliente.Depositos.RecaudacionesDep();
            r.CobroRevertirPorPlanilla(56812, 89876, false, String.Empty);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void DiarioRecaudaciones()
        {
            Intelligob.Cliente.Depositos.RepRecaudacionesDep r = new Intelligob.Cliente.Depositos.RepRecaudacionesDep();
            IEnumerable<Intelligob.Cliente.Referencia.RepRecaudacionesFechaDto> res = r.ReporteRecaudacionesFecha(DateTime.Today, DateTime.Today);
            Assert.IsTrue(res.Count() > 0);
        }

        [TestMethod]
        public void DiarioReversiones()
        {
            Intelligob.Cliente.Depositos.RepRecaudacionesDep r = new Intelligob.Cliente.Depositos.RepRecaudacionesDep();
            IEnumerable<Intelligob.Cliente.Referencia.RepReversionesFechaDto> ls = r.RepReversionesPorFecha(new DateTime(2016, 1, 1), new DateTime(2016, 2, 29));
            Assert.IsTrue(ls.ToList().Count > 0);
        }
    }
}
