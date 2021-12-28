using System.Windows.Input;
using Intelligob.Cliente.Referencia;
using Intelligob.Escritorio.Vistas.Interfaces;
using System;
using System.Collections.ObjectModel;
using Intelligob.Cliente;
using Intelligob.Cliente.Depositos;
using Intelligob.Escritorio.ModeloVista.Comandos;
using System.Linq;
using System.Collections.Generic;
using Intelligob.Escritorio.Vistas.General;

namespace Intelligob.Escritorio.ModeloVista.Agua
{
    public class AguaLecturasVM : BaseMV<IPagina>
    {
        private readonly SeguridadDep seguridadDep = new SeguridadDep();
        private readonly AguaDep aguaDep = new AguaDep();
        
        private string lCodigoSeparador;
        public string LCodigoSeparador
        {
            get { return this.lCodigoSeparador; }
            set { this.lCodigoSeparador = value; OnPropertyChanged("LCodigoSeparador"); }
        }

        #region Variables de consulta y seleccion
        private ObservableCollection<AguaLecturaExtension> lLecturas = new ObservableCollection<AguaLecturaExtension>();
        public ObservableCollection<AguaLecturaExtension> Lecturas
        {
            get { return this.lLecturas; }
            set
            {
                this.lLecturas = value;
                OnPropertyChanged("Lecturas");
            }
        }

        private AguaLecturaExtension seleccionado;
        public AguaLecturaExtension Seleccionado
        {
            get { return this.seleccionado; }
            set { this.seleccionado = value; OnPropertyChanged("Seleccionado"); }
        }

        private string barraEstado;
        public string BarraEstado
        {
            get { return this.barraEstado; }
            set { this.barraEstado = value; OnPropertyChanged("BarraEstado"); }
        }

        private int añoEmision;
        public int AñoEmision
        {
            get { return this.añoEmision; }
            set { this.añoEmision = value; OnPropertyChanged("AñoEmision"); }
        }

        private readonly int lmesEmision;

        public string MesEmision
        {
            get 
            {
                switch (lmesEmision)
                {
                    case 1: { return "Enero"; }
                    case 2: { return "Febrero"; }
                    case 3: { return "Marzo"; }
                    case 4: { return "Abril"; }
                    case 5: { return "Mayo"; }
                    case 6: { return "Junio"; }
                    case 7: { return "Julio"; }
                    case 8: { return "Agosto"; }
                    case 9: { return "Septiembre"; }
                    case 10: { return "Octubre"; }
                    case 11: { return "Noviembre"; }
                    case 12: { return "Diciembre"; }
                    default: { return "Indeterminado"; }
                }
            }
        }

        private int zona = 0;
        private int sector = 0;
        private int manzana = 0;
        public int Zona
        {
            get { return this.zona; }
            set { this.zona = value; OnPropertyChanged("Zona"); }
        }
        public int Sector
        {
            get { return this.sector; }
            set { this.sector = value; OnPropertyChanged("Sector"); }
        }
        public int Manzana
        {
            get { return this.manzana; }
            set { this.manzana = value; OnPropertyChanged("Manzana"); }
        }

        #endregion

        #region Declaracion de comandos

        private bool modificarHabilitado;
        private bool bajasHabilitado;
        private bool imprimirHabilitado;
        private bool hojasHabilitado;

        public ICommand CmdBuscar
        { get; internal set; }

        public ICommand CmdModificar
        { get; internal set; }

        public ICommand CmdBajas
        { get; internal set; }

        public ICommand CmdImprimir
        { get; internal set; }

        public ICommand CmdHojas
        { get; internal set; }

        public ICommand CmdRegresar
        { get; internal set; }

        public ICommand CmdAdelante { get; internal set; }

        #endregion

        public AguaLecturasVM(IPagina vista) : base(vista)
        {
            CargarPrivilegios();
            IniciarComandos();
            TablaClaveDto t = ModeloCache.Instance.McClaves.Where(c => c.Tabla == 3 && c.Clave == 1).FirstOrDefault();
            if (t != null && t.Superior != null)
                AñoEmision = (int)t.Superior;
            t = ModeloCache.Instance.McClaves.Where(c => c.Tabla == 3 && c.Clave == 2).FirstOrDefault();
            if (t != null && t.Superior != null)
            {
                lmesEmision = (int)t.Superior + 1;
                if (lmesEmision == 13)
                {
                    lmesEmision = 1;
                    AñoEmision = AñoEmision + 1;
                }
            }            
            LCodigoSeparador = ModeloCache.Instance.McClaves.Where(c => c.Tabla == 2 && c.Clave == 6).FirstOrDefault().Denominacion.Trim();
        }

