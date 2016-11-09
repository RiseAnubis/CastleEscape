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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CastleEdit.Classes;

namespace CastleEdit
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        void miNewItem_Click(object sender, RoutedEventArgs e)
        {
            winNewItem newItem = new winNewItem();
            newItem.tbID.Focus();

            if (newItem.ShowDialog() == true)
            {
                Item item = new Item { ID = newItem.tbID.Text, Name = newItem.tbName.Text, Description = newItem.tbDescription.Text };
                dgItems.Items.Add(item);
            }
        }

        private void miNewRoom_Click(object sender, RoutedEventArgs e)
        {
            winNewRoom newRoom = new winNewRoom();
            newRoom.tbName.Focus();

            if (newRoom.ShowDialog() == true)
            { }
        }
    }
}
