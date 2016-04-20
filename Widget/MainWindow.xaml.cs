using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Widget
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int licznik;
        private Collection<Odjazd> lista;
        private string day;
        private string nextday;

        public MainWindow()
        {   lista = new ObservableCollection<Odjazd>();
            InitializeComponent();
            var dday = DateTime.Now.DayOfWeek;
            switch (dday)
            {
                case DayOfWeek.Friday:
                    day = "roboczy";
                    nextday = "sobota";
                    break;
                case DayOfWeek.Saturday:
                    day = "sobota";
                    nextday = "niedziela";
                    break;
                case DayOfWeek.Sunday:
                    day = "niedziela";
                    nextday = "roboczy";
                    break;
                default:
                    day = "roboczy";
                    nextday = "roboczy";
                    break;
            }
            ListBox.ItemsSource = lista;
            licznik = 0;
            RefreshList();
            DispatcherTimer timer = new DispatcherTimer();
            
            timer.Tick += new EventHandler(Refresh);
            timer.Interval = TimeSpan.FromSeconds(10);
            timer.Start();
            

        }
        private void RefreshList()
        {
            lista.Clear();
            DBConnect connect = new DBConnect();
            DataTable dt =
                connect.selectQuery(
                    "SELECT w.linia, w.odjazd,w.dzien,w.oznaczenie,w.kierunek from WIDGETTABLE w where strftime('%H:%M','now','localtime') < odjazd and dzien = '" + day+"' group by w.odjazd");
            int il_item;

            switch (licznik)
            {
                case 1:
                    il_item = 12;
                    break;
                case 2:
                    il_item = 18;
                    break;
                default:
                    il_item = 6;
                    break;
            }
            int polnoc = -1;
            int ktory;
            for (int i = 0; i < il_item; i++)
            {
                ktory = i;
                var odjazd = new Odjazd();
                if (i >= dt.Rows.Count)
                {
                    dt =
                connect.selectQuery(
                    "SELECT w.linia, w.odjazd,w.dzien,w.oznaczenie,w.kierunek from WIDGETTABLE w where w.dzien='" + nextday + "' group by w.odjazd");
                    ktory=++polnoc;

                }

                odjazd.Linia = Int32.Parse(dt.Rows[ktory]["linia"].ToString());
                odjazd.Godzina = dt.Rows[ktory]["odjazd"].ToString();
                odjazd.Kierunek = dt.Rows[ktory]["kierunek"].ToString();
                odjazd.Dzien = dt.Rows[ktory]["dzien"].ToString();
                odjazd.Oznaczenie = dt.Rows[ktory]["oznaczenie"].ToString();

                if (odjazd.Oznaczenie != "")
                {
                    DataTable dt2 = connect.selectQuery(
                "SELECT opis from Legenda where linia = " + odjazd.Linia + " and oznaczenie = '" + odjazd.Oznaczenie + "'");
                    odjazd.Legenda = dt2.Rows[0]["opis"].ToString();
                }
                lista.Add(odjazd);
            }
        }
        void Refresh(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void RozwinButton_Click(object sender, RoutedEventArgs e)
        {
            if (licznik < 2)
            {
                licznik ++;
                if (licznik == 2)
                {
                    RozwinButton.IsEnabled = false;
                }
                RefreshList();
                
            }
        }
    }
}
