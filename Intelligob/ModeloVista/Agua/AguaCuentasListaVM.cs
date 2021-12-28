using Intelligob.Cliente;
using Intelligob.Escritorio.ModeloVista.Comandos;
using Intelligob.Cliente.Depositos;
using Intelligob.Cliente.Referencia;
using Intelligob.Escritorio.Vistas;
using Intelligob.Escritorio.Vistas.General;
using Intelligob.Escritorio.Vistas.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Telerik.Reporting;

namespace Intelligob.Escritorio.ModeloVista
{
    public class AguaCuentasListaVM : BaseMV<IPagina>, IDataErrorInfo
    {
        private readonly AguaDep aguaDep;
        private readonly SeguridadDep seguridadDep;
        private readonly TablasDep tablasDep;
        private readonly string codigoSeparador;
        
        private ObservableCollection<AguaPotableDto> lCuentas = new ObservableCollection<AguaPotableDto>();
        public ObservableCollection<AguaPotableDto> LCuentas
        {
            get { return this.lCuentas; }
            set { this.lCuentas = value; OnPropertyChanged("LCuentas"); }
        }
        
        private AguaPotableDto seleccionado;
        public AguaPotableDto Seleccionado
        {
            get { return this.seleccionado; }
            set
            {
                seleccionado = value;
                if (this.seleccionado != null)
                {
                    this.BarraEstado = string.Format("{0} Seleccionado", "[ " + this.Seleccionado.Codigo + " ] " + this.Seleccionado.ContribuyenteNav.Nombres);
                }
                OnPropertyChanged("Seleccionado");
            }

        }
        
        private string barraEstado = "Listo";
        public string BarraEstado
        {
            get { return barraEstado; }
            set
            {
                barraEstado = value;
                OnPropertyChanged("BarraEstado");
            }
        }

        private int tipoBusqueda = 0;
        public int TipoBusqueda
        {
            get { return this.tipoBusqueda; }
            set 
            {
                if (this.tipoBusqueda != value)
                    this.tipoBusqueda = value;
                this.textoBusqueda = String.Empty;
                OnPropertyChanged("TipoBusqueda");
                OnPropertyChanged("TextoBusqueda");
            }
        }

        private int filtro = 0;
        public int Filtro
        {
            get { return this.filtro; }
            set { this.filtro = value; OnPropertyChanged("Filtro"); }
        }

        private string textoBusqueda;
        public string TextoBusqueda
        {
            get { return this.textoBusqueda; }
            set { this.textoBusqueda = value; OnPropertyChanged("TextoBusqueda"); }
        }        

        private ContribuyenteDto buscaContribuyente = null;

        public ContribuyenteDto BuscaContribuyente
        {
            get { return this.buscaContribuyente; }
            set
            {
                this.buscaContribuyente = value;
                OnPropertyChanged("BuscaContribuyente");
            }
        }

        #region Comandos y habilitadores
        private bool nuevo;
        private bool modificar;
        private bool buscar;
        private bool eliminar;
        private bool restaurar;
        private bool suspender;
        private bool imprimir;
        private bool notificaciones;

        public ICommand CmdBuscar
        {
            get;
            internal set;
        }

        public ICommand CmdNuevo
        {
            get;
            internal set;
        }

        public ICommand CmdModificar
        {
            get;
            internal set;
        }

        public ICommand CmdEliminar
        {
            get;
            internal set;
        }

        public ICommand CmdRestaurar
        {
            get;
            internal set;
        }

        public ICommand CmdSuspender
        {
            get;
            internal set;
        }

        public ICommand CmdRegresar
        {
            get;
            internal set;
        }

        public ICommand CmdAdelante { get; internal set; }

        public ICommand CmdImprimir
        {
            get;
            internal set;
        }

        public ICommand CmdNotificaciones
        {
            get;
            internal set;
        }

        public ICommand CmdContribuyente
        {
            get;
            internal set;
        }
        #endregion

        public AguaCuentasListaVM() : this(new AguaCuentasLista(), DepositosControl.Instance.AguaDepositoCrear()) { }

        public AguaCuentasListaVM(AguaDep pClienteWeb) : this(new AguaCuentasLista(), pClienteWeb) { }

