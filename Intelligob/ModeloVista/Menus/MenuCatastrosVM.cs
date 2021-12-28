using Intelligob.Cliente;
using Intelligob.Cliente.Depositos;
using Intelligob.Escritorio.ModeloVista.Catastros;
using Intelligob.Escritorio.ModeloVista.Comandos;
using Intelligob.Escritorio.Vistas.Catastros;
using Intelligob.Escritorio.Vistas.General;
using Intelligob.Escritorio.Vistas.Interfaces;
using System.Windows.Input;

namespace Intelligob.Escritorio.ModeloVista
{
    public class MenuCatastrosVM : BaseMV<IPagina>
    {
        private readonly SeguridadDep seguridadDep;
        
        private bool habContribuyentes;
        public ICommand CmdContribuyentes
        {
            get;
            internal set;
        }

        private bool habPredios;
        public ICommand CmdPredios
        {
            get;
            internal set;
        }

        private bool habPatentes;
        public ICommand CmdPatentes
        {
            get;
            internal set;
        }

        private bool habCementerio;
        public ICommand CmdCementerio
        {
            get;
            internal set;
        }

        private bool habMercado;
        public ICommand CmdMercado
        {
            get;
            internal set;
        }

        private bool habHabilitantes;
        public ICommand CmdHabilitantes
        { get; internal set; }

        public ICommand CmdConsultas
        { get; internal set; }

        public ICommand CmdInformes
        { get; internal set; }

        public MenuCatastrosVM() : this(new MenuCatastros()) { }

        public MenuCatastrosVM(IPagina pVista)
            : base(pVista)
        {
            this.seguridadDep = DepositosControl.Instance.SeguridadDepositoCrear();
            this.ProcesarPrivilegios();
            this.CmdContribuyentes = new ComandoDelegado((o) => this.AccionMostrarContribuyentes(), (o) => this.HabilitaContribuyentes());
            this.CmdPredios = new ComandoDelegado((o) => this.AccionPredios(), (o) => this.HabilitaPredios());
            this.CmdPatentes = new ComandoDelegado((o) => this.AccionPatentes(), (o) => this.HabilitaPatentes());
            this.CmdMercado = new ComandoDelegado((o) => AccionMercado(), (o) => HabilitaMercado());
            this.CmdCementerio = new ComandoDelegado((o) => this.AccionCementerio(), (o) => this.HabilitaCementerio());
            this.CmdHabilitantes = new ComandoDelegado((o) => this.AccionHabilitantes(), (o) => this.HabilitaHabilitantes());
            this.CmdConsultas = new ComandoDelegado((o) => this.AccionConsultas());
            this.CmdInformes = new ComandoDelegado((o) => this.AccionInformes());
        }

        private void ProcesarPrivilegios()
        {
            this.habContribuyentes = false;
            this.habPredios = false;
            this.habHabilitantes = false;
            this.habPatentes = false;
            this.habCementerio = false;
            this.habMercado = false;

            if (SesionUtiles.Instance.EsDesarrollador || seguridadDep.PrivilegiosFuncionPorUsuario(7, SesionUtiles.Instance.UsuarioActivo.Id) != null)
                this.habContribuyentes = true;
            
            if (SesionUtiles.Instance.EsDesarrollador || seguridadDep.PrivilegiosFuncionPorUsuario(13, SesionUtiles.Instance.UsuarioActivo.Id) != null)
                this.habPredios = true;

            if (SesionUtiles.Instance.EsDesarrollador || seguridadDep.PrivilegiosFuncionPorUsuario(19, SesionUtiles.Instance.UsuarioActivo.Id) != null)
                this.habPatentes = true;

            if (SesionUtiles.Instance.EsDesarrollador || seguridadDep.PrivilegiosFuncionPorUsuario(20, SesionUtiles.Instance.UsuarioActivo.Id) != null)
                this.habCementerio = true;

            if (SesionUtiles.Instance.EsDesarrollador || seguridadDep.PrivilegiosFuncionPorUsuario(28, SesionUtiles.Instance.UsuarioActivo.Id) != null)
                this.habHabilitantes = true;

            if (SesionUtiles.Instance.EsDesarrollador || seguridadDep.PrivilegiosFuncionPorUsuario(43, SesionUtiles.Instance.UsuarioActivo.Id) != null)
                this.habMercado = true;
        }

        private bool HabilitaPredios()
        {
            return this.habPredios;
        }

        private void AccionPredios()
        {
            PrediosListaVM p = new PrediosListaVM();
            Navegador.NavigationService.Navigate(p.Vista);
        }

        private bool HabilitaPatentes()
        {
            return this.habPatentes;
        }

        private void AccionPatentes()
        {
            PatentesListaVM p = new PatentesListaVM();
            Navegador.NavigationService.Navigate(p.Vista);
        }

        private bool HabilitaMercado()
        {
            return this.habMercado;
        }

        private void AccionMercado()
        {
            MercadoListaMV v = new MercadoListaMV();
            Navegador.NavigationService.Navigate(v.Vista);
        }
        
        private bool HabilitaCementerio()
        {
            return this.habCementerio;
        }

        private void AccionCementerio()
        {
            // TODO Implementar modulo de cementerio
        }

        private bool HabilitaContribuyentes()
        {
            return this.habContribuyentes;
        }

        private void AccionMostrarContribuyentes()
        {
            ContribuyentesListaVM c = new ContribuyentesListaVM();
            Navegador.NavigationService.Navigate(c.Vista);
        }

        private bool HabilitaHabilitantes()
        {
            return this.habHabilitantes;
        }

        private void AccionHabilitantes()
        {
            Recaudaciones.HabilitantesListaVM h = new Recaudaciones.HabilitantesListaVM(2);
            Navegador.NavigationService.Navigate(h.Vista);
        }

        private void AccionConsultas()
        {
            Catastros.PrediosConsultaVM pc = new PrediosConsultaVM();
            Navegador.NavigationService.Navigate(pc.Vista);
        }

        private void AccionInformes()
        {
            General.InformesListaVM il = new General.InformesListaVM(2);
            Navegador.NavigationService.Navigate(il.Vista);
        }
    }
}
