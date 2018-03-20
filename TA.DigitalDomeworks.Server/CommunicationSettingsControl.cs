// This file is part of the GTD.Integra.FocusingRotator project
// 
// Copyright © 2016-2017 Tigra Astronomy., all rights reserved.
// 
// File: CommunicationSettingsControl.cs  Last modified: 2017-02-10@23:53 by Tim Long

using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TA.DigitalDomeworks.Server.Properties;

namespace TA.DigitalDomeworks.Server
    {
    public partial class CommunicationSettingsControl : UserControl
        {
        public CommunicationSettingsControl()
            {
            InitializeComponent();
            var currentSelection = Settings.Default.CommPortName;
            var ports = new SortedSet<string>(System.IO.Ports.SerialPort.GetPortNames());
            if (!ports.Contains(currentSelection))
                {
                ports.Add(currentSelection);
                }
            CommPortName.Items.Clear();
            CommPortName.Items.AddRange(ports.ToArray());
            var currentIndex = CommPortName.Items.IndexOf(currentSelection);
            CommPortName.SelectedIndex = currentIndex;
            }

        public void Save()
            {
            Settings.Default.ConnectionString = $"{Settings.Default.CommPortName}:9600";
            Settings.Default.Save();
            }
        }
    }