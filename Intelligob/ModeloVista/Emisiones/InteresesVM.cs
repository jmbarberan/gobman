using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Intelligob.Escritorio.ModeloVista.Emisiones
{
    public class InteresesVM : BaseMV<Vistas.Interfaces.IPagina>
    {
        private readonly Cliente.Depositos.CoeficientesDep coeficientesDep = new Cliente.Depositos.CoeficientesDep();
        private readonly System.Collections.Generic.List<Cliente.Referencia.CoeficienteElementoDto> lEliminados = new System.Collections.Generic.List<Cliente.Referencia.CoeficienteElementoDto>();
        
        private Cliente.Referencia.CoeficienteElementoDto seleccionado;
        public Cliente.Referencia.CoeficienteElementoDto Seleccionado
        {
            get { return this.seleccionado; }
            set 
            {                
                if (value != null)
                    this.BarraEstado = "Año " + value.Clave.ToString() + " Seleccionado.";
                this.seleccionado = value;
                OnPropertyChanged("Seleccionado"); 
            }
        }

        private ObservableCollection<Cliente.Referencia.CoeficienteElementoDto> lIntereses = new ObservableCollection<Cliente.Referencia.CoeficienteElementoDto>();
        public ObservableCollection<Cliente.Referencia.CoeficienteElementoDto> LIntereses
        {
            get { return this.lIntereses; }
            set { this.lIntereses = value; OnPropertyChanged("LIntereses"); }
        }

        private string barraEstado = String.Empty;
        public String BarraEstado
        {
            get { return barraEstado; }
            set { this.barraEstado = value; OnPropertyChanged("BarraEstado"); }
        }

        public System.Windows.Input.ICommand CmdActualizar
        { get; internal set; }

        public System.Windows.Input.ICommand CmdInsertar
        { get; internal set; }

        public System.Windows.Input.ICommand CmdRemover
        { get; internal set; }

        public System.Windows.Input.ICommand CmdGuardar
        { get; internal set; }

        public InteresesVM() : base(new Vistas.Emisiones.Intereses())
        {
            this.ActualizarAccion();
            this.CmdActualizar = new Comandos.ComandoDelegado((o) => ActualizarAccion());
            this.CmdInsertar = new Comandos.ComandoDelegado((o) => this.InsertarAccion());
            this.CmdRemover = new Comandos.ComandoDelegado((o) => RemoverAccion(), (o) => RemoverHabilita());
            this.CmdGuardar = new Comandos.ComandoDelegado((o) => GuardarAccion());
        }

        public void ActualizarAccion()
        {
            this.LIntereses = new ObservableCollection<Cliente.Referencia.CoeficienteElementoDto>(coeficientesDep.CoeficienteElementosPorTipoEstadoOrden(2, 0, "clave"));
        }

        public void InsertarAccion()
        {
            General.CapturaNumeroVM cn = new General.CapturaNumeroVM("Digite el año");
            if (cn.Vista.DialogResult == true)
            {
                int año = -1;
                año = cn.Entero;
                bool ins = false;
                foreach(Cliente.Referencia.CoeficienteElementoDto ce in LIntereses)
                {
                    if (ce.Clave != null && (int)ce.Clave == año)
                        ins = true;
                }
                if (ins)
                {
                    Utiles.CuadroMensajes.Alertar("No se puede insertar", "El año ya esta registrado", "El año digitado ya esta registrado", "");
                }
                else
                {
                    Cliente.Referencia.CoeficienteElementoDto cel = new Cliente.Referencia.CoeficienteElementoDto();
                    cel.Coeficiente = 2;
                    cel.Clave = año;
                    cel.Valor = 0;
                    cel.Id = 0;
                    cel.Estado = 0;
                    LIntereses.Add(cel);
                }
            }
        }

        public void RemoverAccion()
        {
            TaskDialogInterop.TaskDialogResult r = Utiles.CuadroMensajes.Preguntar("Confirmar operacion", "Se eliminara este registro", "Los intereses correspondientes a este año no podran calcularse ¿Seguro de continuar?");
            if (r.CustomButtonResult == 0)
            {                
                if (Seleccionado.Id > 0)
                    lEliminados.Add(Seleccionado);
                LIntereses.Remove(Seleccionado);
                Seleccionado = null;
            }
        }

        public Boolean RemoverHabilita()
        {
            return this.Seleccionado != null;
        }

        public void GuardarAccion()
        {
            this.coeficientesDep.CoeficienteElementosGuardar(LIntereses);
            foreach(Cliente.Referencia.CoeficienteElementoDto ce in lEliminados)
            {                
                ce.Estado = 2;
            }
            this.coeficientesDep.CoeficienteElementosGuardar(lEliminados);            
            Utiles.CuadroMensajes.Aceptar("Tabla de intereses", "Operacion completa", "Los cambios se han guardado exitosamente", "", TaskDialogInterop.VistaTaskDialogIcon.Information);
            bool b = false;
            foreach(Cliente.Referencia.CoeficienteElementoDto ce in LIntereses)
            {
                if (ce.Id <= 0)
                {
                    b = true;
                    break;
                }
            }
            if (b)
                this.ActualizarAccion();
        }
    }
}
