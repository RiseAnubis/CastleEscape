using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace CastleEdit.Classes
{
    /// <summary>
    /// Stellt einen Tooltip dar, der sich mit dem Mauszeiger mitbewegt
    /// </summary>
    static class MovingTooltip
    {
        public static readonly DependencyProperty TooltipProperty = DependencyProperty.RegisterAttached("Tooltip", typeof(string), typeof(MovingTooltip), new FrameworkPropertyMetadata(OnShowTooltip));

        /// <summary>
        /// Ruft den anzuzeigenden Tooltip ab
        /// </summary>
        /// <param name="Element">Das Element, dessen Tooltip abgerufen werden soll</param>
        /// <returns></returns>
        public static string GetTooltip(UIElement Element) => (string)Element.GetValue(TooltipProperty);

        /// <summary>
        /// Legt den anzuzeigenden Tooltip fest
        /// </summary>
        /// <param name="Element">Das Element, dessen Tooltip festgelegt werden soll</param>
        /// <param name="Value">Der anzuzeigende Text</param>
        public static void SetTooltip(UIElement Element, string Value) => Element.SetValue(TooltipProperty, Value);

        static void OnShowTooltip(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            UIElement element = o as UIElement;
            ContentControl content = new ContentControl { Content = GetTooltip(element), Style = (Style)Application.Current.Resources["PopupStyle"] };
            Popup p = new Popup { Placement = PlacementMode.Relative, PlacementTarget = element, Child = content };

            element.MouseMove += (sender, e) =>
            {
                p.IsOpen = true;
                Point currentPos = e.GetPosition(element);
                p.HorizontalOffset = currentPos.X + 20;
                p.VerticalOffset = currentPos.Y + 10;
            };

            element.MouseLeave += (sender, e) => p.IsOpen = false;
        }
    }
}
