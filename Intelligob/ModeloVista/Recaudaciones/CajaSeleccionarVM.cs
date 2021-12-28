using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Intelligob.Escritorio.ModeloVista.Recaudaciones
{
    public class CajaSeleccionarVM : BaseMV<Vistas.Interfaces.IVentanaDialogo>
    {
        private readonly Cliente.Depositos.CajasDep cajasDep = new Cliente.Depositos.CajasDep();
        private ObservableCollection<Cliente.Referencia.CajaDto> lcajas;
        public ObservableCollection<Cliente.Referencia.CajaDto> LCajas
        {            
            get { return this.lcajas; }
            set { this.lcajas = value; OnPropertyChanged("LCajas"); }
        }

        private Cliente.Referencia.CajaDto seleccionado = null;
        public Cliente.Referencia.CajaDto Seleccionado
        {
            get { return this.seleccionado; }
            set { this.seleccionado = value; OnPropertyChanged("Seleccionado"); }
        }

        public System.Windows.Input.ICommand CmdSeleccionar
        { get; internal set; }

        public CajaSeleccionarVM(IEnumerable<Cliente.Referencia.CajaDto> pCajas) : base(new Vistas.Recaudaciones.CajaSeleccionar())
        {
            if (pCajas == null || pCajas.Count() <= 0)
            {
                IEnumerable<Cliente.Referencia.CajaDto> cs = cajasDep.CajasPorEstado(0);
                pCajas = cs;
            }
            
            if (pCajas.Count() > 0)
                this.LCajas = new ObservableCollection<Cliente.Referencia.CajaDto>(pCajas);
            this.CmdSeleccionar = new Comandos.ComandoDelegado((s) => SeleccionarAccion(), (s) => SeleccionarHabilita());
            this.Vista.ShowDialog();
        }

        private void SeleccionarAccion()
        {
            this.Vista.DialogResult = true;
            this.Vista.Close();
        }

        private bool SeleccionarHabilita()
        {
            return this.Seleccionado != null;
        }
    }
}
