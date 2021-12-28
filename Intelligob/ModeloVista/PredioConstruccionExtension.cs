using Intelligob.Cliente.Referencia;
using System;
using System.Collections.ObjectModel;

namespace Intelligob.Escritorio.ModeloVista
{
    public class ConstruccionElemento
    {
        public ConstruccionElemento()
        {
            mElemento = new TablaClaveDto();
        }

        public String Superior { get; set; }
        private TablaClaveDto mElemento;
        public TablaClaveDto Elemento
        {
            get { return mElemento; }
            set { mElemento = value; }
        }
    }

    public class ConstruccionComponente
    {
        public ConstruccionComponente()
        {
            Elementos = new ObservableCollection<ConstruccionElemento>();
        }

        public String Denominacion { get; set; }
        public ObservableCollection<ConstruccionElemento> Elementos { get; set; }
    }

    public class ConstruccionPlantilla
    {
        public ConstruccionPlantilla()
        {
            Componentes = new ObservableCollection<ConstruccionComponente>();
        }

        public String Denominacion { get; set; }
        public ObservableCollection<ConstruccionComponente> Componentes { get; set; }
    }
}
