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
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading;
using System.Windows.Controls.Primitives;
using CastleEdit.Classes;
using Microsoft.Win32;
using System.Xml;

//TODO Eventlistener auf das Formular, welches auf Änderungen prüft und dann den Timer in gang setzt. 2s nach dem ändern eines Werts wird gespeichert solange er nicht in ein anderes Feld schreibt. Wird der Timer zurück gesetzt. Ebenfalls wenn er einen anderen Raum auswählt.Dann faded kurz eine grüne Box auf mit dem Hinweis "Änderungen gespeichert".

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
        RoomControl selectedRoom;

        /// <summary>
        /// Liste, die alle Items des Spiels enthält
        /// </summary>
        public ObservableCollection<Item> GameItems { get; } = new ObservableCollection<Item>();

        public MainWindow()
        {
            InitializeComponent();
            scrollviewer.PreviewMouseLeftButtonDown += Scrollviewer_PreviewMouseLeftButtonDown;
            scrollviewer.PreviewMouseLeftButtonUp += Scrollviewer_PreviewMouseLeftButtonUp;
            scrollviewer.PreviewMouseWheel += Scrollviewer_PreviewMouseWheel;
            scrollviewer.PreviewMouseMove += Scrollviewer_PreviewMouseMove;
            scrollviewer.ScrollChanged += Scrollviewer_ScrollChanged;
            scrollviewer.PreviewKeyDown += (sender, e) => e.Handled = true;  // verhindert, dass die RoomProperties deaktiviert werden, sollte ein Raum markiert sein und Eingaben auf der Tastatur gemacht werden 
            cbItemNorth.SelectionChanged += (sender, e) => ChangeRoomProperty(RoomProperties.ItemExitNorth, cbItemNorth.SelectedItem);
            cbItemSouth.SelectionChanged += (sender, e) => ChangeRoomProperty(RoomProperties.ItemExitSouth, cbItemSouth.SelectedItem);
            cbItemEast.SelectionChanged += (sender, e) => ChangeRoomProperty(RoomProperties.ItemExitEast, cbItemEast.SelectedItem);
            cbItemWest.SelectionChanged += (sender, e) => ChangeRoomProperty(RoomProperties.ItemExitWest, cbItemWest.SelectedItem);
            chbNorth.Checked += (sender, e) => ChangeRoomProperty(RoomProperties.HasExitNorth, true);
            chbSouth.Checked += (sender, e) => ChangeRoomProperty(RoomProperties.HasExitSouth, true);
            chbEast.Checked += (sender, e) => ChangeRoomProperty(RoomProperties.HasExitEast, true);
            chbWest.Checked += (sender, e) => ChangeRoomProperty(RoomProperties.HasExitWest, true);
            chbLockNorth.Checked += (sender, e) => ChangeRoomProperty(RoomProperties.IsExitNorthLocked, true);
            chbLockSouth.Checked += (sender, e) => ChangeRoomProperty(RoomProperties.IsExitSouthLocked, true);
            chbLockEast.Checked += (sender, e) => ChangeRoomProperty(RoomProperties.IsExitEastLocked, true);
            chbLockWest.Checked += (sender, e) => ChangeRoomProperty(RoomProperties.IsExitWestLocked, true);
            chbNorth.Unchecked += ChbNorth_Unchecked;
            chbSouth.Unchecked += ChbSouth_Unchecked;
            chbEast.Unchecked += ChbEast_Unchecked;
            chbWest.Unchecked += ChbWest_Unchecked;
            chbLockNorth.Unchecked += ChbLockNorth_Unchecked;
            chbLockSouth.Unchecked += ChbLockSouth_Unchecked;
            chbLockEast.Unchecked += ChbLockEast_Unchecked;
            chbLockWest.Unchecked += ChbLockWest_Unchecked;
            btnAddItem.Click += BtnAddItem_Click;
            btnDeleteItem.Click += (sender, e) => DeleteGameItem();
            //btnChangeItem.Click += BtnChangeItem_Click;
            dgItems.SelectionChanged += DgItems_SelectionChanged;
            dgItems.PreviewKeyDown += DgItems_PreviewKeyDown;
            dgItems.MouseDown += DgItems_MouseDown;
            lbRoomItems.DragOver += LbRoomItems_DragOver;
            lbRoomItems.Drop += LbRoomItems_Drop;
            lbRoomItems.PreviewKeyDown += LbRoomItems_PreviewKeyDown;
            tbRoomName.TextChanged += (sender, e) => ChangeRoomProperty(RoomProperties.RoomName, tbRoomName.Text);
            tbRoomDescription.TextChanged += (sender, e) => ChangeRoomProperty(RoomProperties.RoomDescription, tbRoomDescription.Text);
            miSaveLevel.Click += MiSaveLevel_Click;
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
                    //border.LostFocus += (Sender, Args) => Args.Handled = true;
                    border.KeyDown += Border_KeyDown;     // Löschen eines Raumes per DEL
                    MenuItem miCreateRoom = new MenuItem { Header = "Raum erstellen", Style = (Style)FindResource("ctxMenuItemStyle") };
                    MenuItem miDeleteRoom = new MenuItem { Header = "Raum löschen", IsEnabled = false, Style = (Style)FindResource("ctxMenuItemStyle") };
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

        void MiSaveLevel_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog { FileName = "Game.xml" };

            if (sfd.ShowDialog(this) == true)
                SaveLevel(sfd.FileName);
        }

        void ChbLockNorth_Unchecked(object sender, RoutedEventArgs e)
        {
            cbItemNorth.SelectedIndex = -1;
            ChangeRoomProperty(RoomProperties.IsExitNorthLocked, false);
        }

        void ChbLockSouth_Unchecked(object sender, RoutedEventArgs e)
        {
            cbItemSouth.SelectedIndex = -1;
            ChangeRoomProperty(RoomProperties.IsExitSouthLocked, false);
        }

        void ChbLockEast_Unchecked(object sender, RoutedEventArgs e)
        {
            cbItemEast.SelectedIndex = -1;
            ChangeRoomProperty(RoomProperties.IsExitEastLocked, false);
        }

        void ChbLockWest_Unchecked(object sender, RoutedEventArgs e)
        {
            cbItemWest.SelectedIndex = -1;
            ChangeRoomProperty(RoomProperties.IsExitWestLocked, false);
        }

        void LbRoomItems_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                selectedRoom.RoomItems.Remove(lbRoomItems.SelectedItem as Item);
                lbRoomItems.SelectedIndex = -1;
                //ListBoxItem lbitem = lbRoomItems.SelectedItem as ListBoxItem;
                //if (lbitem != null)
                //    lbRoomItems.Items.Remove(lbitem);
            }
        }

        void LbRoomItems_Drop(object sender, DragEventArgs e)
        {
            Item item = (Item)e.Data.GetData(typeof(Item));
            selectedRoom.RoomItems.Add(item);
            //lbRoomItems.Items.Add(item);
            //lbRoomItems.DisplayMemberPath = "Name";
        }

        void LbRoomItems_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = e.Data.GetDataPresent(typeof(Item)) ? DragDropEffects.Copy : DragDropEffects.None;
            e.Handled = true;
        }

        void DgItems_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Item item = (Item)dgItems.SelectedItem;

            if (item != null)
            {
                DataObject data = new DataObject(typeof(Item), item);
                DragDrop.DoDragDrop(dgItems, data, DragDropEffects.Copy);
            }
        }

        void DgItems_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
                DeleteGameItem();
        }

        void DgItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Item item = (Item)dgItems.SelectedItem;

            if (item != null)
            {
                tbItemName.Text = item.Name;
                tbItemDescription.Text = item.Description;
                btnDeleteItem.IsEnabled = true;
                //btnChangeItem.IsEnabled = true;
            }
            else
            {
                btnDeleteItem.IsEnabled = false;
                //btnChangeItem.IsEnabled = false;
            }
        }

        void BtnChangeItem_Click(object sender, RoutedEventArgs e)
        {
            Item item = GameItems.FirstOrDefault(x => x.Name == tbItemName.Text);

            if (item == null)
                return;
            //GameItems.RemoveAt(GameItems.IndexOf(item));

            item.Name = tbItemName.Text;
            item.Description = tbItemDescription.Text;

            tbItemName.Text = tbItemDescription.Text = "";
        }

        void Border_GotFocus(object sender, RoutedEventArgs e)
        {
            Border b = sender as Border;
            selectedRoom = b.Child as RoomControl;
            statusSelectedRoom.Content = $"Ausgewählter Raum: {Grid.GetColumn(b)}, {Grid.GetRow(b)}";
            gridRoomProperties.IsEnabled = b.Child != null;

            if (selectedRoom == null)
            {
                ResetRoomProperties();
                return;
            }

            chbNorth.IsChecked = selectedRoom.HasExitNorth;
            chbSouth.IsChecked = selectedRoom.HasExitSouth;
            chbEast.IsChecked = selectedRoom.HasExitEast;
            chbWest.IsChecked = selectedRoom.HasExitWest;
            chbLockNorth.IsChecked = selectedRoom.IsExitNorthLocked;
            chbLockSouth.IsChecked = selectedRoom.IsExitSouthLocked;
            chbLockEast.IsChecked = selectedRoom.IsExitEastLocked;
            chbLockWest.IsChecked = selectedRoom.IsExitWestLocked;
            tbRoomName.Text = selectedRoom.RoomName;
            tbRoomDescription.Text = selectedRoom.RoomDescription;
            lbRoomItems.ItemsSource = selectedRoom.RoomItems;
            cbItemNorth.SelectedItem = selectedRoom.ItemExitNorth;
            cbItemSouth.SelectedItem = selectedRoom.ItemExitSouth;
            cbItemEast.SelectedItem = selectedRoom.ItemExitEast;
            cbItemWest.SelectedItem = selectedRoom.ItemExitWest;
        }

        void Border_MouseMove(object sender, MouseEventArgs e)
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
            if (tbItemName.Text == "" || tbItemDescription.Text == "")
            {
                MessageBoxWindow.Show(DialogType.Information, "Es müssen alle Item-Felder ausgefüllt sein!", "Fehler");
                return;
            }

            Item item = new Item { Name = tbItemName.Text, Description = tbItemDescription.Text };

            if (GameItems.Any(i => i.Name == item.Name))
            {
                MessageBoxWindow.Show(DialogType.Information, "Das Item mit der angegebenen ID existiert bereits!", "Information");
                return;
            }

            GameItems.Add(item);
            tbItemName.Text = tbItemDescription.Text = "";
        }

        void ChbWest_Unchecked(object sender, RoutedEventArgs e)
        {
            chbLockWest.IsChecked = false;
            cbItemWest.SelectedIndex = -1;
            ChangeRoomProperty(RoomProperties.HasExitWest, false);
        }

        void ChbEast_Unchecked(object sender, RoutedEventArgs e)
        {
            chbLockEast.IsChecked = false;
            cbItemEast.SelectedIndex = -1;
            ChangeRoomProperty(RoomProperties.HasExitEast, false);
        }

        void ChbSouth_Unchecked(object sender, RoutedEventArgs e)
        {
            chbLockSouth.IsChecked = false;
            cbItemSouth.SelectedIndex = -1;
            ChangeRoomProperty(RoomProperties.HasExitSouth, false);
        }

        void ChbNorth_Unchecked(object sender, RoutedEventArgs e)
        {
            chbLockNorth.IsChecked = false;
            cbItemNorth.SelectedIndex = -1;
            ChangeRoomProperty(RoomProperties.HasExitNorth, false);
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
            {
                sliZoom.Value += e.Delta * 0.001;
                //gridMatrix.Matrix.ScaleAtPrepend(sliZoom.Value, sliZoom.Value, e.GetPosition(roomGrid).X, e.GetPosition(roomGrid).Y);
                //Point position = e.GetPosition(roomGrid);
                //Matrix matrix = (roomGrid.RenderTransform as MatrixTransform).Matrix;
                //matrix.ScaleAtPrepend(sliZoom.Value, sliZoom.Value, position.X, position.Y);
                //roomGrid.RenderTransform = new MatrixTransform(matrix);
            }

            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                double offset = scrollviewer.HorizontalOffset - (e.Delta * 10.0 / 6);

                if (offset < 0)
                    scrollviewer.ScrollToHorizontalOffset(0);
                else if (offset > scrollviewer.ExtentHeight)
                    scrollviewer.ScrollToHorizontalOffset(scrollviewer.ExtentHeight);
                else
                    scrollviewer.ScrollToHorizontalOffset(offset);

                e.Handled = true;
            }
            else
            {
                double offset = scrollviewer.VerticalOffset - (e.Delta * 10.0 / 6);

                if (offset < 0)
                    scrollviewer.ScrollToVerticalOffset(0);
                else if (offset > scrollviewer.ExtentHeight)
                    scrollviewer.ScrollToVerticalOffset(scrollviewer.ExtentHeight);
                else
                    scrollviewer.ScrollToVerticalOffset(offset);

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
            sourceBorder.Child = newRoom;
            disabledItem.IsEnabled = true;
            mi.IsEnabled = false;
            gridRoomProperties.IsEnabled = true;
            selectedRoom = newRoom;
            lbRoomItems.ItemsSource = selectedRoom.RoomItems;
        }

        void MiDeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            ContextMenu ctx = (ContextMenu)mi.Parent;
            MenuItem disabledItem = ctx.Items[0] as MenuItem;
            Border sourceBorder = ctx.PlacementTarget as Border;
            sourceBorder.Child = null;
            disabledItem.IsEnabled = true;
            mi.IsEnabled = false;
            gridRoomProperties.IsEnabled = false;
            ResetRoomProperties();
        }

        void Border_KeyDown(object sender, KeyEventArgs e)
        {
            Border b = sender as Border;

            if (e.Key == Key.Delete)
                b.Child = null;

            (b.ContextMenu.Items[0] as MenuItem).IsEnabled = true;   // Eintrag Neuen Raum erstellen wieder aktivieren
            (b.ContextMenu.Items[1] as MenuItem).IsEnabled = false;  // Eintrag Raum Löschen wieder deaktivieren
            gridRoomProperties.IsEnabled = false;
            ResetRoomProperties();
        }

        /// <summary>
        /// Löscht das ausgewählte Item aus dem DataGrid und aus der Itemliste
        /// </summary>
        void DeleteGameItem()
        {
            if (dgItems.SelectedItem == null)
                return;

            GameItems.Remove((Item)dgItems.SelectedItem);
            tbItemName.Text = tbItemDescription.Text = "";
        }

        /// <summary>
        /// Setzt alle Controls für die Raumeigenschaften auf ihren Standwert zurück
        /// </summary>
        void ResetRoomProperties()
        {
            chbNorth.IsChecked = false;
            chbSouth.IsChecked = false;
            chbEast.IsChecked = false;
            chbWest.IsChecked = false;
            chbLockNorth.IsChecked = false;
            chbLockSouth.IsChecked = false;
            chbLockEast.IsChecked = false;
            chbLockWest.IsChecked = false;
            cbItemNorth.SelectedIndex = -1;
            cbItemSouth.SelectedIndex = -1;
            cbItemEast.SelectedIndex = -1;
            cbItemWest.SelectedIndex = -1;
            lbRoomItems.ItemsSource = null;
            tbRoomName.Text = tbRoomDescription.Text = "";
        }

        /// <summary>
        /// Ändert eine Raumeigenschaft
        /// </summary>
        /// <param name="Property">Die zu ändernde Eigenschaft</param>
        /// <param name="Value">Der neue Wert der Eigenschaft</param>
        void ChangeRoomProperty(RoomProperties Property, object Value)
        {
            if (selectedRoom == null)
                return;

            switch (Property)
            {
                case RoomProperties.RoomName:
                    selectedRoom.RoomName = (string)Value;
                    break;
                case RoomProperties.RoomDescription:
                    selectedRoom.RoomDescription = (string)Value;
                    break;
                case RoomProperties.HasExitNorth:
                    selectedRoom.HasExitNorth = (bool)Value;
                    break;
                case RoomProperties.HasExitSouth:
                    selectedRoom.HasExitSouth = (bool)Value;
                    break;
                case RoomProperties.HasExitEast:
                    selectedRoom.HasExitEast = (bool)Value;
                    break;
                case RoomProperties.HasExitWest:
                    selectedRoom.HasExitWest = (bool)Value;
                    break;
                case RoomProperties.IsExitNorthLocked:
                    selectedRoom.IsExitNorthLocked = (bool)Value;
                    break;
                case RoomProperties.IsExitSouthLocked:
                    selectedRoom.IsExitSouthLocked = (bool)Value;
                    break;
                case RoomProperties.IsExitEastLocked:
                    selectedRoom.IsExitEastLocked = (bool)Value;
                    break;
                case RoomProperties.IsExitWestLocked:
                    selectedRoom.IsExitWestLocked = (bool)Value;
                    break;
                case RoomProperties.ItemExitNorth:
                    selectedRoom.ItemExitNorth = (Item)Value;
                    break;
                case RoomProperties.ItemExitSouth:
                    selectedRoom.ItemExitSouth = (Item)Value;
                    break;
                case RoomProperties.ItemExitEast:
                    selectedRoom.ItemExitEast = (Item)Value;
                    break;
                case RoomProperties.ItemExitWest:
                    selectedRoom.ItemExitWest = (Item)Value;
                    break;
                default:
                    throw new ArgumentException("Die angegebene Raumeigenschaft existiert nicht", nameof(Property));
            }
        }

        /// <summary>
        /// Speichert das Level in einer XML-Datei ab
        /// </summary>
        /// <param name="Path">Der Pfad der zu erstelllenden Datei</param>
        void SaveLevel(string Path)
        {
            string direction = "Direction", necessary = "NecessaryItem", isLocked = "IsLocked";
            List<RoomControl> roomList = (from b in roomGrid.Children.OfType<Border>() where b.Child != null select b.Child as RoomControl).ToList();
            XmlDocument file = new XmlDocument();
            XmlDeclaration decl = file.CreateXmlDeclaration("1.0", "utf-8", "yes");
            file.AppendChild(decl);

            XmlElement level = file.CreateElement("Level"); // XML Root
            level.SetAttribute("Layout", $"{MaxColumns},{MaxRows}");
            level.SetAttribute("StartPosition", $"{tbPosX.Text},{tbPosY.Text}");

            XmlElement rooms = file.CreateElement("Rooms"); // Rooms-Unterelement
            level.AppendChild(rooms);

            foreach (RoomControl room in roomList)
            {
                XmlElement roomElement = file.CreateElement("Room"); // einzelne Räume erstellen
                roomElement.SetAttribute("Position", $"{Grid.GetColumn(room.Parent as Border)},{Grid.GetRow(room.Parent as Border)}");
                roomElement.SetAttribute("Name", room.RoomName);
                XmlElement roomText = file.CreateElement("Text"); // Raumbeschreibung
                roomText.InnerText = room.RoomDescription;
                roomElement.AppendChild(roomText);
                XmlElement roomExits = file.CreateElement("Exits");
                XmlElement exit;

                if (room.HasExitNorth)
                {
                    exit = file.CreateElement("Exit");
                    exit.SetAttribute(direction, "nord");
                    if (room.IsExitNorthLocked)
                    {
                        exit.SetAttribute(isLocked, "true");
                        exit.SetAttribute(necessary, room.ItemExitNorth.Name);
                    }
                    roomExits.AppendChild(exit);
                }
                if (room.HasExitSouth)
                {
                    exit = file.CreateElement("Exit");
                    exit.SetAttribute(direction, "süd");
                    if (room.IsExitSouthLocked)
                    {
                        exit.SetAttribute(isLocked, "true");
                        exit.SetAttribute(necessary, room.ItemExitSouth.Name);
                    }
                    roomExits.AppendChild(exit);
                }
                if (room.HasExitEast)
                {
                    exit = file.CreateElement("Exit");
                    exit.SetAttribute(direction, "ost");
                    if (room.IsExitEastLocked)
                    {
                        exit.SetAttribute(isLocked, "true");
                        exit.SetAttribute(necessary, room.ItemExitEast.Name);
                    }
                    roomExits.AppendChild(exit);
                }
                if (room.HasExitWest)
                {
                    exit = file.CreateElement("Exit");
                    exit.SetAttribute(direction, "west");
                    if (room.IsExitWestLocked)
                    {
                        exit.SetAttribute(isLocked, "true");
                        exit.SetAttribute(necessary, room.ItemExitWest.Name);
                    }
                    roomExits.AppendChild(exit);
                }

                roomElement.AppendChild(roomExits);
                XmlElement roomItems = file.CreateElement("Items");

                foreach (Item item in room.RoomItems)
                {
                    XmlElement itemElement = file.CreateElement("Item");
                    itemElement.SetAttribute("Name", item.Name);
                    roomItems.AppendChild(itemElement);
                }

                roomElement.AppendChild(roomItems);
                rooms.AppendChild(roomElement);
            }

            XmlElement items = file.CreateElement("Items"); // Items-Unterelement

            foreach (Item item in GameItems)
            {
                XmlElement itemElement = file.CreateElement("Item"); // einzelne Items erstellen
                itemElement.SetAttribute("Name", item.Name);
                itemElement.SetAttribute("Description", item.Description);
                items.AppendChild(itemElement);
            }

            level.AppendChild(items);
            file.AppendChild(level);
            file.Save(Path);
            /*XElement level =
                new XElement("Level", new XAttribute("Layout", $"{MaxColumns},{MaxRows}"), new XAttribute("StartPosition", 0),
                    new XElement("Rooms")
                        new XElement("Room", from room in rooms select new XElement("as", room)));
            level.Save(Path);*/
        }
    }

    /// <summary>
    /// Auflistung, die die Eigenschaften eines Raumes enthält
    /// </summary>
    enum RoomProperties
    {
        HasExitNorth,
        HasExitSouth,
        HasExitEast,
        HasExitWest,
        IsExitNorthLocked,
        IsExitSouthLocked,
        IsExitEastLocked,
        IsExitWestLocked,
        ItemExitNorth,
        ItemExitSouth,
        ItemExitEast,
        ItemExitWest,
        RoomName,
        RoomDescription
    }
}
