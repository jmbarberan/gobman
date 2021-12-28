using Intelligob.Cliente.Depositos;
using Intelligob.Cliente.Referencia;
using Intelligob.Escritorio.ModeloVista.Comandos;
using Intelligob.Escritorio.Vistas.Catastros;
using Intelligob.Escritorio.Vistas.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Intelligob.Escritorio.ModeloVista.Catastros
{
    public class MercadoEditorMV : BaseMV<IControlUsuario>
    {
        private MercadoDto epuesto;
        private ContribuyenteDto con;
        private Action accionCerrar;

        #region Atrbutos del modelo

        public int? Puesto
        {
            get { if (epuesto != null) return epuesto.Puesto; else return 0; }
            set 
            {
                if (epuesto != null)
                {
                    epuesto.Puesto = value;
                    OnPropertyChanged("Puesto");
                }
            }
        }

        public String Codigo
        {
            get { if (epuesto != null) return epuesto.Codigo; else return String.Empty; }
            set
            {
                if (epuesto != null)
                {
                    epuesto.Codigo = value;
                    OnPropertyChanged("Codigo");
                }
            }
        }

        public String Actividad
        {
            get { if (epuesto != null) return epuesto.Actividad; else return String.Empty; }
            set 
            {
                if (epuesto != null)
                {
                    epuesto.Actividad = value;
                    OnPropertyChanged("Actividad");
                }
            }
        }

        private string contribuyente = String.Empty;
        public String Contribuyente
        {
            get { return contribuyente; }
            set
            {
                contribuyente = value;
                OnPropertyChanged("Contribuyente");
            }
        }

        public bool Contrato
        {
            get { if (epuesto != null) return epuesto.Contrato == 1; else return false; }
            set 
            {
                if (epuesto != null)
                {
                    if (value)
                        epuesto.Contrato = 1;
                    else
                        epuesto.Contrato = 0;
                }
                OnPropertyChanged("Contrato");
            }
        }

        public DateTime? Inscripcion
        {
            get { if (epuesto != null) return epuesto.InscripcionFecha; else return null; }
            set
            {
                if (epuesto != null)
                {
                    epuesto.InscripcionFecha = value;
                }
                OnPropertyChanged("Inscripcion");
            }
        }

        public DateTime? FechaDesde
        {
            get { if (epuesto != null) return epuesto.Desde; else return null; }
            set
            {
                if (epuesto != null)
                {
                    epuesto.Desde = value;
                }
                OnPropertyChanged("FechaDesde");
            }
        }

        public DateTime? FechaHasta
        {
            get { if (epuesto != null) return epuesto.Hasta; else return null; }
            set
            {
                if (epuesto != null)
                {
                    epuesto.Hasta = value;
                }
                OnPropertyChanged("FechaHasta");
            }
        }

        public String Observaciones
        {
            get { if (epuesto != null) return epuesto.Observaciones; else return String.Empty; }
            set
            {
                if (epuesto != null)
                {
                    epuesto.Observaciones = value;
                }
                OnPropertyChanged("Observaciones");
            }
        }

        #endregion

        #region Comandos

        public ICommand CmdGuardar
        { get; internal set; }

        public ICommand CmdCancelar
        { get; internal set; }

        public ICommand CmdSeleccionarContribuyente
        { get; internal set; }

        #endregion

        public MercadoEditorMV(MercadoDto e, Action ac) : base(new MercadoEditor())
        {
            epuesto = e;
            accionCerrar = ac;
            CmdGuardar = new ComandoDelegado((o) => Guardar(), (o) => GuadarHabilita());
            CmdCancelar = new ComandoDelegado((o) => Cancelar());
            CmdSeleccionarContribuyente = new ComandoDelegado(o => ContribuyenteSeleccionar());
        }

        public MercadoEditorMV(MercadoDto e, Action ac, MercadoEditor v) : base(v)
        {
            epuesto = e;
            if (epuesto.Id > 0 && epuesto.ContribuyenteNav != null)
            {
                con = epuesto.ContribuyenteNav;
                Contribuyente = con.Nombres;
            }
            accionCerrar = ac;
            CmdGuardar = new ComandoDelegado((o) => Guardar(), (o) => GuadarHabilita());
            CmdSeleccionarContribuyente = new ComandoDelegado(o => ContribuyenteSeleccionar());
            CmdCancelar = new ComandoDelegado((o) => Cancelar());
        }

        private bool GuadarHabilita()
        {
            return epuesto != null && Puesto > 0 && epuesto.Contribuyente > 0;
        }

        private void Guardar()
        {
            using (CatastrosDep d = new CatastrosDep())
            {
                if (epuesto.Id > 0)
                    d.MercadoModificar(epuesto);
                else
                    d.MercadoNuevo(epuesto);
            }
            accionCerrar.Invoke();
        }

        private void ContribuyenteSeleccionar()
        {
            SeleccionarContribuyenteVM sc = new SeleccionarContribuyenteVM();
            if (sc.Vista.DialogResult == true)
            {
                con = sc.Seleccionado;
                Contribuyente = con.Nombres;
                epuesto.Contribuyente = sc.Seleccionado.Id;
            }
        }

        private void Cancelar()
        {
            accionCerrar.Invoke();
        }
        
    }
}
