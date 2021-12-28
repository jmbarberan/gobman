using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Intelligob.Cliente.Referencia;

namespace Intelligob.Escritorio.ModeloVista.General
{
    public class ReportesListaVM : BaseMV<Intelligob.Escritorio.Vistas.Interfaces.IPagina>
    {
        readonly Cliente.Depositos.SeguridadDep segd = new Cliente.Depositos.SeguridadDep();
        readonly Cliente.Depositos.RepRecaudacionesDep repd = new Cliente.Depositos.RepRecaudacionesDep();

        private ObservableCollection<Cliente.Referencia.ReporteDto> lReportes;
        public ObservableCollection<Cliente.Referencia.ReporteDto> LReportes
        {
            get { return this.lReportes; }
            set { this.lReportes = value; OnPropertyChanged("LReportes"); }
        }

        private Cliente.Referencia.ReporteDto seleccionado;
        public Cliente.Referencia.ReporteDto Seleccionado
        {
            get { return this.seleccionado; }
            set { this.seleccionado = value; OnPropertyChanged("Seleccionado"); }
        }

        private String barraEstado = "Listo";

        public String BarraEstado
        {
            get { return this.barraEstado; }
            set { this.barraEstado = value; OnPropertyChanged("BarraEstado"); }
        }

        private bool prvNuevo = false;
        private bool prvModificar = false;
        private bool prvEliminar = false;

        public ICommand CmdNuevo
        { get; internal set; }

        public ICommand CmdModificar
        { get; internal set; }

        public ICommand CmdEliminar
        { get; internal set; }

        public ICommand CmdAdelante { get; internal set; }

        public ICommand CmdRegresar { get; internal set; }

        public ICommand CmdPlanilla { get; internal set; }

        public ICommand CmdPlanillaParcial { get; internal set; }

        public ICommand CmdPlanillasQuitar { get; internal set; }

        public ReportesListaVM() : base(new Escritorio.Vistas.General.ReportesLista())
        {
            this.TraerReportes();   
            this.ProcesarPrivilegios();
            this.CmdNuevo = new Comandos.ComandoDelegado((o) => NuevoAccion(), (o) => NuevoHabilita());
            this.CmdModificar = new Comandos.ComandoDelegado((o) => ModificarAccion(), (o) => ModificarHabilita());
            this.CmdEliminar = new Comandos.ComandoDelegado((o) => EliminarAccion(), (o) => EliminarHabilita());
            this.CmdAdelante = new Comandos.ComandoDelegado((o) => this.AccionAdelantar(), (o) => this.PuedeAdelantar());
            this.CmdPlanilla = new Comandos.ComandoDelegado((o) => this.MarcarTitulo(), (o) => this.PuedeMarcarTitulo());
            this.CmdPlanillaParcial = new Comandos.ComandoDelegado((o) => this.MarcarTituloParcial(), (o) => this.PuedeMarcarTituloParcial());
            this.CmdPlanillasQuitar = new Comandos.ComandoDelegado((o) => this.PlanillasQuitar());
        }

        private void TraerReportes()
        {
            this.LReportes = new ObservableCollection<Cliente.Referencia.ReporteDto>(repd.ReportesPorModuloEstado(-1, 0));
        }

        private void ProcesarPrivilegios()
        {
            this.prvNuevo = false;
            this.prvModificar = false;
            this.prvEliminar = false;            

            string c = "";
            if (! Cliente.SesionUtiles.Instance.EsDesarrollador)
            {
                PrivilegioDto p = segd.PrivilegiosFuncionPorUsuario(13, Cliente.SesionUtiles.Instance.UsuarioActivo.Id);
                if (p != null && p.Comandos != null)
                    c = p.Comandos;
            }

            if (Cliente.SesionUtiles.Instance.EsDesarrollador || c.Contains("1"))
                this.prvNuevo = true;
            if (Cliente.SesionUtiles.Instance.EsDesarrollador || c.Contains("2"))
                this.prvModificar = true;
            if (Cliente.SesionUtiles.Instance.EsDesarrollador || c.Contains("3"))
                this.prvEliminar = true;
            /*if (Cliente.SesionUtiles.SesionUtiles.Instance.EsDesarrollador || c.Contains("4"))
                this.restaurar = true;*/
        }

