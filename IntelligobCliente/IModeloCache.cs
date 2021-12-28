using System;
using System.Collections.Generic;
using System.Linq;
using Intelligob.Cliente.Referencia;

namespace Intelligob.Cliente
{
    public interface IModeloCache
    {
        IEnumerable<TablaClaveDto> McClaves
        {
            get;
        }
        IEnumerable<TablaDto> McTablas
        {
            get;
        }
        IEnumerable<CoeficienteElementoDto> McCoeficienteElementos
        {
            get;
        }
        IEnumerable<CoeficienteDto> McCoeficientes
        {
            get;
        }
        IEnumerable<FuncionDto> McFunciones { get; }
        IEnumerable<ComandoDto> McComandos { get; }
        IEnumerable<ConceptoDto> McConceptos { get; }
        IEnumerable<RubroDto> McRubros { get; }
        IEnumerable<RebajaDto> McRebajas { get; }
        void ResetTablaCache(String pTabla);
    }
}
