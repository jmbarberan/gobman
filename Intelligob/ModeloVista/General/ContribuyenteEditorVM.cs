using Intelligob.Cliente.Referencia;
using Intelligob.Cliente.Depositos;
using Intelligob.Escritorio.ModeloVista.Comandos;
using Intelligob.Escritorio.Vistas.General;
using Intelligob.Escritorio.Vistas.Interfaces;
using Intelligob.Utiles;
using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Intelligob.Cliente;
using System.Linq;

namespace Intelligob.Escritorio.ModeloVista
{
    public class ContribuyenteEditorVM : BaseMV<IVista>, IDataErrorInfo
    {
        public ContribuyenteDto EContribuyente
        {
            get;
            private set;
        }

        readonly Action ocultarModal;

        private readonly ContribuyentesDep contribuyentesDep;
        public bool Modificado;

        private bool puedeGuardar;
        public bool PuedeGuardar
        {
            get { return this.puedeGuardar; }
            set { this.puedeGuardar = value; OnPropertyChanged("PuedeGuardar"); }
        }

        public ICommand CmdGuardar
        {
            get;
            internal set;
        }

        public ICommand CmdCancelar
        {
            get;
            internal set;
        }

        private ObservableCollection<TablaClaveDto> lPersonerias;
        public ObservableCollection<TablaClaveDto> LPersonerias
        {
            get { return this.lPersonerias; }
            set { this.lPersonerias = value; OnPropertyChanged("LPersonerias"); }
        }

        public ContribuyenteEditorVM()
            : this(new ContribuyenteDto())
        {
        }


        public ContribuyenteEditorVM(ContribuyenteDto c)
            : this(new ContribuyenteEditor(), c, DepositosControl.Instance.ContribuyentesDepositoCrear()) {}

        public ContribuyenteEditorVM(ContribuyenteDto c, ContribuyentesDep s)
            : this(new ContribuyenteEditorModal(), c, s, null)
        {
        }

        public ContribuyenteEditorVM(IVentanaDialogo vista, ContribuyenteDto c, ContribuyentesDep pClienteWeb)
            : base(vista)
        {
            if (pClienteWeb == null)
                contribuyentesDep = DepositosControl.Instance.ContribuyentesDepositoCrear();
            else
                contribuyentesDep = pClienteWeb;
            this.EContribuyente = c;
            if (c == null || c.Id == 0)
            {
                c.Id = 0;
                c.Estado = 0;
                c.Cedula = String.Empty;
                c.Nombres = String.Empty;
            }
            this.CrearComandos();
            this.LPersonerias = new ObservableCollection<TablaClaveDto>(ModeloCache.Instance.McClaves.Where(t => t.Tabla == 24));
            ((IVentanaDialogo)this.Vista).Owner = App.Current.MainWindow;
            ((IVentanaDialogo)this.Vista).ShowDialog();
        }

        public ContribuyenteEditorVM(IControlUsuario vista, ContribuyenteDto c, ContribuyentesDep dep, Action pOcultar) : base(vista)
        {
            if (dep == null)
                contribuyentesDep = DepositosControl.Instance.ContribuyentesDepositoCrear();
            else
                contribuyentesDep = dep;
            this.EContribuyente = c;
            if (c == null || c.Id == 0)
            {
                c.Id = 0;
                c.Estado = 0;
                c.Cedula = String.Empty;
                c.Nombres = String.Empty;
            }
            CrearComandos();
            this.LPersonerias = new ObservableCollection<TablaClaveDto>(ModeloCache.Instance.McClaves.Where(t => t.Tabla == 24));
            this.ocultarModal = pOcultar;
        }

        private void CrearComandos()
        {
            this.CmdGuardar = new ComandoDelegado((o) => AccionGuardar(), (o) => HabilitaGuardar());
            this.CmdCancelar = new ComandoDelegado((o) => AccionCancelar());            
        }        

        #region Atributos del modelo

        public string Cedula
        {
            get { return this.EContribuyente.Cedula; }
            set { this.EContribuyente.Cedula = value; OnPropertyChanged("Cedula"); }
        }

        public string Nombres
        {
            get { return this.EContribuyente.Nombres; }
            set { this.EContribuyente.Nombres = value; OnPropertyChanged("Nombres"); }
        }

