using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Intelligob.Reportes
{
    public class ManejadorInformes
    {
        readonly Assembly reportAssembly;
        private readonly Cliente.Depositos.SeguridadDep sd = new Cliente.Depositos.SeguridadDep();
        
        public IEnumerable<Cliente.Modelos.Informe> TraerInformesPorModulo(int modulo)
        {
            if (null != this.reportAssembly)
            {
                String si = String.Empty;                
                switch (modulo)
                {                    
                    case 3: { si = "../Imagenes/infomonedas.png"; break; }
                    case 4: { si = "../Imagenes/inforeloj.png"; break; }
                    default: { si = "../Imagenes/informe.png"; break; }
                }
                
                List<Type> types = new List<Type>();
                foreach (Type t in this.reportAssembly.GetTypes())
                {
                    if (IsValidReportType(t, modulo))
                    {                        
                        types.Add(t);
                    }
                }

                types.Sort(delegate(Type t1, Type t2) { return t1.Name.CompareTo(t2.Name); });

                for (int i = 0; i < types.Count; i++)
                {
                    yield return CreateReportInfo(types[i], i, si);
                }
            }
        }        

        public ManejadorInformes() : this(typeof(ManejadorInformes).Assembly) { }

        public ManejadorInformes(Assembly reportAssembly)
        {
            this.reportAssembly = reportAssembly;
        }        

        private bool IsValidReportType(Type t, int modulo)
        {
            if (typeof(Telerik.Reporting.IReportDocument).IsAssignableFrom(t) && !t.IsAbstract)
            {
                object[] attributes = t.GetCustomAttributes(typeof(BrowsableAttribute), false);
                if (attributes.Length > 0)
                {
                    if (((BrowsableAttribute)attributes[0]).Browsable)
                    {
                        FieldInfo inf = t.GetField("EsInforme");
                        if (inf != null && (bool)inf.GetRawConstantValue())
                        {
                            FieldInfo mod = t.GetField("Modulo");
                            if (mod != null && (int)mod.GetRawConstantValue() == modulo)
                            {
                                FieldInfo fun = t.GetField("Funcion");
                                if (fun != null)
                                {
                                    int f = (int)fun.GetRawConstantValue();
                                    if (AccesoPermitido(f))
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    FieldInfo inf = t.GetField("EsInforme");
                    if (inf != null && (bool)inf.GetRawConstantValue())
                    {
                        FieldInfo mod = t.GetField("Modulo");
                        if (mod != null && (int)mod.GetRawConstantValue() == modulo)
                        {
                            FieldInfo fun = t.GetField("Funcion");
                            if (fun != null)
                            {
                                int f = (int)fun.GetRawConstantValue();
                                if (AccesoPermitido(f))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        private bool AccesoPermitido(int func)
        {
            if (Intelligob.Cliente.SesionUtiles.Instance.EsDesarrollador)
                return true;
            else
            {
                Cliente.Referencia.PrivilegioDto prv = sd.PrivilegiosFuncionPorUsuario(func, Cliente.SesionUtiles.Instance.UsuarioActivo.Id);
                if (prv != null && prv.Id > 0)
                {
                    return true;
                }
            }
            return false;
        }

        static Cliente.Modelos.Informe CreateReportInfo(Type t, int index, String ico)
        {
            String nom = String.Empty;
            String description = String.Empty;
            object[] attributes = null;

            attributes = t.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                description = ((DescriptionAttribute)attributes[0]).Description;
            }

            FieldInfo fnom = t.GetField("Nombre");
            if (fnom != null)
                nom = (String)fnom.GetRawConstantValue();
            else
                nom = FormatName(t.Name);

            String alternos = String.Empty;
            FieldInfo falter = t.GetField("Alternativos");
            if (falter != null)
            {
                alternos = (String)falter.GetRawConstantValue();
            }

            Utiles.ElementoSeleccion[] aryalts = null;
            if (alternos.Length > 0)
            {
                String[] ss = alternos.Split('@');
                Array.Resize(ref aryalts, ss.Length);
                for (int i = 0; i <= ss.Length - 1; i++)
                {
                    String[] ps = ss[i].Split(';');
                    aryalts[i] = new Utiles.ElementoSeleccion(i, 0, ps[1], ps[0]);
                }
            }            
            
            
            Cliente.Modelos.Informe reportInfo = new Intelligob.Cliente.Modelos.Informe(nom
                 , FormatDescription(description)
                 , t
                 , index
                 , ico
                 ,aryalts);

            return reportInfo;
        }

        static string FormatDescription(string text)
        {
            if (!text.EndsWith("."))
            {
                text += ".";
            }
            return text;
        }

        static string FormatName(string name)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < name.Length; i++)
            {
                char c = name[i];
                if (i == 0)
                {
                    c = Char.ToUpper(c);
                }
                else if (Char.IsUpper(c))
                {
                    sb.Append(" ");
                }
                sb.Append(c);
            }
            return sb.ToString();
        }

        public IEnumerable<Utiles.ElementoSeleccion> EleOrdenCarpetaCatastral()
        {
            List<Utiles.ElementoSeleccion> eleOrden = new List<Utiles.ElementoSeleccion>();
            Utiles.ElementoSeleccion opn = new Utiles.ElementoSeleccion(1, 0, "nombres", "Nombres del contribuyente");
            eleOrden.Add(opn);
            Utiles.ElementoSeleccion opc = new Utiles.ElementoSeleccion(2, 0, "codigo", "Codigo catastral");
            eleOrden.Add(opc);
            return eleOrden;
        }
    
        public IEnumerable<Utiles.ElementoSeleccion> EleOrdenCuentaCorriente()
        {
            List<Utiles.ElementoSeleccion> eleOrden = new List<Utiles.ElementoSeleccion>();
            Utiles.ElementoSeleccion opd = new Utiles.ElementoSeleccion(1, 0, "deuda", "Monto total adeudado");
            eleOrden.Add(opd);
            Utiles.ElementoSeleccion opn = new Utiles.ElementoSeleccion(2, 0, "nombres", "Nombres del contribuyente");
            eleOrden.Add(opn);
            Utiles.ElementoSeleccion opt = new Utiles.ElementoSeleccion(3, 0, "titulos", "Cantidad de titulos inpagos");
            eleOrden.Add(opt);
            return eleOrden;
        }

        public IEnumerable<Utiles.ElementoSeleccion> EleConceptosCuentaCorriente()
        {
            List<Utiles.ElementoSeleccion> eleOrden = new List<Utiles.ElementoSeleccion>();
            Utiles.ElementoSeleccion opd = new Utiles.ElementoSeleccion(1, 1, "pu", "Predios Urbanos");
            eleOrden.Add(opd);
            Utiles.ElementoSeleccion opn = new Utiles.ElementoSeleccion(2, 2, "pr", "Predios Rurales");
            eleOrden.Add(opn);
            Utiles.ElementoSeleccion opt = new Utiles.ElementoSeleccion(3, 3, "pt", "Patentes Municipales");
            eleOrden.Add(opt);
            Utiles.ElementoSeleccion opa = new Utiles.ElementoSeleccion(6, 6, "ap", "Agua Potable");
            eleOrden.Add(opa);
            return eleOrden;
        }

        [Telerik.Reporting.Expressions.Function]
        public static int ContribuyentePorCodigo(string codigo)
        {
            int ret = 0;
            if (! String.IsNullOrWhiteSpace(codigo))
            {
                string[] ss = codigo.Trim().Split(']');
                foreach (String s in ss)
                {
                    if (!String.IsNullOrWhiteSpace(s))
                    {
                        /*if (i <= 2)
                        {
                            if (!String.IsNullOrWhiteSpace(s))
                            {
                                String m = s.Trim().Substring(1);
                                ContribuyenteDto c = this.Servicio.ReadContribuyente(String.Format(this.FormatoClave, m));
                                if (cons.Length > 0)
                                    cons = cons + ", ";

                                cons = cons + c.Nombres;
                            }
                        }
                        else
                        {
                            cons = cons + String.Format("... {0} mas", ss.Length - 3);
                            break;
                        }
                        i = i + 1;*/

                        if (ss.Count() > 1)
                        {
                            // Escoger el primer
                            string sid = s.Trim().Substring(1);
                            ret = Convert.ToInt32(sid);
                            break;
                        }
                        else
                        {
                            string sid = s.Trim().Substring(1);
                            ret = Convert.ToInt32(sid);
                        }
                    }                    
                }
            }
            
            return ret;
        }
    }

    
}
