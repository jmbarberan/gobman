using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Intelligob.Cliente;
using Intelligob.Cliente.Depositos;
using Intelligob.Cliente.Referencia;
using Intelligob.Escritorio.ModeloVista.Comandos;
using Intelligob.Escritorio.Vistas;
using Intelligob.Escritorio.Vistas.Interfaces;
using Intelligob.Utiles;

namespace Intelligob.Escritorio.ModeloVista
{
    public class AguaCuentaEditorVM : BaseMV<IVentanaDialogo>, IDataErrorInfo
    {
        public AguaPotableDto ECuenta
        {
            get;
            private set;
        }
        private readonly string lCodigoSeparador;
        private string lCodigoPrefijo;

        public string LCodigoPrefijo
        {
            get { return this.lCodigoPrefijo; }
            set
            {
                this.lCodigoPrefijo = value;
                OnPropertyChanged("LCodigoPrefijo");
            }
        }

        private AguaServicioDto servicioSeleccionado;
        public AguaServicioDto ServicioSeleccionado
        {
            get { return this.servicioSeleccionado; }
            set 
            { 
                this.servicioSeleccionado = value;
                OnPropertyChanged("ServicioSeleccionado");
            }
        }

        public TablaClaveDto ServicioSeleccionadoEstado
        {
            get { return this.ServicioSeleccionado.EstadoNav; }
            set 
            {
                this.ServicioSeleccionado.Estado = value.Id;
                this.ServicioSeleccionado.EstadoNav = value;
                OnPropertyChanged("ServicioSeleccionadoEstado");
            }
        }

        private readonly ObservableCollection<AguaServicioDto> lServiciosEliminados = new ObservableCollection<AguaServicioDto>();
        private ObservableCollection<AguaServicioDto> lServicios;

        public ObservableCollection<AguaServicioDto> LServicios
        {
            get { return lServicios; }
            private set
            {
                this.lServicios = value;
                OnPropertyChanged("LServicios");
            }
        }

        private ObservableCollection<TablaClaveDto> lClasesOcupante;
        public ObservableCollection<TablaClaveDto> LClasesOcupante
        {
            get { return this.lClasesOcupante; }
            set { this.lClasesOcupante = value; OnPropertyChanged("LClasesOcupante"); }
        }

        private ObservableCollection<TablaClaveDto> lEstados;
        public ObservableCollection<TablaClaveDto> LEstados
        {
            get { return this.lEstados; }
            set { this.lEstados = value; OnPropertyChanged("LEstados"); }
        }

        private ObservableCollection<TablaClaveDto> lTiposDominio;
        public ObservableCollection<TablaClaveDto> LTiposDominio
        {
            get { return this.lTiposDominio; }
            set { this.lTiposDominio = value; OnPropertyChanged("LTiposDominio"); }
        }

        private ObservableCollection<CoeficienteElementoDto> lcategorias;
        public ObservableCollection<CoeficienteElementoDto> LCategorias
        {
            get { return lcategorias; }
            set { this.lcategorias = value; OnPropertyChanged("LCategorias"); }
        }        

        private ObservableCollection<CoeficienteElementoDto> lsubcategorias;
        public ObservableCollection<CoeficienteElementoDto> LSubcategorias
        {
            get { return this.lsubcategorias; }
            set { this.lsubcategorias = value; OnPropertyChanged("LSubcategorias"); }
        }

        private readonly AguaDep aguaDep;
        private readonly TablasDep tablasDep;
        private readonly CoeficientesDep coeficientesDep;
        public bool Modificado;

        private bool popCodigoAbierto = false;
        public bool PopCodigoAbierto
        {
            get { return this.popCodigoAbierto; }
            set { this.popCodigoAbierto = value; OnPropertyChanged("PopCodigoAbierto"); }
        }

        #region Comandos

        public ICommand CmdGuardar
        {
            get;
            internal set;
        }

        public ICommand CmdQuitarSrv
        {
            get;
            internal set;
        }

        public ICommand CmdAgregarSrv
        {
            get;
            internal set;
        }

        public ICommand CmdModificarCodigo
        {
            get;
            internal set;
        }

