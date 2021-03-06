﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Beauty.Core.Interfaces
{
    public interface IMessageService
    {
        void ShowMessage(string title, string message, MessageBoxImage icon);
        void ShowInformation(string title, string message);
        void ShowError(string title, string message);
        void ShowExclamation(string title, string message);
        void ShowQuestion(string title, string message);
    }
}
