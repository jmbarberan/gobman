using System;
using System.Linq;


namespace Intelligob.Cliente.Depositos
{
    public interface IDepositosControl
    {
        CatastrosDep CatastrosDepositoCrear();

        TablasDep TablasDepositoCrear();

        SeguridadDep SeguridadDepositoCrear();

        ContribuyentesDep ContribuyentesDepositoCrear();

        AguaDep AguaDepositoCrear();

        ConceptosDep ConceptosDepositoCrear();

        CoeficientesDep CoeficientesDepositoCrear();

        Intelligob.Cliente.Referencia.IEntidades Servicio
        {
            get;
            set;
        }

    }
}
