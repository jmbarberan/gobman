using Intelligob.Utiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Intelligob.Escritorio.ModeloVista.General
{
    public partial class ConsultasListaVM : BaseMV<Escritorio.Vistas.Interfaces.IPagina>
    {
        private ObservableCollection<Cliente.Modelos.Consulta> lConsultas = new ObservableCollection<Cliente.Modelos.Consulta>();
        public ObservableCollection<Cliente.Modelos.Consulta> LConsultas
        {
            get { return this.lConsultas; }
            set { this.lConsultas = value; OnPropertyChanged("LConsulta"); }
        }

        private Cliente.Modelos.Consulta seleccionado;
        public Cliente.Modelos.Consulta Seleccionado
        {
            get { return this.seleccionado; }
            set { this.seleccionado = value; OnPropertyChanged("Seleccionado"); this.BarraEstado = "Seleccionado: " + seleccionado.Nombre; }
        }

        private string barraEstado = "Listo";
        public String BarraEstado
        {
            get { return this.barraEstado; }
            set { this.barraEstado = value; OnPropertyChanged("BarraEstado"); }
        }

        public ICommand CmdConsultar
        { get; internal set; }

        public ICommand CmdRegresar
        { get; internal set; }

        public ICommand CmdAdelante 
        { get; internal set; }

        public ConsultasListaVM(List<Cliente.Modelos.Consulta> lconsultas) : this(lconsultas, new Escritorio.Vistas.General.ConsultasLista()) { }

        public ConsultasListaVM(List<Cliente.Modelos.Consulta> lconsultas, Escritorio.Vistas.Interfaces.IPagina vista) : base(vista)
        {
            this.LConsultas = new ObservableCollection<Cliente.Modelos.Consulta>(lconsultas);
            this.CmdRegresar = new Comandos.ComandoDelegado((o) => RegresarAccion(), (o) => RegresarHabilita());
            this.CmdConsultar = new Comandos.ComandoDelegado((o) => ConsultarAccion(), (o) => ConsultarHabilita());
            this.CmdAdelante = new Comandos.ComandoDelegado((o) => AdelantarAccion(), (o) => AdelantarHabilita());
        }

        #region Acciones comandos
        public void RegresarAccion()
        {
            Escritorio.Vistas.General.Navegador.NavigationService.GoBack();
        }

        public void AdelantarAccion()
        {
            Escritorio.Vistas.General.Navegador.NavigationService.GoForward();
        }

        public void ConsultarAccion()
        {         
            var o = Activator.CreateInstance(Seleccionado.TipoConsulta);
            try
            {
                Escritorio.Vistas.General.Navegador.NavigationService.Navigate(((Escritorio.ModeloVista.BaseMV<Escritorio.Vistas.Interfaces.IPagina>)o).Vista);
            }
            catch (Exception ex)
            {
                CuadroMensajes.Alertar("Atencion", "Consulta no disponible", ex.Message, "");
            }
        }
        #endregion

        #region Habilitadores de comandos
        public Boolean RegresarHabilita()
        {
            return Escritorio.Vistas.General.Navegador.NavigationService.CanGoBack;
        }

        public Boolean AdelantarHabilita()
        {
            return Escritorio.Vistas.General.Navegador.NavigationService.CanGoForward;
        }

        public Boolean ConsultarHabilita()
        {
            return this.Seleccionado != null;
        }
        #endregion
    }
}
