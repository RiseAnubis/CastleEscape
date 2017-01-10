using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CastleEscape;

namespace CastleEdit
{
    /// <summary>
    /// Interaktionslogik für RoomControl.xaml
    /// </summary>
    public partial class RoomControl : UserControl
    {
        public static readonly DependencyProperty HasExitNorthProperty = DependencyProperty.Register(
            "HasExitNorth", typeof(bool), typeof(RoomControl), new PropertyMetadata(false, OnHasExitChanged));

        public static readonly DependencyProperty HasExitSouthProperty = DependencyProperty.Register(
            "HasExitSouth", typeof (bool), typeof (RoomControl), new PropertyMetadata(false, OnHasExitChanged));

        public static readonly DependencyProperty HasExitEastProperty = DependencyProperty.Register(
            "HasExitEast", typeof (bool), typeof (RoomControl), new PropertyMetadata(false, OnHasExitChanged));

        public static readonly DependencyProperty HasExitWestProperty = DependencyProperty.Register(
            "HasExitWest", typeof (bool), typeof (RoomControl), new PropertyMetadata(false, OnHasExitChanged));

        public static readonly DependencyProperty IsExitNorthLockedProperty = DependencyProperty.Register(
            "IsExitNorthLocked", typeof (bool), typeof (RoomControl), new PropertyMetadata(false, OnExitLockedChanged));

        public static readonly DependencyProperty IsExitSouthLockedProperty = DependencyProperty.Register(
            "IsExitSouthLocked", typeof (bool), typeof (RoomControl), new PropertyMetadata(false, OnExitLockedChanged));

        public static readonly DependencyProperty IsExitEastLockedProperty = DependencyProperty.Register(
            "IsExitEastLocked", typeof (bool), typeof (RoomControl), new PropertyMetadata(false, OnExitLockedChanged));

        public static readonly DependencyProperty IsExitWestLockedProperty = DependencyProperty.Register(
            "IsExitWestLocked", typeof (bool), typeof (RoomControl), new PropertyMetadata(false, OnExitLockedChanged));

        public static readonly DependencyProperty ItemExitNorthProperty = DependencyProperty.Register("ItemExitNorth", typeof (Item), typeof (RoomControl));

        public static readonly DependencyProperty ItemExitSouthProperty = DependencyProperty.Register("ItemExitSouth", typeof (Item), typeof (RoomControl));

        public static readonly DependencyProperty ItemExitEastProperty = DependencyProperty.Register("ItemExitEast", typeof (Item), typeof (RoomControl));

        public static readonly DependencyProperty ItemExitWestProperty = DependencyProperty.Register("ItemExitWest", typeof (Item), typeof (RoomControl));

        public Item ItemExitWest
        {
            get { return (Item)GetValue(ItemExitWestProperty); }
            set { SetValue(ItemExitWestProperty, value); }
        }
        public Item ItemExitEast
        {
            get { return (Item)GetValue(ItemExitEastProperty); }
            set { SetValue(ItemExitEastProperty, value); }
        }
        public Item ItemExitSouth
        {
            get { return (Item)GetValue(ItemExitSouthProperty); }
            set { SetValue(ItemExitSouthProperty, value); }
        }

        public Item ItemExitNorth
        {
            get { return (Item)GetValue(ItemExitNorthProperty); }
            set { SetValue(ItemExitNorthProperty, value); }
        }

        public bool IsExitWestLocked
        {
            get { return (bool)GetValue(IsExitWestLockedProperty); }
            set { SetValue(IsExitWestLockedProperty, value); }
        }

        public bool IsExitEastLocked
        {
            get { return (bool)GetValue(IsExitEastLockedProperty); }
            set { SetValue(IsExitEastLockedProperty, value); }
        }

        public bool IsExitSouthLocked
        {
            get { return (bool)GetValue(IsExitSouthLockedProperty); }
            set { SetValue(IsExitSouthLockedProperty, value); }
        }

        public bool IsExitNorthLocked
        {
            get { return (bool)GetValue(IsExitNorthLockedProperty); }
            set { SetValue(IsExitNorthLockedProperty, value); }
        }

        public bool HasExitWest
        {
            get { return (bool)GetValue(HasExitWestProperty); }
            set { SetValue(HasExitWestProperty, value); }
        }

        public bool HasExitEast
        {
            get { return (bool)GetValue(HasExitEastProperty); }
            set { SetValue(HasExitEastProperty, value); }
        }

        public bool HasExitSouth
        {
            get { return (bool)GetValue(HasExitSouthProperty); }
            set { SetValue(HasExitSouthProperty, value); }
        }

        public bool HasExitNorth
        {
            get { return (bool)GetValue(HasExitNorthProperty); }
            set { SetValue(HasExitNorthProperty, value); }
        }

        /// <summary>
        /// Der Name des Raumes
        /// </summary>
        public string RoomName
        {
            get { return txt_RoomName.Text; }
            set { txt_RoomName.Text = value; }
        }

        /// <summary>
        /// Der Text, der beim Betreten des Raumes angezeigt werden soll
        /// </summary>
        public string RoomDescription { get; set; }

        /// <summary>
        /// Liste, die alle Items enthält, die sich im Raum befinden
        /// </summary>
        public ObservableCollection<Item> RoomItems { get; } = new ObservableCollection<Item>();

        public RoomControl()
        {
            InitializeComponent();
        }

        static void OnHasExitChanged(DependencyObject O, DependencyPropertyChangedEventArgs Args)
        {
            RoomControl sender = O as RoomControl;

            switch (Args.Property.Name)
            {
                case "HasExitNorth":
                    sender.dirNorth.Visibility = (bool)Args.NewValue ? Visibility.Visible : Visibility.Hidden;
                    break;
                case "HasExitSouth":
                    sender.dirSouth.Visibility = (bool)Args.NewValue ? Visibility.Visible : Visibility.Hidden;
                    break;
                case "HasExitEast":
                    sender.dirEast.Visibility = (bool)Args.NewValue ? Visibility.Visible : Visibility.Hidden;
                    break;
                case "HasExitWest":
                    sender.dirWest.Visibility = (bool)Args.NewValue ? Visibility.Visible : Visibility.Hidden;
                    break;
            }
        }

        static void OnExitLockedChanged(DependencyObject O, DependencyPropertyChangedEventArgs Args)
        {
            RoomControl sender = O as RoomControl;

            switch (Args.Property.Name)
            {
                case "IsExitNorthLocked":
                    sender.dirNorth.Fill = (bool)Args.NewValue ? Brushes.DarkOrange : Brushes.LawnGreen;
                    break;
                case "IsExitSouthLocked":
                    sender.dirSouth.Fill = (bool)Args.NewValue ? Brushes.DarkOrange : Brushes.LawnGreen;
                    break;
                case "IsExitEastLocked":
                    sender.dirEast.Fill = (bool)Args.NewValue ? Brushes.DarkOrange : Brushes.LawnGreen;
                    break;
                case "IsExitWestLocked":
                    sender.dirWest.Fill = (bool)Args.NewValue ? Brushes.DarkOrange : Brushes.LawnGreen;
                    break;
            }
        }
    }
}
