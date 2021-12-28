using System;
using System.Linq;

namespace Intelligob.Cliente.Referencia
{
    public partial class AguaLecturaDto
    {
        public Double LecturaAnterior
        {
            get 
            {
                Double l = 0;
                TablaClaveDto tm = ModeloCache.Instance.McClaves.Where(c => c.Tabla == 3 && c.Clave == 2).FirstOrDefault();
                int mes = (int)tm.Superior;
                tm = ModeloCache.Instance.McClaves.Where(c => c.Tabla == 3 && c.Clave == 1).FirstOrDefault();
                int año = (int)tm.Superior;
                if (this.Año == año)
                {
                    if (mes >= 7)
                    {
                        switch (mes)
                        {
                            case 7:
                                {
                                    if (Mes7 != null)
                                        l = (Double)Mes7;
                                    break;
                                }
                            case 8:
                                {
                                    if (Mes8 != null)
                                        l = (Double)Mes8;
                                    break;
                                }
                            case 9:
                                {
                                    if (Mes9 != null)
                                        l = (Double)Mes9;
                                    break;
                                }
                            case 10:
                                {
                                    if (Mes10 != null)
                                        l = (Double)Mes10;
                                    break;
                                }
                            case 11:
                                {
                                    if (Mes11 != null)
                                        l = (Double)Mes11;
                                    break;
                                }
                            case 12:
                                {
                                    if (Mes12 != null)
                                        l = (Double)Mes12;
                                    break;
                                }
                        }
                    }
                    else
                    {
                        switch (mes)
                        {
                            case 1:
                                {
                                    if (Mes1 != null)
                                        l = (Double)Mes1;
                                    break;
                                }
                            case 2:
                                {
                                    if (Mes2 != null)
                                        l = (Double)Mes2;
                                    break;
                                }
                            case 3:
                                {
                                    if (Mes3 != null)
                                        l = (Double)Mes3;
                                    break;
                                }
                            case 4:
                                {
                                    if (Mes4 != null)
                                        l = (Double)Mes4;
                                    break;
                                }
                            case 5:
                                {
                                    if (Mes5 != null)
                                        l = (Double)Mes5;
                                    break;
                                }
                            case 6:
                                {
                                    if (Mes6 != null)
                                        l = (Double)Mes6;
                                    break;
                                }
                        }
                    }
                }
                else
                {
                    if (this.Año > año)
                    {
                        Depositos.AguaDep ad = new Depositos.AguaDep();
                        AguaLecturaDto lec = ad.LecturaTraerPorCuentaAño((int)this.Cuenta, año);
                        if (lec != null)
                        {
                            if (mes >= 7)
                            {
                                switch (mes)
                                {
                                    case 7:
                                        {
                                            if (lec.Mes7 != null)
                                                l = (Double)lec.Mes7;
                                            break;
                                        }
                                    case 8:
                                        {
                                            if (lec.Mes8 != null)
                                                l = (Double)lec.Mes8;
                                            break;
                                        }
                                    case 9:
                                        {
                                            if (lec.Mes9 != null)
                                                l = (Double)lec.Mes9;
                                            break;
                                        }
                                    case 10:
                                        {
                                            if (lec.Mes10 != null)
                                                l = (Double)lec.Mes10;
                                            break;
                                        }
                                    case 11:
                                        {
                                            if (lec.Mes11 != null)
                                                l = (Double)lec.Mes11;
                                            break;
                                        }
                                    case 12:
                                        {
                                            if (lec.Mes12 != null)
                                                l = (Double)lec.Mes12;
                                            break;
                                        }
                                }
                            }
                            else
                            {
                                switch (mes)
                                {
                                    case 1:
                                        {
                                            if (lec.Mes1 != null)
                                                l = (Double)lec.Mes1;
                                            break;
                                        }
                                    case 2:
                                        {
                                            if (lec.Mes2 != null)
                                                l = (Double)lec.Mes2;
                                            break;
                                        }
                                    case 3:
                                        {
                                            if (lec.Mes3 != null)
                                                l = (Double)lec.Mes3;
                                            break;
                                        }
                                    case 4:
                                        {
                                            if (lec.Mes4 != null)
                                                l = (Double)lec.Mes4;
                                            break;
                                        }
                                    case 5:
                                        {
                                            if (lec.Mes5 != null)
                                                l = (Double)lec.Mes5;
                                            break;
                                        }
                                    case 6:
                                        {
                                            if (lec.Mes6 != null)
                                                l = (Double)lec.Mes6;
                                            break;
                                        }
                                }
                            }
                        }
                    }
                }

                return l;
            }
        }

