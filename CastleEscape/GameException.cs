﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleEscape
{
    public class GameException : Exception
    {
        public GameException() { }
        public GameException(string Message) : base(Message) { }
    }
}
