using Intelligob.Escritorio.Vistas.General;
using Intelligob.Escritorio.Vistas.Interfaces;
using Intelligob.Utiles;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using TaskDialogInterop;

namespace Intelligob.Escritorio.ModeloVista
{
    public class ConexionServidorVM : BaseMV<IVentanaDialogo>, IDataErrorInfo
    {
        #region Comandos

        public ICommand CmdGuardar
        {
            get;
            internal set;
        }

        public ICommand CmdProbar
        {
            get;
            internal set;
        }

        #endregion
                
        private string direccionBase;
        public string DireccionBase
        {
            get { return this.direccionBase; }
            set { this.direccionBase = value; OnPropertyChanged("DireccionBase"); }
        }

        private bool puedeProbar;
        public bool PuedeProbar
        {
            get { return this.puedeProbar; }
            set { this.puedeProbar = value; OnPropertyChanged("PuedeProbar"); }
        }

        private bool puedeGuardar;

        public ConexionServidorVM()
            : this( new ConexionServidor())
        {
        }

        public ConexionServidorVM(IVentanaDialogo vista)
            : base( vista )
        {
            this.direccionBase = Configuracion.DireccionServidor;
            this.CmdGuardar = new Comandos.ComandoDelegado((o) => AccionGuardar(), (o) => HabilitaGuardar());
            this.CmdProbar = new Comandos.ComandoDelegado((o) => AccionProbar());
        }

        private void AccionProbar()
        {
            ComprobarServidor(false);
        }

        private void AccionGuardar()
        {
            bool bCerrar = true;
            if (string.IsNullOrWhiteSpace(this.DireccionBase))
            {
                TaskDialogResult r = CuadroMensajes.Alertar("No se puede guardar", "Debe digitar la direccion del servidor", "Sin una direccion valida el sistema no puede iniciar", "Cerrar de todas formas");
                if (r.VerificationChecked == true)
                    bCerrar = true;
            }
            else
            {
                if (ComprobarServidor(true))
                {
                    Configuracion.DireccionServidor = this.DireccionBase;
                    this.Vista.DialogResult = true;
                }
                else
                {
                    TaskDialogResult r = CuadroMensajes.Alertar("No se puede guardar", "La direccion no es valida", "Compruebe que la direccion proporcionada sea correcta", "Cerrar de todas formas");
                    if (r.VerificationChecked == true)
                    {
                        bCerrar = true;
                    }
                }                
            }
            if (bCerrar)
                this.Vista.Close();
        }

        private bool ComprobarServidor(bool silencio)
        {
            puedeGuardar = false;
            try
            {
                Intelligob.Cliente.Referencia.IEntidades er = new Intelligob.Cliente.Referencia.EntidadesClient(Configuracion.ConfiguracionPunto, DireccionBase);
                if (!silencio)
                    CuadroMensajes.Aceptar("Informacion", "Operacion exitosa", "La conexion se ha podido establecer con exito, puede guardar los cambios", "", TaskDialogInterop.VistaTaskDialogIcon.Information);
                puedeGuardar = true;
            }
            catch
            {
                if (!silencio)
                    CuadroMensajes.Alertar("Atencion", "Operacion fallida", "No se puede establecer una conexion con el servidor", "");
                puedeGuardar = false;
            }
            return puedeGuardar;
        }

        private bool HabilitaGuardar()
        {
            return this.puedeGuardar;
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                if (columnName == "DireccionBase")
                {
                    if (string.IsNullOrWhiteSpace(this.DireccionBase))
                    {
                        error = "Se requiere la direccion del servidor";
                    }
                }
                PuedeProbar = error == String.Empty;
                return error;
            }
        }
    }
}
