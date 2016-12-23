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
using System.Drawing;
using System.Media;
using System.Windows.Interop;

namespace CastleEdit
{
    /// <summary>
    /// Zeigt einen modalen Nachrichten-Dialog an
    /// </summary>
    public partial class MessageBoxWindow : Window
    {
        MessageBoxWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Ruft den Dialog auf
        /// </summary>
        /// <param name="Type">Die Art des anzuzeigenden Dialogs</param>
        /// <param name="Message">Die anzuzeigende Meldung</param>
        /// <param name="Caption">Die Überschrift des Dialogs</param>
        public static MessageBoxResult Show(DialogType Type, string Message, string Caption)
        {
            Button button;
            MessageBoxResult result = MessageBoxResult.None;
            MessageBoxWindow box = new MessageBoxWindow { Title = Caption, Owner = Application.Current.MainWindow, txtMessage = { Text = Message } };

            switch (Type)
            {
                case DialogType.Error:
                    box.imgIcon.Source = Imaging.CreateBitmapSourceFromHIcon(SystemIcons.Error.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    button = new Button { Content = "OK", Width = 80 };
                    button.Click += (Sender, Args) => box.Close();
                    box.spButtons.Children.Add(button);
                    SystemSounds.Exclamation.Play();
                    result = MessageBoxResult.OK;
                    break;
                case DialogType.Information:
                    box.imgIcon.Source = Imaging.CreateBitmapSourceFromHIcon(SystemIcons.Information.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    button = new Button { Content = "OK", Width = 80 };
                    box.spButtons.Children.Add(button);
                    button.Click += (Sender, Args) => box.Close();
                    SystemSounds.Asterisk.Play();
                    result = MessageBoxResult.OK;
                    break;
                case DialogType.Warning:
                    box.imgIcon.Source = Imaging.CreateBitmapSourceFromHIcon(SystemIcons.Warning.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    button = new Button { Content = "OK", Width = 80 };
                    button.Click += (Sender, Args) => box.Close();
                    box.spButtons.Children.Add(button);
                    SystemSounds.Asterisk.Play();
                    result = MessageBoxResult.OK;
                    break;
            }

            box.ShowDialog();
            return result;
        }
    }

    public enum DialogType { Warning, Error, Information }
}