        public Double LecturaActual
        {            
            get
            {                                
                Double l = 0;

                TablaClaveDto tm = ModeloCache.Instance.McClaves.Where(c => c.Tabla == 3 && c.Clave == 2).FirstOrDefault();
                int mes = (int)tm.Superior;
                tm = ModeloCache.Instance.McClaves.Where(c => c.Tabla == 3 && c.Clave == 1).FirstOrDefault();
                int año = (int)tm.Superior;
                if (mes == 12)
                { mes = 1; año++; }
                else
                    mes++;
                if (this.Año == año)
                {
                    mes++;
                    if (mes >= 7)
                    {
                        switch (mes)
                        {
                            case 7:
                                {
                                    if (Mes7 != null)
                                        l = (Double)Mes7;
                                    break;
                                }
                            case 8:
                                {
                                    if (Mes8 != null)
                                        l = (Double)Mes8;
                                    break;
                                }
                            case 9:
                                {
                                    if (Mes9 != null)
                                        l = (Double)Mes9;
                                    break;
                                }
                            case 10:
                                {
                                    if (Mes10 != null)
                                        l = (Double)Mes10;
                                    break;
                                }
                            case 11:
                                {
                                    if (Mes11 != null)
                                        l = (Double)Mes11;
                                    break;
                                }
                            case 12:
                                {
                                    if (Mes12 != null)
                                        l = (Double)Mes12;
                                    break;
                                }
                        }
                    }
                    else
                    {
                        switch (mes)
                        {
                            case 1:
                                {
                                    if (Mes1 != null)
                                        l = (Double)Mes1;
                                    break;
                                }
                            case 2:
                                {
                                    if (Mes2 != null)
                                        l = (Double)Mes2;
                                    break;
                                }
                            case 3:
                                {
                                    if (Mes3 != null)
                                        l = (Double)Mes3;
                                    break;
                                }
                            case 4:
                                {
                                    if (Mes4 != null)
                                        l = (Double)Mes4;
                                    break;
                                }
                            case 5:
                                {
                                    if (Mes5 != null)
                                        l = (Double)Mes5;
                                    break;
                                }
                            case 6:
                                {
                                    if (Mes6 != null)
                                        l = (Double)Mes6;
                                    break;
                                }
                        }
                    }
                }
                
                return l;
            }

            set
            {
                TablaClaveDto tm = ModeloCache.Instance.McClaves.Where(c => c.Tabla == 3 && c.Clave == 2).FirstOrDefault();
                int mes = (int)tm.Superior;
                tm = ModeloCache.Instance.McClaves.Where(c => c.Tabla == 3 && c.Clave == 1).FirstOrDefault();
                int año = (int)tm.Superior;
                if (mes == 12)
                { mes = 1; año++; }
                else
                    mes++;
                if (this.Año == año)
                {
                    mes++;
                    if (mes >= 7)
                    {
                        switch (mes)
                        {
                            case 7:
                                {
                                    this.Mes7 = value;
                                    break;
                                }
                            case 8:
                                {
                                    this.Mes8 = value;
                                    break;
                                }
                            case 9:
                                {
                                    this.Mes9 = value;
                                    break;
                                }
                            case 10:
                                {
                                    this.Mes10 = value;
                                    break;
                                }
                            case 11:
                                {
                                    this.Mes11 = value;
                                    break;
                                }
                            case 12:
                                {
                                    this.Mes12 = value;
                                    break;
                                }
                        }
                    }
                    else
                    {
                        switch (mes)
                        {
                            case 1:
                                {
                                    this.Mes1 = value;
                                    break;
                                }
                            case 2:
                                {
                                    this.Mes2 = value;
                                    break;
                                }
                            case 3:
                                {
                                    this.Mes3 = value;
                                    break;
                                }
                            case 4:
                                {
                                    this.Mes4 = value;
                                    break;
                                }
                            case 5:
                                {
                                    this.Mes5 = value;
                                    break;
                                }
                            case 6:
                                {
                                    this.Mes6 = value;
                                    break;
                                }
                        }
                    }
                }
            }
        }
    
        public Double Comsumo
        {
            get { return this.LecturaActual - this.LecturaAnterior; }
        }
    }
}
