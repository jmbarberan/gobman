using Intelligob.Cliente.Depositos;
using Intelligob.Cliente.Modelos;
using Intelligob.Cliente.Referencia;
using Intelligob.Escritorio.ModeloVista.General;
using Intelligob.Escritorio.Vistas.Emisiones;
using Intelligob.Escritorio.Vistas.General;
using Intelligob.Utiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Intelligob.Escritorio.ModeloVista.Emisiones
{
    class MercadoIngresoVM : BaseMV<Intelligob.Escritorio.Vistas.Interfaces.IVentanaDialogo>
    {
        MercadoDto puesto;
        PlanillaDto planilla = new PlanillaDto();
        PlanillaRubroDto prubro = new PlanillaRubroDto();
        PlanillaMesDto pmeses = new PlanillaMesDto();
        List<PlanillaAtributoDto> atribs = new List<PlanillaAtributoDto>();
        ContribuyentesDep nd = new ContribuyentesDep();
        bool insertando = false;
        public bool Fila0Ins = false;

        private int local = 0;
        public int Local
        {
            get { return local; }
            set 
            {
                local = value; 
                if (value > 0)
                {
                    using (EmisionesDep d = new EmisionesDep())
                    {
                        puesto = d.MercadoPuestoPorNumero(value);
                    }
                }
                OnPropertyChanged("Local");
                OnPropertyChanged("Contribuyente");
                OnPropertyChanged("Actividad");
            }
        }

        private int anio = DateTime.Today.Year;
        public int Anio
        {
            get { return anio; }
            set { anio = value; OnPropertyChanged("Anio"); }
        }

        private double valor = 0;
        public double Valor
        {
            get { return valor; }
            set { valor = value; OnPropertyChanged("Valor"); }
        }

        private double cuota = 30;
        public double Cuota
        {
            get { return cuota; }
            set { cuota = value; OnPropertyChanged("Cuota"); }
        }

        public String Contribuyente
        {
            get
            {
                String ret = String.Empty;
                if (puesto != null && puesto.ContribuyenteNav != null)
                    ret = puesto.ContribuyenteNav.Nombres;
                return ret;
            }
        }

        public String Actividad
        {
            get
            {
                String ret = String.Empty;
                if (puesto != null)
                    ret = puesto.Actividad;
                return ret;
            }
        }

        private DateTime fecha = DateTime.Now;
        public DateTime Fecha
        {
            get { return fecha; }
            set
            {
                fecha = value;
                OnPropertyChanged("Fecha");
            }
        }

        private double total = 0;
        public double Total
        {
            get { return total; }
            set { total = value; OnPropertyChanged("Total"); }
        }

        public ICommand CmdAgregar
        { get; internal set; }

        public ICommand CmdRemover
        { get; internal set; }

        public ICommand CmdGuardar
        { get; internal set; }

        public ICommand CmdNuevo
        { get; internal set; }

        public ICommand CmdProcesar
        { get; internal set; }

        public ICommand CmdConsultar
        { get; internal set; }

        private PlanillaMeses mesesSel;
        public PlanillaMeses MesesSel
        {
            get { return mesesSel; }
            set { mesesSel = value; OnPropertyChanged("MesesSel"); }
        }

        private ObservableCollection<PlanillaMeses> lMeses = new ObservableCollection<PlanillaMeses>();
        public ObservableCollection<PlanillaMeses> LMeses
        {
            get { return lMeses; }
            set { lMeses = value; OnPropertyChanged("LMeses"); }
        }

        private CobroRapVM cobroSel;
        public CobroRapVM CobroSel
        {
            get { return cobroSel; }
            set { cobroSel = value; OnPropertyChanged("CobroSel"); }
        }

        private ObservableCollection<CobroRapVM> lCobros = new ObservableCollection<CobroRapVM>();
        public ObservableCollection<CobroRapVM> LCobros
        {
            get { return lCobros; }
            set { 
                lCobros = value; 
                lCobros.CollectionChanged += (o, e) => CalcularTotal(o, e);
                OnPropertyChanged("LCobros"); 
            }
        }

        public MercadoIngresoVM() : base(new MercadoIngreso())
        {
            CmdProcesar = new Comandos.ComandoDelegado((o) => ProcesarAccion());
            CmdAgregar = new Comandos.ComandoDelegado((o) => AgregarAccion());
            CmdRemover = new Comandos.ComandoDelegado((o) => RemoverAccion());
            CmdNuevo = new Comandos.ComandoDelegado((o) => NuevoAccion());
            CmdGuardar = new Comandos.ComandoDelegado((o) => GuardarAccion());
            //CmdConsultar = new Comandos.ComandoDelegado((o) => Consultar());
            LCobros = new ObservableCollection<CobroRapVM>();
            this.MostrarVista();
        }

        public void CargarLocal(int i)
        {
            using (CatastrosDep cd = new CatastrosDep())
            {
                MercadoDto m = cd.MercadoPorPuesto(i);
                if (m != null)
                {
                    if (CobroSel.Item != null)
                        CobroSel.Item.Referencia = m.Id;
                    if (m.ContribuyenteNav != null)
                    {
                        CobroSel.Denominacion = m.ContribuyenteNav.Nombres;
                    }
                    else
                    {
                        if (m.Contribuyente != null && m.Contribuyente > 0)
                        {
                            ContribuyenteDto con = nd.ContribuyentePorId((int)m.Contribuyente);
                            if (con != null)
                            {
                                CobroSel.Denominacion = con.Nombres;
                            }
                        }
                    }
                    OnPropertyChanged("LCobros");
                }
            }
        }

        private void Limpiar()
        {
            Local = 0;
            Valor = 0;

            LMeses.Clear();
            LCobros.Clear();
        }

        private void ProcesarAccion()
        {
            LMeses.Clear();
            PlanillaMeses m = new PlanillaMeses();
            double val = Valor / Cuota;
            int meses = Convert.ToInt32(val);
            double resto = val % meses;
            for (int i = 1; i <= meses; i++)
            {
                if (i < 7)
                {
                    switch (i)
                    {
                        case 1: { m.Mes1 = Cuota; break; }
                        case 2: { m.Mes2 = Cuota; break; }
                        case 3: { m.Mes3 = Cuota; break; }
                        case 4: { m.Mes4 = Cuota; break; }
                        case 5: { m.Mes5 = Cuota; break; }
                        case 6: { m.Mes6 = Cuota; break; }
                    }
                }
                else
                {
                    switch (i)
                    {
                        case 7: { m.Mes7 = Cuota; break; }
                        case 8: { m.Mes8 = Cuota; break; }
                        case 9: { m.Mes9 = Cuota; break; }
                        case 10: { m.Mes10 = Cuota; break; }
                        case 11: { m.Mes11 = Cuota; break; }
                        case 12: { m.Mes12 = Cuota; break; }
                    }
                }
            }
            /*int i = 12;
            for (i = 12; i > 12 - meses; i--)
            {
                if (i < 7)
                {
                    switch(i)
                    {
                        case 1: { m.Mes1 = Cuota; break; }
                        case 2: { m.Mes2 = Cuota; break; }
                        case 3: { m.Mes3 = Cuota; break; }
                        case 4: { m.Mes4 = Cuota; break; }
                        case 5: { m.Mes5 = Cuota; break; }
                        case 6: { m.Mes6 = Cuota; break; }
                    }
                }
                else
                {
                    switch (i)
                    {
                        case 7: { m.Mes7 = Cuota; break; }
                        case 8: { m.Mes8 = Cuota; break; }
                        case 9: { m.Mes9 = Cuota; break; }
                        case 10: { m.Mes10 = Cuota; break; }
                        case 11: { m.Mes11 = Cuota; break; }
                        case 12: { m.Mes12 = Cuota; break; }
                    }
                }
            }

            if (resto > 0)
            {
                resto = Valor - (Cuota * meses);
                if (i < 7)
                {
                    switch (i)
                    {
                        case 1: { m.Mes1 = resto; break; }
                        case 2: { m.Mes2 = resto; break; }
                        case 3: { m.Mes3 = resto; break; }
                        case 4: { m.Mes4 = resto; break; }
                        case 5: { m.Mes5 = resto; break; }
                        case 6: { m.Mes6 = resto; break; }
                    }
                }
                else
                {
                    switch (i)
                    {
                        case 7: { m.Mes7 = resto; break; }
                        case 8: { m.Mes8 = resto; break; }
                        case 9: { m.Mes9 = resto; break; }
                        case 10: { m.Mes10 = resto; break; }
                        case 11: { m.Mes11 = resto; break; }
                        case 12: { m.Mes12 = resto; break; }
                    }
                }
            }*/

            List<PlanillaMeses> p = new List<PlanillaMeses> { m };
            LMeses = new ObservableCollection<PlanillaMeses>(p);
            MesesSel = LMeses[0];
        }

        private void AgregarAccion()
        {
            CobroRapVM c = new CobroRapVM
            {
                Local = 0,
                Valor = 1.5
            };
            insertando = true;
            LCobros.Add(c);
            insertando = false;
        }

        private void RemoverAccion()
        {
            LCobros.Remove(CobroSel);
            CobroSel = null;
        }

        private void NuevoAccion()
        {
            Limpiar();
        }

        private void GuardarAccion()
        {
            //TODO Validar que los numeros de tickets no esten registrados

            #region Codigo anterior
            /*double cob = LCobros.Sum((s) => (double)s.Valor);
            List<PlanillaAtributoDto> ats = new List<PlanillaAtributoDto>();
            if (planilla.Id <= 0)
            {
                planilla.Año = Anio;
                planilla.Recargos = 0;
                planilla.Rebajas = 0;
                planilla.Convenios = 0;
                planilla.Concepto = 14; // Concepto Mercado
                planilla.Contribuyente = null;
                planilla.Contribuyentes = "[" + puesto.Contribuyente.ToString() + "]";
                if (puesto != null)
                {
                    if (puesto.ContribuyenteNav != null)
                        planilla.Codigo = puesto.ContribuyenteNav.Cedula;

                }
                planilla.Servicio = puesto.Id;
                planilla.FechaEmision = new DateTime(Anio, 1, 1);
                planilla.Total = Valor;
                planilla.Pagos = cob;
                int est = 0;
                if (Valor == cob)
                    est = 1;
                planilla.Estado = est;

                // Rubros
                if (prubro != null)
                {
                    prubro.Planilla = 0;
                    prubro.Rubro = 45; // Rubro Mercado
                    prubro.Origen = 1;
                    prubro.Valor = Valor;
                    prubro.Estado = 0;
                }

                // Meses
                if (MesesSel != null)
                {
                    pmeses.Planilla = 0;
                    pmeses.Rubro = 45;
                    pmeses.Mes1 = MesesSel.Mes1;
                    pmeses.Mes2 = MesesSel.Mes2;
                    pmeses.Mes3 = MesesSel.Mes3;
                    pmeses.Mes4 = MesesSel.Mes4;
                    pmeses.Mes5 = MesesSel.Mes5;
                    pmeses.Mes6 = MesesSel.Mes6;
                    pmeses.Mes7 = MesesSel.Mes7;
                    pmeses.Mes8 = MesesSel.Mes8;
                    pmeses.Mes9 = MesesSel.Mes9;
                    pmeses.Mes10 = MesesSel.Mes10;
                    pmeses.Mes11 = MesesSel.Mes11;
                    pmeses.Mes12 = MesesSel.Mes12;
                    pmeses.Estado = 0;
                }

                PlanillaAtributoDto pa = new PlanillaAtributoDto();
                pa.Planilla = 0;
                pa.Denominacion = "Local No.";
                pa.Tipo = 0;
                pa.ValorC = Local.ToString();
                pa.Estado = 0;
                ats.Add(pa);

                PlanillaAtributoDto pc = new PlanillaAtributoDto();
                pc.Planilla = 0;
                pc.Denominacion = "Actividad";
                pc.Tipo = 0;
                if (puesto != null)
                    pc.ValorC = puesto.Actividad;
                pc.Estado = 0;
                ats.Add(pc);
            }*/

            /*using(EmisionesDep d = new EmisionesDep())
            {
                d.CrearPlanillaMercado(planilla, prubro, pmeses, ats, LCobros.ToList(), Local);
            }*/
            #endregion

            foreach (CobroRapVM cob in LCobros)
            {
                if (cob.Local > 0 && cob.Valor > 0)
                {
                    using (EmisionesDep ed = new EmisionesDep())
                    {
                        // Paramertros: # de local, Id del local, # del ticket, Valor recaudado, Fecha de recaudacion
                        ed.RegistrarTicketMercado(cob.Local, cob.Item.Referencia, cob.Numero, cob.Valor, Fecha);
                    }
                }
            }
            CuadroMensajes.Aceptar("Registro de tickets", "Guardado satisfactoriamente", "Los tickets se registraron satiosfactoriamente", "", TaskDialogInterop.VistaTaskDialogIcon.Information); 
            Limpiar();
        }

        private void MostrarVista()
        {
            Vista.Owner = App.Current.MainWindow;
            Vista.ShowDialog();
        }
    
        public void Consultar()
        {
            Valor = 0;
            if (Anio > 0 && Local > 0)
            {
                using(EmisionesDep d = new EmisionesDep())
                {
                    planilla = d.PlanillaMercadoPorAñoLocal(Anio, puesto.Id);
                }
            }

            if (planilla != null)
            {
                Valor = (Double)planilla.Total;
            }
        }
   
        public void CalcularTotal(Object sender, NotifyCollectionChangedEventArgs e)
        {
            Total = LCobros.Sum(s => s.Valor);
        }
    }

    public class PlanillaMeses
    {
        public PlanillaMeses()
        {
            Mes1 = 0;
            Mes2 = 0;
            Mes3 = 0;
            Mes4 = 0;
            Mes5 = 0;
            Mes6 = 0;
            Mes7 = 0;
            Mes8 = 0;
            Mes9 = 0;
            Mes10 = 0;
            Mes11 = 0;
            Mes12 = 0;
        }

        public Double Mes1 { get; set; }
        public Double Mes2 { get; set; }
        public Double Mes3 { get; set; }
        public Double Mes4 { get; set; }
        public Double Mes5 { get; set; }
        public Double Mes6 { get; set; }
        public Double Mes7 { get; set; }
        public Double Mes8 { get; set; }
        public Double Mes9 { get; set; }
        public Double Mes10 { get; set; }
        public Double Mes11 { get; set; }
        public Double Mes12 { get; set; }
    }

}
