using System;
//using System.Linq;

namespace Intelligob.Cliente.Referencia
{
    public partial class CarpetaCatastralCorteItem
    {
        //private String Codigo = String.Empty;
        public int CodigoNumerico
        {
            get 
            {
                int res = 0;
                int man = 0;
                int pre = 0;
                if (this.codigo != null)
                {
                    if (this.codigo.Contains("-"))
                    {
                        string[] ss = this.codigo.Trim().Split('-');
                        if (ss.Length >=2 )
                        {
                            string cman = ss[ss.Length-2];
                            string cpre = ss[ss.Length-1];
                            man = Convert.ToInt32(cman);
                            pre = Convert.ToInt32(cpre);
                            res = man * 1000 + pre;
                        }
                    }
                    else
                    {
                        try
                        {
                            res = Convert.ToInt32(this.codigo);
                        }
                        catch
                        {
                            res = 0;
                        }
                    }

                    // procesar codigo
                    /*int sep1 = this.Codigo.IndexOf("-", 3);
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
                    res = iman + Convert.ToInt32(cpre);*/
                }
                return res;
            }
        }
    
        public String CodigoRelleno
        {
            get
            {
                String res = this.codigo.Trim();
                if (this.codigo.Contains("-"))
                {
                    string[] ss = this.codigo.Trim().Split('-');
                    if (ss.Length >=2 )
                    {
                        string csector = String.Empty;
                        if (ss.Length >= 3)
                        {
                            csector = ss[ss.Length - 3].PadLeft(3, '0');
                        }
                        string cman = ss[ss.Length - 2];
                        string cpre = ss[ss.Length - 1];
                        if (cman.Length <= 3 && cpre.Length <= 3)
                            res = csector + cman.PadLeft(3, '0') + cpre.PadLeft(3, '0');
                    }                    
                }
                return res;
            }
        }
    }
}
