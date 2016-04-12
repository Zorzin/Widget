using System;
using System.Collections.Generic;
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

        private void Refresh()
        {
            DBConnect connect = new DBConnect();
            DataTable dt =
                connect.selectQuery(
                    "SELECT linia, godzina, minuta from WIDGETTABLE where strftime('%H','now','localtime') = godzina"); //sortowanie po godzinie i minucie i wyswietlanie kolejno tam gdzie godzina >= aktualnej i mnut
            for (int i = 0; i < 6; i++)
            {
                string godzina = dt.Rows[i]["godzina"].ToString();
                string linia = dt.Rows[i]["linia"].ToString();
                string minuta = dt.Rows[i]["minuta"].ToString();
                string caly = godzina + " " + minuta + " " + linia;
                Label label = new Label();
                label.Content = caly;
                label.Height = 50;
                ListBox.Items[i] = label;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            Refresh();
            DispatcherTimer timer = new DispatcherTimer();
            
            timer.Tick += new EventHandler(Refresh);
            timer.Interval = TimeSpan.FromSeconds(10);
            timer.Start();
            licznik = 0;

        }

        void Refresh(object sender, EventArgs e)
        {
            Refresh();
        }

        static void SetValue(ListBoxItem list)
        {
            
        }

        private void RozwinButton_Click(object sender, RoutedEventArgs e)
        {
            if (licznik < 1)
            {
                licznik ++;
                for (int i = 0; i < 6; i++)
                {
                    ListBox.Items.Add(new ListBoxItem());
                }
            }
        }
    }
}
