using Intelligob.Cliente;
using Intelligob.Cliente.Depositos;
using Intelligob.Escritorio.ModeloVista.Comandos;
using Intelligob.Escritorio.ModeloVista.Emisiones;
using Intelligob.Escritorio.ModeloVista.Recaudaciones;
using Intelligob.Escritorio.Vistas.General;
using Intelligob.Escritorio.Vistas.Interfaces;
using Intelligob.Escritorio.Vistas.Recaudaciones;
using System;
using System.Linq;
using System.Windows.Input;

namespace Intelligob.Escritorio.ModeloVista
{
    public class MenuRecaudacionesVM : BaseMV<IPagina>
    {
        private readonly SeguridadDep seguridadDep = new SeguridadDep();

        private bool prvCobros;
        private bool prvCajas;
        private bool prvContribuyentes;
        private bool prvReversiones;
        private bool prvHabilitantes;
        private bool prvComprobantes;
        private bool prvMercado;

        public ICommand CmdCobros
        { get; internal set; }

        public ICommand CmdCajas
        { get; internal set; }

        public ICommand CmdContribuyentes
        { get; internal set; }

        public ICommand CmdReversiones
        { get; internal set; }

        public ICommand CmdHabilitantes
        { get; internal set; }

        public ICommand CmdComprobantes
        { get; internal set; }

        public ICommand CmdMercado
        { get; internal set; }

        public ICommand CmdInformes
        { get; internal set; }

        public MenuRecaudacionesVM() : this(new MenuRecaudaciones()) { }

        public MenuRecaudacionesVM(IPagina vista) : base(vista)
        {
            ProcesarPrivilegios();
            this.CmdCobros = new ComandoDelegado((o) => this.AccionCobro(), (o) => HabilitaCobro());
            this.CmdContribuyentes = new ComandoDelegado((o) => AccionContribuyente(), (o) => HabilitaContrbuyentes());
            this.CmdCajas = new ComandoDelegado((o) => AccionCajas(), (o) => HabilitaCajas());
            this.CmdReversiones = new ComandoDelegado((o) => AccionReversion(), (o) => HabilitaReversiones());
            this.CmdHabilitantes = new ComandoDelegado((o) => AccionTransaccion(), (o) => HabilitaTransacciones());
            this.CmdInformes = new ComandoDelegado((o) => AccionInformes());
            this.CmdComprobantes = new ComandoDelegado((o) => AccionComprobantes(), (o) => HabilitaComprobantes());
            this.CmdMercado = new Comandos.ComandoDelegado((o) => MercadoAccion(), (o) => HabilitaMercado());
        }

        private void ProcesarPrivilegios()
        {
            this.prvCobros = false;
            this.prvReversiones = false;
            this.prvContribuyentes = false;            
            this.prvHabilitantes = false;
            this.prvCajas = false;
            this.prvMercado = false;

            if (SesionUtiles.Instance.EsDesarrollador || seguridadDep.PrivilegiosFuncionPorUsuario(12, SesionUtiles.Instance.UsuarioActivo.Id) != null)
                this.prvCobros = true;

            if (SesionUtiles.Instance.EsDesarrollador == true || seguridadDep.PrivilegiosFuncionPorUsuario(21, SesionUtiles.Instance.UsuarioActivo.Id) != null)
                this.prvReversiones = true;

            if (SesionUtiles.Instance.EsDesarrollador == true || seguridadDep.PrivilegiosFuncionPorUsuario(7, SesionUtiles.Instance.UsuarioActivo.Id) != null)
                this.prvContribuyentes = true;

            if (SesionUtiles.Instance.EsDesarrollador == true || seguridadDep.PrivilegiosFuncionPorUsuario(28, SesionUtiles.Instance.UsuarioActivo.Id) != null)
                this.prvHabilitantes = true;

            if (SesionUtiles.Instance.EsDesarrollador == true || seguridadDep.PrivilegiosFuncionPorUsuario(38, SesionUtiles.Instance.UsuarioActivo.Id) != null)
                this.prvCajas = true;

            if (SesionUtiles.Instance.EsDesarrollador == true || seguridadDep.PrivilegiosFuncionPorUsuario(40, SesionUtiles.Instance.UsuarioActivo.Id) != null)
                this.prvComprobantes = true;

            if (SesionUtiles.Instance.EsDesarrollador == true || seguridadDep.PrivilegiosFuncionPorUsuario(44, SesionUtiles.Instance.UsuarioActivo.Id) != null)
                this.prvMercado = true;

        }

        private bool HabilitaCobro()
        {
            return this.prvCobros;
        }

        private bool HabilitaContrbuyentes()
        {
            return this.prvContribuyentes;
        }

        private bool HabilitaReversiones()
        {
            return this.prvReversiones;
        }

        private bool HabilitaCajas()
        {
            return this.prvCajas;
        }

        private bool HabilitaTransacciones()
        {
            return this.prvHabilitantes;
        }
        
        private bool HabilitaComprobantes()
        {
            return this.prvComprobantes;
        }

        private bool HabilitaMercado()
        {
            return this.prvMercado;
        }

        private void AccionCobro()
        {
            CobroConsultaVM c = new CobroConsultaVM();
            Navegador.NavigationService.Navigate(c.Vista);
        }

        private void AccionContribuyente()
        {
            ContribuyentesListaVM c = new ContribuyentesListaVM();
            Navegador.NavigationService.Navigate(c.Vista);
        }

        private void AccionReversion()
        {
            ReversionCobroVM r = new ReversionCobroVM();
            Navegador.NavigationService.Navigate(r.Vista);
        }

        private void AccionCajas()
        {
            CajasListaVM c = new CajasListaVM();
            Navegador.NavigationService.Navigate(c.Vista);
        }

        private void AccionTransaccion()
        {
            HabilitantesListaVM h = new HabilitantesListaVM(3);
            Navegador.NavigationService.Navigate(h.Vista);
        }

        private void AccionInformes()
        {
            General.InformesListaVM il = new General.InformesListaVM(3);
            Navegador.NavigationService.Navigate(il.Vista);
        }

        private void AccionComprobantes()
        {
            Recaudaciones.CajaComprobantesVM cc = new CajaComprobantesVM();
            Navegador.NavigationService.Navigate(cc.Vista);
        }

        private void MercadoAccion()
        {
            MercadoIngresoVM m = new MercadoIngresoVM();
        }
    }
}
