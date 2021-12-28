using Intelligob.Escritorio.ModeloVista.Comandos;
using Intelligob.Cliente.Referencia;
using Intelligob.Cliente.Depositos;
using Intelligob.Escritorio.Vistas.Interfaces;
using Intelligob.Escritorio.Vistas.Seguridad;
using Intelligob.Utiles;
using Intelligob.Utilerias;
using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Intelligob.Escritorio.ModeloVista
{
    public class UsuarioEditorVM : BaseMV<IVentanaDialogo>, IDataErrorInfo
    {
        private readonly BitmapImage imInvalido = new BitmapImage();
        private readonly BitmapImage imValido = new BitmapImage();
        private readonly SeguridadDep seguridadDep;
        public readonly UsuarioDto EUsuario;
        private string claveOriginal = "";

        private bool debeValidar;

        private bool esadministrador = false;
        public Boolean EsAdministrador
        {
            get { return this.esadministrador; }
            set
            {
                this.esadministrador = value;
                OnPropertyChanged("EsAdministrador");
            }
        }

        public Boolean EsAdministradorNegativo 
        {
            get { return !this.EsAdministrador; }
        }

        public ICommand CmdGuardar
        { get; internal set; }

        public ICommand CmdValidarClave
        { get; internal set; }

        public UsuarioEditorVM() : this(new UsuarioEditor(), new UsuarioDto(), DepositosControl.Instance.SeguridadDepositoCrear()) { }

        public UsuarioEditorVM(UsuarioDto pUsr) : this(new UsuarioEditor(), pUsr, DepositosControl.Instance.SeguridadDepositoCrear()) { }

        public UsuarioEditorVM(SeguridadDep dep) : this(new UsuarioEditor(), new UsuarioDto(), dep) { }

        public UsuarioEditorVM(UsuarioDto usr, SeguridadDep dep) : this(new UsuarioEditor(), usr, dep) { }

        public UsuarioEditorVM(IVentanaDialogo pVista, UsuarioDto pUsr, SeguridadDep dep) : base(pVista) 
        {
            this.EUsuario = pUsr;
            if (pUsr == null || pUsr.Id == 0)
            {
                pUsr.Estado = 0;
                pUsr.Caduca = false;
            }
            else
            {
                if (pUsr.Id > 0)
                {
                    if (pUsr.Id == 1)
                        EsAdministrador = true;
                    this.claveOriginal = this.Clave;
                    this.debeValidar = false;
                    this.imgValidez = imValido;
                }
            }
            if (dep == null)
                this.seguridadDep = DepositosControl.Instance.SeguridadDepositoCrear();
            else
                this.seguridadDep = dep;
            this.imInvalido.BeginInit();
            this.imInvalido.UriSource = new Uri("../Imagenes/invalido.png", UriKind.Relative);
            this.imInvalido.EndInit();
            this.imValido.BeginInit();
            this.imValido.UriSource = new Uri("../Imagenes/valido.png", UriKind.Relative);
            this.imValido.EndInit();
            this.CmdGuardar = new ComandoDelegado((o) => AccionGuardar(), (o) => HabilitaGuardar());
            this.CmdValidarClave = new ComandoDelegado((o) => AccionValidarClave(), (o) => HabilitaValidar());
            ((IVentanaMetodo)this.Vista).Ejecutar();
            this.Vista.Owner = App.Current.MainWindow;
            this.MostrarVista();
        }

        private void MostrarVista()
        {
            Vista.Owner = App.Current.MainWindow;
            Vista.ShowDialog();
        }        

        #region Habilitadores y Acciones de comandos

        private bool HabilitaGuardar()
        {
            bool res = true;
            if (this["Codigo"].Length > 0)
            {
                res = false;
            }
            else
            {
                if (this["Nombres"].Length > 0)
                {
                    res = false;
                }
            }
            return res && !this.debeValidar;
        }

        private void AccionGuardar()
        {
            if (this.EUsuario.Id > 0)
                seguridadDep.UsuarioModificar(this.EUsuario);
            else
            {
                int i = seguridadDep.UsuarioNuevo(this.EUsuario);
                this.EUsuario.Id = i;
            }
                
            this.Vista.DialogResult = true;
            CuadroMensajes.Aceptar("Guardar", "Operacion exitosa", "Los cambios se guardaron satisfactoriamente", "", TaskDialogInterop.VistaTaskDialogIcon.Information);
            this.Vista.Close();
        }

        private bool HabilitaValidar()
        {
            return this.debeValidar;
        }

        private void AccionValidarClave()
        {
            if (!String.IsNullOrWhiteSpace(this.Clave))
            {
                Clave cve = new Clave();
                cve.ShowDialog();
                if (cve.DialogResult == true)
                {
                    if (cve.Contraseña == this.Clave)
                    {
                        this.claveOriginal = cve.Contraseña;
                        ImgValidez = imValido;
                        debeValidar = false;
                    }
                    else
                    {
                        ImgValidez = imInvalido;
                        debeValidar = true;
                        CuadroMensajes.Alertar("Operacion incompleta", "La contraseña no es valida", "No coincide con la contraseña digitada", "");
                    }
                }
            }
            else
            {
                CuadroMensajes.Alertar("Operacion incompleta", "La contraseña no se pudo validar", "El cuadro de contraseña esta vacio", "");
            }
        }

        #endregion

        #region Atributos del modelo
        public String Codigo
        {
            get { return this.EUsuario.Codigo; }
            set
            {
                this.EUsuario.Codigo = value;
                OnPropertyChanged("Codigo");
            }
        }

        public String Nombres
        {
            get { return this.EUsuario.Nombres; }
            set
            {
                this.EUsuario.Nombres = value;
                OnPropertyChanged("Nombres");
            }
        }

        public String Clave
        {
            get { return Cifrador.RijndaelSimple.Desencriptar(this.EUsuario.Clave); }
            set
            {
                this.EUsuario.Clave = Cifrador.RijndaelSimple.Encriptar(value);
                OnPropertyChanged("Clave");
                if (value != this.claveOriginal || String.IsNullOrWhiteSpace(value))
                {
                    debeValidar = true;
                    ImgValidez = imInvalido;
                }
                else
                {
                    debeValidar = false;
                    ImgValidez = imValido;
                }
            }
        }

        public bool Caduca
        {
            get { return this.EUsuario.Caduca; }
            set
            {
                this.EUsuario.Caduca = value;
                OnPropertyChanged("Caduca");
            }
        }

        public DateTime? CaducaFecha
        {
            get { return this.EUsuario.CaducaFecha; }
            set
            {
                this.EUsuario.CaducaFecha = value;
                OnPropertyChanged("CaducaFecha");
            }
        }

        private BitmapImage imgValidez;
        public BitmapImage ImgValidez
        {
            get { return this.imgValidez; }
            set
            {
                this.imgValidez = value;
                OnPropertyChanged("ImgValidez");
            }
        }

        #endregion

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Codigo":
                        {
                            if (!EsAdministrador)
                            {
                                if (String.IsNullOrWhiteSpace(this.Codigo))
                                {
                                    error = "Se requiere el codigo del usuario";
                                }
                                else
                                {
                                    if (this.Codigo.ToUpper() == "DESARROLLADOR" || this.Codigo.ToUpper() == "ADMINISTRADOR")
                                    {
                                        error = "Este codigo de usuario esta restringido, digite otro distinto";
                                    }
                                    else
                                    {
                                        if (seguridadDep.UsuarioCodigoRegistrado(this.Codigo, this.EUsuario.Id))
                                        {
                                            error = "El codigo digitado ya esta registrado con otro usuario";
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    case "Nombres":
                        {
                            if (!EsAdministrador)
                            {
                                if (String.IsNullOrWhiteSpace(this.Nombres))
                                {
                                    error = "Dgite el nombre del usuario";
                                }
                            }
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
    }
}
