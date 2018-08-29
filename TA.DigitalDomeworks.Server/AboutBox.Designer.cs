namespace TA.DigitalDomeworks.Server
{
    partial class AboutBox
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
            System.Windows.Forms.Label DriverVersionLabel;
            System.Windows.Forms.Label label3;
            this.ProductIcon = new System.Windows.Forms.PictureBox();
            this.LogoBanner = new System.Windows.Forms.PictureBox();
            this.TigraLogo = new System.Windows.Forms.PictureBox();
            this.OkCommand = new System.Windows.Forms.Button();
            this.ProductTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DriverVersion = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.ShowUserGuideCommand = new System.Windows.Forms.Button();
            this.InformationalVersion = new System.Windows.Forms.Label();
            DriverVersionLabel = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ProductIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LogoBanner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TigraLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // DriverVersionLabel
            // 
            DriverVersionLabel.AutoSize = true;
            DriverVersionLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            DriverVersionLabel.Location = new System.Drawing.Point(13, 211);
            DriverVersionLabel.Name = "DriverVersionLabel";
            DriverVersionLabel.Size = new System.Drawing.Size(111, 21);
            DriverVersionLabel.TabIndex = 5;
            DriverVersionLabel.Text = "Build Number";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label3.Location = new System.Drawing.Point(15, 232);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(126, 21);
            label3.TabIndex = 11;
            label3.Text = "Product Version";
            // 
            // ProductIcon
            // 
            this.ProductIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ProductIcon.Image = global::TA.DigitalDomeworks.Server.Properties.Resources.DigitalDomeworks;
            this.ProductIcon.Location = new System.Drawing.Point(324, 144);
            this.ProductIcon.Name = "ProductIcon";
            this.ProductIcon.Size = new System.Drawing.Size(200, 200);
            this.ProductIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ProductIcon.TabIndex = 0;
            this.ProductIcon.TabStop = false;
            this.ProductIcon.Tag = "http://homedome.com/";
            this.ProductIcon.Click += new System.EventHandler(this.NavigateToWebPage);
            // 
            // LogoBanner
            // 
            this.LogoBanner.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.LogoBanner.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LogoBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.LogoBanner.Image = global::TA.DigitalDomeworks.Server.Properties.Resources.AuroraWideWithText;
            this.LogoBanner.Location = new System.Drawing.Point(0, 0);
            this.LogoBanner.Name = "LogoBanner";
            this.LogoBanner.Size = new System.Drawing.Size(536, 138);
            this.LogoBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.LogoBanner.TabIndex = 1;
            this.LogoBanner.TabStop = false;
            this.LogoBanner.Tag = "http://www.geminitelescope.com/";
            this.LogoBanner.Click += new System.EventHandler(this.NavigateToWebPage);
            // 
            // TigraLogo
            // 
            this.TigraLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TigraLogo.Image = global::TA.DigitalDomeworks.Server.Properties.Resources.TigraAstronomyLogo;
            this.TigraLogo.Location = new System.Drawing.Point(324, 350);
            this.TigraLogo.Name = "TigraLogo";
            this.TigraLogo.Size = new System.Drawing.Size(200, 200);
            this.TigraLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.TigraLogo.TabIndex = 0;
            this.TigraLogo.TabStop = false;
            this.TigraLogo.Tag = "http://tigra-astronomy.com";
            this.TigraLogo.Click += new System.EventHandler(this.NavigateToWebPage);
            // 
            // OkCommand
            // 
            this.OkCommand.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkCommand.Location = new System.Drawing.Point(324, 577);
            this.OkCommand.Name = "OkCommand";
            this.OkCommand.Size = new System.Drawing.Size(200, 23);
            this.OkCommand.TabIndex = 2;
            this.OkCommand.Text = "OK";
            this.OkCommand.UseVisualStyleBackColor = true;
            this.OkCommand.Click += new System.EventHandler(this.OkCommand_Click);
            // 
            // ProductTitle
            // 
            this.ProductTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProductTitle.Location = new System.Drawing.Point(13, 166);
            this.ProductTitle.Name = "ProductTitle";
            this.ProductTitle.Size = new System.Drawing.Size(305, 30);
            this.ProductTitle.TabIndex = 3;
            this.ProductTitle.Text = "Digital Domeworks 2018";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 350);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(303, 177);
            this.label1.TabIndex = 4;
            this.label1.Text = "ASCOM Multi-instance Server\r\nProfessionally produced by\r\n\r\nTigra Astronomy\r\n\r\nWe " +
    "are available for hire to create your ASCOM driver, firmware, or application";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // DriverVersion
            // 
            this.DriverVersion.AutoSize = true;
            this.DriverVersion.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DriverVersion.Location = new System.Drawing.Point(154, 211);
            this.DriverVersion.Name = "DriverVersion";
            this.DriverVersion.Size = new System.Drawing.Size(60, 21);
            this.DriverVersion.TabIndex = 5;
            this.DriverVersion.Text = "(unset)";
            this.DriverVersion.Click += new System.EventHandler(this.DriverVersion_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLabel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(12, 527);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(297, 23);
            this.linkLabel1.TabIndex = 6;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "http://tigra-astronomy.com";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ShowUserGuideCommand
            // 
            this.ShowUserGuideCommand.AutoSize = true;
            this.ShowUserGuideCommand.Enabled = false;
            this.ShowUserGuideCommand.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShowUserGuideCommand.Location = new System.Drawing.Point(107, 300);
            this.ShowUserGuideCommand.Name = "ShowUserGuideCommand";
            this.ShowUserGuideCommand.Size = new System.Drawing.Size(121, 27);
            this.ShowUserGuideCommand.TabIndex = 9;
            this.ShowUserGuideCommand.Tag = "http://www.geminitelescope.com/Manuals/Integra85_manual.pdf";
            this.ShowUserGuideCommand.Text = "Show User Guide";
            this.ShowUserGuideCommand.UseVisualStyleBackColor = true;
            this.ShowUserGuideCommand.Click += new System.EventHandler(this.NavigateToWebPage);
            // 
            // InformationalVersion
            // 
            this.InformationalVersion.AutoSize = true;
            this.InformationalVersion.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InformationalVersion.Location = new System.Drawing.Point(156, 232);
            this.InformationalVersion.Name = "InformationalVersion";
            this.InformationalVersion.Size = new System.Drawing.Size(60, 21);
            this.InformationalVersion.TabIndex = 10;
            this.InformationalVersion.Text = "(unset)";
            // 
            // AboutBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(536, 612);
            this.Controls.Add(this.InformationalVersion);
            this.Controls.Add(label3);
            this.Controls.Add(this.ShowUserGuideCommand);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.DriverVersion);
            this.Controls.Add(DriverVersionLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ProductTitle);
            this.Controls.Add(this.OkCommand);
            this.Controls.Add(this.LogoBanner);
            this.Controls.Add(this.TigraLogo);
            this.Controls.Add(this.ProductIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AboutBox";
            this.Text = "About this software";
            this.Load += new System.EventHandler(this.AboutBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ProductIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LogoBanner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TigraLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox ProductIcon;
        private System.Windows.Forms.PictureBox LogoBanner;
        private System.Windows.Forms.PictureBox TigraLogo;
        private System.Windows.Forms.Button OkCommand;
        private System.Windows.Forms.Label ProductTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label DriverVersion;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button ShowUserGuideCommand;
        private System.Windows.Forms.Label InformationalVersion;
    }
}