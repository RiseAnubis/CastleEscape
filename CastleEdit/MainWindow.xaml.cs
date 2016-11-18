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
using System.Collections;
using System.Collections.ObjectModel;
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

        Point? lastCenterPositionOnTarget;
        Point? lastMousePositionOnTarget;
        Point? lastDragPoint;

        public ObservableCollection<Item> GameItems { get; } = new ObservableCollection<Item>();

        public MainWindow()
        {
            InitializeComponent();
            scrollviewer.PreviewMouseLeftButtonDown += Scrollviewer_PreviewMouseLeftButtonDown;
            scrollviewer.PreviewMouseLeftButtonUp += Scrollviewer_PreviewMouseLeftButtonUp;
            scrollviewer.PreviewMouseWheel += Scrollviewer_PreviewMouseWheel;
            scrollviewer.PreviewMouseMove += Scrollviewer_PreviewMouseMove;
            scrollviewer.ScrollChanged += Scrollviewer_ScrollChanged;
            chbNorth.Unchecked += ChbNorth_Unchecked;
            chbSouth.Unchecked += ChbSouth_Unchecked;
            chbEast.Unchecked += ChbEast_Unchecked;
            chbWest.Unchecked += ChbWest_Unchecked;
            chbLockNorth.Unchecked += (sender, e) => cbItemNorth.SelectedIndex = -1;
            chbLockSouth.Unchecked += (sender, e) => cbItemSouth.SelectedIndex = -1;
            chbLockEast.Unchecked += (sender, e) => cbItemEast.SelectedIndex = -1;
            chbLockWest.Unchecked += (sender, e) => cbItemWest.SelectedIndex = -1;
            btnAddItem.Click += BtnAddItem_Click;
            btnChangeItem.Click += BtnChangeItem_Click;
            btnDeleteItem.Click += (sender, e) => DeleteItem();
            dgItems.SelectionChanged += DgItems_SelectionChanged;
            dgItems.PreviewKeyDown += DgItems_PreviewKeyDown;
            GameItems.CollectionChanged += (sender, e) => statusItemCount.Content = $"Items: {GameItems.Count}";

            for (int i = 0; i < MaxColumns; i++)
                roomGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300) });

            for (int i = 0; i < MaxRows; i++)
                roomGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(300) });

            for (int i = 0; i < roomGrid.ColumnDefinitions.Count; i++)
                for (int j = 0; j < roomGrid.RowDefinitions.Count; j++)
                {
                    Border border = new Border { Style = (Style)FindResource("RoomBorderStyle") };
                    border.PreviewMouseDown += Border_PreviewMouseDown;
                    border.MouseMove += Border_MouseMove; // zur Anzeige der Koordinaten
                    border.GotFocus += Border_GotFocus;   // zum Fokussieren
                    border.KeyDown += Border_KeyDown;     // Löschen eines Raumes per DEL
                    MenuItem miCreateRoom = new MenuItem { Header = "Raum erstellen" };
                    MenuItem miDeleteRoom = new MenuItem { Header = "Raum löschen", IsEnabled = false };
                    miCreateRoom.Click += MiCreateRoom_Click;
                    miDeleteRoom.Click += MiDeleteRoom_Click;
                    ContextMenu ctx = new ContextMenu();
                    ctx.Items.Add(miCreateRoom);
                    ctx.Items.Add(miDeleteRoom);
                    border.ContextMenu = ctx;
                    Grid.SetColumn(border, i);
                    Grid.SetRow(border, j);
                    roomGrid.Children.Add(border);
                }

            statusRoomsize.Content = $"Levelgröße: {MaxColumns} x {MaxRows}";
            statusItemCount.Content = $"Items: {GameItems.Count}";
        }

        private void DgItems_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
                DeleteItem();
        }

        private void Border_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
                (sender as Border).Child = null;
        }

        private void DgItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Item item = (Item)dgItems.SelectedItem;

            if (item != null)
            {
                tbItemID.Text = item.ID;
                tbItemName.Text = item.Name;
                tbItemDescription.Text = item.Description;
                btnDeleteItem.IsEnabled = true;
                btnChangeItem.IsEnabled = true;
            }
            else
            {
                btnDeleteItem.IsEnabled = false;
                btnChangeItem.IsEnabled = false;
            }
        }

        private void BtnChangeItem_Click(object sender, RoutedEventArgs e)
        {
            //Item item = GameItems.First(x => x = (Item)dgItems.SelectedItem != null);

            foreach (Item i in GameItems)
                
            tbItemID.Text = tbItemName.Text = tbItemDescription.Text = "";
        }

        private void Border_GotFocus(object sender, RoutedEventArgs e)
        {
            Border b = sender as Border;
            statusSelectedRoom.Content = $"Ausgewählter Raum: {Grid.GetColumn(b)}, {Grid.GetRow(b)}";
            spRoomProperties.IsEnabled = b.Child != null;
        }

        private void Border_MouseMove(object sender, MouseEventArgs e)
        {
            Border b = sender as Border;
            statusCoordinates.Content = $"Koordinaten: {Grid.GetColumn(b)}, {Grid.GetRow(b)}";
        }

        void Border_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            (sender as Border)?.Focus();
            e.Handled = true; // Zum Unterbinden des "Weiterblubberns" des Events, ansonsten schluckt der Scrollviewer das Event
        }

        void BtnAddItem_Click(object sender, RoutedEventArgs e)
        {
            if (tbItemID.Text == "" || tbItemName.Text == "" || tbItemDescription.Text == "")
            {
                MessageBox.Show("Es müssen alle Item-Felder ausgefüllt sein!", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Item item = new Item { ID = tbItemID.Text, Name = tbItemName.Text, Description = tbItemDescription.Text };

            foreach (Item i in GameItems)
                if (i.ID == item.ID)
                {
                    MessageBox.Show("Das Item mit der angegebenen ID existiert bereits!", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

            GameItems.Add(item);
            tbItemID.Text = tbItemName.Text = tbItemDescription.Text = "";
        }

        void ChbWest_Unchecked(object sender, RoutedEventArgs e)
        {
            chbLockWest.IsChecked = false;
            cbItemWest.SelectedIndex = -1;
        }

        void ChbEast_Unchecked(object sender, RoutedEventArgs e)
        {
            chbLockEast.IsChecked = false;
            cbItemEast.SelectedIndex = -1;
        }

        void ChbSouth_Unchecked(object sender, RoutedEventArgs e)
        {
            chbLockSouth.IsChecked = false;
            cbItemSouth.SelectedIndex = -1;
        }

        void ChbNorth_Unchecked(object sender, RoutedEventArgs e)
        {
            chbLockNorth.IsChecked = false;
            cbItemNorth.SelectedIndex = -1;
        }

        void Scrollviewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.ExtentHeightChange != 0 || e.ExtentWidthChange != 0)
            {
                Point? targetBefore = null;
                Point? targetNow = null;

                if (!lastMousePositionOnTarget.HasValue)
                {
                    if (lastCenterPositionOnTarget.HasValue)
                    {
                        var centerOfViewport = new Point(scrollviewer.ViewportWidth / 2, scrollviewer.ViewportHeight / 2);
                        Point centerOfTargetNow = scrollviewer.TranslatePoint(centerOfViewport, roomGrid);
                        targetBefore = lastCenterPositionOnTarget;
                        targetNow = centerOfTargetNow;
                    }
                }
                else
                {
                    targetBefore = lastMousePositionOnTarget;
                    targetNow = Mouse.GetPosition(roomGrid);

                    lastMousePositionOnTarget = null;
                }

                if (targetBefore.HasValue)
                {
                    double dXInTargetPixels = targetNow.Value.X - targetBefore.Value.X;
                    double dYInTargetPixels = targetNow.Value.Y - targetBefore.Value.Y;

                    double multiplicatorX = e.ExtentWidth / roomGrid.Width;
                    double multiplicatorY = e.ExtentHeight / roomGrid.Height;

                    double newOffsetX = scrollviewer.HorizontalOffset - dXInTargetPixels * multiplicatorX;
                    double newOffsetY = scrollviewer.VerticalOffset - dYInTargetPixels * multiplicatorY;

                    if (double.IsNaN(newOffsetX) || double.IsNaN(newOffsetY))
                        return;

                    scrollviewer.ScrollToHorizontalOffset(newOffsetX);
                    scrollviewer.ScrollToVerticalOffset(newOffsetY);
                }
            }
        }

        void Scrollviewer_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            scrollviewer.Cursor = Cursors.Arrow;
            scrollviewer.ReleaseMouseCapture();
            lastDragPoint = null;
        }

        void Scrollviewer_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (lastDragPoint.HasValue)
            {
                Point posNow = e.GetPosition(scrollviewer);

                double dX = posNow.X - lastDragPoint.Value.X;
                double dY = posNow.Y - lastDragPoint.Value.Y;

                lastDragPoint = posNow;

                scrollviewer.ScrollToHorizontalOffset(scrollviewer.HorizontalOffset - dX);
                scrollviewer.ScrollToVerticalOffset(scrollviewer.VerticalOffset - dY);
            }
        }

        void Scrollviewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                sliZoom.Value += e.Delta * 0.001;
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                if (e.Delta > 0)
                    scrollviewer.LineLeft();
                else
                    scrollviewer.LineRight();

                e.Handled = true;
            }
        }

        void Scrollviewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2) // Zoom bei Doppelklick auf Standardwert setzen
                sliZoom.Value = 1;

            var mousePos = e.GetPosition(scrollviewer);

            if (mousePos.X <= scrollviewer.ViewportWidth && mousePos.Y < scrollviewer.ViewportHeight) //make sure we still can use the scrollbars
            {
                scrollviewer.Cursor = Cursors.SizeAll;
                lastDragPoint = mousePos;
                scrollviewer.CaptureMouse();
            }
        }

        void MiCreateRoom_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            ContextMenu ctx = (ContextMenu)mi.Parent;
            MenuItem disabledItem = ctx.Items[1] as MenuItem;
            Border sourceBorder = ctx.PlacementTarget as Border;
            RoomControl newRoom = new RoomControl();
            //newRoom.PreviewMouseDown += NewRoom_MouseDown;
            //Grid.SetColumn(newRoom, Grid.GetColumn(sourceBorder));
            //Grid.SetRow(newRoom, Grid.GetRow(sourceBorder));
            //roomGrid.Children.Add(newRoom);
            sourceBorder.Child = newRoom;
            disabledItem.IsEnabled = true;
            mi.IsEnabled = false;
        }

        private void MiDeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            ContextMenu ctx = (ContextMenu)mi.Parent;
            MenuItem disabledItem = ctx.Items[0] as MenuItem;
            Border sourceBorder = ctx.PlacementTarget as Border;
            sourceBorder.Child = null;
            disabledItem.IsEnabled = true;
            mi.IsEnabled = false;
        }

        private void NewRoom_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RoomControl room = sender as RoomControl;
            Border sourceBorder = room.Parent as Border;
            if (e.LeftButton == MouseButtonState.Pressed)
                sourceBorder.Focus();
        }

        /// <summary>
        /// Löscht das ausgewählte Item aus dem DataGrid und aus der Itemliste
        /// </summary>
        void DeleteItem()
        {
            if (dgItems.SelectedItem == null)
                return;

            GameItems.Remove((Item)dgItems.SelectedItem);
            tbItemID.Text = tbItemName.Text = tbItemDescription.Text = "";
        }
    }
}
