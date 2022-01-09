using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Cumparator.xaml
    /// </summary>
    public partial class Cumparator : Window
    {
        public Cumparator()
        {
            InitializeComponent();
        }

        private void Salveaza_Click(object sender, RoutedEventArgs e)
        {
            bool verif()
            {
                if ((Nume.Text.Trim() == "") ||
                    (Prenume.Text.Trim() == "") ||
                    (Adresa.Text.Trim() == "") ||
                    (INDP.Text.Trim() == "") ||
                    (Telefon.Text.Trim() == ""))

                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            cump cum = new cump();
            int idauto = Convert.ToInt32(id.Text);
            string nm = Nume.Text;
            string pr = Prenume.Text;
            string adres = Adresa.Text;
            string indp = INDP.Text;
            DateTime data = Datap.SelectedDate.Value;
            int tel = Convert.ToInt32(Telefon.Text);

            if (verif())
            {
                if (cum.inserare(idauto, nm, pr, adres, indp, data, tel))
                {
                    MessageBox.Show("Client Adaugat", "Client Adauga", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Eroare", "Client adauga", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Camp gol", "Client adauga", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

    }
}

