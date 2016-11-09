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

namespace CastleEdit
{
    /// <summary>
    /// Interaktionslogik für winNewItem.xaml
    /// </summary>
    public partial class winNewItem : Window
    {
        public winNewItem()
        {
            InitializeComponent();
        }

        void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (tbDescription.Text != "" && tbID.Text != "" && tbName.Text != "")
                DialogResult = true;
            else
                MessageBox.Show("Alle Felder müssen ausgefüllt sein!", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