        public AguaCuentasListaVM(IPagina pVista, AguaDep pClienteWeb) : base(pVista)
        {
            if (pClienteWeb == null)
                aguaDep = DepositosControl.Instance.AguaDepositoCrear();
            else
                aguaDep = pClienteWeb;
            seguridadDep = DepositosControl.Instance.SeguridadDepositoCrear();
            tablasDep = DepositosControl.Instance.TablasDepositoCrear();
            this.ProcesarPrivilegios();
            this.codigoSeparador = tablasDep.CodigoSeparador;
            this.CmdBuscar = new ComandoDelegado((o) => AccionBuscar(), (o) => HabilitaBuscar());
            this.CmdNuevo = new ComandoDelegado((o) => AccionNuevo(), (o) => HabilitaNuevo());
            this.CmdModificar = new ComandoDelegado((o) => AccionModificar(), (o) => HabilitaModificar());
            this.CmdEliminar = new ComandoDelegado((o) => AccionEliminar(), (o) => HabilitaEliminar());
            this.CmdRestaurar = new ComandoDelegado((o) => AccionRestaurar(), (o) => HabilitaRestaurar());
            this.CmdSuspender = new ComandoDelegado((o) => AccionSuspender(), (o) => HabilitaSuspender());
            this.CmdImprimir = new ComandoDelegado((o) => AccionImprimir(), (o) => HabilitaImprimir());
            this.CmdNotificaciones = new ComandoDelegado((o) => AccionNotificaciones(), (o) => HabilitaNotificaciones());
            this.CmdRegresar = new ComandoDelegado((o) => AccionRegresar(), (o) => HabilitaRegresar());
            this.CmdAdelante = new ComandoDelegado((o) => this.AccionAdelantar(), (o) => this.PuedeAdelantar());
            this.CmdContribuyente = new ComandoDelegado((o) => AccionContribuyente(), (o) => HabilitaContribuyente());
            this.BarraEstado = "Listo para ejecutar busquedas";
        }

        private void ProcesarPrivilegios()
        {
            this.nuevo = false;
            this.modificar = false;
            this.eliminar = false;
            this.restaurar = false;
            this.suspender = false;            
            this.notificaciones = false;
            
            string c = "";
            if (!SesionUtiles.Instance.EsDesarrollador)
            {
                PrivilegioDto p = seguridadDep.PrivilegiosFuncionPorUsuario(8, SesionUtiles.Instance.UsuarioActivo.Id);
                if (p != null && p.Comandos != null)
                {
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
            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("5"))
                this.suspender = true;
            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("6"))
                this.notificaciones = true;
            this.imprimir = true;
        }

        public string Error
        {
            get { return String.Empty; }
        }

        public string this[string pAtributo]
        {
            get
            {
                string error = string.Empty;
                if (pAtributo == "TextoBusqueda")
                {
                    if (this.tipoBusqueda == 0)
                    {
                        if (this.buscaContribuyente == null)
                        {
                            error = "Debe seleccionar un contribuyente";
                        }
                    }
                    else
                    {
                        if (String.IsNullOrWhiteSpace(this.textoBusqueda))
                        {
                            error = "Debe digitar el codigo o sector a buscar";
                        }
                    }
                }
                this.buscar = error == string.Empty;
                return error;
            }
        }

        #region Habilitadores de comandos

        private bool HabilitaBuscar()
        { return this.buscar; }

        private bool HabilitaNuevo()
        {
            return this.nuevo;
        }

        private bool HabilitaModificar()
        { return (this.modificar && this.Seleccionado != null && this.Seleccionado.Estado == 0); }

        private bool HabilitaEliminar()
        { return (this.eliminar && this.Seleccionado != null && this.Seleccionado.Estado <= 1); }

        private bool HabilitaRestaurar()
        {
            string s = "1-2";
            return (this.restaurar && this.Seleccionado != null && s.Contains(this.Seleccionado.Estado.ToString().Trim()) );
        }

        private bool HabilitaSuspender()
        { return (this.suspender && this.Seleccionado != null && this.Seleccionado.Estado == 0); }

        private bool HabilitaImprimir()
        { return (this.imprimir && this.Seleccionado != null); }

        private bool HabilitaNotificaciones()
        { return this.notificaciones; }

        private bool HabilitaRegresar()
        { return Navegador.NavigationService.CanGoBack; }

        private bool PuedeAdelantar()
        {
            return Navegador.NavigationService.CanGoForward;
        }

        private bool HabilitaContribuyente()
        {
            return this.TipoBusqueda == 0;
        }
        
        #endregion

        #region Acciones de comandos

        private void AccionBuscar()
        {
            int mEstado = Filtro;
            if (Filtro == 3)
            {
                mEstado = 9;
            }
            if (TipoBusqueda == 0)
            {
                if (BuscaContribuyente != null && BuscaContribuyente.Id > 0)
                {
                    LCuentas = new ObservableCollection<AguaPotableDto>(aguaDep.CuentasPorContribuyente(BuscaContribuyente.Id, mEstado));
                }
            }
            else
            {
                if (TipoBusqueda == 1)
                {
                    if (TextoBusqueda.Trim().Length > 0)
                    {
                        LCuentas = new ObservableCollection<AguaPotableDto>(aguaDep.CuentasPorCodigo(TextoBusqueda, mEstado, TipoBusqueda));
                    }
                }
                else
                {
                    if (TextoBusqueda.Trim().Length > 0)
                    {
                        string s = this.TextoBusqueda;
                        s = s.Replace(this.codigoSeparador + "0" + this.codigoSeparador, this.codigoSeparador + "%" + this.codigoSeparador);
                        LCuentas = new ObservableCollection<AguaPotableDto>(aguaDep.CuentasPorCodigo(s, mEstado, TipoBusqueda));
                    }
                }
            }
        }

