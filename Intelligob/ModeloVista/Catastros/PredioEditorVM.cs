using Intelligob.Cliente;
using Intelligob.Cliente.Depositos;
using Intelligob.Cliente.Referencia;
using Intelligob.Escritorio.ModeloVista.Comandos;
using Intelligob.Escritorio.Vistas;
using Intelligob.Escritorio.Vistas.Interfaces;
using Intelligob.Utiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Linq;

namespace Intelligob.Escritorio.ModeloVista
{
    class PredioEditorVM : BaseMV<IVentanaDialogo>, IDataErrorInfo
    {
        #region variables locales

        private readonly CatastrosDep catastrosDep = DepositosControl.Instance.CatastrosDepositoCrear();
        private readonly TablasDep tablasDep = DepositosControl.Instance.TablasDepositoCrear();
        private readonly SeguridadDep seguridadDep = new SeguridadDep();
        private readonly bool iniciando = true;
        public bool Modificado;
        public PredioBaseDto EPredio = new PredioBaseDto();

        private General.DigitalizarVM digitalizador;
        public General.DigitalizarVM Digitalizador
        {
            get { return this.digitalizador; }
            set { this.digitalizador = value; OnPropertyChanged("Digitalizador"); }
        }
    
        private PredioTerrenoDto iTerreno = new PredioTerrenoDto();
        public PredioTerrenoDto ETerreno
        {
            get { return iTerreno; }
            set { iTerreno = value; OnPropertyChanged("ETerreno"); }
        }

        private bool puedeGuardar;
        public bool PuedeGuardar
        {
            get { return this.puedeGuardar; }
            set { this.puedeGuardar = value; OnPropertyChanged("PuedeGuardar"); }
        }

        private int insPisoBloque = 0;
        public int InsPisoBloque
        {
            get { return this.insPisoBloque; }
            set { this.insPisoBloque = value; OnPropertyChanged("InsPisoBloque"); }
        }

        private bool esPisoSeleccionado = false;
        public bool EsPisoSeleccionado
        {
            get { return this.esPisoSeleccionado; }
            set { this.esPisoSeleccionado = value; OnPropertyChanged("EsPisoSeleccionado"); }
        }

        private readonly string codigoSeparador = "";
        private string codigoPrefijo = "";

        public string MCodigoPrefijo
        {
            get { return this.codigoPrefijo; }
            set { this.codigoPrefijo = value; OnPropertyChanged("MCodigoPrefijo"); }
        }

        private bool popCodigoUrbanoAbierto = false;
        public bool PopCodigoUrbanoAbierto
        {
            get { return this.popCodigoUrbanoAbierto; }
            set { this.popCodigoUrbanoAbierto = value; OnPropertyChanged("PopCodigoUrbanoAbierto"); }
        }

        private bool popCodigoRuralAbierto = false;
        public bool PopCodigoRuralAbierto
        {
            get { return this.popCodigoRuralAbierto; }
            set { this.popCodigoRuralAbierto = value; OnPropertyChanged("PopCodigoRuralAbierto"); }
        }

        private bool popDigitalizarAbierto = false;
        public bool PopDigitalizarAbierto 
        {
            get { return popDigitalizarAbierto; }
            set { popDigitalizarAbierto = value; OnPropertyChanged("PopDigitalizarAbierto"); }
        }

        public System.Windows.Visibility VisibilidadRustico
        {
            get 
            {
                if (TipoPredio == 0)
                    return System.Windows.Visibility.Collapsed;
                else
                    return System.Windows.Visibility.Visible;
            }
        }

        public System.Windows.Visibility PropietariosValido
        {
            get
            {
                System.Windows.Visibility v = System.Windows.Visibility.Visible;
                if (LPropietarios.Count > 0)
                {
                    v = System.Windows.Visibility.Hidden;
                }
                return v;
            }
        }

        #region Listas del modelo

        private ObservableCollection<PredioTerrenoDto> lTerrenos = new ObservableCollection<PredioTerrenoDto>();
        public ObservableCollection<PredioTerrenoDto> LTerrenos
        {
            get { return this.lTerrenos; }
            set { this.lTerrenos = value; OnPropertyChanged("LTerrenos"); }
        }


        private ObservableCollection<PredioPropietarioDto> lPropietarios = new ObservableCollection<PredioPropietarioDto>();
        public ObservableCollection<PredioPropietarioDto> LPropietarios
        {
            get { return this.lPropietarios; }
            set { this.lPropietarios = value; OnPropertyChanged("LPropietarios"); }
        }

        private ObservableCollection<PredioFrenteDto> lFrentes = new ObservableCollection<PredioFrenteDto>();
        public ObservableCollection<PredioFrenteDto> LFrentes
        {
            get { return this.lFrentes; }
            set { this.lFrentes = value; OnPropertyChanged("LFrentes"); }
        }

        private ObservableCollection<PredioFotoDto> lFotos = new ObservableCollection<PredioFotoDto>();
        public ObservableCollection<PredioFotoDto> LFotos
        {
            get { return this.lFotos; }
            set { this.lFotos = value; OnPropertyChanged("LFotos"); }
        }

        private ObservableCollection<ConstruccionPlantilla> lComponentes = new ObservableCollection<ConstruccionPlantilla>();
        public ObservableCollection<ConstruccionPlantilla> LComponentes
        {
            get { return this.lComponentes; }
            set { this.lComponentes = value; OnPropertyChanged("LComponentes"); }
        }

        private ObservableCollection<PredioBloqueDto> lBloques = new ObservableCollection<PredioBloqueDto>();
        public ObservableCollection<PredioBloqueDto> LBloques
        {
            get { return this.lBloques; }
            set { this.lBloques = value; OnPropertyChanged("LBloques"); }
        }

        private PredioPropietarioDto propietarioSeleccionado;
        public PredioPropietarioDto PropietarioSeleccionado
        {
            get { return this.propietarioSeleccionado; }
            set { this.propietarioSeleccionado = value; OnPropertyChanged("PropietarioSeleccionado"); }
        }

        private PredioFrenteDto frenteSeleccionado;
        public PredioFrenteDto FrenteSeleccionado
        {
            get { return this.frenteSeleccionado; }
            set { this.frenteSeleccionado = value; OnPropertyChanged("FrenteSeleccionado"); }
        }