        #region Metodos Locales

        private void CargarPrivilegios()
        {
            // TODO Implementar funcion comando 4: Iniciar lecturas anuales deberia de hacerse automaticamente al terminar la emision de diciembre
            this.modificarHabilitado = false;
            this.bajasHabilitado = false;
            this.imprimirHabilitado = false;
            this.hojasHabilitado = false;
            
            string c = "";
            if (!SesionUtiles.Instance.EsDesarrollador)
            {
                PrivilegioDto p = seguridadDep.PrivilegiosFuncionPorUsuario(16, SesionUtiles.Instance.UsuarioActivo.Id);
                if (p != null && p.Comandos != null)
                    c = p.Comandos;
            }
                
            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("1"))
                modificarHabilitado = true;
            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("2"))
                imprimirHabilitado = true;
            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("3"))
                hojasHabilitado = true;
            /*if (SesionUtiles.Instance.EsDesarrollador || c.Contains("4"))
                btIniciar.IsEnabled = true;*/
            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("5"))
                bajasHabilitado = true;
        }

        private void IniciarComandos()
        {
            this.CmdBuscar = new ComandoDelegado((o) => BuscarAccion());
            this.CmdModificar = new ComandoDelegado((o) => ModificaAccion(), (o) => ModificarHabilitado());
            this.CmdBajas = new ComandoDelegado((o) => BajasAccion(), (o) => BajasHabilitado());
            this.CmdImprimir = new ComandoDelegado((o) => ImprimirAccion(), (o) => ImprimirHabilitado());
            this.CmdHojas = new ComandoDelegado((o) => HojasAccion(), (o) => HojasHabilitado());
            this.CmdRegresar = new ComandoDelegado((o) => RegresarAccion(), (o) => RegresarHabilitado());
            this.CmdAdelante = new ComandoDelegado((o) => this.AccionAdelantar(), (o) => this.PuedeAdelantar());
        }

        #endregion

        #region Habilitadores de Comandos

        private bool ModificarHabilitado()
        {
            return this.modificarHabilitado && this.Seleccionado != null;
        }

        private bool BajasHabilitado()
        {
            return this.bajasHabilitado && this.Seleccionado != null;
        }

        private bool ImprimirHabilitado()
        {
            return this.imprimirHabilitado && this.Lecturas.Count > 0;
        }

        private bool HojasHabilitado()
        {
            return this.hojasHabilitado && this.Lecturas.Count > 0;
        }

        private bool RegresarHabilitado()
        {
            return Navegador.NavigationService.CanGoBack;
        }

        #endregion

        #region Acciones de comandos

        private void BuscarAccion()
        {
            Lecturas.Clear();

            string cod = "";
            if (Zona > 0)
                cod = cod + Zona.ToString();
            else
                cod = "%";
            if (Sector > 0)
                cod = cod + LCodigoSeparador + Sector.ToString();
            else
                cod = cod + LCodigoSeparador + "%";
            if (Manzana > 0)
                cod = cod + LCodigoSeparador + Manzana.ToString();
            else
                cod = cod + LCodigoSeparador + "%";
            // numero de predio
            if (!cod.EndsWith("%"))
                cod = cod + LCodigoSeparador + "%";

            IEnumerable<AguaLecturaDto> lecs = aguaDep.LecturasTraerPorAñoCodigo(AñoEmision, cod);
            if (lecs.Count() > 0)
            {
                foreach (AguaLecturaDto l in lecs)
                {
                    Lecturas.Add(new AguaLecturaExtension(l, lmesEmision));
                }
                BarraEstado = "Busqueda completa";
            }
            else
            {
                BarraEstado = "No se encontraron registros coincidentes a la busqueda";
            }
        }

        private void ModificaAccion()
        {
            // Modificar lecturas
        }

        private void BajasAccion()
        {
            // TODO Bajas y Altas
        }

        private void ImprimirAccion()
        {
            // TODO Imprimir lecturas en pantalla
        }

        private void HojasAccion()
        {
            // TODO Imprimir hojas para toma de lectura
        }

        private void RegresarAccion()
        {
            Navegador.NavigationService.GoBack();
        }

        private bool PuedeAdelantar()
        {
            return Navegador.NavigationService.CanGoForward;
        }

        private void AccionAdelantar()
        {
            Navegador.NavigationService.GoForward();
        }

        #endregion

    }
}
