using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleEscape
{
    static class TextBuffer
    {
        static List<string> buffer = new List<string>();

        public static void WriteLine(string Text)
        {
            string text = Text + "\n";
            buffer.Add(text);
            Console.WriteLine(Text);
        }

        public static void ShowBuffer()
        {
            Console.Clear();

            foreach (string s in buffer)
                Console.WriteLine(s);
        }

        public static void ClearBuffer() => buffer.Clear();
    }
}
