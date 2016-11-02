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
            string result = "";
            string[] lines = buffer.Split('\n');

            foreach (string line in lines)
            {
                int lineLength = 0;
                string[] words = line.Split(' ');

                foreach (string word in words)
                {
                    if (word.Length + lineLength > Console.WindowWidth - 1)
                    {
                        result += "\n";
                        lineLength = 0;
                    }

                    result += word + " ";
                    lineLength += word.Length + 1;
                }

                result += "\n";
            }
            /*if (buffer.Length > Console.WindowWidth)
            {
                int lastSpace = buffer.LastIndexOf(' ');
                buffer.Replace(' ', '\n').Where(buffer.LastIndexOf(' '));
            }*/


            Console.Write(result);
            buffer = "";
        }
    }
}
