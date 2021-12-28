using Intelligob.Cliente.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Intelligob.Escritorio.ModeloVista.General
{
    public class CobroRapVM : INotifyPropertyChanged
    {
        private CobroRap item;
        public CobroRap Item
        {
            get
            {
                return item;
            }
            set
            {
                item = value;
            }
        }

        public CobroRapVM(CobroRap pItem)
        {
            Item = pItem;
        }

        public CobroRapVM()
        {
            Item = new CobroRap();
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public int Local
        {
            get
            {
                if (Item != null)
                {
                    return Item.Indice;
                }
                return 0;
            }
            set
            {
                if (Item != null)
                {
                    Item.Indice = value;
                    OnPropertyChanged("Local");
                }
            }
        }

        private string denominacion = string.Empty;
        public string Denominacion
        {
            get
            {
                return denominacion;
            }
            set
            {
                denominacion = value;
                OnPropertyChanged("Denominacion");
            }
        }
            
        public int Numero
        {
            get { return Item.Numero; }
            set { Item.Numero = value; OnPropertyChanged("Numero"); }
        }

        public double Valor
        {
            get { return Item.Valor; }
            set { Item.Valor = value; OnPropertyChanged("Valor"); }
        }
        
    }
}
