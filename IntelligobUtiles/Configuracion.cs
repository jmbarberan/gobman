using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using WIA;

namespace Intelligob.Utiles
{
    public class Configuracion
    {        
        static readonly Configuracion mInstance = new Configuracion();
        public static Configuracion Instance
        {
            get { return mInstance; }
        }

        public Configuracion() { }

        public static string DireccionServidor
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("DireccionBase").ToString();
            }
            set
            {
                Configuration configura = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configura.AppSettings.Settings["DireccionBase"].Value = value;                
                configura.Save();
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        public static string ConfiguracionPunto
        {
            get { return ConfigurationManager.AppSettings.Get("ConfiguracionPunto").ToString(); }
            set
            {
                Configuration configura = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configura.AppSettings.Settings["ConfiguracionPunto"].Value = value;
                configura.Save();
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        public static Boolean CobrosMostrarCabeceraAgregados
        {
            get { return Convert.ToBoolean(ConfigurationManager.AppSettings.Get("CobrosMostrarCabeceraAgregados")); }
            set
            {
                Configuration configura = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configura.AppSettings.Settings["CobrosMostrarCabeceraAgregados"].Value = value.ToString();
                configura.Save();
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        public static Boolean CobrosMostrarColumnasAgregados
        {
            get { return Convert.ToBoolean(ConfigurationManager.AppSettings.Get("CobrosMostrarColumnasAgregados")); }
            set
            {
                Configuration configura = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configura.AppSettings.Settings["CobrosMostrarColumnasAgregados"].Value = value.ToString();
                configura.Save();
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        public static Boolean CobrosAutoDesplegarGrupos
        {
            get { return Convert.ToBoolean(ConfigurationManager.AppSettings.Get("CobrosDesplegarGrupos")); }
            set
            {
                Configuration configura = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configura.AppSettings.Settings["CobrosDesplegarGrupos"].Value = value.ToString();
                configura.Save();
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        public static string ImagenInicio
        {
            get { return ConfigurationManager.AppSettings.Get("ImagenInicio").ToString(); }
            set
            {
                Configuration configura = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configura.AppSettings.Settings["ImagenInicio"].Value = value;
                configura.Save();
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        public static string NombrePc
        {
            get { return System.Environment.MachineName; }
        }

        public static string IPLocal
        {
            get
            {
                IPHostEntry host;
                string localIP = "";
                host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        localIP = ip.ToString();
                        break;
                    }
                }
                return localIP;
            }
        }

        public static string MenuInicial
        {
            get { return ConfigurationManager.AppSettings.Get("MenuInicial").ToString(); }
            set
            {
                try
                {
                    Configuration configura = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    configura.AppSettings.Settings["MenuInicial"].Value = value;
                    configura.Save();
                    ConfigurationManager.RefreshSection("appSettings");
                }
                catch (ConfigurationErrorsException e)
                {
                    MessageBox.Show(e.ToString(), "Error de configuracion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static string DireccionesMac
        {
            get 
            {
                String ret = String.Empty;

                IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                ret = String.Format("Informacion de interfaz de red {0}.{1}     ",
                        computerProperties.HostName, computerProperties.DomainName);
                foreach (NetworkInterface adapter in nics)
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    ret = ret + "\n\r" + adapter.Description;
                    ret = ret + "\n\r" + String.Empty.PadLeft(adapter.Description.Length,'=');
                    ret = ret + "\n\r" + String.Format("  Interface type .......................... : {0}", adapter.NetworkInterfaceType);
                    ret = ret + "\n\r" + String.Format("  Physical Address ........................ : {0}", adapter.GetPhysicalAddress().ToString());
                    ret = ret + "\n\r" + String.Format("  Is receive only.......................... : {0}", adapter.IsReceiveOnly);
                    ret = ret + "\n\r" + String.Format("  Multicast................................ : {0}", adapter.SupportsMulticast);
                    
                }

                return ret;
            }
        }

        public static byte[] StrToByteArray(string str)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            return encoding.GetBytes(str);
        }

        public static Boolean ConexionComprobada
        {
            get;
            set;
        }        

        private static readonly PrincipalAuxiliar insPriAuxiliar = new PrincipalAuxiliar();

        public static PrincipalAuxiliar InsPriAuxiliar
        {
            get
            {
                return insPriAuxiliar;
            }
        }

        public static List<Scanner> Escanneres
        {
            get 
            {
                List<Scanner> ret = new List<Scanner>();

                var deviceManager = new DeviceManager();

                for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++)
                {
                    //Add the device to the list if it is a scanner
                    if (deviceManager.DeviceInfos[i].Type != WiaDeviceType.ScannerDeviceType)
                    {
                        continue;
                    }

                    ret.Add(new Scanner(deviceManager.DeviceInfos[i]));
                }

                return ret;
            }
            
        }

        public String EntidadPorId(int pEntidad)
        {            
            String descripcion = "N/D";
            Utiles.EntidadesEnum f = (Utiles.EntidadesEnum)Enum.GetValues(typeof(Utiles.EntidadesEnum)).GetValue(pEntidad);

            System.Reflection.FieldInfo fieldInfo = f.GetType().GetField(f.ToString());
            System.ComponentModel.DescriptionAttribute[] attributos = (System.ComponentModel.DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);

            if (attributos != null && attributos.Length > 0)
            {
                descripcion = attributos[0].Description;
            }
            return descripcion;            
        }
    }

    public class PrincipalAuxiliar : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private System.Windows.Visibility moduloVisibilidad = System.Windows.Visibility.Hidden;
        public System.Windows.Visibility ModuloVisibilidad
        {
            get { return moduloVisibilidad; }
            set
            {
                moduloVisibilidad = value;
                OnPropertyChanged("ModuloVisibilidad");
            }
        }

        private System.Windows.Visibility modVisAgua = System.Windows.Visibility.Collapsed;
        public System.Windows.Visibility ModVisAgua
        {
            get { return modVisAgua; }
            set
            {
                modVisAgua = value;
                OnPropertyChanged("ModVisAgua");
            }
        }

        private System.Windows.Visibility modVisSeguridad = System.Windows.Visibility.Collapsed;
        public System.Windows.Visibility ModVisSeguridad
        {
            get { return modVisSeguridad; }
            set
            {
                modVisSeguridad = value;
                OnPropertyChanged("ModVisSeguridad");
            }
        }

        private System.Windows.Visibility modVisCatastros = System.Windows.Visibility.Collapsed;
        public System.Windows.Visibility ModVisCatastros
        {
            get { return modVisCatastros; }
            set
            {
                modVisCatastros = value;
                OnPropertyChanged("ModVisCatastros");
            }
        }

        private System.Windows.Visibility modVisRecaudaciones = System.Windows.Visibility.Collapsed;
        
        public System.Windows.Visibility ModVisRecaudaciones
        {
            get { return modVisRecaudaciones; }
            set
            {
                modVisRecaudaciones = value;
                OnPropertyChanged("ModVisRecaudaciones");
            }
        }

        private System.Windows.Visibility modVisRentas = System.Windows.Visibility.Collapsed;

        public System.Windows.Visibility ModVisRentas
        {
            get { return modVisRentas; }
            set
            {
                modVisRentas = value;
                OnPropertyChanged("ModVisRentas");
            }
        }

        public System.Windows.Visibility ModVisConfiguracion
        {
            get { return System.Windows.Visibility.Visible; }
        }
            

        private object this[string propertyName]
        {
            get
            {
                Type myType = this.GetType();
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                return myPropInfo.GetValue(this, null);
            }
            set
            {
                Type myType = this.GetType();
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                myPropInfo.SetValue(this, value, null);
            }

        }
        
        public void HabilitarModulo(string pNombre)
        {            
            this[pNombre] = System.Windows.Visibility.Visible;
        }

        public bool ModuloHabilitado(string pModulo)
        {
            bool res = false;
            System.Windows.Visibility v = (System.Windows.Visibility)this[pModulo];
            res = v == System.Windows.Visibility.Visible;
            return res;
        }

        public string ModuloPredeterminado()
        {
            string ret = "MenuConfiguracionVM";
            if (this.ModVisCatastros == System.Windows.Visibility.Visible)
            {
                ret = "MenuCatastrosVM";
            }
            else
            {
                if (this.ModVisAgua == System.Windows.Visibility.Visible)
                {
                    ret = "MenuAguaVM";
                }                
            }
            return ret;
        }

        public void ModulosTodos(System.Windows.Visibility pVisibilidad)
        {
            this.ModVisAgua = pVisibilidad;
            this.ModVisCatastros = pVisibilidad;
            this.ModVisSeguridad = pVisibilidad;
            this.ModVisRecaudaciones = pVisibilidad;
            this.ModVisRentas = pVisibilidad;
        }

        private bool menuExpandido = false;
        public bool MenuExpandido
        {
            get { return this.menuExpandido; }
            set { this.menuExpandido = value; OnPropertyChanged("MenuExpandido"); }
        }

        public bool Iniciando = true;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }       

    }

    public interface ITextRow
    {
        String Output();
        void Output(StringBuilder sb);
        Object Tag { get; set; }
    }

    public class TablaCadena : IEnumerable<ITextRow>
    {
        protected class TextRow : List<String>, ITextRow
        {
            protected TablaCadena owner = null;
            public TextRow(TablaCadena pOwner)
            {
                owner = pOwner;
                if (owner == null) throw new ArgumentException("Owner");
            }
            public String Output()
            {
                StringBuilder sb = new StringBuilder();
                Output(sb);
                return sb.ToString();
            }
            public void Output(StringBuilder sb)
            {
                try
                {
                    sb.AppendFormat(owner.FormatString, this.ToArray());
                }
                catch (Exception)
                {
                    foreach (String s in this)
                    {
                        sb.AppendLine(s);
                    }
                }
            }
            public Object Tag { get; set; }
        }

        public String Separator { get; set; }
        public int ColsNum { get; set; }

        protected List<ITextRow> rows = new List<ITextRow>();
        protected List<int> colLength = new List<int>();

        public TablaCadena()
        {
            Separator = "  ";
        }

        public TablaCadena(String separator)
            : this()
        {
            Separator = separator;
        }

        public ITextRow AddRow(params object[] cols)
        {
            TextRow row = new TextRow(this);            
            foreach (object o in cols)
            {
                String str = String.Empty;
                if (o != null)
                    str = o.ToString().Trim();
                row.Add(str);
                if (colLength.Count >= row.Count)
                {
                    int curLength = colLength[row.Count - 1];
                    if (str.Length > curLength) 
                        colLength[row.Count - 1] = str.Length;
                }
                else
                {
                    colLength.Add(str.Length);
                }
            }
            rows.Add(row);
            return row;
        }

        public ITextRow AddRowMerged(String pfila)
        {
            TextRow filam = new TextRow(this);
            filam.Add(pfila);
            return filam;
        }

        protected String fmtString = null;
        public String FormatString
        {
            get
            {
                if (fmtString == null)
                {
                    String format = "";
                    int i = 0;
                    foreach (int len in colLength)
                    {
                        format += String.Format("{{{0},-{1}}}{2}", i++, len, Separator);
                    }
                    format += "\r\n";
                    fmtString = format;
                }
                return fmtString;
            }
        }

        public String Output()
        {
            StringBuilder sb = new StringBuilder();
            foreach (TextRow row in rows)
            {
                row.Output(sb);
            }
            return sb.ToString();
        }

        #region IEnumerable Members

        public IEnumerator<ITextRow> GetEnumerator()
        {
            return rows.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return rows.GetEnumerator();
        }

        #endregion
    }

}