        private PredioFotoDto fotoSeleccionada;
        public PredioFotoDto FotoSeleccionada
        {
            get { return this.fotoSeleccionada; }
            set { this.fotoSeleccionada = value; OnPropertyChanged("FotoSeleccionada"); }
        }

        private ConstruccionElemento componenteSeleccionado;
        public ConstruccionElemento ComponenteSeleccionado
        {
            get { return this.componenteSeleccionado; }
            set { this.componenteSeleccionado = value; OnPropertyChanged("ComponenteSeleccionado"); }
        }

        private PredioPisoDto pisoSeleccionado;
        public PredioPisoDto PisoSeleccionado
        {
            get { return this.pisoSeleccionado; }
            set { this.pisoSeleccionado = value; OnPropertyChanged("PisoSeleccionado"); }
        }

        private PredioBloqueDto bloqueSeleccionado;
        public PredioBloqueDto BloqueSeleccionado
        {
            get { return this.bloqueSeleccionado; }
            set { this.bloqueSeleccionado = value; OnPropertyChanged("BloqueSeleccionado"); }
        }

        private PredioConstruccionDto construccionSeleccionada;
        public PredioConstruccionDto ConstruccionSeleccionada
        {
            get { return this.construccionSeleccionada; }
            set { this.construccionSeleccionada = value; OnPropertyChanged("ConstruccionSeleccionada"); }
        }

        #endregion
        
        #endregion        

        #region Declaracion de comandos

        private bool contribuyenteCrearHabilitado;
        private bool contribuyenteModificarHabilitado;
        
        public ICommand CmdModificarCodigo
        {
            get;
            internal set;
        }

        public ICommand CmdGuardarCodigoUrbano
        {
            get;
            internal set;
        }

        public ICommand CmdGuardarCodigoRural
        {
            get;
            internal set;
        }

        public ICommand CmdCerrarCodigoRural
        { get; internal set; }

        public ICommand CmdCerrarCodigoUrbano
        { get; internal set; }

        public ICommand CmdContribuyenteAgregar
        {
            get;
            internal set;
        }

        public ICommand CmdContribuyenteModificar
        {
            get;
            internal set;
        }

        public ICommand CmdContribuyenteRemover
        {
            get;
            internal set;
        }

        public ICommand CmdFrenteAgregar
        {
            get;
            internal set;
        }

        public ICommand CmdFrenteRemover
        {
            get;
            internal set;
        }

        public ICommand CmdBloqueAgregar
        {
            get;
            internal set;
        }

        public ICommand CmdBloqueRemover
        {
            get;
            internal set;
        }

        public ICommand CmdComponenteAgregar
        {
            get;
            internal set;
        }

        public ICommand CmdComponenteRemover
        {
            get;
            internal set;
        }

        public ICommand CmdFotoAgregar
        {
            get;
            internal set;
        }

        public ICommand CmdFotoRemover
        {
            get;
            internal set;
        }

        public ICommand CmdFotoDigitalizar
        { get; internal set; }

        public ICommand CmdGuardar
        { get; internal set; }

        public ICommand CmdContribuyenteCrear
        { get; internal set; }

        public ICommand CmdTerrenoAgregar
        { get; internal set; }

        public ICommand CmdTerrenoQuitar
        { get; internal set; }

        #endregion

        #region Constructores
        public PredioEditorVM() : this(new PredioEditor(), new PredioBaseDto()) { }

        public PredioEditorVM(PredioBaseDto pre) : this(new PredioEditor(), pre) { }

        public PredioEditorVM(IVentanaDialogo vista, PredioBaseDto pre) : base(vista)
        {
            codigoSeparador = tablasDep.CodigoSeparador;
            MCodigoPrefijo = tablasDep.CodigoPrefijo;
            CargarPrivilegios();
            IniciarDatos();
            if (pre != null)
            {
                if (pre.Id > 0)
                {
                    EPredio = pre;
                    IniciarPredioExistente();
                    OnPropertyChanged("PropietariosValido");
                }
                else
                {
                    CrearPredio();
                    OnPropertyChanged("PropietariosValido");
                }
            }
            else
            {
                CrearPredio();
            }
            IniciarComandos();            
            if (App.Current != null && App.Current.MainWindow != null)
                this.Vista.Owner = App.Current.MainWindow;
            ((IVentanaMetodo)this.Vista).Ejecutar();
            this.Vista.ShowDialog();
            iniciando = false;
        }

        #endregion

        #region Metodos internos

        private void IniciarDatos()
        {
            // Traer opciones de combos            
            LPreAgua = ModeloCache.Instance.McClaves.Where(ag => ag.Tabla == 8);
            LPreAlcantarillado = ModeloCache.Instance.McClaves.Where(al => al.Tabla == 9);
            LPreDominio = ModeloCache.Instance.McClaves.Where(d => d.Tabla == 16);
            LPreCondicionTenencia = ModeloCache.Instance.McClaves.Where(t => t.Tabla == 17);
            LPreTipoAsociacion = ModeloCache.Instance.McClaves.Where(a => a.Tabla == 18);
            LTerCalidadSuelo = ModeloCache.Instance.McClaves.Where(s => s.Tabla == 12);
            LTerLocManzana = ModeloCache.Instance.McClaves.Where(m => m.Tabla == 19);
            LTerNivelRazante = ModeloCache.Instance.McClaves.Where(r => r.Tabla == 20);
            LPreViaMaterial = ModeloCache.Instance.McClaves.Where(m => m.Tabla == 11);
            LPisoConservacion = ModeloCache.Instance.McClaves.Where(c => c.Tabla == 13);

            // Armar plantillas de construccion
            IEnumerable<TablaClaveDto> cons = tablasDep.ClavesPorJerarquia(14, 0);
            IEnumerable<TablaClaveDto> cops = new List<TablaClaveDto>();
            IEnumerable<TablaClaveDto> eles = new List<TablaClaveDto>();

            foreach (TablaClaveDto con in cons)
            {
                ConstruccionPlantilla p = new ConstruccionPlantilla();
                p.Denominacion = con.Denominacion;
                cops = tablasDep.ClavesPorJerarquia(14, (int)con.Clave);
                foreach (TablaClaveDto cop in cops)
                {
                    ConstruccionComponente c = new ConstruccionComponente();
                    c.Denominacion = cop.Denominacion.TrimEnd();
                    eles = tablasDep.ClavesPorJerarquia(14, Convert.ToInt32(cop.Clave));
                    foreach (TablaClaveDto ele in eles)
                    {
                        ConstruccionElemento e = new ConstruccionElemento();
                        e.Superior = c.Denominacion.TrimEnd();
                        e.Elemento = ele;
                        c.Elementos.Add(e);
                    }
                    p.Componentes.Add(c);
                }
                LComponentes.Add(p);
            }

            this.Digitalizador = new General.DigitalizarVM(this.OcultarDigitalizar, this.InsertarDigitalizacion);
        }

