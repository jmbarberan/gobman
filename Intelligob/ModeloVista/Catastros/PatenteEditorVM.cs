using Intelligob.Cliente;
using Intelligob.Cliente.Depositos;
using Intelligob.Cliente.Referencia;
using Intelligob.Escritorio.ModeloVista.Comandos;
using Intelligob.Escritorio.ModeloVista.Emisiones;
using Intelligob.Escritorio.Vistas.Catastros;
using Intelligob.Escritorio.Vistas.Interfaces;
using Intelligob.Utiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Intelligob.Escritorio.ModeloVista.Catastros
{
    public class PatenteEditorVM : BaseMV<IVentanaDialogo>, IDataErrorInfo
    {
        readonly SeguridadDep seguridadDep = new SeguridadDep();
        readonly CatastrosDep catastrosDep = new CatastrosDep();

        #region Constructores
        public PatenteEditorVM() : this(new PatenteEditor(), null) { }
        public PatenteEditorVM(PatenteDto pat) : this(new PatenteEditor(), pat) { }
        public PatenteEditorVM(PatenteEditor vista, PatenteDto pat) : base(vista) 
        {
            ProcesarPrivilegios();
            CrearComandos();
            this.EPatente = pat;
            if (this.EPatente == null)
            {
                this.EPatente = new PatenteDto();
                IniciarPatenteNueva();
            }
            if (this.EPatente.Estado == null)
                IniciarPatenteNueva();

            if (this.EPatente.Id > 0)
                LComponentes = new ObservableCollection<PatentesComponenteDto>(catastrosDep.PatenteComponentesPorId(pat.Id));
            else
                LComponentes = new ObservableCollection<PatentesComponenteDto>();
            
            this.Vista.Owner = App.Current.MainWindow;
            this.Vista.ShowDialog();
        }

        #endregion

        #region Variables de modelo

        private bool contribuyenteModificable;
        private bool contribuyenteCreable;

        private PatenteDto ePatente;
        public PatenteDto EPatente
        {
            get { return this.ePatente; }
            set { this.ePatente = value; OnPropertyChanged("EPatente"); }
        }

        private PatentesComponenteDto componenteSeleccionado;
        public PatentesComponenteDto ComponenteSeleccionado
        {
            get { return this.componenteSeleccionado; }
            set { this.componenteSeleccionado = value; OnPropertyChanged("ComponenteSeleccionado"); }
        }

        private ObservableCollection<PatentesComponenteDto> lComponentes;
        public ObservableCollection<PatentesComponenteDto> LComponentes
        {
            get { return this.lComponentes; }
            set { this.lComponentes = value; OnPropertyChanged("LComponentes"); }
        }

        private readonly List<PatentesComponenteDto> componentesEliminados = new List<PatentesComponenteDto>();

        public System.Windows.Visibility ComponentesValido
        {
            get
            {
                System.Windows.Visibility v = System.Windows.Visibility.Visible;
                if (LComponentes.Count > 0)
                {
                    v = System.Windows.Visibility.Hidden;
                }
                return v;
            }
        }

        #endregion

        #region Declaracion de comandos

        public ICommand CmdGuardar
        { get; internal set; }

        public ICommand CmdContribuyenteSeleccionar
        { get; internal set; }

        public ICommand CmdContribuyenteModificar
        { get; internal set; }

        public ICommand CmdContribuyenteCrear
        { get; internal set; }

        public ICommand CmdComponenteAgregar
        { get; internal set; }

        public ICommand CmdComponenteRemover
        { get; internal set; }

        public ICommand CmdSeleccionarTabla
        { get; internal set; }

        public ICommand CmdSeleccionarClase
        { get; internal set; }

        #endregion

        #region Metodos internos
        private void ProcesarPrivilegios()
        { 
            this.contribuyenteCreable = false;
            this.contribuyenteModificable = false;

            string c = "";
            if (!SesionUtiles.Instance.EsDesarrollador)
            {
                PrivilegioDto p = seguridadDep.PrivilegiosFuncionPorUsuario(7, SesionUtiles.Instance.UsuarioActivo.Id);
                if (p != null && p.Comandos != null)
                    c = p.Comandos;
            }
                
            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("1"))
                this.contribuyenteCreable = true;
            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("2"))
                this.contribuyenteModificable = true;
        }

        private void CrearComandos()
        {
            this.CmdGuardar = new ComandoDelegado((o) => GuardarAccion(), (o) => GuardarHabilitado());
            this.CmdContribuyenteSeleccionar = new ComandoDelegado((o) => ContribuyenteSeleccionarAccion());
            this.CmdContribuyenteCrear = new ComandoDelegado((o) => ContribuyenteCrearAccion(), (o) => ContribuyenteCrearHabilitado());
            this.CmdContribuyenteModificar = new ComandoDelegado((o) => ContribuyenteModificarAccion(), (o) => ContribuyenteModificarHabilitado());
            this.CmdComponenteAgregar = new ComandoDelegado((o) => ComponenteAgregarAccion());
            this.CmdComponenteRemover = new ComandoDelegado((o) => ComponenteRemoverAccion(), (o) => ComponenteRemoverHabilitado());
            this.CmdSeleccionarTabla = new ComandoDelegado((o) => SeleccionarTablaAccion(), (o) => SeleccionarTablaHabilitado());
            this.CmdSeleccionarClase = new ComandoDelegado((o) => SeleccionarClaseAccion(), (o) => SeleccionarClaseHabilitado());
        }

        private void IniciarPatenteNueva()
        {
            EPatente.Codigo = String.Empty;
            EPatente.Estado = 0;
            EPatente.Artesano = false;
            EPatente.ContabilidadRequerida = false;
            EPatente.Contribuyente = 0;
            EPatente.Predio = 0;
        }

        public string this[string atributo]
        {
            get
            {
                String error = String.Empty;
                switch (atributo)
                {
                    case "EPatente.ContribuyenteNav.Nombres":
                        {
                            if (EPatente.ContribuyenteNav == null || EPatente.ContribuyenteNav.Id <= 0)
                                error = "Debe seleccionar un contribuyente";
                            break;
                        }
                    case "EPatente.Codigo":
                        {
                            if (String.IsNullOrWhiteSpace(EPatente.Codigo))
                                error = "Debe digitar el codigo de la patente";
                            else
                            {
                                if (catastrosDep.PatenteCodigoRegistrado(EPatente.Codigo, EPatente.Id))
                                    error = "El codigo ya esta registrado";
                            }
                            break;
                        }
                    case "LComponentes":
                        {
                            if (LComponentes.Count == 0)
                                error = "Debe seleccionar los rubros componentes";
                            break;
                        }
                }
                return error;
            }
        }

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Habilitadores de comandos

        private bool GuardarHabilitado()
        {
            return this.EPatente.ContribuyenteNav != null && LComponentes.Count > 0 && this.EPatente.Codigo != null && this.EPatente.Codigo.Length > 0;
        }

        private bool ContribuyenteCrearHabilitado()
        {
            return this.contribuyenteCreable;
        }

        private bool ContribuyenteModificarHabilitado()
        {
            return this.contribuyenteModificable && this.EPatente.ContribuyenteNav != null;
        }

        private bool ComponenteRemoverHabilitado()
        {
            return this.ComponenteSeleccionado != null;
        }

        private bool SeleccionarTablaHabilitado()
        {
            return this.ComponenteSeleccionado != null;
        }

        private bool SeleccionarClaseHabilitado()
        {
            return this.ComponenteSeleccionado != null && this.ComponenteSeleccionado.ConceptoNav != null;
        }

        #endregion

        #region Acciones de comandos

        private void GuardarAccion()
        {
            int id = EPatente.Id;
            if (id <= 0)
                id = this.catastrosDep.PatenteNuevo(EPatente);
            else
            {
                this.catastrosDep.PatenteModificar(EPatente);
            }
            foreach (PatentesComponenteDto c in LComponentes)
            {
                if (c.Patente <= 0)
                    c.Patente = id;
            }
            this.catastrosDep.PatenteComponentesModificar(LComponentes, componentesEliminados);
            CuadroMensajes.Aceptar("Guardar cambios", "Operacion completa", "Los cambios se han guardado satisfactoriamente", "", TaskDialogInterop.VistaTaskDialogIcon.Information);
            this.Vista.DialogResult = true;
            this.Vista.Close();
        }

        private void ContribuyenteSeleccionarAccion()
        {
            SeleccionarContribuyenteVM sc = new SeleccionarContribuyenteVM();
            if (sc.Vista.DialogResult == true)
            {
                this.EPatente.ContribuyenteNav = sc.Seleccionado;
                this.ePatente.Contribuyente = sc.Seleccionado.Id;
            }
        }

        private void ContribuyenteCrearAccion()
        {
            ContribuyenteEditorVM ce = new ContribuyenteEditorVM();
            if (ce.Modificado)
            {
                this.EPatente.ContribuyenteNav = ce.EContribuyente;
                this.ePatente.Contribuyente = ce.EContribuyente.Id;
            }
        }

        private void ContribuyenteModificarAccion()
        {
            ContribuyenteEditorVM ce = new ContribuyenteEditorVM(this.EPatente.ContribuyenteNav);
            if (ce.Modificado)
            {
                this.EPatente.ContribuyenteNav = ce.EContribuyente;
            }
        }

        private void ComponenteAgregarAccion()
        {
            PatenteComponenteSeleccionarVM sel = new PatenteComponenteSeleccionarVM();
            if (sel.Vista.DialogResult == true)
            {
                ConceptoDto c = sel.Seleccionado;
                Boolean ins = false;
                if (EPatente.ComponentesNav != null)
                {
                    foreach (PatentesComponenteDto pci in EPatente.ComponentesNav)
                    {
                        if (pci.ConceptoNav.Id == c.Id)
                        {
                            ins = true;
                            break;
                        }
                    }
                }
                
                if (!ins)
                {
                    PatentesComponenteDto p = new PatentesComponenteDto();
                    p.Id = 0;
                    p.Patente = EPatente.Id;
                    p.Concepto = c.Id;
                    p.ConceptoNav = c;
                    p.Estado = 0;
                    p.BaseImponible = 0; // Capital o Activos
                    p.Categoria = 0; // 1 o 2 - La columna
                    p.CoeficientesTipo = 0; // Tabla
                    p.CoeficientesIndice = 0; // Indice de la tabla
                    p.Estado = 0;
                    LComponentes.Add(p);
                }
            }
        }

        private void ComponenteRemoverAccion()
        {
            if (ComponenteSeleccionado != null)
            { 
                if (ComponenteSeleccionado.Id > 0)
                    this.componentesEliminados.Add(ComponenteSeleccionado);
                this.LComponentes.Remove(ComponenteSeleccionado);
            }
        }

        private void SeleccionarTablaAccion()
        {
            CoeficienteSeleccionarVM cs = new CoeficienteSeleccionarVM();
            if (cs.Vista.DialogResult == true)
            {
                ComponenteSeleccionado.CoeficienteNav = cs.Seleccionado;
                ComponenteSeleccionado.CoeficientesTipo = cs.Seleccionado.Id;
            }
        }

        private void SeleccionarClaseAccion()
        {     
            if (ComponenteSeleccionado.CoeficienteNav.Id > 0)
            {
                CoeficienteElementoSeleccionarVM es = new CoeficienteElementoSeleccionarVM(ComponenteSeleccionado.CoeficienteNav.Id);
                if (es.Vista.DialogResult == true)
                {
                    ComponenteSeleccionado.CoeficienteEleNav = es.Seleccionado;
                    ComponenteSeleccionado.CoeficientesIndice = es.Seleccionado.Id;
                }
            }
            else
            {
                CuadroMensajes.Alertar("No se puede buscar", "", "Debe seleccionar una tabla antes de continuar", "");
            }
        }

        #endregion

    }
}
