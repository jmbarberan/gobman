using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml;

namespace Intelligob.Escritorio.ModeloVista.General
{
    public class ReporteEditorVM : BaseMV<Intelligob.Escritorio.Vistas.Interfaces.IVentanaDialogo>
    {
        private readonly Cliente.Depositos.SeguridadDep sdep = new Cliente.Depositos.SeguridadDep();
        private readonly Cliente.Depositos.RepRecaudacionesDep rdep = new Cliente.Depositos.RepRecaudacionesDep();
        private readonly Cliente.Depositos.TablasDep tdep = new Cliente.Depositos.TablasDep();

        private ObservableCollection<Utiles.ElementoSeleccion> lModulos;
        public ObservableCollection<Utiles.ElementoSeleccion> LModulos
        {
            get { return this.lModulos; }
            set { this.lModulos = value; OnPropertyChanged("LModulos"); }
        }
        private Cliente.Referencia.ReporteDto eReporte;        

        public String Denominacion
        {
            get { return this.eReporte.Denominacion; }
            set { this.eReporte.Denominacion = value; OnPropertyChanged("Denominacion"); }
        }

        private Utiles.ElementoSeleccion modulo;
        public Utiles.ElementoSeleccion Modulo
        {
            get { return modulo /*this.eReporte.ModuloNav*/; }
            set { modulo = value; OnPropertyChanged("Modulo"); }
        }

        private String archivo = "";
        public String Archivo
        {
            get { return this.archivo; }
            set { this.archivo = value; OnPropertyChanged("Archivo"); }
        }

        public ICommand CmdArchivo
        { get; internal set; }

        public ICommand CmdAceptar
        { get; internal set; }


        public ReporteEditorVM() : base(new Intelligob.Escritorio.Vistas.General.ReporteEditor())
        {
            this.CargarLista();
            this.CrearReporte();
            Iniciar();
        }

        public ReporteEditorVM(Cliente.Referencia.ReporteDto rep)
            : base(new Intelligob.Escritorio.Vistas.General.ReporteEditor())
        {
            this.CargarLista();
            this.eReporte = rep;
            if (eReporte.Definicion.Length > 0)
                this.Archivo = "Archivo cargado";
            if (eReporte != null)
            {
                if (eReporte.ModuloNav != null)
                {
                    this.Modulo = new Utiles.ElementoSeleccion((int)eReporte.Modulo, eReporte.ModuloNav.Denominacion);
                }
                else
                {
                    if (eReporte.Modulo != null && eReporte.Modulo > 0)
                    {
                        Cliente.Referencia.ModuloDto m = tdep.ModuloPorId(eReporte.Modulo);
                        this.Modulo = new Utiles.ElementoSeleccion(m.Id, m.Denominacion);
                    }
                }
            }
            Iniciar();
        }

        private void CargarLista()
        {
            List<Utiles.ElementoSeleccion> le = new List<Utiles.ElementoSeleccion>();
            List<Cliente.Referencia.ModuloDto> lm = new List<Cliente.Referencia.ModuloDto>(sdep.ModulosPorEstado(0));
            foreach(Cliente.Referencia.ModuloDto m in lm)
            {
                le.Add(new Utiles.ElementoSeleccion(m.Id, m.Denominacion));
            }
            LModulos = new ObservableCollection<Utiles.ElementoSeleccion>(le);
        }

        private void Iniciar()
        {            
            OnPropertyChanged("Modulo");            
            this.CmdArchivo = new Comandos.ComandoDelegado((o) => SeleccionarArchivo());
            this.CmdAceptar = new Comandos.ComandoDelegado((o) => Guardar(), (o) => HabilitaGuardar());            
            this.Vista.ShowDialog();
        }

        private void CrearReporte()
        {
            if (eReporte == null)
                this.eReporte = new Cliente.Referencia.ReporteDto();
            this.eReporte.Id = 0;
            this.eReporte.Modulo = 0;
            this.eReporte.ModuloNav = null;
            this.eReporte.Denominacion = "";
            this.eReporte.Definicion = "";
            this.eReporte.Estado = 0;            
        }

        private bool HabilitaGuardar()
        {
            return this.Denominacion.Length > 0 && this.Modulo != null && this.Archivo.Length > 0; 
        }

        private void SeleccionarArchivo()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Archivos de reporte (*.trdx)|*.trdx";
            dlg.CheckFileExists = true;
            dlg.Title = "Seleccionar archivo de reporte";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.Archivo = dlg.FileName;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(dlg.FileName);
                eReporte.Definicion = xmlDoc.InnerXml;
            }
        }

        private void Guardar()
        {
            if (modulo != null && modulo.Id > 0)
                eReporte.Modulo = modulo.Id;
            if (eReporte.Id == 0)
                rdep.CrearReporte(eReporte);
            else
                rdep.ModificarReporte(eReporte);
            Utiles.CuadroMensajes.Aceptar("Reportes", "Operacion completa", "Los cambios se han guardado satisfactoriamente", "", TaskDialogInterop.VistaTaskDialogIcon.Information);
            this.Vista.DialogResult = true;            
            this.Vista.Close();
        }
    }
}
