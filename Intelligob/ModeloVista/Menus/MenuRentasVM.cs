using Intelligob.Cliente;
using Intelligob.Escritorio.ModeloVista.Emisiones;
using Intelligob.Escritorio.Vistas.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Intelligob.Escritorio.ModeloVista
{
    public class MenuRentasVM : BaseMV<Intelligob.Escritorio.Vistas.Interfaces.IPagina>
    {
        private readonly Intelligob.Cliente.Depositos.SeguridadDep seguridadDep = new Cliente.Depositos.SeguridadDep();

        private bool prvBajas = false;
        private bool prvContribuyentes = false;
        private bool prvRubros = false;
        private bool prvEmisiones = false;
        private bool prvIntereses = false;

        public ICommand CmdEmisiones
        { get; internal set; }

        public ICommand CmdBajas
        { get; internal set; }

        public ICommand CmdContribuyentes
        { get; internal set; }

        public ICommand CmdRubros
        { get; internal set; }

        public ICommand CmdIntereses
        { get; internal set; }

        public ICommand CmdTransacciones
        { get; internal set; }

        public ICommand CmdConsultas
        { get; internal set; }

        public ICommand CmdInformes
        { get; internal set; }

        public MenuRentasVM() : this(new Intelligob.Escritorio.Vistas.MenuRentas()) { }

        public MenuRentasVM(Intelligob.Escritorio.Vistas.Interfaces.IPagina pVista) : base(pVista)
        {
            ProcesarPrivilegios();
            this.CmdEmisiones = new Comandos.ComandoDelegado((o) => EmisionesAccion(), (o) => EmisionesHabilita());
            this.CmdBajas = new Comandos.ComandoDelegado((o) => BajasAccion(), (o) => BajasHabilita());
            this.CmdContribuyentes = new Comandos.ComandoDelegado((o) => ContribuyentesAccion(), (o) => ContribuyentesHabilita());
            this.CmdRubros = new Comandos.ComandoDelegado((o) => RubrosAccion(), (o) => RubrosHabilita());
            this.CmdTransacciones = new Comandos.ComandoDelegado((o) => TransaccionesAccion());
            this.CmdConsultas = new Comandos.ComandoDelegado((o) => ConsultasAccion());
            this.CmdInformes = new Comandos.ComandoDelegado((o) => InformesAccion());
            this.CmdIntereses = new Comandos.ComandoDelegado((o) => InteresesAccion(), (o) => InteresesHabilita());
        }

        private void ProcesarPrivilegios()
        {
            this.prvContribuyentes = false;
            this.prvBajas = false;
            this.prvRubros = false;
            this.prvIntereses = false;

            if (SesionUtiles.Instance.EsDesarrollador == true || seguridadDep.PrivilegiosFuncionPorUsuario(7, SesionUtiles.Instance.UsuarioActivo.Id) != null)
                this.prvContribuyentes = true;

            if (SesionUtiles.Instance.EsDesarrollador == true || seguridadDep.PrivilegiosFuncionPorUsuario(23, SesionUtiles.Instance.UsuarioActivo.Id) != null)
                this.prvBajas = true;

            if (SesionUtiles.Instance.EsDesarrollador == true || seguridadDep.PrivilegiosFuncionPorUsuario(24, SesionUtiles.Instance.UsuarioActivo.Id) != null)
                this.prvRubros = true;

            if (SesionUtiles.Instance.EsDesarrollador == true || TieneAccesoConceptos())
                this.prvEmisiones = true;

            if (SesionUtiles.Instance.EsDesarrollador == true || seguridadDep.PrivilegiosFuncionPorUsuario(39, SesionUtiles.Instance.UsuarioActivo.Id) != null)
                this.prvIntereses = true;
        }

        private bool TieneAccesoConceptos()
        {
            bool res = false;
            Cliente.Depositos.ConceptosDep dp = new Cliente.Depositos.ConceptosDep();

            IEnumerable<Cliente.Referencia.ConceptoDto> cs = dp.ConceptosParaEmision();
            if (cs != null && cs.Count() > 0)
                res = true;

            return res;
        }

        private bool BajasHabilita()
        {
            return this.prvBajas;
        }

        private bool ContribuyentesHabilita()
        {
            return this.prvContribuyentes;
        }

        private bool RubrosHabilita()
        {
            return this.prvRubros;
        }

        private bool EmisionesHabilita()
        {
            return this.prvEmisiones;
        }

        private bool InteresesHabilita()
        {
            return this.prvIntereses;
        }

        private void EmisionesAccion()
        {
            EmisionesListaVM emi = new EmisionesListaVM();
            Navegador.NavigationService.Navigate(emi.Vista);
        }

        private void BajasAccion()
        {
            BajasVM bj = new BajasVM();
            Navegador.NavigationService.Navigate(bj.Vista);
        }

        private void ContribuyentesAccion()
        {
            ContribuyentesListaVM c = new ContribuyentesListaVM();
            Navegador.NavigationService.Navigate(c.Vista);
        }

        private void RubrosAccion()
        {
            // TODO Rubros
        }

        private void TransaccionesAccion()
        {
            // TODO Transacciones
        }

        private void ConsultasAccion()
        {
            bool hEmisSal = false;
            bool hResEmis = false;

            if (SesionUtiles.Instance.EsDesarrollador == true || seguridadDep.PrivilegiosFuncionPorUsuario(39, SesionUtiles.Instance.UsuarioActivo.Id) != null)
                hEmisSal = true;
            if (SesionUtiles.Instance.EsDesarrollador == true || seguridadDep.PrivilegiosFuncionPorUsuario(41, SesionUtiles.Instance.UsuarioActivo.Id) != null)
                hResEmis = true;

            List<Cliente.Modelos.Consulta> lc = new List<Cliente.Modelos.Consulta>();

            if (hEmisSal) 
                lc.Add(new Cliente.Modelos.Consulta("Consulta general de emisiones y saldos", "../Imagenes/hojadatosimpresora.png", typeof(RepEmisionesSaldoVM)));

            if (hResEmis)
                lc.Add(new Cliente.Modelos.Consulta("Resumen de emisiones por periodo", "../Imagenes/hojadatosimpresora.png", typeof(RepResumenEmisionesVM)));

            ModeloVista.General.ConsultasListaVM cr = new ModeloVista.General.ConsultasListaVM(lc);
            Navegador.NavigationService.Navigate(cr.Vista);
        }

        private void InformesAccion()
        {
            General.InformesListaVM il = new General.InformesListaVM(5);
            Navegador.NavigationService.Navigate(il.Vista);
        }
    
        private void InteresesAccion()
        {
            InteresesVM inte = new InteresesVM();
            Navegador.NavigationService.Navigate(inte.Vista);
        }
    }

}
