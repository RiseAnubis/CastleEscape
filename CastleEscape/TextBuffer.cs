using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleEscape
{
    static class TextBuffer
    {
        static string buffer;

        public static void WriteLine(string Text) => buffer += Text + "\n";

        public static void ShowBuffer()
        {
            Console.Clear();

            /*if (buffer.Length > Console.WindowWidth)
            {
                int lastSpace = buffer.LastIndexOf(' ');
                buffer.Replace(' ', '\n').Where(buffer.LastIndexOf(' '));
            }*/

            Console.Write(buffer);
            buffer = "";
        }
    }
}
