﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator
{
    public class History
    {
        List<string> messageHistory;
        DummyClass c;
        public History()
        {
            messageHistory = new List<string>();

        }

        public void Save(string message)
        {

            messageHistory.Add(message);
            Console.WriteLine("Send message: " + message);
        }

        public void done()
        {

        }
    }
}
