using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Intelligob.Escritorio.ModeloVista.Emisiones
{
    public class NotaCreditoSeleccionarVM : BaseMV<Vistas.Interfaces.IVentanaDialogo>
    {
        private readonly Cliente.Depositos.EmisionesDep emisionesDep = new Cliente.Depositos.EmisionesDep();
        private ObservableCollection<Cliente.Referencia.ConvenioDto> lNCreditos;
        public ObservableCollection<Cliente.Referencia.ConvenioDto> LNCreditos
        {
            get { return lNCreditos; }
            set { this.lNCreditos = value; OnPropertyChanged("LNCreditos"); }
        }

        private Cliente.Referencia.ConvenioDto seleccionado;
        public Cliente.Referencia.ConvenioDto Seleccionado
        {
            get { return this.seleccionado; }
            set { this.seleccionado = value; OnPropertyChanged("Seleccionado"); }
        }

        public System.Windows.Input.ICommand CmdSeleccionar
        { get; internal set; }        

        public NotaCreditoSeleccionarVM(IEnumerable<int> pContribuyentes) : base(new Vistas.Emisiones.NotaCreditoSeleccionar())
        {
            this.CmdSeleccionar = new Comandos.ComandoDelegado((s) => SeleccionarAccion(), (s) => SeleccionarHabilita());
            CargarNCPorContribuyentes(pContribuyentes);
            this.Vista.ShowDialog();
        }

        private void CargarNCPorContribuyentes(IEnumerable<int> pCons)
        {
            List<Cliente.Referencia.ConvenioDto> lc = new List<Cliente.Referencia.ConvenioDto>();
            foreach(int c in pCons)
            {

                lc.AddRange(this.emisionesDep.NCreditosPorContribuyenteEstado(c, 0, true));
            }
            this.LNCreditos = new ObservableCollection<Cliente.Referencia.ConvenioDto>(lc);
        }

        private void SeleccionarAccion()
        {
            this.Vista.DialogResult = true;
            this.Vista.Close();
        }

        private Boolean SeleccionarHabilita()
        {
            return this.Seleccionado != null;
        }
    }
}
