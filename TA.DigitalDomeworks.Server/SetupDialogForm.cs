// This file is part of the TA.DigitalDomeworks project
// 
// Copyright � 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: SetupDialogForm.cs  Last modified: 2018-03-28@22:20 by Tim Long

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TA.DigitalDomeworks.Server
    {
    [ComVisible(false)] // Form not registered for COM!
    public partial class SetupDialogForm : Form
        {
        public SetupDialogForm()
            {
            InitializeComponent();
            }

        private void cmdOK_Click(object sender, EventArgs e) // OK button event handler
            {
            communicationSettingsControl1.Save();
            }

        private void cmdCancel_Click(object sender, EventArgs e) // Cancel button event handler
            {
            Close();
            }

        private void BrowseToAscom(object sender, EventArgs e) // Click on ASCOM logo event handler
            {
            try
                {
                Process.Start("http://ascom-standards.org/");
                }
            catch (Win32Exception noBrowser)
                {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
                }
            catch (Exception other)
                {
                MessageBox.Show(other.Message);
                }
            }

        private void SetupDialogForm_Load(object sender, EventArgs e)
            {
            var onlineClients = SharedResources.ConnectionManager.OnlineClientCount;
            if (onlineClients == 0)
                {
                communicationSettingsControl1.Enabled = true;
                ConnectionErrorProvider.SetError(communicationSettingsControl1, string.Empty);
                }
            else
                {
                communicationSettingsControl1.Enabled = false;
                ConnectionErrorProvider.SetError(communicationSettingsControl1,
                    "Connection settings cannot be changed while there are connected clients");
                }
            }

        private void AboutBox_Click(object sender, EventArgs e)
            {
            using (var aboutBox = new AboutBox())
                {
                aboutBox.ShowDialog();
                }
            }

        private void PresetHD6_Click(object sender, EventArgs e)
            {
            FullRotationTimeSeconds.Value = 60;
            ShutterOpenCloseTimeSeconds.Value = 120;
            }

        private void PresetHD10_Click(object sender, EventArgs e)
            {
            FullRotationTimeSeconds.Value = 90;
            ShutterOpenCloseTimeSeconds.Value = 180;
            }

        private void PresetHD15_Click(object sender, EventArgs e)
            {
            FullRotationTimeSeconds.Value = 120;
            ShutterOpenCloseTimeSeconds.Value = 240;
            }

        private void IgnoreShutterSensor_CheckedChanged(object sender, EventArgs e)
            {
            if (IgnoreShutterSensor.Checked)
                {
                var resault = MessageBox.Show(
                    "This is a potentially unsafe setting.\nPlease be sure you understand the implications\nbefore enabling this!",
                    "Potentially unsafe configuration", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);
                if (resault != DialogResult.OK)
                    IgnoreShutterSensor.Checked = false;
                }

            IgnoreShutterSensor.ForeColor = IgnoreShutterSensor.Checked ? Color.DarkRed : DefaultForeColor;
            }
        }
    }