        private void AccionNuevo()
        {
            AguaCuentaEditorVM ce = new AguaCuentaEditorVM();
            if (ce.Modificado)
            {
                //this.LCuentas.Add(ce.ECuenta); -- como el id esta cero al crear mejor dejo que lo busque
                this.BarraEstado = string.Format("{0} Fue creado", "[ " + ce.ECuenta.Codigo + " ] " + ce.ECuenta.ContribuyenteNav.Nombres);
            }
        }

        private void AccionModificar()
        {
            if (this.Seleccionado != null && this.Seleccionado.Id > 0)
            {
                AguaCuentaEditorVM ce = new AguaCuentaEditorVM(this.Seleccionado, this.aguaDep);
                if (ce.Modificado)
                {
                    int index = this.LCuentas.IndexOf(this.Seleccionado);
                    this.LCuentas.Remove(this.Seleccionado);
                    this.LCuentas.Insert(index, ce.ECuenta);
                    this.BarraEstado = string.Format("{0} Fue modificado", "[ " + ce.ECuenta.Codigo + " ] " + ce.ECuenta.ContribuyenteNav.Nombres);
                }
            }
        }

        private void AccionEliminar()
        {
            if (this.Seleccionado != null)
            {
                string c = "[ " + this.Seleccionado.Codigo + " ] " + this.Seleccionado.ContribuyenteNav.Nombres;
                this.Seleccionado.Estado = 2;
                aguaDep.CuentaModificar(this.Seleccionado);
                this.AccionBuscar();
                this.BarraEstado = string.Format("{0} Fue eliminado", c);
            }
            else
            {
                this.BarraEstado = "Seleccione una cuenta de la lista";
            }
        }

        private void AccionRestaurar()
        {
            if (this.Seleccionado != null)
            {
                string c = "[ " + this.Seleccionado.Codigo + " ] " + this.Seleccionado.ContribuyenteNav.Nombres;
                this.Seleccionado.Estado = 0;
                aguaDep.CuentaModificar(this.Seleccionado);
                this.AccionBuscar();
                this.BarraEstado = string.Format("{0} Fue restaurado", c);
            }
            else
            {
                this.BarraEstado = "Seleccione un elemento de la lista";
            }
        }

        private void AccionSuspender()
        {
            if (this.Seleccionado != null)
            {
                string c = "[ " + this.Seleccionado.Codigo + " ] " + this.Seleccionado.ContribuyenteNav.Nombres;
                this.Seleccionado.Estado = 1;
                aguaDep.CuentaModificar(this.Seleccionado);
                this.AccionBuscar();
                this.BarraEstado = string.Format("{0} Fue suspendido", c);
            }
            else
            {
                this.BarraEstado = "Seleccione un elemento de la lista";
            }
        }

        private void AccionImprimir()
        {
            ImpresionAjustesVM iajustes = new ImpresionAjustesVM();
            if (iajustes.Vista.ShowDialog() == true)
            {
                Telerik.Reporting.Processing.ReportProcessor reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();                
                InstanceReportSource rs = new InstanceReportSource();
                Intelligob.Reportes.Agua.AguaCuentaFicha rf = new Intelligob.Reportes.Agua.AguaCuentaFicha();
                rs.ReportDocument = rf;
                rs.Parameters.Add(new Telerik.Reporting.Parameter("pCuentaId", this.Seleccionado.Id));
                if (SesionUtiles.Instance.EsDesarrollador)
                    rs.Parameters.Add(new Telerik.Reporting.Parameter("pUsuario", "Desarrollador"));
                else
                    rs.Parameters.Add(new Telerik.Reporting.Parameter("pUsuario", SesionUtiles.Instance.UsuarioActivo.Nombres));
                rs.Parameters.Add(new Telerik.Reporting.Parameter("pEmpresa", tablasDep.NombreEmpresa));
                reportProcessor.PrintReport(rs, iajustes.AjustesImpresion);
            }
        }

        private void AccionNotificaciones()
        {
            // Imprimir Notificaciones para usuarios
        }

        private void AccionRegresar()
        {
            Navegador.NavigationService.GoBack();
        }

        private void AccionAdelantar()
        {
            Navegador.NavigationService.GoForward();
        }

        private void AccionContribuyente()
        {
            SeleccionarContribuyenteVM sc = new SeleccionarContribuyenteVM();
            if (sc.Vista.DialogResult == true)
            {
                this.BuscaContribuyente = sc.Seleccionado;
                this.TextoBusqueda = sc.Seleccionado.Nombres;
                this.AccionBuscar();
            }
        }

        #endregion

    }
}
