using Intelligob.Cliente.Referencia;
using Intelligob.Escritorio.Vistas.Agua;
using Intelligob.Escritorio.Vistas.Interfaces;
using System;
using System.Linq;

namespace Intelligob.Escritorio.ModeloVista.Agua
{
    public class LecturaEditorVM : BaseMV<IVentanaDialogo>
    {
        readonly AguaLecturaDto eLectura;

        public LecturaEditorVM(int mes, AguaLecturaDto lec) : base(new LecturaEditor())
        {
            eLectura = lec;
            AñoConsulta = (int)eLectura.Año;
            añoEmision = AñoConsulta;
            mesEmision = mes;
        }

        #region Variables locales

        private int añoEmision;
        private int mesEmision;
        private int añoConsulta;
        public int AñoConsulta
        {
            get { return añoConsulta; }
            set { this.añoConsulta = value; OnPropertyChanged("AñoConsulta"); }
        }

        private bool eneEditable;
        public bool EneEditable
        {
            get { return this.eneEditable; }
            set { this.eneEditable = value; OnPropertyChanged("EneEditable"); }
        }

        private bool febEditable;
        public bool FebEditable
        {
            get { return this.febEditable; }
            set { this.febEditable = value; OnPropertyChanged("FebEditable"); }
        }

        private bool marEditable;
        public bool MarEditable
        {
            get { return this.marEditable; }
            set { this.marEditable = value; OnPropertyChanged("MarEditable"); }
        }

        private bool abrEditable;
        public bool AbrEditable
        {
            get { return this.abrEditable; }
            set { this.abrEditable = value; OnPropertyChanged("AbrEditable"); }
        }

        private bool mayEditable;
        public bool MayEditable
        {
            get { return this.mayEditable; }
            set { this.mayEditable = value; OnPropertyChanged("MayEditable"); }
        }

        private bool junEditable;
        public bool JunEditable
        {
            get { return this.junEditable; }
            set { this.junEditable = value; OnPropertyChanged("JunEditable"); }
        }

        private bool julEditable;
        public bool JulEditable
        {
            get { return this.julEditable; }
            set { this.julEditable = value; OnPropertyChanged("JulEditable"); }
        }

        private bool agoEditable;
        public bool AgoEditable
        {
            get { return this.agoEditable; }
            set { this.agoEditable = value; OnPropertyChanged("AgoEditable"); }
        }

        private bool sepEditable;
        public bool SepEditable
        {
            get { return this.sepEditable; }
            set { this.sepEditable = value; OnPropertyChanged("SepEditable"); }
        }

        private bool octEditable;
        public bool OctEditable
        {
            get { return this.octEditable; }
            set { this.octEditable = value; OnPropertyChanged("OctEditable"); }
        }

        private bool novEditable;
        public bool NovEditable
        {
            get { return this.novEditable; }
            set { this.novEditable = value; OnPropertyChanged("NovEditable"); }
        }

        private bool dicEditable;
        public bool DicEditable
        {
            get { return this.dicEditable; }
            set { this.dicEditable = value; OnPropertyChanged("DicEditable"); }
        }

        #endregion
    
        private void ProcesarEditables()
        {
            EneEditable = false;
            FebEditable = false;
            MarEditable = false;
            AbrEditable = false;
            MayEditable = false;
            JunEditable = false;
            JulEditable = false;
            AgoEditable = false;
            SepEditable = false;
            OctEditable = false;
            NovEditable = false;
            DicEditable = false;

            if (AñoConsulta == añoEmision)
            {
                EneEditable = (mesEmision > 1);
                FebEditable = (mesEmision > 2);
                MarEditable = (mesEmision > 3);
                AbrEditable = (mesEmision > 4);
                MayEditable = (mesEmision > 5);
                JunEditable = (mesEmision > 6);
                JulEditable = (mesEmision > 7);
                AgoEditable = (mesEmision > 8);
                SepEditable = (mesEmision > 9);
                OctEditable = (mesEmision > 10);
                NovEditable = (mesEmision > 11);
                // del año en curso diciembre nunca podria modificarse
            }
            else
            {
                if (AñoConsulta < añoEmision)
                {
                    EneEditable = true;
                    FebEditable = true;
                    MarEditable = true;
                    AbrEditable = true;
                    MayEditable = true;
                    JunEditable = true;
                    JulEditable = true;
                    AgoEditable = true;
                    SepEditable = true;
                    OctEditable = true;
                    NovEditable = true;
                    DicEditable = true;
                }
            }
        }
    

    }
}