        public ICommand CmdSeleccionarContribuyente
        {
            get;
            internal set;
        }

        public ICommand CmdNuevoContribuyente
        {
            get;
            internal set;
        }

        public ICommand CmdModificarContribuyente
        {
            get;
            internal set;
        }

        public ICommand CmdGuardarCodigo
        {
            get;
            internal set;
        }

        public ICommand CmdCerrarPopCodigo
        {
            get;
            internal set;
        }

        #endregion

        #region Constructores

        public AguaCuentaEditorVM()
            : this(new AguaCuentaEditor(), new AguaPotableDto(), DepositosControl.Instance.AguaDepositoCrear())
        { }

        public AguaCuentaEditorVM(AguaPotableDto pCuenta)
            : this(new AguaCuentaEditor(), pCuenta, DepositosControl.Instance.AguaDepositoCrear())
        { }

        public AguaCuentaEditorVM(AguaDep pClienteWeb)
            : this(new AguaCuentaEditor(), new AguaPotableDto(), pClienteWeb)
        { }

        public AguaCuentaEditorVM(AguaPotableDto pCuenta, AguaDep pClienteWeb)
            : this(new AguaCuentaEditor(), pCuenta, pClienteWeb)
        { }

        public AguaCuentaEditorVM(IVentanaDialogo vista, AguaPotableDto c, AguaDep pClienteWeb)
            : base(vista)
        {
            this.tablasDep = DepositosControl.Instance.TablasDepositoCrear();
            this.lCodigoSeparador = tablasDep.CodigoSeparador;
            if (pClienteWeb == null)
                this.aguaDep = DepositosControl.Instance.AguaDepositoCrear();
            else
                this.aguaDep = pClienteWeb;
            this.ECuenta = c;
            if (c != null && c.Id > 0)
            {
                lServicios = new ObservableCollection<AguaServicioDto>(aguaDep.AguaServiciosPorCuenta(this.ECuenta.Id).ToList());
                foreach (AguaServicioDto s in lServicios)
                {
                    if (s.Estado != null && s.Estado > 0)
                        s.EstadoNav = ModeloCache.Instance.McClaves.Where(es => es.Id == s.Estado).FirstOrDefault();
                }
                string codpre = this.ECuenta.PredioCodigo;
                if (codpre != null && codpre.Trim().Length > 0)
                {
                    String[] cod = codpre.Trim().Split(Convert.ToChar(lCodigoSeparador));
                    if (cod.Length > 3)
                    {
                        this.Zona = Convert.ToInt32(cod[0]);
                        this.Sector = Convert.ToInt32(cod[1]);
                        this.Manzana = Convert.ToInt32(cod[2]);
                        this.Predio = Convert.ToInt32(cod[3]);
                        if (cod.Length > 4)
                        {
                            String div = "";
                            for (int i = 3; i == cod.Length - 1; i++)
                            {
                                if (div.Trim().Length > 0)
                                { div = div + lCodigoSeparador + cod[i]; }
                                else
                                { div = cod[i]; }
                            }
                            this.Division = div;
                        }
                    }
                }
            }
            else
            {
                this.ECuenta.Id = 0;
                this.ECuenta.Codigo = "";
                this.ECuenta.Contribuyente = 0;
                this.ECuenta.Estado = 0;
                this.ECuenta.ServicioEstado = aguaDep.CuentaServicioActivo().Id;
                this.ECuenta.MedidorInstalado = false;
                this.ECuenta.Piscina = false;
                this.ECuenta.PromedioUsar = false;
                this.ECuenta.TanqueElevado = false;
                lServicios = new ObservableCollection<AguaServicioDto>(new List<AguaServicioDto>());
            }            
            this.coeficientesDep = DepositosControl.Instance.CoeficientesDepositoCrear();
            this.CmdGuardar = new ComandoDelegado((o) => AccionGuardar(), (o) => HabilitaGuardar());
            this.CmdQuitarSrv = new ComandoDelegado((o) => AccionQuitarSrv(), (o) => HabilitaQuitarSrv());
            this.CmdAgregarSrv = new ComandoDelegado((o) => AccionAgregarSrv());
            this.CmdModificarCodigo = new ComandoDelegado((o) => AccionModificarCodigo());
            this.CmdSeleccionarContribuyente = new ComandoDelegado((o) => AccionSeleccionContribuyente());
            this.CmdNuevoContribuyente = new ComandoDelegado((o) => AccionNuevoContribuyente());
            this.CmdModificarContribuyente = new ComandoDelegado((o) => AccionModificarContribuyente(), (o) => HabilitaModContribuyente());
            this.CmdCerrarPopCodigo = new ComandoDelegado((o) => AccionCerrarPopCodigo());
            this.CmdGuardarCodigo = new ComandoDelegado((o) => AccionGuardarCodigo(), (o) => HabilitaGuardarCodigo());            
            this.lCodigoPrefijo = tablasDep.CodigoPrefijo;
            this.LEstados = new ObservableCollection<TablaClaveDto>(ModeloCache.Instance.McClaves.Where(e => e.Tabla == 22).ToList());
            this.LClasesOcupante = new ObservableCollection<TablaClaveDto>(ModeloCache.Instance.McClaves.Where(o => o.Tabla == 23).ToList());
            this.LTiposDominio = new ObservableCollection<TablaClaveDto>(ModeloCache.Instance.McClaves.Where(d => d.Tabla == 16));
            LCategorias = new ObservableCollection<CoeficienteElementoDto>(ModeloCache.Instance.McCoeficienteElementos.Where(gl => gl.Coeficiente == 1));
            if (this.Categoria != null)
            {                   
                if (this.ECuenta.Clasificacion != null && this.ECuenta.Clasificacion > 0 && this.Categoria.EnteroDesde > 0)
                    this.Subcategoria = ModeloCache.Instance.McCoeficienteElementos.Where(sub => sub.Coeficiente == this.ECuenta.CategoriaNav.EnteroDesde && sub.Clave == this.ECuenta.Clasificacion).FirstOrDefault();
                this.CargarSubcategorias(Convert.ToInt32(this.Categoria.EnteroDesde));
            }
            
            this.MostrarVista();
        }

