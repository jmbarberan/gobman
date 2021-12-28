using Intelligob.Cliente;
using Intelligob.Cliente.Depositos;
using Intelligob.Cliente.Referencia;
using Intelligob.Escritorio.ModeloVista.Comandos;
using Intelligob.Escritorio.Vistas.Catastros;
using Intelligob.Escritorio.Vistas.General;
using Intelligob.Escritorio.Vistas.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Intelligob.Escritorio.ModeloVista.Catastros
{
    public class MercadoListaMV : BaseMV<IPagina>
    {
        #region Atributos de la vista

        private bool mostrarModal = false;
        public bool MostrarModal
        {
            get { return mostrarModal; }
            set { mostrarModal = value; OnPropertyChanged("MostrarModal"); }
        }

        private object vistaModal;
        public object VistaModal
        {
            get { return vistaModal; }
            set { vistaModal = value; OnPropertyChanged("VistaModal"); }
        }

        private bool mostrarEliminados = false;
        public bool MostrarEliminados
        {
            get { return mostrarEliminados; }
            set { mostrarEliminados = value; OnPropertyChanged("MostrarEliminados"); }
        }

        private bool mostrarDesocupados = false;
        public bool MostrarDesocupados
        {
            get { return mostrarDesocupados; }
            set { mostrarDesocupados = value; OnPropertyChanged("MostrarDesocupados"); }
        }

        private String barraEstado = String.Empty;
        public String BarraEstado
        {
            get { return barraEstado; }
            set { barraEstado = value; OnPropertyChanged("BarraEstado"); }
        }

        #endregion

        #region Atributos del modelo

        private int busqueda = 0;
        public int Busqueda
        {
            get { return busqueda; }
            set { busqueda = value; OnPropertyChanged("Busqueda"); }
        }

        private MercadoDto seleccionado;
        public MercadoDto Seleccionado
        {
            get { return seleccionado; }
            set { seleccionado = value; OnPropertyChanged("Seleccionado"); }
        }
        
        private ObservableCollection<MercadoDto> lPuestos = new ObservableCollection<MercadoDto>();
        public ObservableCollection<MercadoDto> LPuestos
        {
            get { return lPuestos; }
            set { lPuestos = value; OnPropertyChanged("LPuestos"); }
        }

        #endregion

        #region Comandos

        private bool nuevo = false;
        private bool modificar = false;
        private bool eliminar = false;
        private bool restaurar = false;

        public ICommand CmdBuscar
        { get; internal set; }

        public ICommand CmdNuevo
        { get; internal set; }

        public ICommand CmdModificar
        { get; internal set; }

        public ICommand CmdEliminar
        { get; internal set; }

        public ICommand CmdRestaurar
        { get; internal set; }

        public ICommand CmdDesHabilitar
        { get; internal set; }

        public ICommand CmdRegresar
        { get; internal set; }

        public ICommand CmdAdelante 
        { get; internal set; }

        #endregion

        public MercadoListaMV() : base(new MercadoLista())
        {
            ProcesarPrivilegios();
            CrearComandos();
            Buscar();
        }

        #region Funciones privadas

        private void CrearComandos()
        {
            CmdBuscar = new ComandoDelegado(o => Buscar());
            CmdNuevo = new ComandoDelegado(o => Crear(), o => HabilitaNuevo());
            CmdModificar = new ComandoDelegado(o => Modificar(), o => HabilitaModificar());
            CmdEliminar = new ComandoDelegado(o => Eliminar(), o => HabilitaEliminar());
            CmdRestaurar = new ComandoDelegado(o => Restaurar(), o => HabilitaRestaurar());
            CmdDesHabilitar = new ComandoDelegado(o => DesHabilitar(), o => HabilitaDesHabilitar());
            CmdRegresar = new ComandoDelegado(o => AccionRegresar(), o => HabilitaRegresar());
            CmdAdelante = new ComandoDelegado(o => AccionAdelantar(), o => HabilitaAdelantar());
        }

        private void ProcesarPrivilegios()
        {
            this.nuevo = false;
            this.modificar = false;
            this.eliminar = false;
            this.restaurar = false;

            string c = "";
            if (!SesionUtiles.Instance.EsDesarrollador)
            {
                using(SeguridadDep d = new SeguridadDep())
                {
                    PrivilegioDto p = d.PrivilegiosFuncionPorUsuario(43, SesionUtiles.Instance.UsuarioActivo.Id);
                    if (p != null && p.Comandos != null)
                        c = p.Comandos;
                }
            }

            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("1"))
                this.nuevo = true;
            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("2"))
                this.modificar = true;
            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("3"))
                this.eliminar = true;
            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("4"))
                this.restaurar = true;
        }

        #endregion

        public void Buscar()
        {
            using(CatastrosDep d = new CatastrosDep())
            {
                LPuestos = new ObservableCollection<MercadoDto>(d.MercadoPuestos(MostrarEliminados, MostrarDesocupados, Busqueda));
            }
        }

        #region Acciones de comandos

        private void Crear()
        {
            MercadoDto m = new MercadoDto();
            m.Estado = 0;
            m.Puesto = 0;
            m.Actividad = String.Empty;
            m.Codigo = String.Empty;
            m.Contrato = 0;
            m.Contribuyente = 0;
            m.Observaciones = string.Empty;
            MercadoEditor v = new MercadoEditor();
            VistaModal = v;
            MercadoEditorMV e = new MercadoEditorMV(m, AccionOcultar, v);
            MostrarModal = true;
        }

        private void Modificar()
        {
            MercadoEditor v = new MercadoEditor();
            VistaModal = v;
            MercadoEditorMV e = new MercadoEditorMV(Seleccionado, AccionOcultar, v);
            MostrarModal = true;
        }

        private void Eliminar()
        {
            using(CatastrosDep d = new CatastrosDep())
            {
                d.MercadoAlterarEstado(Seleccionado.Id, 2);
            }
            Buscar();
        }

        private void Restaurar()
        {
            using (CatastrosDep d = new CatastrosDep())
            {
                d.MercadoAlterarEstado(Seleccionado.Id, 0);
            }
            Buscar();
        }

        private void DesHabilitar()
        {
            using (CatastrosDep d = new CatastrosDep())
            {
                if (Seleccionado.Estado == 0)
                    d.MercadoAlterarEstado(Seleccionado.Id, 1);
                else
                    d.MercadoAlterarEstado(Seleccionado.Id, 0);
            }
            Buscar();
        }

        private void AccionRegresar()
        {
            Navegador.NavigationService.GoBack();
        }

        private void AccionAdelantar()
        {
            Escritorio.Vistas.General.Navegador.NavigationService.GoForward();
        }

        public void AccionOcultar()
        {
            MostrarModal = false;
            Buscar();
        }

        #endregion

        #region Habilitadores de comandos

        private bool HabilitaNuevo()
        { return this.nuevo && !MostrarModal; }

        private bool HabilitaModificar()
        { return this.modificar && this.Seleccionado != null && this.Seleccionado.Estado == 0; }

        private bool HabilitaEliminar()
        { return this.eliminar && this.Seleccionado != null && this.Seleccionado.Estado != 2; }

        private bool HabilitaRestaurar()
        { return this.restaurar && this.Seleccionado != null && this.Seleccionado.Estado == 2; }

        private bool HabilitaDesHabilitar()
        { return this.eliminar && this.Seleccionado != null && this.Seleccionado.Estado != 2; }

        private bool HabilitaRegresar()
        { return Navegador.NavigationService.CanGoBack; }

        private bool HabilitaAdelantar()
        { return Navegador.NavigationService.CanGoForward; }

        #endregion
    }
}
