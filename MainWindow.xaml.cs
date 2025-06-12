using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kolcsonzes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Laptop> laptoplista = new List<Laptop>();
        public MainWindow()
        {
            InitializeComponent();
            laptoplista = LoadLaptopList();
            var letariSzamok = laptoplista.Select(l => l.LeltariSzam)
                .Distinct()
                .OrderBy(x => x);
            foreach (var laptopleltarszam in letariSzamok)
            {
                laptopok.Items.Add(laptopleltarszam);
            }
        }
        public List<Laptop> LoadLaptopList()
        {
            List<Laptop> lista = new List<Laptop>();
            StreamReader beolvas = new StreamReader("../../../laptoprentals2022.csv");
            beolvas.ReadLine();
            while(!beolvas.EndOfStream)
            { 
                string line = beolvas.ReadLine();
                string[] adatok = line.Split(";");
                Laptop newLine = new Laptop(adatok[6],
                                     adatok[7],
                                     Convert.ToInt32(adatok[9]),
                                     adatok[10],
                                     Convert.ToInt32(adatok[11]),
                                     Convert.ToInt32(adatok[12])
                                     );
                lista.Add(newLine);
            }
            return lista;
        }
        public class Laptop
        {
            public string LeltariSzam;
            public string Model;
            public int Ram;
            public string Color;
            public int Dayfee;
            public int Deposit;

            public Laptop(string leltariSzam, string model, int ram, string color, int dayfee, int deposit)
            {
                LeltariSzam = leltariSzam;
                Model = model;
                Ram = ram;
                Color = color;
                Dayfee = dayfee;
                Deposit = deposit;
            }
        }

        private void laptopok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listOfSelectedRental = laptoplista
                .Where(l => l.LeltariSzam == laptopok.SelectedItem.ToString())
                .ToList();
            kaucio.Content = listOfSelectedRental[0].Deposit.ToString();
            napi.Content = listOfSelectedRental[0].Dayfee.ToString();
            szin.Content = listOfSelectedRental[0].Color.ToString();
            ram.Content = listOfSelectedRental[0].Ram.ToString();
            model.Content = listOfSelectedRental[0].Model;
            leltariszam.Content = listOfSelectedRental[0].LeltariSzam;
            berlesekdarab.Content = listOfSelectedRental.Count;
        }
    }
}