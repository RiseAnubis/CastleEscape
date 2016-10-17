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

        public static void WriteLine(string Text)
        {
            buffer += Text + "\n";
            Console.WriteLine(Text);
        }

        public static void ShowBuffer()
        {
            Console.Clear();
            Console.Write(buffer);
            buffer = "";
        }
    }
}