        private void MostrarVista()
        {
            Vista.Owner = App.Current.MainWindow;
            Vista.ShowDialog();
        }

        #endregion

        private void CargarSubcategorias(int pCat)
        {
            this.LSubcategorias = new ObservableCollection<CoeficienteElementoDto>(ModeloCache.Instance.McCoeficienteElementos.Where(sc => sc.Coeficiente == pCat));
        }

        #region Atributos del modelo

        public string Codigo
        {
            get { return this.ECuenta.Codigo; }
            set { this.ECuenta.Codigo = value; OnPropertyChanged("Codigo"); }
        }

        public ContribuyenteDto EContribuyente
        {
            get { return this.ECuenta.ContribuyenteNav; }
            set 
            {
                this.ECuenta.ContribuyenteNav = value;
                this.ECuenta.Contribuyente = value.Id;
                OnPropertyChanged("EContribuyente");
                OnPropertyChanged("ContribuyentePresentacion"); 
            }
        }

        public String ContribuyentePresentacion
        {
            get 
            {
                string p = "";
                if (this.EContribuyente != null)
                {
                    string ced = "";
                    if (!String.IsNullOrWhiteSpace(EContribuyente.Cedula))
                        ced = " [" + EContribuyente.Cedula + "]";
                    p = EContribuyente.Nombres + ced;
                }
                return p;
            }
        }

        public CoeficienteElementoDto Categoria
        {
            get { return ECuenta.CategoriaNav; }
            set 
            {
                ECuenta.CategoriaNav = value;
                this.ECuenta.Categoria = value.Id;
                this.Subcategoria = null;
                this.ECuenta.Clasificacion = 0;
                if (value != null && value.EnteroDesde != null && value.EnteroDesde > 0)
                    this.CargarSubcategorias(Convert.ToInt32(value.EnteroDesde));
                OnPropertyChanged("Categoria");
            }
        }        

