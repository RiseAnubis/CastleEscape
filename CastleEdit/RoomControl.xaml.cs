using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaktionslogik für RoomControl.xaml
    /// </summary>
    public partial class RoomControl : UserControl
    {
        bool isExitNorthLocked;
        bool isExitSouthLocked;
        bool isExitEastLocked;
        bool isExitWestLocked;

        public bool HasExitNorth
        {
            get { return dirNorth.Visibility == Visibility.Visible; }
            set { dirNorth.Visibility = value ? Visibility.Visible : Visibility.Hidden; }
        }

        public bool HasExitSouth
        {
            get { return dirSouth.Visibility == Visibility.Visible; }
            set { dirSouth.Visibility = value ? Visibility.Visible : Visibility.Hidden; }
        }

        public bool HasExitEast
        {
            get { return dirEast.Visibility == Visibility.Visible; }
            set { dirEast.Visibility = value ? Visibility.Visible : Visibility.Hidden; }
        }

        public bool HasExitWest
        {
            get { return dirWest.Visibility == Visibility.Visible; }
            set { dirWest.Visibility = value ? Visibility.Visible : Visibility.Hidden; }
        }

        public bool IsExitNorthLocked
        {
            get { return isExitNorthLocked; }
            set
            {
                isExitNorthLocked = value;
                dirNorth.Fill = isExitNorthLocked ? Brushes.DarkOrange : Brushes.LawnGreen;
            }
        }

        public bool IsExitSouthLocked
        {
            get { return isExitSouthLocked; }
            set
            {
                isExitSouthLocked = value;
                dirSouth.Fill = isExitSouthLocked ? Brushes.DarkOrange : Brushes.LawnGreen;
            }
        }

        public bool IsExitEastLocked
        {
            get { return isExitEastLocked; }
            set
            {
                isExitEastLocked = value;
                dirEast.Fill = isExitEastLocked ? Brushes.DarkOrange : Brushes.LawnGreen;
            }
        }

        public bool IsExitWestLocked
        {
            get { return isExitWestLocked; }
            set
            {
                isExitWestLocked = value;
                dirWest.Fill = isExitWestLocked ? Brushes.DarkOrange : Brushes.LawnGreen;
            }
        }

        public Item ItemExitNorth { get; set; }
        public Item ItemExitSouth { get; set; }
        public Item ItemExitEast { get; set; }
        public Item ItemExitWest { get; set; }

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

        public ObservableCollection<Item> RoomItems { get; } = new ObservableCollection<Item>();

        public RoomControl()
        {
            InitializeComponent();
        }
    }
}
