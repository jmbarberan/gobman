using System;
using System.Collections.Generic;
using System.Linq;

namespace Intelligob.Cliente.Referencia
{
    public partial class ConceptoDto
    {
        private double totalEmision = 0;
        public Double TotalEmision
        {
            get { return this.totalEmision; }
            set { this.totalEmision = value; RaisePropertyChanged("TotalEmision"); }
        }

        private ConceptosEmisionDto parametroSeleccionado;
        public ConceptosEmisionDto ParametroSeleccionado
        {
            get { return this.parametroSeleccionado; }
            set { this.parametroSeleccionado = value; RaisePropertyChanged("ParametroSeleccionado"); }
        }

        private IEnumerable<RubroCalcularConcepto> rubrosCalculo;

        public IEnumerable<RubroCalcularConcepto> RubrosCalculo
        {
            get { return this.rubrosCalculo; }
            set { this.rubrosCalculo = value; RaisePropertyChanged("RubrosCalculo"); }
        }
    }
}
