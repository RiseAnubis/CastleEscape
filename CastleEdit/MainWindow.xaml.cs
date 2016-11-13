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
        const int MaxColumns = 10;
        const int MaxRows = 15;
        internal static List<Item> GameItems { get; } = new List<Item>();

        public MainWindow()
        {
            InitializeComponent();
            roomGrid.MouseRightButtonDown += RoomGrid_MouseRightButtonDown;
            roomGrid.MouseDown += RoomGrid_MouseDown;

            for (int i = 0; i < MaxColumns; i++)
                roomGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300) });

            for (int i = 0; i < MaxRows; i++)
                roomGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(300) });

            for (int i = 0; i < roomGrid.ColumnDefinitions.Count; i++)
                for (int j = 0; j < roomGrid.RowDefinitions.Count; j++)
                {
                    Rectangle rect = new Rectangle { Fill = Brushes.Transparent, Stroke = Brushes.Blue };
                    MenuItem miCreateRoom = new MenuItem { Header = "Raum erstellen" };
                    miCreateRoom.Click += MiCreateRoom_Click;
                    ContextMenu ctx = new ContextMenu();
                    ctx.Items.Add(miCreateRoom);
                    rect.ContextMenu = ctx;
                    Grid.SetColumn(rect, i);
                    Grid.SetRow(rect, j);
                    roomGrid.Children.Add(rect);
                }
        }

        private void RoomGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                
            }
        }

        private void MiCreateRoom_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            Rectangle sourceRect = null;

            if (mi != null)
                sourceRect = ((ContextMenu)mi.Parent).PlacementTarget as Rectangle;

            RoomControl newRoom = new RoomControl();
            Grid.SetColumn(newRoom, Grid.GetColumn(sourceRect));
            Grid.SetRow(newRoom, Grid.GetRow(sourceRect));
            roomGrid.Children.Add(newRoom);
        }

        void RoomGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        void miNewItem_Click(object sender, RoutedEventArgs e)
        {
            winNewItem newItem = new winNewItem();
            newItem.tbID.Focus();

            if (newItem.ShowDialog() == true)
            {
                Item item = new Item { ID = newItem.tbID.Text, Name = newItem.tbName.Text, Description = newItem.tbDescription.Text };
                dgItems.Items.Add(item);
                GameItems.Add(item);
            }
        }

        private void miNewRoom_Click(object sender, RoutedEventArgs e)
        {
            winNewRoom newRoom = new winNewRoom();
            newRoom.tbName.Focus();

            if (newRoom.ShowDialog() == true)
            {
                RoomControl room = new RoomControl();

                if (newRoom.chbNorth.IsChecked == false)
                    room.dirNorth.Visibility = Visibility.Hidden;
                if (newRoom.chbSouth.IsChecked == false)
                    room.dirSouth.Visibility = Visibility.Hidden;
                if (newRoom.chbEast.IsChecked == false)
                    room.dirEast.Visibility = Visibility.Hidden;
                if (newRoom.chbWest.IsChecked == false)
                    room.dirWest.Visibility = Visibility.Hidden;
            }
        }
    }
}