        public string PredioCodigo
        {
            get { return this.ECuenta.PredioCodigo; }
            set { this.ECuenta.PredioCodigo = value; OnPropertyChanged("PredioCodigo"); }
        }

        public string Barrio
        {
            get { return this.ECuenta.DireccionBarrio; }
            set { this.ECuenta.DireccionBarrio = value; OnPropertyChanged("Barrio"); }
        }

        public string Direccion
        {
            get { return this.ECuenta.Direccion; }
            set { this.ECuenta.Direccion = value; OnPropertyChanged("Direccion"); }
        }

        public TablaClaveDto ClaseOcupante
        {
            get { return this.ECuenta.FormaPropiedadNav; }
            set 
            {
                this.ECuenta.FormaPropiedadNav = value;
                this.ECuenta.FormaPropiedad = value.Id;
                OnPropertyChanged("ClaseOcupante"); 
            }
        }

        public TablaClaveDto TipoDominio
        {
            get { return this.ECuenta.DominioNav; }
            set 
            {
                this.ECuenta.DominioNav = value;
                this.ECuenta.TipoDominio = value.Id;
                OnPropertyChanged("TipoDominio"); 
            }
        }

        public bool? MedidorInstalado
        {
            get { return this.ECuenta.MedidorInstalado; }
            set { this.ECuenta.MedidorInstalado = value; OnPropertyChanged("MedidorInstalado"); }
        }

        public string MedidorCodigo
        {
            get { return this.ECuenta.MedidorCodigo; }
            set { this.ECuenta.MedidorCodigo = value; OnPropertyChanged("MedidorCodigo"); }
        }

        public double? ConexionDiametro
        {
            get { return this.ECuenta.MedidorDiametro; }
            set { this.ECuenta.MedidorDiametro = value; OnPropertyChanged("ConexionDiametro"); }
        }

        public DateTime? FechaInstalacion
        {
            get { return this.ECuenta.MedidorFecha; }
            set { this.ECuenta.MedidorFecha = value; OnPropertyChanged("FechaInstalacion"); }
        }

        public string Reservorio
        {
            get { return this.ECuenta.Reservorio; }
            set { this.ECuenta.Reservorio = value; OnPropertyChanged("Reservorio"); }
        }

        public bool? TienePiscina
        {
            get { return this.ECuenta.Piscina; }
            set { this.ECuenta.Piscina = value; OnPropertyChanged("TienePiscina"); }
        }

        public bool? TanqueElevado
        {
            get { return this.ECuenta.TanqueElevado; }
            set { this.ECuenta.TanqueElevado = value; OnPropertyChanged("TanqueElevado"); }
        }

        public bool? PromedioUsar
        {
            get { return this.ECuenta.PromedioUsar; }
            set { this.ECuenta.PromedioUsar = value; OnPropertyChanged("PromedioUsar"); }
        }

        public double? PromedioIndividual
        {
            get { return this.ECuenta.PromedioIndividual; }
            set { this.ECuenta.PromedioIndividual = value; OnPropertyChanged("PromedioIndividual"); }
        }
        
        private int zona;
        public int Zona
        {
            get { return this.zona; }
            set { this.zona = value; OnPropertyChanged("Zona"); }
        }

        private int sector;
        public int Sector
        {
            get { return this.sector; }
            set { this.sector = value; OnPropertyChanged("Sector"); }
        }

        private int manzana;
        public int Manzana
        {
            get { return this.manzana; }
            set { this.manzana = value; OnPropertyChanged("Manzana"); }
        }

        private int predio;
        public int Predio
        {
            get { return this.predio; }
            set { this.predio = value; OnPropertyChanged("Predio"); }
        }

        private string division;
        public string Division
        {
            get { return this.division; }
            set { this.division = value; OnPropertyChanged("Division"); }
        }

        private CoeficienteElementoDto subcategoria;
        public CoeficienteElementoDto Subcategoria
        {
            get 
            {                                
                return this.subcategoria;
            }
            set 
            {
                this.ECuenta.Clasificacion = 0;
                this.subcategoria = value;
                if (value != null)
                {
                    this.ECuenta.Clasificacion = value.Clave; 
                    OnPropertyChanged("Subcategoria");
                }
            }
        }

