using System;
using System.Linq;
using Intelligob.Utiles;

namespace Intelligob.Cliente.Depositos
{
    public class DepositosControl: IDepositosControl
    {
        private static IDepositosControl instance;
        public static IDepositosControl Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DepositosControl();                   
                }                
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        public Intelligob.Cliente.Referencia.IEntidades Servicio { get; set; }

        public DepositosControl()
        {
            if (this.Servicio == null)
            {
                this.Servicio = new Intelligob.Cliente.Referencia.EntidadesClient(Configuracion.ConfiguracionPunto, Configuracion.DireccionServidor);
            }
        }

        public CatastrosDep CatastrosDepositoCrear()
        {
            return new CatastrosDep(this.Servicio);
        }

        public TablasDep TablasDepositoCrear()
        {
            return new TablasDep(this.Servicio);
        }

        public SeguridadDep SeguridadDepositoCrear()
        {
            return new SeguridadDep(this.Servicio);
        }

        public ContribuyentesDep ContribuyentesDepositoCrear()
        {
            return new ContribuyentesDep(this.Servicio);
        }

        public AguaDep AguaDepositoCrear()
        {
            return new AguaDep(this.Servicio);
        }

        public ConceptosDep ConceptosDepositoCrear()
        {
            return new ConceptosDep(this.Servicio);
        }

        public CoeficientesDep CoeficientesDepositoCrear()
        {
            return new CoeficientesDep(this.Servicio);
        }
    }
}
