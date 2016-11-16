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

        Point? lastCenterPositionOnTarget;
        Point? lastMousePositionOnTarget;
        Point? lastDragPoint;

        internal static List<Item> GameItems { get; } = new List<Item>();

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
            btnAddItem.Click += BtnAddItem_Click;

            for (int i = 0; i < MaxColumns; i++)
                roomGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300) });

            for (int i = 0; i < MaxRows; i++)
                roomGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(300) });

            for (int i = 0; i < roomGrid.ColumnDefinitions.Count; i++)
                for (int j = 0; j < roomGrid.RowDefinitions.Count; j++)
                {
                    Border border = new Border { Style = (Style)FindResource("RoomBorderStyle") };
                    border.PreviewMouseDown += Border_PreviewMouseDown;
                    MenuItem miCreateRoom = new MenuItem { Header = "Raum erstellen" };
                    miCreateRoom.Click += MiCreateRoom_Click;
                    ContextMenu ctx = new ContextMenu();
                    ctx.Items.Add(miCreateRoom);
                    border.ContextMenu = ctx;
                    Grid.SetColumn(border, i);
                    Grid.SetRow(border, j);
                    roomGrid.Children.Add(border);
                }
        }

        private void Border_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Border b = sender as Border;
            b.Focus();
            e.Handled = true; // Zum Unterbinden des "Weiterblubberns" des Events, ansonsten schluckt der Scrollviewer das Event
        }

        void BtnAddItem_Click(object sender, RoutedEventArgs e)
        {
            if (tbItemID.Text == "" || tbItemName.Text == "" || tbItemDescription.Text == "")
            {
                MessageBox.Show("Es müssen alle Felder ausgefüllt sein!", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Item item = new Item { ID = tbItemID.Text, Name = tbItemName.Text, Description = tbItemDescription.Text };
            dgItems.Items.Add(item);
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
            Border sourceBorder = ((ContextMenu)mi.Parent).PlacementTarget as Border;
            RoomControl newRoom = new RoomControl();
            Grid.SetColumn(newRoom, Grid.GetColumn(sourceBorder));
            Grid.SetRow(newRoom, Grid.GetRow(sourceBorder));
            roomGrid.Children.Add(newRoom);
        }

        /*void miNewRoom_Click(object sender, RoutedEventArgs e)
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
        }*/
    }
}
