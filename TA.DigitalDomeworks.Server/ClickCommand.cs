﻿// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: ClickCommand.cs  Last modified: 2018-03-28@04:02 by Tim Long

using System;
using System.Windows.Forms;
using JetBrains.Annotations;

namespace TA.DigitalDomeworks.Server
    {
    internal class ClickCommand : IDisposable
        {
        [CanBeNull] private readonly Func<bool> canExecute;
        [NotNull] private readonly Control control;
        [NotNull] private readonly Action execute;

        public ClickCommand(Control control, Action execute, Func<bool> canExecute = null)
            {
            this.control = control;
            this.execute = execute;
            this.canExecute = canExecute;
            control.Click += Execute;
            }

        public void Execute(object sender, EventArgs eventArgs) => execute.Invoke();

        public void CanExecuteChanged()
            {
            control.Enabled = canExecute?.Invoke() ?? true;
            }

        #region IDisposable Pattern
        // The IDisposable pattern, as described at
        // http://www.codeproject.com/Articles/15360/Implementing-IDisposable-and-the-Dispose-Pattern-P


        /// <summary>
        ///     Finalizes this instance (called prior to garbage collection by the CLR)
        /// </summary>
        ~ClickCommand()
            {
            Dispose(fromUserCode: false);
            }

        public void Dispose()
            {
            Dispose(fromUserCode: true);
            GC.SuppressFinalize(this);
            }

        private bool disposed;

        protected virtual void Dispose(bool fromUserCode)
            {
            if (!disposed)
                if (fromUserCode)
                    control.Click -= Execute;

            disposed = true;

            // ToDo: Call the base class's Dispose(Boolean) method, if available.
            // base.Dispose(fromUserCode);
            }
        #endregion
        }

    internal static class ControlExtensions
        {
        public static ClickCommand AttachCommand(this Control clickableControl, Action execute,
            Func<bool> canExecute = null)
            {
            var command = new ClickCommand(clickableControl, execute, canExecute);
            return command;
            }
        }
    }