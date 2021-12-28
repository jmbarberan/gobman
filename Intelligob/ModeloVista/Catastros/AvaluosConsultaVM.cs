using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Intelligob.Escritorio.ModeloVista.Catastros
{
    public class AvaluosConsultaVM : BaseMV<Escritorio.Vistas.Interfaces.IVentanaDialogo>
    {
        Cliente.Depositos.RepRecaudacionesDep r = new Cliente.Depositos.RepRecaudacionesDep();

        private ObservableCollection<Cliente.Referencia.BaseImponibleAño> lAvaluos = new ObservableCollection<Cliente.Referencia.BaseImponibleAño>();
        public ObservableCollection<Cliente.Referencia.BaseImponibleAño> LAvaluos
        {
            get { return this.lAvaluos; }
            set { this.lAvaluos = value; OnPropertyChanged("LAvaluos"); }
        }

        public AvaluosConsultaVM(int pConcepto, string pCodigo) : base(new Escritorio.Vistas.Catastros.AvaluosConsulta())
        {
            LAvaluos = new ObservableCollection<Cliente.Referencia.BaseImponibleAño>(r.BaseImponiblePorConceptoCodigo(pConcepto, pCodigo));
            this.Vista.Owner = App.Current.MainWindow;
            this.Vista.ShowDialog();
        }
    }
}
