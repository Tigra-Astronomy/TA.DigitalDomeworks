// This file is part of the TI.DigitalDomeWorks project
// 
// Copyright © 2014 TiGra Astronomy, all rights reserved.
// 
// File: Settings.cs  Created: 2014-10-05@00:56
// Last modified: 2014-11-12@05:55 by Tim

using System.ComponentModel;
using System.Configuration;
using NLog;

namespace TI.DigitalDomeWorks.Simulator
    {
    // This class allows you to handle specific events on the settings class:
    //  The SettingChanging event is raised before a setting's value is changed.
    //  The PropertyChanged event is raised after a setting's value is changed.
    //  The SettingsLoaded event is raised after the setting values are loaded.
    //  The SettingsSaving event is raised before the setting values are saved.
    internal sealed class Settings
        {
        private readonly Logger log = LogManager.GetCurrentClassLogger();

        private void SettingChangingEventHandler(object sender, SettingChangingEventArgs e)
            {
            log.Info("Settings changed");
            }

        private void SettingsSavingEventHandler(object sender, CancelEventArgs e)
            {
            log.Info("Settings saving");
            }
        }
    }