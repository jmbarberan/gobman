using System;
using System.Linq;

namespace Intelligob.Cliente.Referencia
{
    public partial class PlanillaAtributoDto
    {
        public string AtributoCadena
        {
            get 
            { 
                string res = String.Empty;
                switch(this.Tipo)
                {
                    case 1:
                        {
                            if (this.ValorI != null)
                                res = this.ValorI.ToString();
                            break;
                        }
                    case 2:
                        {
                            if (this.ValorD != null)
                                res = String.Format("{0:N}", this.ValorD);
                            break;
                        }
                    case 3:
                        {
                            if (this.ValorF != null)
                                res = this.ValorF.ToString();
                            break;
                        }
                    default:
                        {
                            if (this.ValorC != null)
                                res = this.ValorC;
                            break;
                        }
                }
                return res;
            }
        }
    }
}
