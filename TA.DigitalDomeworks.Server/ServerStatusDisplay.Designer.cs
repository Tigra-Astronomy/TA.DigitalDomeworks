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
            this.ClientStatus = new System.Windows.Forms.ListBox();
            this.annunciatorPanel1 = new ASCOM.Controls.AnnunciatorPanel();
            this.FocuserActivity = new ASCOM.Controls.Annunciator();
            this.FocuserPosition = new ASCOM.Controls.Annunciator();
            this.FocuserMoving = new ASCOM.Controls.Annunciator();
            this.Calibrating = new ASCOM.Controls.Annunciator();
            this.RotatorActivity = new ASCOM.Controls.Annunciator();
            this.RotatorPosition = new ASCOM.Controls.Annunciator();
            this.RotatorMoving = new ASCOM.Controls.Annunciator();
            this.SetupCommand = new System.Windows.Forms.Button();
            this.annunciatorPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Registered clients:";
            // 
            // registeredClientCount
            // 
            this.registeredClientCount.AutoSize = true;
            this.registeredClientCount.Location = new System.Drawing.Point(122, 10);
            this.registeredClientCount.Name = "registeredClientCount";
            this.registeredClientCount.Size = new System.Drawing.Size(13, 13);
            this.registeredClientCount.TabIndex = 1;
            this.registeredClientCount.Text = "0";
            // 
            // OnlineClients
            // 
            this.OnlineClients.AutoSize = true;
            this.OnlineClients.Location = new System.Drawing.Point(226, 10);
            this.OnlineClients.Name = "OnlineClients";
            this.OnlineClients.Size = new System.Drawing.Size(13, 13);
            this.OnlineClients.TabIndex = 3;
            this.OnlineClients.Text = "0";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(166, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "Online:";
            // 
            // ClientStatus
            // 
            this.ClientStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ClientStatus.FormattingEnabled = true;
            this.ClientStatus.Location = new System.Drawing.Point(13, 61);
            this.ClientStatus.Name = "ClientStatus";
            this.ClientStatus.Size = new System.Drawing.Size(618, 160);
            this.ClientStatus.TabIndex = 4;
            // 
            // annunciatorPanel1
            // 
            this.annunciatorPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.annunciatorPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.annunciatorPanel1.Controls.Add(this.FocuserActivity);
            this.annunciatorPanel1.Controls.Add(this.FocuserPosition);
            this.annunciatorPanel1.Controls.Add(this.FocuserMoving);
            this.annunciatorPanel1.Controls.Add(this.Calibrating);
            this.annunciatorPanel1.Controls.Add(this.RotatorActivity);
            this.annunciatorPanel1.Controls.Add(this.RotatorPosition);
            this.annunciatorPanel1.Controls.Add(this.RotatorMoving);
            this.annunciatorPanel1.Location = new System.Drawing.Point(319, 12);
            this.annunciatorPanel1.Name = "annunciatorPanel1";
            this.annunciatorPanel1.Size = new System.Drawing.Size(312, 33);
            this.annunciatorPanel1.TabIndex = 5;
            // 
            // FocuserActivity
            // 
            this.FocuserActivity.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.FocuserActivity.AutoSize = true;
            this.FocuserActivity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FocuserActivity.Enabled = false;
            this.FocuserActivity.Font = new System.Drawing.Font("Consolas", 10F);
            this.FocuserActivity.ForeColor = System.Drawing.Color.Yellow;
            this.FocuserActivity.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.FocuserActivity.Location = new System.Drawing.Point(3, 0);
            this.FocuserActivity.Mute = false;
            this.FocuserActivity.Name = "FocuserActivity";
            this.FocuserActivity.Size = new System.Drawing.Size(64, 17);
            this.FocuserActivity.TabIndex = 1;
            this.FocuserActivity.Text = "Focuser";
            // 
            // FocuserPosition
            // 
            this.FocuserPosition.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.FocuserPosition.AutoSize = true;
            this.FocuserPosition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FocuserPosition.Font = new System.Drawing.Font("Consolas", 10F);
            this.FocuserPosition.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.FocuserPosition.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.FocuserPosition.Location = new System.Drawing.Point(73, 0);
            this.FocuserPosition.Mute = false;
            this.FocuserPosition.Name = "FocuserPosition";
            this.FocuserPosition.Size = new System.Drawing.Size(56, 17);
            this.FocuserPosition.TabIndex = 3;
            this.FocuserPosition.Text = "200000";
            // 
            // FocuserMoving
            // 
            this.FocuserMoving.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.FocuserMoving.AutoSize = true;
            this.FocuserMoving.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FocuserMoving.Cadence = ASCOM.Controls.CadencePattern.BlinkAlarm;
            this.FocuserMoving.Enabled = false;
            this.FocuserMoving.Font = new System.Drawing.Font("Consolas", 10F);
            this.FocuserMoving.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.FocuserMoving.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.FocuserMoving.Location = new System.Drawing.Point(135, 0);
            this.FocuserMoving.Mute = false;
            this.FocuserMoving.Name = "FocuserMoving";
            this.FocuserMoving.Size = new System.Drawing.Size(56, 17);
            this.FocuserMoving.TabIndex = 0;
            this.FocuserMoving.Text = "Moving";
            // 
            // Calibrating
            // 
            this.Calibrating.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.Calibrating.AutoSize = true;
            this.Calibrating.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Calibrating.Cadence = ASCOM.Controls.CadencePattern.BlinkAlarm;
            this.Calibrating.Enabled = false;
            this.Calibrating.Font = new System.Drawing.Font("Consolas", 10F);
            this.Calibrating.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.Calibrating.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.Calibrating.Location = new System.Drawing.Point(197, 0);
            this.Calibrating.Mute = false;
            this.Calibrating.Name = "Calibrating";
            this.Calibrating.Size = new System.Drawing.Size(96, 17);
            this.Calibrating.TabIndex = 2;
            this.Calibrating.Text = "Calibrating";
            // 
            // RotatorActivity
            // 
            this.RotatorActivity.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.RotatorActivity.AutoSize = true;
            this.RotatorActivity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.RotatorActivity.Enabled = false;
            this.RotatorActivity.Font = new System.Drawing.Font("Consolas", 10F);
            this.RotatorActivity.ForeColor = System.Drawing.Color.Yellow;
            this.RotatorActivity.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.RotatorActivity.Location = new System.Drawing.Point(3, 17);
            this.RotatorActivity.Mute = false;
            this.RotatorActivity.Name = "RotatorActivity";
            this.RotatorActivity.Size = new System.Drawing.Size(64, 17);
            this.RotatorActivity.TabIndex = 1;
            this.RotatorActivity.Text = "Rotator";
            // 
            // RotatorPosition
            // 
            this.RotatorPosition.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.RotatorPosition.AutoSize = true;
            this.RotatorPosition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.RotatorPosition.Font = new System.Drawing.Font("Consolas", 10F);
            this.RotatorPosition.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.RotatorPosition.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.RotatorPosition.Location = new System.Drawing.Point(73, 17);
            this.RotatorPosition.Mute = false;
            this.RotatorPosition.Name = "RotatorPosition";
            this.RotatorPosition.Size = new System.Drawing.Size(64, 17);
            this.RotatorPosition.TabIndex = 4;
            this.RotatorPosition.Text = "360.00°";
            // 
            // RotatorMoving
            // 
            this.RotatorMoving.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.RotatorMoving.AutoSize = true;
            this.RotatorMoving.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.RotatorMoving.Cadence = ASCOM.Controls.CadencePattern.BlinkAlarm;
            this.RotatorMoving.Enabled = false;
            this.RotatorMoving.Font = new System.Drawing.Font("Consolas", 10F);
            this.RotatorMoving.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.RotatorMoving.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.RotatorMoving.Location = new System.Drawing.Point(143, 17);
            this.RotatorMoving.Mute = false;
            this.RotatorMoving.Name = "RotatorMoving";
            this.RotatorMoving.Size = new System.Drawing.Size(56, 17);
            this.RotatorMoving.TabIndex = 0;
            this.RotatorMoving.Text = "Moving";
            // 
            // SetupCommand
            // 
            this.SetupCommand.Location = new System.Drawing.Point(226, 12);
            this.SetupCommand.Name = "SetupCommand";
            this.SetupCommand.Size = new System.Drawing.Size(75, 23);
            this.SetupCommand.TabIndex = 8;
            this.SetupCommand.Text = "Setup...";
            this.SetupCommand.UseVisualStyleBackColor = true;
            this.SetupCommand.Click += new System.EventHandler(this.SetupCommand_Click);
            // 
            // ServerStatusDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 238);
            this.Controls.Add(this.SetupCommand);
            this.Controls.Add(this.annunciatorPanel1);
            this.Controls.Add(this.ClientStatus);
            this.Controls.Add(this.OnlineClients);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.registeredClientCount);
            this.Controls.Add(this.label1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::TA.DigitalDomeworks.Server.Properties.Settings.Default, "MainFormLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Location = global::TA.DigitalDomeworks.Server.Properties.Settings.Default.MainFormLocation;
            this.Name = "ServerStatusDisplay";
            this.Text = "DIgital Domeworks ASCOM Server";
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
        private System.Windows.Forms.ListBox ClientStatus;
        private ASCOM.Controls.AnnunciatorPanel annunciatorPanel1;
        private ASCOM.Controls.Annunciator FocuserMoving;
        private ASCOM.Controls.Annunciator FocuserActivity;
        private ASCOM.Controls.Annunciator RotatorActivity;
        private ASCOM.Controls.Annunciator Calibrating;
        private ASCOM.Controls.Annunciator FocuserPosition;
        private ASCOM.Controls.Annunciator RotatorPosition;
        private ASCOM.Controls.Annunciator RotatorMoving;
        private System.Windows.Forms.Button SetupCommand;
    }
}

