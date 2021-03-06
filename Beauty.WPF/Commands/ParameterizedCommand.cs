﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beauty.WPF.Commands
{
    public sealed class ParameterizedCommand : BaseCommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public ParameterizedCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            return canExecute is null || canExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
