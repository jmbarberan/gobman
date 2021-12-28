using Intelligob.Escritorio.ModeloVista.Comandos;
using Intelligob.Escritorio.Vistas.General;
using Intelligob.Escritorio.Vistas.Interfaces;
using System;
using System.Linq;
using System.Windows.Input;

namespace Intelligob.Escritorio.ModeloVista
{
    public class MenuConfiguracionVM : BaseMV<IPagina>
    {
        private readonly Cliente.Depositos.SeguridadDep seguridadDep = new Cliente.Depositos.SeguridadDep();
        private bool prvReportes;
        public ICommand CmdServidor
        {
            get;
            internal set;
        }

        public ICommand CmdReportes
        { get; internal set; }

        public MenuConfiguracionVM() : this(new MenuConfiguracion()) { }

        public MenuConfiguracionVM(IPagina pVista)
            : base(pVista)
        {
            CmdServidor = new ComandoDelegado((o) => AccionServidor());
            CmdReportes = new ComandoDelegado((o) => AccionReportes(), (o) => HabilitaReporte());
            ProcesarPrivilegios();
        }

        private void ProcesarPrivilegios()
        {
            this.prvReportes = false;

            if (Cliente.SesionUtiles.Instance.EsDesarrollador || seguridadDep.PrivilegiosFuncionPorUsuario(27, Cliente.SesionUtiles.Instance.UsuarioActivo.Id) != null)
                this.prvReportes = true;
        }

        private void AccionServidor()
        {
            ConexionServidorVM cs = new ConexionServidorVM();
            cs.Vista.Owner = App.Current.MainWindow;
            cs.Vista.ShowDialog();
        }

        private bool HabilitaReporte()
        {
            return this.prvReportes;
        }

        private void AccionReportes()
        {
            Escritorio.ModeloVista.General.ReportesListaVM r = new Escritorio.ModeloVista.General.ReportesListaVM();
            Navegador.NavigationService.Navigate(r.Vista);
        }
    }
}