        private bool NuevoHabilita()
        {
            return this.prvNuevo;
        }

        private bool ModificarHabilita()
        {
            return this.prvModificar && this.Seleccionado != null;
        }

        private bool EliminarHabilita()
        {
            return this.prvEliminar && this.Seleccionado != null;
        }

        private bool PuedeAdelantar()
        {
            return Escritorio.Vistas.General.Navegador.NavigationService.CanGoForward;
        }

        private bool PuedeRegresar()
        {
            return Escritorio.Vistas.General.Navegador.NavigationService.CanGoBack;
        }

        private bool PuedeMarcarTitulo()
        {
            return this.Seleccionado != null && this.Seleccionado.Definicion != null && this.Seleccionado.Definicion.Length > 0;
        }

        private bool PuedeMarcarTituloParcial()
        {
            return this.Seleccionado != null && this.Seleccionado.Definicion != null && this.Seleccionado.Definicion.Length > 0;
        }

        private void NuevoAccion()
        {
            ReporteEditorVM re = new ReporteEditorVM();            
            if (re.Vista.DialogResult == true)
            {
                this.TraerReportes(); 
            }
        }

        private void ModificarAccion()
        {
            ReporteEditorVM re = new ReporteEditorVM(this.Seleccionado);
            if (re.Vista.DialogResult == true)
            {
                this.TraerReportes();
            }
        }

        private void EliminarAccion()
        {
            this.Seleccionado.Estado = 2;
            repd.ModificarReporte(this.Seleccionado);
            this.TraerReportes();
        }

        private void AccionAdelantar()
        {
            Escritorio.Vistas.General.Navegador.NavigationService.GoForward();
        }

        private void AccionRegresar()
        {
            Escritorio.Vistas.General.Navegador.NavigationService.GoBack();
        }
    
        private void MarcarTitulo()
        {
            Cliente.Depositos.TablasDep dep = new Cliente.Depositos.TablasDep();
            Cliente.Referencia.TablaClaveDto cve = Cliente.ModeloCache.Instance.McClaves.Where(o => o.Tabla == 2 && o.Clave == 10).FirstOrDefault();
            cve.Superior = Seleccionado.Id;
            dep.ModificarClave(cve);

            this.BarraEstado = "Reporte " + this.Seleccionado.Denominacion.Trim() + " Marcado como plantilla de impresion de titulos";
        }

        private void MarcarTituloParcial()
        {
            Cliente.Depositos.TablasDep dep = new Cliente.Depositos.TablasDep();
            Cliente.Referencia.TablaClaveDto cve = Cliente.ModeloCache.Instance.McClaves.Where(o => o.Tabla == 2 && o.Clave == 11).FirstOrDefault();
            cve.Superior = Seleccionado.Id;
            dep.ModificarClave(cve);

            this.BarraEstado = "Reporte " + this.Seleccionado.Denominacion.Trim() + " Marcado como plantilla de impresion de titulos (cobros parciales)";
        }
    
        private void PlanillasQuitar()
        {
            Cliente.Depositos.TablasDep dep = new Cliente.Depositos.TablasDep();
            Cliente.Referencia.TablaClaveDto cvep = Cliente.ModeloCache.Instance.McClaves.Where(o => o.Tabla == 2 && o.Clave == 10).FirstOrDefault();
            cvep.Superior = 0;
            dep.ModificarClave(cvep);

            Cliente.Referencia.TablaClaveDto cve = Cliente.ModeloCache.Instance.McClaves.Where(o => o.Tabla == 2 && o.Clave == 11).FirstOrDefault();
            cve.Superior = 0;
            dep.ModificarClave(cve);

            this.BarraEstado = "Plantillas de impresion desmarcadas";
        }
    }
}