        private void IniciarComandos()
        {
            this.CmdModificarCodigo = new ComandoDelegado((o) => ModificarCodigo());
            this.CmdContribuyenteCrear = new ComandoDelegado((o) => ContribuyenteCrear(), (o) => ContribuyentePuedeCraer());
            this.CmdContribuyenteRemover = new ComandoDelegado((o) => ContribuyenteRemover(), (o) => ContribuyentePuedeRemover());
            this.CmdContribuyenteAgregar = new ComandoDelegado((o) => ContribuyenteAgregar());
            this.CmdContribuyenteModificar = new ComandoDelegado((o) => ContribuyenteModificar(), (o) => ContribuyentePuedeModificar());
            this.CmdFrenteAgregar = new ComandoDelegado((o) => FrenteAgregar());
            this.CmdFrenteRemover = new ComandoDelegado((o) => FrenteRemover(), (o) => FrentePuedeRemover());
            this.CmdTerrenoAgregar = new ComandoDelegado((o) => TerrenoAgregar());
            this.CmdTerrenoQuitar = new ComandoDelegado((o) => TerrenoQuitar(), (o) => TerrenoPuedeQuitar());
            this.CmdBloqueAgregar = new ComandoDelegado((o) => BloqueAgregar(), (o) => BloquePuedeAgregar());
            this.CmdBloqueRemover = new ComandoDelegado((o) => BloqueRemover(), (o) => BloquePuedeRemover());
            this.CmdComponenteAgregar = new ComandoDelegado((o) => ComponenteAgregar(), (o) => ComponentePuedeAgregar());
            this.CmdComponenteRemover = new ComandoDelegado((o) => ComponenteRemover(), (o) => ComponentePuedeRemover());
            this.CmdFotoAgregar = new ComandoDelegado((o) => FotoAgregar());
            this.CmdFotoRemover = new ComandoDelegado((o) => FotoRemover(), (o) => FotoPuedeRemover());
            this.CmdFotoDigitalizar = new ComandoDelegado((o) => FotoDigitalizar());
            this.CmdGuardarCodigoUrbano = new ComandoDelegado((o) => CodigoUrbanoGuardar(), (o) => CodigoPuedeGuardar());
            this.CmdGuardarCodigoRural = new ComandoDelegado((o) => CodigoRuralGuardar(), (o) => CodigoRuralPuedeGuardar());
            this.CmdCerrarCodigoUrbano = new ComandoDelegado((o) => CodigoUrbanoCerrar());
            this.CmdCerrarCodigoRural = new ComandoDelegado((o) => CodigoRuralCerrar());
            this.CmdGuardar = new ComandoDelegado((o) => Guardar(), (o) => HabilitaGuardar());
        }

        private void CrearPredio()
        {
            EPredio = new PredioBaseDto();            
            EPredio.Id = 0;
            EPredio.FormatoCodigo = 0;
            EPredio.Codigo = "";
            EPredio.PredioAgua = 0;
            EPredio.PredioAlcantarillado = 0;
            EPredio.PredioElectricidad = false;
            EPredio.Escritura = false;
            EPredio.ViaAceras = false;
            EPredio.ViaAlcantarillado = false;
            EPredio.ViaAlumbrado = false;            
            EPredio.DominioNav = null;
            EPredio.ModoPropiedadNav = null;
            EPredio.PreAguaNav = null;
            EPredio.PreAlcantarilladoNav = null;
            EPredio.TipoPropiedadNav = null;
            EPredio.ViaMaterialNav = null;
            EPredio.Estado = 0;
            this.ETerreno = this.CrearTerreno();
            this.LTerrenos.Add(ETerreno);
            // Crear listas vacias
            this.LBloques = new ObservableCollection<PredioBloqueDto>();
            this.LPropietarios = new ObservableCollection<PredioPropietarioDto>();
            this.LFotos = new ObservableCollection<PredioFotoDto>();
            this.LFrentes = new ObservableCollection<PredioFrenteDto>();
            this.Zona = 0;
            this.Sector = 0;
            this.Manzana = 0;
            this.Poligono = 0;
            this.Predio = 0;
            this.Division = "";
            this.InsPisoBloque = 1;
        }