        #endregion

        #region Habilitadores y acciones de comandos

        private bool HabilitaGuardar()
        {
            Boolean res = true;
            if (this.EContribuyente == null || this.EContribuyente.Id <= 0)
                //error = "debe seleccionar el contribuyente";
                res = false;
            else
            {
                if (String.IsNullOrWhiteSpace(this.Codigo))
                    //error = "Digite el codigo de la cuenta";
                    res = false;
                else
                {
                    if (this.Categoria == null || this.Categoria.Id <= 0)
                        res = false;
                    else
                    {
                        if (this.Subcategoria == null || this.Subcategoria.Id <= 0)
                        {
                            res = false;
                        }
                        else
                        {
                            int i = 0;
                            if (this.ECuenta != null && this.ECuenta.Id > 0)
                                i = this.ECuenta.Id;
                            if (aguaDep.CuentaCodigoRegistrado(this.Codigo, i))
                            {
                                //error = "El codigo digitado ya esta registrado en otra cuenta";
                                res = false;
                            }
                        }
                    }
                    
                }
            }
            return res;
        }

        private void AccionGuardar()
        {
            Modificado = true;
            //this.ECuenta.ServiciosNav = LServicios.ToArray();
            int id = this.ECuenta.Id;
            try
            {
                if (id <= 0)
                {
                    id = aguaDep.CuentaCrear(this.ECuenta);
                }
                else
                {
                    aguaDep.CuentaModificar(this.ECuenta);
                }
                foreach(AguaServicioDto srv in LServicios)
                {
                    if (srv.Cuenta <= 0)
                        srv.Cuenta = id;
                }
                aguaDep.CuentaServiciosModificar(LServicios, lServiciosEliminados);
                Modificado = true;
            }
            catch (Exception ex)
            {
                CuadroMensajes.Alertar("No se pudo guardar", "Ocurrio el siguiente error", ex.Message, "");
                Modificado = false;
            }
            if (Modificado)
                CuadroMensajes.Aceptar("Guardar", "Operacion exitosa", "Los cambios se guardaron satisfactoriamente", "", TaskDialogInterop.VistaTaskDialogIcon.Information);
            this.Vista.DialogResult = Modificado;
            this.Vista.Close();      
        }

        private bool HabilitaQuitarSrv()
        {
            return this.ServicioSeleccionado != null;
        }

        private void AccionQuitarSrv()
        {
            if (this.ServicioSeleccionado.Id > 0)
            {
                AguaServicioDto a = this.ServicioSeleccionado;
                this.lServiciosEliminados.Add(a);
            }
            LServicios.Remove(this.ServicioSeleccionado);
        }

        public void AccionAgregarSrv()
        {
            SeleccionarConceptoVM sc = new SeleccionarConceptoVM();
            if (sc.Vista.DialogResult == true)
            {
                ConceptoDto c = sc.Seleccionado;
                if (c.Id > 0)
                {
                    Boolean ins = false;
                    foreach (AguaServicioDto si in LServicios)
                    {
                        if (si.ConceptoNav.Id == c.Id)
                        {
                            ins = true;
                            break;
                        }
                    }
                    if (!ins)
                    {
                        AguaServicioDto s = new AguaServicioDto();
                        s.Id = 0;
                        s.Cuenta = this.ECuenta.Id;
                        s.Concepto = c.Id;
                        s.ConceptoNav = c;
                        s.EstadoNav = aguaDep.CuentaServicioActivo();
                        s.Estado = s.EstadoNav.Id;
                        lServicios.Add(s);                        
                    }
                }
            }
        }

        public void AccionModificarCodigo()
        {
            if (this.PopCodigoAbierto == false)
            {
                this.PopCodigoAbierto = true;
            }
        }

        public void AccionSeleccionContribuyente()
        {
            SeleccionarContribuyenteVM sc = new SeleccionarContribuyenteVM();
            if (sc.Vista.DialogResult == true)
            {
                this.EContribuyente = sc.Seleccionado;
            }
        }

