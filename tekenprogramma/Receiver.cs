﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tekenprogramma
{
    public class Receiver
    {
        private string action;

        public Receiver()
        {
        }

        public void Actions(string action)
        {
            this.action = action;
        }
    }

    /*
    public class ConcreteCommand : ICommand
    {
        public ConcreteCommand()
        {
        }

        void ICommand.Execute()
        {
            throw new NotImplementedException();
        }

        void ICommand.Undo()
        {
            throw new NotImplementedException();
        }
    }
    */

    interface ICommand
    {
        void Execute();
        //void Undo();
    }
}