        private void IniciarPredioExistente()
        {
            if (Codigo.Trim().Length > 0)
            {
                String[] cod = Codigo.Trim().Split(Convert.ToChar(codigoSeparador));
                if (TipoPredio == 0)
                {
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
                                { div = div + codigoSeparador + cod[i]; }
                                else
                                { div = cod[i]; }
                            }
                            this.Division = div;
                        }
                    }
                }
                else
                {
                    if (cod.Length > 1)
                    {
                        Zona = Convert.ToInt32(cod[0]);
                        Poligono = Convert.ToInt32(cod[1]);
                        Predio = Convert.ToInt32(cod[2]);
                        if (cod.Length > 3)
                        {
                            String div = "";
                            for (int i = 3; i == cod.Length - 1; i++)
                            {
                                if (div.Trim().Length > 0)
                                { div = div + codigoSeparador + cod[i]; }
                                else
                                { div = cod[i]; }
                            }
                            Division = div;
                        }
                    }
                }
            }
            this.LPropietarios = new ObservableCollection<PredioPropietarioDto>(catastrosDep.PropietariosPorPredio(EPredio.Id));
            this.LTerrenos = new ObservableCollection<PredioTerrenoDto>(catastrosDep.TerrenosPorPredio(EPredio.Id));
            this.ETerreno = LTerrenos.ElementAt(0);
            // Armar bloques / pisos / construcciones
            this.LBloques = new ObservableCollection<PredioBloqueDto>(catastrosDep.BloquesConstruccionPorPredio(EPredio.Id));
            this.LFotos = new ObservableCollection<PredioFotoDto>(catastrosDep.FotosPorPredio(EPredio.Id));
            this.LFrentes = new ObservableCollection<PredioFrenteDto>(catastrosDep.FrentesPorPredio(EPredio.Id));
        }

        private void CargarPrivilegios()
        {
            string c = "";
            if (!SesionUtiles.Instance.EsDesarrollador)
            {
                PrivilegioDto p = seguridadDep.PrivilegiosFuncionPorUsuario(7, SesionUtiles.Instance.UsuarioActivo.Id);
                if (p != null && p.Comandos != null)
                    c = p.Comandos;
            }
            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("1"))
                this.contribuyenteCrearHabilitado = true;
            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("2"))
                this.contribuyenteModificarHabilitado = true;
        }

        private Boolean ComponenteInsertado(int? pSuperior, int? pClave)
        {
            Boolean ret = false;
            if (PisoSeleccionado != null)
            {
                foreach (PredioConstruccionDto pc in PisoSeleccionado.ConstruccionesLista)
                {
                    if (pc.ConsElementoNav.Superior == pSuperior && pc.ConsElementoNav.Clave == pClave)
                    {
                        ret = true;
                        break;
                    }
                }
            }
            return ret;
        }

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
                    case "Codigo":
                        {
                            if (String.IsNullOrWhiteSpace(this.Codigo))
                                error = "Digite el codigo de la cuenta";
                            else
                            {
                                int i = 0;
                                if (this.EPredio != null && this.EPredio.Id > 0)
                                    i = this.EPredio.Id;
                                if (catastrosDep.PredioCodigoRegistrado(this.Codigo, i, TipoPredio))
                                {
                                    error = "El codigo digitado ya esta registrado en otra cuenta";
                                }
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
                            if (TipoPredio == 0)
                            {
                                if (this.Manzana <= 0)
                                {
                                    error = "Digite la Manzana";                                  
                                }
                            }
                            break;
                        }
                    case "Poligono":
                        {
                            if (TipoPredio == 1)
                            {
                                if (this.Poligono <= 0)
                                {
                                    error = "Digite el poligono";
                                }
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

        private PredioTerrenoDto CrearTerreno()
        {
            PredioTerrenoDto t = new PredioTerrenoDto();
            t.Id = 0;
            t.Predio = 0;
            t.Estado = 0;
            t.Frente = 0;
            t.Fondo = 0;
            t.LinderoNorteExtension = 0;
            t.LinderoSurExtension = 0;
            t.LinderoEsteExtension = 0;
            t.LinderoOesteExtension = 0;
            t.Perimetro = 0;
            t.Superficie = 0;
            t.NumeroLados = 0;
            t.NumeroAngulosRectos = 0;
            t.CalidadSueloNav = null;
            t.LocManzanaNav = null;
            t.RazanteNav = null;
            t.ClaseTierra = 3;
            t.ZonaHomogenea = 1;
            return t;
        }

        private void InsertarDigitalizacion(byte[] pArregloFoto)
        {
            if (this.Digitalizador.DigitalizacionCompleta)
            {
                this.Digitalizador.DigitalizacionCompleta = false;
                PredioFotoDto f = new PredioFotoDto();
                f.Estado = 0;
                f.Predio = EPredio.Id;
                f.Indice = 0;
                f.Descripcion = "Digitalizacion";
                f.Foto = pArregloFoto;
                LFotos.Add(f);
            }
        }

        #endregion

        #region Atributos del modelo

        public string Codigo
        {
            get { return this.EPredio.Codigo; }
            set { this.EPredio.Codigo = value; OnPropertyChanged("Codigo"); }
        }

        public string Direccion
        {
            get { return this.EPredio.Direccion; }
            set { this.EPredio.Direccion = value; OnPropertyChanged("Direccion"); }
        }

        public string Ubicacion
        {
            get { return this.EPredio.Ubicacion; }
            set { this.EPredio.Ubicacion = value; OnPropertyChanged("Ubicacion"); }
        }

        public bool? Escrituras
        {
            get { return this.EPredio.Escritura; }
            set { this.EPredio.Escritura = value; OnPropertyChanged("Escrituras"); }
        }

        public string Observaciones
        {
            get { return this.EPredio.Observaciones; }
            set { this.EPredio.Observaciones = value; OnPropertyChanged("Observaciones"); }
        }

        public TablaClaveDto ClaseDominio
        {
            get { return this.EPredio.DominioNav; }
            set { this.EPredio.DominioNav = value; this.EPredio.Dominio = value.Id; OnPropertyChanged("ClaseDominio"); }
        }

        public TablaClaveDto CondicionTenencia
        {
            get { return this.EPredio.ModoPropiedadNav; }
            set { this.EPredio.ModoPropiedadNav = value; this.EPredio.ModoPropiedad = value.Id; OnPropertyChanged("CondicionTenencia"); }
        }

        public TablaClaveDto TipoAsociacion
        {
            get { return this.EPredio.TipoPropiedadNav; }
            set { this.EPredio.TipoPropiedadNav = value; this.EPredio.TipoPropiedad = value.Id; OnPropertyChanged("TipoAsociacion"); }
        }

        public int? TipoPredio
        {
            get 
            {
                int? i = 0;
                if (this.EPredio != null)
                    i = this.EPredio.FormatoCodigo; 
                return i; 
            }
            set 
            {
                this.EPredio.FormatoCodigo = value;
                OnPropertyChanged("TipoPredio");
                OnPropertyChanged("Zona");
                OnPropertyChanged("Sector");
                OnPropertyChanged("Manzana");
                OnPropertyChanged("Poligono");
                OnPropertyChanged("Predio");
            }
        }

        public string NombreInmueble
        {
            get { return this.EPredio.NombreInmueble; }
            set { this.EPredio.NombreInmueble = value; OnPropertyChanged("NombreInmueble"); }
        }

        public bool? PredioElectricidad
        {
            get { return this.EPredio.PredioElectricidad; }
            set { this.EPredio.PredioElectricidad = value; OnPropertyChanged("PredioElectricidad"); }
        }

        public TablaClaveDto PredioAgua
        {
            get { return this.EPredio.PreAguaNav; }
            set { this.EPredio.PreAguaNav = value; this.EPredio.PredioAgua = value.Id ; OnPropertyChanged("PredioAgua"); }
        }

        public TablaClaveDto PredioAlcantarillado
        {
            get { return EPredio.PreAlcantarilladoNav; }
            set { EPredio.PreAlcantarilladoNav = value; EPredio.PredioAlcantarillado = value.Id; OnPropertyChanged("PredioAlcantarillado"); }
        }

        public TablaClaveDto ViaMaterial
        {
            get { return this.EPredio.ViaMaterialNav; }
            set { this.EPredio.ViaMaterialNav = value; EPredio.ViaMaterial = value.Id; OnPropertyChanged("ViaMaterial"); }
        }

        public int? ViaAccesibilidad
        {
            get { return this.EPredio.ViaAccesibilidad; }
            set { this.EPredio.ViaAccesibilidad = value ; OnPropertyChanged("ViaAccesibilidad"); }
        }

        public bool? ViaAlumbrado
        {
            get { return this.EPredio.ViaAlumbrado; }
            set { this.EPredio.ViaAlumbrado = value; OnPropertyChanged("ViaAlumbrado"); }
        }

        public bool? ViaAceras
        {
            get { return this.EPredio.ViaAceras; }
            set { this.EPredio.ViaAceras = value; OnPropertyChanged("ViaAceras"); }
        }

        public bool? ViaAlcantarillado
        {
            get { return this.EPredio.ViaAlcantarillado; }
            set { this.EPredio.ViaAlcantarillado = value; OnPropertyChanged("ViaAlcantarillado"); }
        }

        public TablaClaveDto TerrenoCalidadSuelo
        {
            get { return ETerreno.CalidadSueloNav; }
            set { this.ETerreno.CalidadSueloNav = value; this.ETerreno.CalidadSuelo = value.Id; OnPropertyChanged("TerrenoCalidadSuelo"); }
        }

        public TablaClaveDto TerrenoLocalizacionManzana
        {
            get { return ETerreno.LocManzanaNav; }
            set { this.ETerreno.LocManzanaNav = value; this.ETerreno.LocalizacionManzana = value.Id; OnPropertyChanged("TerrenoLocalizacionManzana"); }
        }

        public TablaClaveDto TerrenoNivelRazante
        {
            get { return this.ETerreno.RazanteNav; }
            set { this.ETerreno.RazanteNav = value; this.ETerreno.NivelRazante = value.Id; OnPropertyChanged("TerrenoNivelRazante"); }
        }

        public double? TerrenoFrente
        {
            get { return this.ETerreno.Frente; }
            set { this.ETerreno.Frente = value; OnPropertyChanged("TerrenoFrente"); }
        }

        public double? TerrenoFondo
        {
            get { return this.ETerreno.Fondo; }
            set { this.ETerreno.Fondo = value; OnPropertyChanged("TerrenoFondo"); }
        }

        public double? TerrenoPerimetro
        {
            get { return this.ETerreno.Perimetro; }
            set { this.ETerreno.Perimetro = value; OnPropertyChanged("TerrenoPerimetro"); }
        }        

        public int? TerrenoNumeroLados
        {
            get { return this.ETerreno.NumeroLados; }
            set { this.ETerreno.NumeroLados = value; OnPropertyChanged("TerrenoNumeroLados"); }
        }

        public int? TerrenoNumeroAngulos
        {
            get { return this.ETerreno.NumeroAngulosRectos; }
            set { this.ETerreno.NumeroAngulosRectos = value; OnPropertyChanged("TerrenoNumeroAngulos"); }
        }

        public string LinderoNorteNombres
        {
            get { return ETerreno.LinderoNorteNombres; }
            set { this.ETerreno.LinderoNorteNombres = value; OnPropertyChanged("LinderoNorteNombres"); }
        }

        public double? LinderoNorteExtension
        {
            get { return ETerreno.LinderoNorteExtension; }
            set { ETerreno.LinderoNorteExtension = value; OnPropertyChanged("LinderoNorteExtension"); }
        }

        public string LinderoSurNombres
        {
            get { return ETerreno.LinderoSurNombres; }
            set { this.ETerreno.LinderoSurNombres = value; OnPropertyChanged("LinderoSurNombres"); }
        }

        public double? LinderoSurExtension
        {
            get { return ETerreno.LinderoSurExtension; }
            set { ETerreno.LinderoSurExtension = value; OnPropertyChanged("LinderoSurExtension"); }
        }

        public string LinderoEsteNombres
        {
            get { return ETerreno.LinderoEsteNombres; }
            set { this.ETerreno.LinderoEsteNombres = value; OnPropertyChanged("LinderoEsteNombres"); }
        }

        public double? LinderoEsteExtension
        {
            get { return ETerreno.LinderoEsteExtension; }
            set { ETerreno.LinderoEsteExtension = value; OnPropertyChanged("LinderoEsteExtension"); }
        }

        public string LinderoOesteNombres
        {
            get { return ETerreno.LinderoOesteNombres; }
            set { this.ETerreno.LinderoOesteNombres = value; OnPropertyChanged("LinderoOesteNombres"); }
        }

        public double? LinderoOesteExtension
        {
            get { return ETerreno.LinderoOesteExtension; }
            set { ETerreno.LinderoOesteExtension = value; OnPropertyChanged("LinderoOesteExtension"); }
        }

        private int zona = 0;
        public int Zona
        {
            get { return this.zona; }
            set { this.zona = value; OnPropertyChanged("Zona"); }
        }

        private int sector = 0;
        public int Sector
        {
            get { return this.sector; }
            set { this.sector = value; OnPropertyChanged("Sector"); }
        }

        private int manzana = 0;
        public int Manzana
        {
            get { return this.manzana; }
            set { this.manzana = value; OnPropertyChanged("Manzana"); }
        }

        private int predio = 0;
        public int Predio
        {
            get { return this.predio; }
            set { this.predio = value; OnPropertyChanged("Predio"); }
        }

        private string division = "";
        public string Division
        {
            get { return this.division; }
            set { this.division = value; OnPropertyChanged("Division"); }
        }

        private int poligono;
        public int Poligono
        {
            get { return this.poligono; }
            set { this.poligono = value; OnPropertyChanged("Poligono"); }
        }

        #endregion

        #region Combos auxiliares
        private IEnumerable<TablaClaveDto> mPreAgua;
        private IEnumerable<TablaClaveDto> mPreAlcantarillado;
        private IEnumerable<TablaClaveDto> mPreModoTenencia;
        private IEnumerable<TablaClaveDto> mPreTipoPropiedad;
        private IEnumerable<TablaClaveDto> mPreDominio;
        private IEnumerable<TablaClaveDto> mTerCalidadSuelo;
        private IEnumerable<TablaClaveDto> mTerLocManzana;
        private IEnumerable<TablaClaveDto> mTerNivelRazante;
        private IEnumerable<TablaClaveDto> mPreViaMaterial;
        private IEnumerable<TablaClaveDto> mPisoConservacion;

        public IEnumerable<TablaClaveDto> LPreAgua
        {
            get { return this.mPreAgua; }
            set { this.mPreAgua = value; OnPropertyChanged("LPreAgua"); }
        }

        public IEnumerable<TablaClaveDto> LPreAlcantarillado
        {
            get { return this.mPreAlcantarillado; }
            set { this.mPreAlcantarillado = value; OnPropertyChanged("LPreAlcantarillado"); }
        }

        public IEnumerable<TablaClaveDto> LPreCondicionTenencia
        {
            get { return this.mPreModoTenencia; }
            set { this.mPreModoTenencia = value; OnPropertyChanged("LPreCondicionTenencia"); }
        }

        public IEnumerable<TablaClaveDto> LPreTipoAsociacion
        {
            get { return this.mPreTipoPropiedad; }
            set { this.mPreTipoPropiedad = value; OnPropertyChanged("LPreTipoAsociacion"); }
        }
        public IEnumerable<TablaClaveDto> LPreDominio
        {
            get { return this.mPreDominio; }
            set { this.mPreDominio = value; OnPropertyChanged("LPreDominio"); }
        }

        public IEnumerable<TablaClaveDto> LTerCalidadSuelo
        {
            get { return this.mTerCalidadSuelo; }
            set { this.mTerCalidadSuelo = value; OnPropertyChanged("LTerCalidadSuelo"); }
        }

        public IEnumerable<TablaClaveDto> LTerLocManzana
        {
            get { return this.mTerLocManzana; }
            set { this.mTerLocManzana = value; OnPropertyChanged("LTerLocManzana"); }
        }

        public IEnumerable<TablaClaveDto> LTerNivelRazante
        {
            get { return this.mTerNivelRazante; }
            set { this.mTerNivelRazante = value; OnPropertyChanged("LTerNivelRazante"); }
        }

        public IEnumerable<TablaClaveDto> LPreViaMaterial
        {
            get { return this.mPreViaMaterial; }
            set { this.mPreViaMaterial = value; OnPropertyChanged("LPreViaMaterial"); }
        }

        public IEnumerable<TablaClaveDto> LPisoConservacion
        {
            get { return this.mPisoConservacion; }
            set { this.mPisoConservacion = value; OnPropertyChanged("LPisoConservacion"); }
        }

        #endregion

        #region Lista de eliminados

        private readonly List<PredioPropietarioDto> mPropietariosEliminados = new List<PredioPropietarioDto>();
        private readonly List<PredioFrenteDto> mFrentesEliminados = new List<PredioFrenteDto>();
        private readonly List<PredioFotoDto> mFotosEliminados = new List<PredioFotoDto>();
        private readonly List<PredioConstruccionDto> mConstruccionEliminados = new List<PredioConstruccionDto>();
        private readonly List<PredioPisoDto> mPisosEliminados = new List<PredioPisoDto>();
        private readonly List<PredioBloqueDto> mBloquesEliminados = new List<PredioBloqueDto>();
        private readonly List<PredioTerrenoDto> mTerrenosEliminados = new List<PredioTerrenoDto>();

        #endregion

        #region Habilitadores de comandos

        private bool ContribuyentePuedeRemover()
        {
            return this.PropietarioSeleccionado != null;
        }

        private bool ContribuyentePuedeModificar()
        {
            return this.contribuyenteModificarHabilitado && this.PropietarioSeleccionado != null;
        }

        private bool ContribuyentePuedeCraer()
        {
            return this.contribuyenteCrearHabilitado;
        }

        private bool TerrenoPuedeQuitar()
        {
            return this.ETerreno != null;
        }

        private bool FrentePuedeRemover()
        {
            return this.FrenteSeleccionado != null;
        }

        private bool BloquePuedeAgregar()
        {
            bool res = false;
            if (InsPisoBloque == 1)
            {
                res = true;
            }
            else
            {
                if (this.BloqueSeleccionado != null)
                {
                    res = true;
                }
            }
            return res;
        }

        private bool BloquePuedeRemover()
        {
            return this.BloqueSeleccionado != null || this.PisoSeleccionado != null;
        }

        private bool ComponentePuedeAgregar()
        {
            return this.ComponenteSeleccionado != null && this.PisoSeleccionado != null;
        }

        private bool ComponentePuedeRemover()
        {
            return this.ConstruccionSeleccionada != null;
        }

        private bool FotoPuedeRemover()
        {
            return this.FotoSeleccionada != null;
        }

        private bool CodigoPuedeGuardar()
        {
            bool res = true;
            if (! this.iniciando)
            {
                if (this["Zona"].Length > 0)
                {
                    res = false;
                }
                else
                {
                    if (this["Sector"].Length > 0)
                    {
                        res = false;
                    }
                    else
                    {
                        if (this["Manzana"].Length > 0)
                        {
                            res = false;
                        }
                        else
                        {
                            if (this["Predio"].Length > 0)
                            {
                                res = false;
                            }
                        }
                    }
                }
            }
            return res;
        }

        private bool CodigoRuralPuedeGuardar()
        {
            bool res = true;
            if (! this.iniciando)
            {
                if (this["zona"].Length > 0)
                {
                    res = false;
                }
                else
                {
                    if (this["Poligono"].Length > 0)
                    {
                        res = false;
                    }
                    else
                    {
                        if (this["Predio"].Length > 0)
                        {
                            res = false;
                        }
                    }
                }
                
            }
            return res;
        }

        private bool HabilitaGuardar()
        {
            bool res = true;
            if (! this.iniciando)
            {
                if (LPropietarios.Count <= 0)
                {
                    res = false;
                }
                else
                {
                    if (this["Codigo"].Length > 0)
                    {
                        res = false;
                    }
                }
            }
            return res;
        }

        #endregion

        #region Acciones de comandos

        private void ModificarCodigo()
        {
            if (TipoPredio == 0)
            {
                if (this.PopCodigoUrbanoAbierto == false)
                {
                    this.PopCodigoUrbanoAbierto = true;
                }
            }
            else
            {
                if (this.PopCodigoRuralAbierto == false)
                {
                    this.PopCodigoRuralAbierto = true;
                }
            }
        }

        private void CodigoUrbanoGuardar()
        {
            String cod = "";
            String div = "";
            if (this.Division.Trim().Length > 0)
            {
                div = codigoSeparador + this.Division.Trim();
            }
            cod = this.Zona.ToString().Trim() + codigoSeparador + this.Sector.ToString().Trim() + codigoSeparador + this.Manzana.ToString().Trim() + codigoSeparador + this.Predio.ToString().Trim() + div;
            Codigo = cod;
            PopCodigoUrbanoAbierto = false;
        }

        private void CodigoRuralGuardar()
        {
            String cod = "";
            String div = "";
            if (this.Division.Length > 0)
            {
                div = codigoSeparador + this.Division.Trim();
            }

            cod = this.Zona.ToString().Trim() + codigoSeparador + Poligono.ToString().Trim() + codigoSeparador + Predio.ToString().Trim() + div;
            Codigo = cod;
            PopCodigoRuralAbierto = false;
        }

        private void CodigoRuralCerrar()
        {
            PopCodigoRuralAbierto = false;
        }

        private void CodigoUrbanoCerrar()
        {
            PopCodigoUrbanoAbierto = false;
        }

        private void ContribuyenteCrear()
        {
            ContribuyenteEditorVM ce = new ContribuyenteEditorVM();
            if (ce.Modificado)
            {
                PredioPropietarioDto mPro = new PredioPropietarioDto();
                mPro.Estado = 0;
                mPro.ContribuyenteNav = ce.EContribuyente;
                mPro.Contribuyente = ce.EContribuyente.Id;
                mPro.Predio = this.EPredio.Id;
                LPropietarios.Add(mPro);
                OnPropertyChanged("PropietariosValido");
            }
        }

        private void ContribuyenteAgregar()
        {
            // Agregar contribuyente
            SeleccionarContribuyenteVM sc = new SeleccionarContribuyenteVM();
            if (sc.Vista.DialogResult == true)
            {
                if (sc.Seleccionado != null && sc.Seleccionado.Id > 0)
                {
                    Boolean estaInsertado = false;
                    if (LPropietarios.Count > 0)
                    {
                        foreach (PredioPropietarioDto p in LPropietarios)
                        {
                            if (p.ContribuyenteNav.Id == sc.Seleccionado.Id)
                            {
                                estaInsertado = true;
                                CuadroMensajes.Alertar("Operacion incompleta", "No se puede agregar", "Este contribuyente ya esta registrado como propietario de este predio", "");
                                break;
                            }
                        }
                    }
                    if (estaInsertado == false)
                    {
                        PredioPropietarioDto mPro = new PredioPropietarioDto();
                        mPro.Estado = 0;
                        mPro.Predio = this.EPredio.Id;
                        mPro.ContribuyenteNav = sc.Seleccionado;
                        mPro.Contribuyente = sc.Seleccionado.Id;
                        LPropietarios.Add(mPro);
                        OnPropertyChanged("PropietariosValido");
                    }
                }
            }            
            
        }

        private void ContribuyenteRemover()
        {
            if (this.PropietarioSeleccionado.Id > 0)
            {
                PredioPropietarioDto p = this.PropietarioSeleccionado;
                this.mPropietariosEliminados.Add(p);
            }
            this.LPropietarios.Remove(this.PropietarioSeleccionado);
            OnPropertyChanged("PropietariosValido");
        }

        private void ContribuyenteModificar()
        {
            if (this.PropietarioSeleccionado != null && this.PropietarioSeleccionado.ContribuyenteNav != null && this.PropietarioSeleccionado.ContribuyenteNav.Id > 0)
            {
                ContribuyenteEditorVM ce = new ContribuyenteEditorVM(this.PropietarioSeleccionado.ContribuyenteNav);
                if (ce.Modificado)
                {
                    this.PropietarioSeleccionado.ContribuyenteNav = ce.EContribuyente;
                }
            } 
        }

        private void TerrenoAgregar()
        {
            this.ETerreno = this.CrearTerreno();
            this.ETerreno.Predio = this.EPredio.Id;
            this.LTerrenos.Add(this.ETerreno);
        }

        private void TerrenoQuitar()
        {
            if (ETerreno.Id > 0)
            {
                mTerrenosEliminados.Add(ETerreno);
            }
            LTerrenos.Remove(ETerreno);
        }

        private void FrenteAgregar()
        {
            PredioFrenteDto fre = new PredioFrenteDto();
            fre.Estado = 0;
            fre.Id = 0;
            fre.Superficie = 0;
            fre.Frente = 0;
            fre.Predio = EPredio.Id;
            fre.PredioNav = EPredio;
            LFrentes.Add(fre);            
        }

        private void FrenteRemover()
        {
            if (FrenteSeleccionado.Id > 0)
            {
                mFrentesEliminados.Add(FrenteSeleccionado);
            }
            LFrentes.Remove(FrenteSeleccionado);
        }

        private void BloqueAgregar()
        {
            if (InsPisoBloque == 1)
            {
                PredioBloqueDto pb = new PredioBloqueDto();
                pb.Id = 0;
                pb.Descripcion = "Bloque 1";
                pb.Bloque = 0;
                pb.Predio = EPredio.Id;
                LBloques.Add(pb);
            }
            else
            {
                PredioPisoDto pp = new PredioPisoDto();
                pp.Id = 0;
                pp.Piso = 0;
                pp.Descripcion = "Piso";
                pp.EdadConstruccion = 0;
                pp.Superficie = 0;
                pp.Observaciones = "";
                pp.Bloque = BloqueSeleccionado.Id;
                BloqueSeleccionado.PisosLista.Add(pp);
                OnPropertyChanged("BloqueSeleccionado.PisosLista");
            }            
        }

        private void BloqueRemover()
        {
            string mContenido = "¿Seguro de quitar este";
            if (PisoSeleccionado != null)
            { mContenido = mContenido + "piso? " + "Tambien se quitaran todos sus componentes de construccion."; }
            else
            { mContenido = mContenido + "bloque? " + "Tambien se quitaran todos sus pisos y componentes."; }            
            if (CuadroMensajes.Preguntar("Quitar elemento", "Confirme esta operacion", mContenido).CustomButtonResult == 0)
            {
                if (PisoSeleccionado != null)
                {
                    if (PisoSeleccionado.Id > 0)
                    {
                        foreach (PredioConstruccionDto pc in PisoSeleccionado.ConstruccionesLista)
                        {
                            if (pc.Id > 0)
                            {
                                mConstruccionEliminados.Add(pc);
                            }
                        }
                        mPisosEliminados.Add(PisoSeleccionado);
                    }
                    //LBloques.FirstOrDefault().PisosLista.Remove(PisoSeleccionado);
                    foreach(PredioBloqueDto b in LBloques)
                    {
                        if (b.PisosLista.Contains(PisoSeleccionado))
                        {
                            b.PisosLista.Remove(PisoSeleccionado);
                        }
                    }
                    OnPropertyChanged("PisoSeleccionado");
                }
                else
                {
                    if (BloqueSeleccionado != null)
                    {
                        if (BloqueSeleccionado.Id > 0)
                        {
                            foreach (PredioPisoDto ps in BloqueSeleccionado.PisosLista)
                            {
                                if (ps.Id > 0)
                                {
                                    foreach (PredioConstruccionDto pc in ps.ConstruccionesLista)
                                    {
                                        if (pc.Id > 0)
                                        {
                                            mConstruccionEliminados.Add(pc);
                                        }
                                    }
                                    mPisosEliminados.Add(ps);
                                }                                
                            }
                            mBloquesEliminados.Add(BloqueSeleccionado);                            
                        }
                        LBloques.Remove(BloqueSeleccionado);
                    }
                }
            }
            
        }

        private void ComponenteAgregar()
        {
            if (!ComponenteInsertado(ComponenteSeleccionado.Elemento.Superior, ComponenteSeleccionado.Elemento.Clave))
            {
                PredioConstruccionDto pc = new PredioConstruccionDto();
                pc.Id = 0;
                pc.Piso = PisoSeleccionado.Id;
                pc.Elemento = ComponenteSeleccionado.Elemento.Id;
                pc.Clase = ComponenteSeleccionado.Superior.TrimEnd();
                pc.ConsElementoNav = ComponenteSeleccionado.Elemento;
                pc.Estado = 0;
                PisoSeleccionado.ConstruccionesLista.Add(pc);
            }
        }

        private void ComponenteRemover()
        {            
            if (ConstruccionSeleccionada.Id > 0)
                this.mConstruccionEliminados.Add(ConstruccionSeleccionada);
            PisoSeleccionado.ConstruccionesLista.Remove(ConstruccionSeleccionada);
        }
            
        private void FotoAgregar()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Seleccione una foto";
            op.Filter = "Todas las imagenes |*.jpg;*.jpeg;| " +
                "JPEG (*.jpg,*.jpeg) |*.jpg;*.jpeg";
            op.ShowDialog();
            if (!String.IsNullOrWhiteSpace(op.FileName))
            {
                BitmapImage img = new BitmapImage(new Uri(op.FileName));
                PredioFotoDto f = new PredioFotoDto();
                f.Estado = 0;
                f.Predio = EPredio.Id;
                f.Indice = 0;
                f.Descripcion = "Foto";
                JpegBitmapEncoder enc = new JpegBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(img));
                using (MemoryStream ms = new MemoryStream())
                {
                    enc.Save(ms);
                    f.Foto = ms.ToArray();
                }
                LFotos.Add(f);
            }
        }

        private void FotoRemover()
        {
            if (FotoSeleccionada.Id > 0)
            {
                mFotosEliminados.Add(FotoSeleccionada);
            }
            LFotos.Remove(FotoSeleccionada);
        }

        private void FotoDigitalizar()
        {
            if (this.PopDigitalizarAbierto == false)
            {
                this.PopDigitalizarAbierto = true;
            }
        }

        private void OcultarDigitalizar()
        {
            this.PopDigitalizarAbierto = false;
        }

        private void Guardar()
        {
            try
            {
                if (this.EPredio.Id > 0)
                {
                    catastrosDep.PredioModificar(EPredio);
                    if (mTerrenosEliminados.Count > 0) { catastrosDep.TerrenosEliminar(mTerrenosEliminados); }
                    if (mBloquesEliminados.Count > 0) { catastrosDep.BloquesEliminar(mBloquesEliminados); }
                    if (mConstruccionEliminados.Count > 0) { catastrosDep.ConstruccionesEliminar(mConstruccionEliminados); }
                    if (mFrentesEliminados.Count > 0) { catastrosDep.FrentesEliminar(mFrentesEliminados); }
                    if (mPisosEliminados.Count > 0) { catastrosDep.PisosEliminar(mPisosEliminados); }
                    if (mFotosEliminados.Count > 0) { catastrosDep.FotosEliminar(mFotosEliminados); }
                    if (mPropietariosEliminados.Count > 0) { catastrosDep.PropietariosEliminar(mPropietariosEliminados); }
                }
                else
                {
                    int i = catastrosDep.PredioCrear(EPredio);
                    
                    foreach (PredioTerrenoDto t in LTerrenos)
                    { t.Predio = i; }
                                        
                    foreach (PredioPropietarioDto pro in LPropietarios)
                    { pro.Predio = i; }

                    foreach (PredioFrenteDto fre in LFrentes)
                    { fre.Predio = i; }

                    foreach (PredioFotoDto fot in LFotos)
                    { fot.Predio = i; }

                    foreach (PredioBloqueDto blq in LBloques)
                    { blq.Predio = i; }
                }
                catastrosDep.PropietariosModificar(LPropietarios);
                catastrosDep.TerrenosModificar(LTerrenos);
                catastrosDep.FrentesModificar(LFrentes);                
                this.ProcesarBloquesLista(this.LBloques);
                catastrosDep.BloquesGuardar(LBloques);
                catastrosDep.FotosModificar(LFotos);
                this.Modificado = true;
                this.Vista.DialogResult = true;
                CuadroMensajes.Aceptar("Guardar cambios", "Operacion exitosa", "Los cambios se han guardado satisfactoriamente", "", TaskDialogInterop.VistaTaskDialogIcon.Information);
                this.Vista.Close();
            }
            catch (Exception ex)
            {
                CuadroMensajes.Alertar("Atencion", "Ha ocurrido el siguiente error", ex.Message, "");
            }
        }

        private void ProcesarBloquesLista(IEnumerable<PredioBloqueDto> bloques)
        {
            foreach(PredioBloqueDto blq in bloques)
            {                
                foreach(PredioPisoDto piso in blq.PisosLista)
                {
                    if (piso.ConservacionNav != null)
                        piso.Conservacion = piso.ConservacionNav.Id;
                    piso.ConstruccionesNav = piso.ConstruccionesLista.ToArray();
                }
                blq.PisosNav = blq.PisosLista.ToArray();
            }
        }

        #endregion
        
    }
}
