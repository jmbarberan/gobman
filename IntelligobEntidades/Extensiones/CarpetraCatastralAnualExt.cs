using System;
using System.Linq;

namespace Intelligob.Entidades
{
    public partial class CarpetaCatastralAnual
    {
        /*public int CodigoNumerico
        {
            get 
            {                
                int res = 0;
                if (this.Codigo != null)
                {
                    int sep1 = this.Codigo.IndexOf("-", 3);
                    int sep2 = this.Codigo.IndexOf("-", sep1 + 1);
                    int largo = sep2 - sep1;
                    String cman = this.Codigo.Substring(sep1 + 1, largo - 1);
                    int iman = Convert.ToInt32(cman) * 1000;
                    int nuepos = this.Codigo.IndexOf("-", 1);
                    int pos = 0;
                    while (nuepos > 0)
                    {
                        nuepos = this.Codigo.IndexOf('-', nuepos + 1);
                        if (nuepos > 0)
                            pos = nuepos;
                    }
                    largo = this.Codigo.Trim().Length - pos;
                    string cpre = this.Codigo.Substring(pos + 1, largo - 1);
                    res = iman + Convert.ToInt32(cpre);
                }
                return res;
            }
        }*/
    }
}
