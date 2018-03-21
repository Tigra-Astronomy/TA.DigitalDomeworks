namespace TA.DigitalDomeworks.Server
{
    partial class ServerStatusDisplay
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.registeredClientCount = new System.Windows.Forms.Label();
            this.OnlineClients = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.annunciatorPanel1 = new ASCOM.Controls.AnnunciatorPanel();
            this.RotationTitle = new ASCOM.Controls.Annunciator();
            this.CounterClockwiseAnnunciator = new ASCOM.Controls.Annunciator();
            this.AzimuthMotorAnnunciator = new ASCOM.Controls.Annunciator();
            this.ClockwiseAnnunciator = new ASCOM.Controls.Annunciator();
            this.AzimuthPositionAnnunciator = new ASCOM.Controls.Annunciator();
            this.ShutterTitle = new ASCOM.Controls.Annunciator();
            this.ShutterOpeningAnnunciator = new ASCOM.Controls.Annunciator();
            this.ShutterMotorAnnunciator = new ASCOM.Controls.Annunciator();
            this.ShutterClosingAnnunciator = new ASCOM.Controls.Annunciator();
            this.ShutterCurrentAnnunciator = new ASCOM.Controls.Annunciator();
            this.SetupCommand = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.OpenButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.annunciatorPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(93, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Registered clients:";
            // 
            // registeredClientCount
            // 
            this.registeredClientCount.AutoSize = true;
            this.registeredClientCount.Location = new System.Drawing.Point(193, 75);
            this.registeredClientCount.Name = "registeredClientCount";
            this.registeredClientCount.Size = new System.Drawing.Size(13, 13);
            this.registeredClientCount.TabIndex = 1;
            this.registeredClientCount.Text = "0";
            // 
            // OnlineClients
            // 
            this.OnlineClients.AutoSize = true;
            this.OnlineClients.Location = new System.Drawing.Point(291, 75);
            this.OnlineClients.Name = "OnlineClients";
            this.OnlineClients.Size = new System.Drawing.Size(13, 13);
            this.OnlineClients.TabIndex = 3;
            this.OnlineClients.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(245, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Online:";
            // 
            // annunciatorPanel1
            // 
            this.annunciatorPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.annunciatorPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.annunciatorPanel1.Controls.Add(this.RotationTitle);
            this.annunciatorPanel1.Controls.Add(this.CounterClockwiseAnnunciator);
            this.annunciatorPanel1.Controls.Add(this.AzimuthMotorAnnunciator);
            this.annunciatorPanel1.Controls.Add(this.ClockwiseAnnunciator);
            this.annunciatorPanel1.Controls.Add(this.AzimuthPositionAnnunciator);
            this.annunciatorPanel1.Controls.Add(this.ShutterTitle);
            this.annunciatorPanel1.Controls.Add(this.ShutterOpeningAnnunciator);
            this.annunciatorPanel1.Controls.Add(this.ShutterMotorAnnunciator);
            this.annunciatorPanel1.Controls.Add(this.ShutterClosingAnnunciator);
            this.annunciatorPanel1.Controls.Add(this.ShutterCurrentAnnunciator);
            this.annunciatorPanel1.Location = new System.Drawing.Point(96, 12);
            this.annunciatorPanel1.Name = "annunciatorPanel1";
            this.annunciatorPanel1.Size = new System.Drawing.Size(219, 37);
            this.annunciatorPanel1.TabIndex = 5;
            // 
            // RotationTitle
            // 
            this.RotationTitle.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.RotationTitle.AutoSize = true;
            this.RotationTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.RotationTitle.Font = new System.Drawing.Font("Consolas", 10F);
            this.RotationTitle.ForeColor = System.Drawing.Color.White;
            this.RotationTitle.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.RotationTitle.Location = new System.Drawing.Point(3, 0);
            this.RotationTitle.Mute = false;
            this.RotationTitle.Name = "RotationTitle";
            this.RotationTitle.Size = new System.Drawing.Size(64, 17);
            this.RotationTitle.TabIndex = 1;
            this.RotationTitle.Text = "Azimuth";
            // 
            // CounterClockwiseAnnunciator
            // 
            this.CounterClockwiseAnnunciator.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.CounterClockwiseAnnunciator.AutoSize = true;
            this.CounterClockwiseAnnunciator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CounterClockwiseAnnunciator.Font = new System.Drawing.Font("Consolas", 10F);
            this.CounterClockwiseAnnunciator.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.CounterClockwiseAnnunciator.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.CounterClockwiseAnnunciator.Location = new System.Drawing.Point(73, 0);
            this.CounterClockwiseAnnunciator.Mute = false;
            this.CounterClockwiseAnnunciator.Name = "CounterClockwiseAnnunciator";
            this.CounterClockwiseAnnunciator.Size = new System.Drawing.Size(16, 17);
            this.CounterClockwiseAnnunciator.TabIndex = 2;
            this.CounterClockwiseAnnunciator.Text = "◄";
            // 
            // AzimuthMotorAnnunciator
            // 
            this.AzimuthMotorAnnunciator.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.AzimuthMotorAnnunciator.AutoSize = true;
            this.AzimuthMotorAnnunciator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AzimuthMotorAnnunciator.Cadence = ASCOM.Controls.CadencePattern.BlinkAlarm;
            this.AzimuthMotorAnnunciator.Font = new System.Drawing.Font("Consolas", 10F);
            this.AzimuthMotorAnnunciator.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.AzimuthMotorAnnunciator.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.AzimuthMotorAnnunciator.Location = new System.Drawing.Point(95, 0);
            this.AzimuthMotorAnnunciator.Mute = false;
            this.AzimuthMotorAnnunciator.Name = "AzimuthMotorAnnunciator";
            this.AzimuthMotorAnnunciator.Size = new System.Drawing.Size(48, 17);
            this.AzimuthMotorAnnunciator.TabIndex = 3;
            this.AzimuthMotorAnnunciator.Text = "Motor";
            // 
            // ClockwiseAnnunciator
            // 
            this.ClockwiseAnnunciator.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.ClockwiseAnnunciator.AutoSize = true;
            this.ClockwiseAnnunciator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ClockwiseAnnunciator.Font = new System.Drawing.Font("Consolas", 10F);
            this.ClockwiseAnnunciator.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.ClockwiseAnnunciator.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.ClockwiseAnnunciator.Location = new System.Drawing.Point(149, 0);
            this.ClockwiseAnnunciator.Mute = false;
            this.ClockwiseAnnunciator.Name = "ClockwiseAnnunciator";
            this.ClockwiseAnnunciator.Size = new System.Drawing.Size(16, 17);
            this.ClockwiseAnnunciator.TabIndex = 4;
            this.ClockwiseAnnunciator.Text = "►";
            // 
            // AzimuthPositionAnnunciator
            // 
            this.AzimuthPositionAnnunciator.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.AzimuthPositionAnnunciator.AutoSize = true;
            this.AzimuthPositionAnnunciator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AzimuthPositionAnnunciator.Font = new System.Drawing.Font("Consolas", 10F);
            this.AzimuthPositionAnnunciator.ForeColor = System.Drawing.Color.PaleGoldenrod;
            this.AzimuthPositionAnnunciator.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.AzimuthPositionAnnunciator.Location = new System.Drawing.Point(171, 0);
            this.AzimuthPositionAnnunciator.Mute = false;
            this.AzimuthPositionAnnunciator.Name = "AzimuthPositionAnnunciator";
            this.AzimuthPositionAnnunciator.Size = new System.Drawing.Size(40, 17);
            this.AzimuthPositionAnnunciator.TabIndex = 5;
            this.AzimuthPositionAnnunciator.Tag = "{0:###}°";
            this.AzimuthPositionAnnunciator.Text = "000°";
            // 
            // ShutterTitle
            // 
            this.ShutterTitle.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.ShutterTitle.AutoSize = true;
            this.ShutterTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ShutterTitle.Font = new System.Drawing.Font("Consolas", 10F);
            this.ShutterTitle.ForeColor = System.Drawing.Color.White;
            this.ShutterTitle.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.ShutterTitle.Location = new System.Drawing.Point(3, 17);
            this.ShutterTitle.Mute = false;
            this.ShutterTitle.Name = "ShutterTitle";
            this.ShutterTitle.Size = new System.Drawing.Size(64, 17);
            this.ShutterTitle.TabIndex = 6;
            this.ShutterTitle.Text = "Shutter";
            // 
            // ShutterOpeningAnnunciator
            // 
            this.ShutterOpeningAnnunciator.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.ShutterOpeningAnnunciator.AutoSize = true;
            this.ShutterOpeningAnnunciator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ShutterOpeningAnnunciator.Font = new System.Drawing.Font("Consolas", 10F);
            this.ShutterOpeningAnnunciator.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.ShutterOpeningAnnunciator.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.ShutterOpeningAnnunciator.Location = new System.Drawing.Point(73, 17);
            this.ShutterOpeningAnnunciator.Mute = false;
            this.ShutterOpeningAnnunciator.Name = "ShutterOpeningAnnunciator";
            this.ShutterOpeningAnnunciator.Size = new System.Drawing.Size(16, 17);
            this.ShutterOpeningAnnunciator.TabIndex = 7;
            this.ShutterOpeningAnnunciator.Text = "▲";
            // 
            // ShutterMotorAnnunciator
            // 
            this.ShutterMotorAnnunciator.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.ShutterMotorAnnunciator.AutoSize = true;
            this.ShutterMotorAnnunciator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ShutterMotorAnnunciator.Cadence = ASCOM.Controls.CadencePattern.BlinkAlarm;
            this.ShutterMotorAnnunciator.Font = new System.Drawing.Font("Consolas", 10F);
            this.ShutterMotorAnnunciator.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.ShutterMotorAnnunciator.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.ShutterMotorAnnunciator.Location = new System.Drawing.Point(95, 17);
            this.ShutterMotorAnnunciator.Mute = false;
            this.ShutterMotorAnnunciator.Name = "ShutterMotorAnnunciator";
            this.ShutterMotorAnnunciator.Size = new System.Drawing.Size(48, 17);
            this.ShutterMotorAnnunciator.TabIndex = 8;
            this.ShutterMotorAnnunciator.Text = "Motor";
            // 
            // ShutterClosingAnnunciator
            // 
            this.ShutterClosingAnnunciator.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.ShutterClosingAnnunciator.AutoSize = true;
            this.ShutterClosingAnnunciator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ShutterClosingAnnunciator.Font = new System.Drawing.Font("Consolas", 10F);
            this.ShutterClosingAnnunciator.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.ShutterClosingAnnunciator.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.ShutterClosingAnnunciator.Location = new System.Drawing.Point(149, 17);
            this.ShutterClosingAnnunciator.Mute = false;
            this.ShutterClosingAnnunciator.Name = "ShutterClosingAnnunciator";
            this.ShutterClosingAnnunciator.Size = new System.Drawing.Size(16, 17);
            this.ShutterClosingAnnunciator.TabIndex = 9;
            this.ShutterClosingAnnunciator.Text = "▼";
            // 
            // ShutterCurrentAnnunciator
            // 
            this.ShutterCurrentAnnunciator.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.ShutterCurrentAnnunciator.AutoSize = true;
            this.ShutterCurrentAnnunciator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ShutterCurrentAnnunciator.Font = new System.Drawing.Font("Consolas", 10F);
            this.ShutterCurrentAnnunciator.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.ShutterCurrentAnnunciator.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.ShutterCurrentAnnunciator.Location = new System.Drawing.Point(171, 17);
            this.ShutterCurrentAnnunciator.Mute = false;
            this.ShutterCurrentAnnunciator.Name = "ShutterCurrentAnnunciator";
            this.ShutterCurrentAnnunciator.Size = new System.Drawing.Size(32, 17);
            this.ShutterCurrentAnnunciator.TabIndex = 10;
            this.ShutterCurrentAnnunciator.Tag = "{0:###}";
            this.ShutterCurrentAnnunciator.Text = "000";
            // 
            // SetupCommand
            // 
            this.SetupCommand.Location = new System.Drawing.Point(12, 70);
            this.SetupCommand.Name = "SetupCommand";
            this.SetupCommand.Size = new System.Drawing.Size(75, 23);
            this.SetupCommand.TabIndex = 8;
            this.SetupCommand.Text = "Setup...";
            this.SetupCommand.UseVisualStyleBackColor = true;
            this.SetupCommand.Click += new System.EventHandler(this.SetupCommand_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.progressBar1.Location = new System.Drawing.Point(96, 55);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(219, 10);
            this.progressBar1.TabIndex = 11;
            // 
            // OpenButton
            // 
            this.OpenButton.Location = new System.Drawing.Point(12, 12);
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(75, 23);
            this.OpenButton.TabIndex = 12;
            this.OpenButton.Text = "Open";
            this.OpenButton.UseVisualStyleBackColor = true;
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(12, 41);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 13;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            // 
            // ServerStatusDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(331, 105);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.OpenButton);
            this.Controls.Add(this.SetupCommand);
            this.Controls.Add(this.annunciatorPanel1);
            this.Controls.Add(this.OnlineClients);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.registeredClientCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::TA.DigitalDomeworks.Server.Properties.Settings.Default, "MainFormLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Location = global::TA.DigitalDomeworks.Server.Properties.Settings.Default.MainFormLocation;
            this.Name = "ServerStatusDisplay";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "DIgital Domeworks ASCOM Server";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.LocationChanged += new System.EventHandler(this.frmMain_LocationChanged);
            this.annunciatorPanel1.ResumeLayout(false);
            this.annunciatorPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label registeredClientCount;
        private System.Windows.Forms.Label OnlineClients;
        private System.Windows.Forms.Label label3;
        private ASCOM.Controls.AnnunciatorPanel annunciatorPanel1;
        private ASCOM.Controls.Annunciator RotationTitle;
        private System.Windows.Forms.Button SetupCommand;
        private ASCOM.Controls.Annunciator CounterClockwiseAnnunciator;
        private ASCOM.Controls.Annunciator AzimuthMotorAnnunciator;
        private ASCOM.Controls.Annunciator ClockwiseAnnunciator;
        private ASCOM.Controls.Annunciator AzimuthPositionAnnunciator;
        private ASCOM.Controls.Annunciator ShutterTitle;
        private ASCOM.Controls.Annunciator ShutterOpeningAnnunciator;
        private ASCOM.Controls.Annunciator ShutterMotorAnnunciator;
        private ASCOM.Controls.Annunciator ShutterClosingAnnunciator;
        private ASCOM.Controls.Annunciator ShutterCurrentAnnunciator;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button OpenButton;
        private System.Windows.Forms.Button CloseButton;
    }
}