        public void AccionNuevoContribuyente()
        {
            ContribuyenteEditorVM ce = new ContribuyenteEditorVM();
            if (ce.Modificado)
            {
                this.EContribuyente = ce.EContribuyente;
            }
        }

        private bool HabilitaModContribuyente()
        {
            return this.EContribuyente != null;
        }

        public void AccionModificarContribuyente()
        {
            if (this.EContribuyente != null && this.EContribuyente.Id > 0)
            {
                ContribuyenteEditorVM ce = new ContribuyenteEditorVM(this.EContribuyente);
                if (ce.Modificado)
                {
                    this.EContribuyente = ce.EContribuyente;
                }
            } 
        }

        private bool HabilitaGuardarCodigo()
        {
            Boolean res = true;
            String s = this["Zona"];
            if (!String.IsNullOrEmpty(s))
                res = false;
            else
            {
                s = this["Sector"];
                if (!String.IsNullOrEmpty(s))
                    res = false;
                else
                {
                    s = this["Manzana"];
                    if (!String.IsNullOrEmpty(s))
                        res = false;
                    else
                    {
                        s = this["Predio"];
                        if (!String.IsNullOrEmpty(s))
                            res = false;
                    }
                }
            }
            return res;
        }

        private void AccionGuardarCodigo()
        {
            String cod = "";
            String div = "";
            if (this.Division != null && this.Division.Trim().Length > 0)
            {
                div = this.lCodigoSeparador + this.Division.Trim();
            }
            cod = this.Zona.ToString().Trim() + lCodigoSeparador + this.Sector.ToString().Trim() + lCodigoSeparador + this.Manzana.ToString().Trim() + lCodigoSeparador + this.Predio.ToString().Trim() + div;
            this.ECuenta.PredioCodigo = cod;
            this.PopCodigoAbierto = false;
            OnPropertyChanged("PredioCodigo");
        }

        private void AccionCerrarPopCodigo()
        {
            this.PopCodigoAbierto = false;
        }

        #endregion

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string pAtributo]
        {
            get 
            {
                string error = String.Empty;
                switch (pAtributo)
                {
                    case "EContribuyente":
                        {
                            if (this.EContribuyente == null || this.EContribuyente.Id <= 0)
                                error = "debe seleccionar el contribuyente";
                            break;
                        }
                    case "ContribuyentePresentacion":
                        {
                            if (this.EContribuyente == null || this.EContribuyente.Id <= 0)
                                error = "debe seleccionar el contribuyente";
                            break;
                        }
                    case "Codigo":
                        {
                            if (String.IsNullOrWhiteSpace(this.Codigo))
                                error = "Digite el codigo de la cuenta";
                            else
                            {
                                int i = 0;
                                if (this.ECuenta != null && this.ECuenta.Id > 0)
                                    i = this.ECuenta.Id;
                                if (aguaDep.CuentaCodigoRegistrado(this.Codigo, i))
                                {
                                    error = "El codigo digitado ya esta registrado en otra cuenta";
                                }
                            }
                            break;
                        }
                    case "Categoria":
                            {
                                if (this.Categoria == null || this.Categoria.Id <= 0)
                                {
                                    error = "Seleccione la categoria de la cuenta";
                                }
                                break;
                            }
                    case "Subcategoria":
                            {
                                if (this.ECuenta.Clasificacion <= 0 || this.Subcategoria == null || this.Subcategoria.Id <= 0)
                                {
                                    error = "Seleccione la subcategoria de la cuenta";
                                }
                                break;
                            }
                    case "Zona":
                            {
                                if (this.Zona <= 0)
                                {
                                    error = "Digite la Zona";
                                }
                                break;
                            }
                    case "Sector":
                            {
                                if (this.Sector <= 0)
                                {
                                    error = "Digite la Sector";
                                }
                                break;
                            }
                    case "Manzana":
                            {
                                if (this.Manzana <= 0)
                                {
                                    error = "Digite la Manzana";
                                }
                                break;
                            }
                    case "Predio":
                            {
                                if (this.Predio <= 0)
                                {
                                    error = "Digite la Predio";
                                }
                                break;
                            }
                }
                return error;
            }
        }
    }
}
