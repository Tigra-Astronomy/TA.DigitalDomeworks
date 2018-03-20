// This file is part of the GTD.Integra.FocusingRotator project
// 
// Copyright © 2016-2017 Tigra Astronomy., all rights reserved.
// 
// File: AboutBox.cs  Last modified: 2017-02-27@23:48 by Tim Long

using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace TA.DigitalDomeworks.Server
    {
    public partial class AboutBox : Form
        {
        public AboutBox()
            {
            InitializeComponent();
            }

        void label1_Click(object sender, EventArgs e) {}

        void AboutBox_Load(object sender, EventArgs e)
            {
            var me = Assembly.GetExecutingAssembly();
            var name = me.GetName();
            var driverVersion = name.Version;
            DriverVersion.Text = driverVersion.ToString();
            }

        void DriverVersion_Click(object sender, EventArgs e) {}

        void NavigateToWebPage(object sender, EventArgs e)
            {
            var control = sender as Control;
            if (control == null)
                return;
            var url = control.Tag.ToString();
            if (!url.StartsWith("http:"))
                return;
            try
                {
                Process.Start(url);
                }
            catch (Exception)
                {
                // Just fail silently
                }
            }

        void OkCommand_Click(object sender, EventArgs e)
            {
            Close();
            }
        }
    }