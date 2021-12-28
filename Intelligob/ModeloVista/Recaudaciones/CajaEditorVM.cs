using System;
using System.Linq;

namespace Intelligob.Escritorio.ModeloVista.Recaudaciones
{
    public class CajaEditorVM : BaseMV<Vistas.Interfaces.IVentanaDialogo>, System.ComponentModel.IDataErrorInfo
    {
        private readonly Cliente.Depositos.CajasDep cajasDep = new Cliente.Depositos.CajasDep();
        private readonly Cliente.Referencia.CajaDto oCaja;        

        public String Codigo
        {
            get 
            {
                string s = String.Empty;
                if (this.oCaja != null)
                    s = this.oCaja.Codigo;
                return s;
            }
            set 
            {
                if (this.oCaja != null)
                {
                    this.oCaja.Codigo = value;
                    OnPropertyChanged("Codigo");
                }
            }
        }

        public String Descripcion
        {
            get 
            {
                string s = String.Empty;
                if (this.oCaja != null)
                    s = this.oCaja.Descripcion;
                return s;
            }
            set 
            {
                if (this.oCaja != null)
                    this.oCaja.Descripcion = value; OnPropertyChanged("Descripcion");
            }
        }

        public System.Windows.Input.ICommand CmdGuardar { get; internal set; }

        public CajaEditorVM() : this(null) { }

        public CajaEditorVM(Cliente.Referencia.CajaDto pCaja) : base(new Vistas.Recaudaciones.CajaEditor())
        {            
            if (pCaja == null)
            {
                pCaja = new Cliente.Referencia.CajaDto();
                pCaja.Id = 0;
                pCaja.Estado = 0;
                pCaja.Saldo = 0;
                pCaja.Codigo = String.Empty;
                pCaja.Descripcion = String.Empty;
            }
            this.oCaja = pCaja;
            this.CmdGuardar = new Comandos.ComandoDelegado((o) => this.GuardarAccion(), (o) => this.GuardarHabilita());
            this.Vista.Owner = App.Current.MainWindow;
            this.Vista.ShowDialog();
        }

        private bool GuardarHabilita()
        {
            return String.IsNullOrEmpty(this["Codigo"]) && String.IsNullOrEmpty(this["Descripcion"]);
        }

        private void GuardarAccion()
        {
            if (this.oCaja.Id > 0)
            {
                this.cajasDep.ModificarCaja(this.oCaja);
            }
            else
            {
                int i = this.cajasDep.CajaCrear(this.oCaja);
                this.oCaja.Id = i;
            }
            this.Vista.DialogResult = true;
            this.Vista.Close();
        }

        public string this[string columna]
        {
            get
            {
                String error = String.Empty;
                if (columna == "Codigo")
                {
                    if (String.IsNullOrEmpty(this.Codigo))
                        error = "Debe digitar un codigo para la caja";
                    else 
                    {
                        if (cajasDep.CajaExistePorCodigo(this.Codigo, this.oCaja.Id))
                            error = "Este codigo ya se encuentra registrado";
                    }                    
                }
                else
                {
                    if (columna == "Descripcion")
                    {
                        if (String.IsNullOrWhiteSpace(this.Descripcion))
                            error = "Se requiere una descripcion de la caja";
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
