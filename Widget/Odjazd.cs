using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Widget
{
    class Odjazd : INotifyPropertyChanged
    {
        private string godzina;
        private string kierunek;
        private int linia;
        private string oznaczenie;
        private string dzien;
        private string legenda;

        public string Godzina
        {
            get { return ToString(); }
            set { godzina = value; OnPropertyChanged("wypisz"); }
        }

        public string Kierunek
        {
            get { return kierunek; }
            set { kierunek = value; OnPropertyChanged("wypisz"); }
        }

        public int Linia
        {
            get { return linia; }
            set { linia = value; OnPropertyChanged("wypisz"); }
        }

        public string Oznaczenie
        {
            get { return oznaczenie; }
            set { oznaczenie = value; OnPropertyChanged("wypisz"); }
        }

        public string Dzien
        {
            get { return dzien; }
            set { dzien = value; }
        }

        public string Legenda
        {
            get { return legenda; }
            set { legenda = value; OnPropertyChanged("wypisz"); }
        }

        public string wypisz
        {
            get
            {
                string ret = "Linia nr: " + linia.ToString() + " o godzinie " + godzina + Environment.NewLine + "Kierunek " + kierunek + Environment.NewLine;
                if (oznaczenie != null)
                {
                    ret += Legenda + Environment.NewLine;
                }
                return ret;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this,
                new PropertyChangedEventArgs(property));
        }
    }
}
