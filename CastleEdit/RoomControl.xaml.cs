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

        public RoomControl()
        {
            InitializeComponent();
        }
    }
}