        public string Representante
        {
            get { return this.EContribuyente.Representante; }
            set { this.EContribuyente.Representante = value; OnPropertyChanged("Representante"); }
        }

        public string Direccion
        {
            get { return this.EContribuyente.Direccion; }
            set { this.EContribuyente.Direccion = value; OnPropertyChanged("Direccion"); }
        }

        public string Telefonos
        {
            get { return this.EContribuyente.Telefonos; }
            set { this.EContribuyente.Telefonos = value; OnPropertyChanged("Telefonos"); }
        }

        public int? Estado
        {
            get { return this.EContribuyente.Estado; }
            set { this.EContribuyente.Estado = value; OnPropertyChanged("Estado"); }
        }

        public TablaClaveDto Personeria
        {
            get { return this.EContribuyente.PersoneriaNav; }
            set { this.EContribuyente.PersoneriaNav = value; OnPropertyChanged("Personeria"); }
        }

        public string Presentacion
        {
            get 
            {
                string ced = "";
                if (!String.IsNullOrWhiteSpace(EContribuyente.Cedula))
                    ced = " [" + EContribuyente.Cedula + "]";
                return EContribuyente.Nombres + ced; 
            }
        }
        #endregion

        private bool HabilitaGuardar()
        {
            bool res = true;            
            return res;
        }

        private void AccionGuardar()
        {
            bool res = true;
            if (this["Nombres"].Length > 0)
            {
                res = false;
            }
            else
            {
                if (this["Cedula"].Length > 0)
                {
                    res = false;
                }
                else
                {
                    if (this["Personeria"].Length > 0)
                    {
                        res = false;
                    }
                }
            }

            if (res)
            {
                try
                {
                    EContribuyente.Personeria = Personeria.Id;
                    if (this.EContribuyente.Id == 0)
                    {

                        int i = contribuyentesDep.ContribuyenteNuevo(EContribuyente);
                        EContribuyente.Id = i;
                    }
                    else
                    {
                        contribuyentesDep.ContribuyenteModificar(EContribuyente);
                    }
                    Modificado = true;
                }
                catch (Exception ex)
                {
                    CuadroMensajes.Alertar("No se pudo guardar", "Ocurrio el siguiente error", ex.Message, "");
                    Modificado = false;
                }
                if (ocultarModal != null)
                    ocultarModal();
                else
                {
                    ((IVentanaDialogo)this.Vista).DialogResult = Modificado;
                    if (Modificado)
                        CuadroMensajes.Aceptar("Guardar", "Operacion exitosa", "Los cambios se guardaron satisfactoriamente", "", TaskDialogInterop.VistaTaskDialogIcon.Information);
                    ((IVentanaDialogo)this.Vista).Close();
                }
            }
            else
            {
                CuadroMensajes.Alertar("Atencion", "No se puede guardar", "Los cambios no se pueden guardar porque la informacion es invalida o esta incompleta", "");
            }
        }

        private void AccionCancelar()
        {
            Modificado = false;
            if (ocultarModal != null)
                ocultarModal();
        }

        

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string nomAtributo]
        {
            get
            {
                string error = string.Empty;
                switch (nomAtributo)
                {
                    case "Nombres":
                        {
                            if (String.IsNullOrWhiteSpace(this.Nombres))
                                error = "Debe digitar al menos el nombre del contribuyente";
                            else
                            {
                                if (contribuyentesDep.ContribuyenteNombreRegistrado(EContribuyente))
                                    error = "Este nombre ya se encuentra registrado";
                            }
                            break;
                        }
                    case "Cedula":
                        {
                            if (!String.IsNullOrWhiteSpace(this.Cedula))
                            {
                                try
                                {
                                    long c = Convert.ToInt64(this.Cedula);
                                    if (contribuyentesDep.ContribuyenteCedulaRegistrada(EContribuyente))
                                    {
                                        error = "El No. de cedula ya esta registrado en otro contribuyente";
                                    }
                                }
                                catch
                                {
                                    error = "El No. de cedula contiene caracteres invalidos (ejemplo: @, -, #, A-Z)";
                                }
                            }
                            break;
                        }
                    case "Personeria":
                        {
                            if (this.Personeria == null)
                                error = "Seleccione una personeria";
                            break;
                        }
                }
                return error;
            }
        }

    }
}
