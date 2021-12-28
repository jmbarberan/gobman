using Intelligob.Escritorio.ModeloVista.Comandos;
using Intelligob.Escritorio.Vistas.General;
using Intelligob.Escritorio.Vistas.Interfaces;
using System.Windows.Input;
using Telerik.Reporting;
using Telerik.Reporting.Configuration;

namespace Intelligob.Escritorio.ModeloVista
{
    public class ReporteVisorVM : BaseMV<IPagina>
    {
        private ReportSource rsInstancia;
        public ReportSource RSInstancia 
        {
            get { return this.rsInstancia; }
            set
            {
                this.rsInstancia = value;
                OnPropertyChanged("RSInstancia");
            }
        }
        
        public ICommand CmdRegresar
        {
            get;
            internal set;
        }

        public ICommand CmdAdelantar
        {
            get;
            internal set;
        }

        public ReporteVisorVM(ReportSource rs) : this(new ReportesVisor(), rs) { }
        public ReporteVisorVM(IPagina vista, ReportSource reporte): base(vista)
        {
            this.RSInstancia = reporte;
            this.CmdRegresar = new ComandoDelegado((o) => AccionRegresar(), (o) => HabilitaRegresar());
            this.CmdAdelantar = new ComandoDelegado((o) => AccionAdelantar(), (o) => HabilitaAdelantar());
        }

        private bool HabilitaAdelantar()
        {
            return Navegador.NavigationService.CanGoForward;
        }

        private bool HabilitaRegresar()
        {
            return Navegador.NavigationService.CanGoBack;
        }

        private void AccionAdelantar()
        {
            Navegador.NavigationService.GoForward();
        }

        private void AccionRegresar()
        {
            Navegador.NavigationService.GoBack();
        }
    }
}
