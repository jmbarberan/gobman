using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Intelligob.Escritorio.ModeloVista.Recaudaciones
{
    public class CajaComprobantesVM : BaseMV<Escritorio.Vistas.Interfaces.IPagina>
    {
        private readonly Cliente.Depositos.SeguridadDep seguridadDep = new Cliente.Depositos.SeguridadDep();
        private readonly Cliente.Depositos.TablasDep tablasDep = new Cliente.Depositos.TablasDep();
        private readonly Cliente.Depositos.CajasDep cajasDep = new Cliente.Depositos.CajasDep();
        private bool administradorCajas = false;
        private String barraEstado = "Listo";
        public String BarraEstado
        {
            get { return this.barraEstado; }
            set { this.barraEstado = value; OnPropertyChanged("BarraEstado"); }
        }

        #region Atributos de consulta
        private bool consultaOcupada = false;
        public bool ConsultaOcupada
        {
            get { return this.consultaOcupada; }
            set { this.consultaOcupada = value; OnPropertyChanged("ConsultaOcupada"); }
        }

        private Cliente.Referencia.CajaComprobanteDto comprobanteSeleccionado;
        public Cliente.Referencia.CajaComprobanteDto ComprobanteSeleccionado
        {
            get { return this.comprobanteSeleccionado; }
            set { this.comprobanteSeleccionado = value; OnPropertyChanged("ComprobanteSeleccionado"); }
        }

        private ObservableCollection<Cliente.Referencia.CajaComprobanteDto> lCompprobantes;
        public ObservableCollection<Cliente.Referencia.CajaComprobanteDto> LComprobantes
        {
            get { return this.lCompprobantes; }
            set { this.lCompprobantes = value; OnPropertyChanged("LComprobantes"); }
        }

        private Cliente.Referencia.CajaDto cajaSeleccionada;
        public Cliente.Referencia.CajaDto CajaSeleccionada
        {
            get { return this.cajaSeleccionada; }
            set { this.cajaSeleccionada = value; OnPropertyChanged("CajaSeleccionada"); }
        }

        private ObservableCollection<Cliente.Referencia.CajaDto> lCajas;
        public ObservableCollection<Cliente.Referencia.CajaDto> LCajas
        {
            get { return this.lCajas; }
            set { this.lCajas = value; OnPropertyChanged("LCajas"); }
        }

        private Cliente.Referencia.TablaClaveDto tipoSeleccionado;
        public Cliente.Referencia.TablaClaveDto TipoSeleccionado
        {
            get { return this.tipoSeleccionado; }
            set { this.tipoSeleccionado = value; OnPropertyChanged("TipoSeleccionado"); }
        }

        private ObservableCollection<Cliente.Referencia.TablaClaveDto> lTiposComprobante;
        public ObservableCollection<Cliente.Referencia.TablaClaveDto> LTiposComprobante
        {
            get { return this.lTiposComprobante; }
            set { this.lTiposComprobante = value; OnPropertyChanged("LTiposComprobante"); }
        }

        private DateTime fechaInicio = DateTime.Today;
        public DateTime FechaInicio
        {
            get { return this.fechaInicio; }
            set { this.fechaInicio = value; OnPropertyChanged("FechaInicio"); }
        }

        private DateTime fechaCorte = DateTime.Today;
        public DateTime FechaCorte
        {
            get { return this.fechaCorte; }
            set { this.fechaCorte = value; OnPropertyChanged("FechaCorte"); }
        }

        private int filtro = 0;
        public int Filtro
        {
            get { return this.filtro; }
            set { this.filtro = value; OnPropertyChanged("Filtro"); }
        }

        #endregion

        #region Atributos de comandos

        private bool prvNuevo = false;
        private bool prvModificar = false;
        private bool prvEliminar = false;
        private bool prvRestaurar = false;

        public System.Windows.Input.ICommand CmdBuscar
        { get; internal set; }
        public System.Windows.Input.ICommand CmdNuevo
        { get; internal set; }

        public System.Windows.Input.ICommand CmdModificar
        { get; internal set; }

        public System.Windows.Input.ICommand CmdEliminar
        { get; internal set; }

        public System.Windows.Input.ICommand CmdRestaurar
        { get; internal set; }

        public System.Windows.Input.ICommand CmdRegresar
        { get; internal set; }

        public System.Windows.Input.ICommand CmdAvanzar
        { get; internal set; }

        #endregion

        public CajaComprobantesVM() : base(new Escritorio.Vistas.Recaudaciones.CajaComprobantes())
        {
            this.ProcesarPrivilegios();            
            this.IniciarTablas();
            this.CrearComandos();
        }

        private void IniciarTablas()
        {
            this.LTiposComprobante = new ObservableCollection<Cliente.Referencia.TablaClaveDto>(tablasDep.ClavesPorTabla(25));
            if (this.administradorCajas)
            {
                this.LCajas = new ObservableCollection<Cliente.Referencia.CajaDto>(cajasDep.CajasPorEstado(0));
            }
            else
            {
                List<Cliente.Referencia.CajaDto> cajas = new List<Cliente.Referencia.CajaDto>();
                IEnumerable<Cliente.Referencia.CajasUsuarioDto> cs;
                cs = cajasDep.CajasPorUsuarioEstado(0, 0, true);
                foreach (Cliente.Referencia.CajasUsuarioDto c in cs)
                {
                    if (c.CajaNav != null)
                        cajas.Add(c.CajaNav);
                }
                this.LCajas = new ObservableCollection<Cliente.Referencia.CajaDto>(cajas);
            }
        }

        private void ProcesarPrivilegios()
        {
            prvNuevo = false;
            prvModificar = false;
            prvEliminar = false;
            prvRestaurar = false;

            string c = "";
            if (Cliente.SesionUtiles.Instance.EsDesarrollador)
                this.administradorCajas = true;
            else
            {
                Cliente.Referencia.PrivilegioDto p = seguridadDep.PrivilegiosFuncionPorUsuario(40, Cliente.SesionUtiles.Instance.UsuarioActivo.Id);
                if (p != null && p.Comandos != null)
                    c = p.Comandos;

                // Administrador de cajas
                p = seguridadDep.PrivilegiosFuncionPorUsuario(38, Cliente.SesionUtiles.Instance.UsuarioActivo.Id);
                if (p != null && p.Comandos != null && p.Comandos.Contains("5"))
                {
                    this.administradorCajas = true;
                }
            }
            

            if (Cliente.SesionUtiles.Instance.EsDesarrollador || c.Contains("1"))
                this.prvNuevo = true;
            if (Cliente.SesionUtiles.Instance.EsDesarrollador || c.Contains("2"))
                this.prvModificar = true;
            if (Cliente.SesionUtiles.Instance.EsDesarrollador || c.Contains("3"))
                this.prvEliminar = true;
            if (Cliente.SesionUtiles.Instance.EsDesarrollador || c.Contains("4"))
                this.prvRestaurar = true;
        }

        private bool ComprobanteCreable()
        {
            bool ret = false;
            if (this.TipoSeleccionado != null)
            {
                if (String.IsNullOrWhiteSpace(this.TipoSeleccionado.Codigo))
                    ret = true;
                else
                {
                    if (this.TipoSeleccionado.Codigo != null && this.TipoSeleccionado.Codigo == "DEP")
                        ret = true;
                }
            }
            return ret;
        }

        private void CrearComandos()
        {
            this.CmdBuscar = new Comandos.ComandoDelegado((o) => BuscarAccion(), (o) => BuscarHabilita());
            this.CmdNuevo = new Comandos.ComandoDelegado((o) => NuevoAccion(), (o) => NuevoHabilita());
            this.CmdModificar = new Comandos.ComandoDelegado((o) => ModificarAccion(), (o) => ModificarHabilita());
            this.CmdEliminar = new Comandos.ComandoDelegado((o) => EliminarAccion(), (o) => EliminarHabilita());
            this.CmdRestaurar = new Comandos.ComandoDelegado((o) => RestaurarAccion(), (o) => RestaurarHabilita());
            this.CmdRegresar = new Comandos.ComandoDelegado((o) => RegresarAccion(), (o) => RegresarHabilita());
            this.CmdAvanzar = new Comandos.ComandoDelegado((o) => AdelantarAccion(), (o) => AdelantarHabilita());
        }

        #region Habilitadores de Comandos
        private bool BuscarHabilita()
        {
            bool res = true;
            if (this.ConsultaOcupada)
                res = false;
            else
            {
                if (this.TipoSeleccionado == null)
                    res = false;
                else
                {
                    if (this.CajaSeleccionada == null)
                        res = false;
                }
            }            
            return res;
        }

        private bool NuevoHabilita()
        {
            return !this.ConsultaOcupada && this.prvNuevo && ComprobanteCreable();
        }

        private bool ModificarHabilita()
        {
            return !this.ConsultaOcupada && this.prvModificar && this.ComprobanteSeleccionado != null && this.ComprobanteSeleccionado.Estado == 0;
        }

        private bool EliminarHabilita()
        {
            return !this.ConsultaOcupada && this.prvEliminar && this.ComprobanteSeleccionado != null && this.ComprobanteSeleccionado.Estado == 0;
        }

        private bool RestaurarHabilita()
        {
            return !this.ConsultaOcupada && this.prvRestaurar && this.ComprobanteSeleccionado != null && this.ComprobanteSeleccionado.Estado == 2;
        }

        private bool RegresarHabilita()
        {
            return Escritorio.Vistas.General.Navegador.NavigationService.CanGoBack;
        }

        private bool AdelantarHabilita()
        {
            return Escritorio.Vistas.General.Navegador.NavigationService.CanGoForward;
        }
        #endregion

        #region Acciones de Comandos
        private void BuscarAccion()
        {

        }
        private void NuevoAccion()
        {

        }

        private void ModificarAccion()
        {

        }

        private void EliminarAccion()
        {
            String c = ComprobanteSeleccionado.TipoNav.Denominacion + " No." + ComprobanteSeleccionado.Numero.ToString();
            this.ComprobanteSeleccionado.Estado = 2;
            cajasDep.ComprobanteCajaModificar(this.ComprobanteSeleccionado);
            this.BuscarAccion();
            this.BarraEstado = string.Format("{0} fue eliminado", c);
        }

        private void RestaurarAccion()
        {
            String c = ComprobanteSeleccionado.TipoNav.Denominacion + " No." + ComprobanteSeleccionado.Numero.ToString();
            this.ComprobanteSeleccionado.Estado = 0;
            cajasDep.ComprobanteCajaModificar(this.ComprobanteSeleccionado);
            this.BuscarAccion();
            this.BarraEstado = string.Format("{0} fue eliminado", c);
        }

        private void RegresarAccion()
        {
            Escritorio.Vistas.General.Navegador.NavigationService.GoBack();
        }

        private void AdelantarAccion()
        {
            Escritorio.Vistas.General.Navegador.NavigationService.GoForward();
        }
        #endregion

    }
}
