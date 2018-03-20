﻿namespace TA.DigitalDomeworks.Server
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
            this.Integra85Icon = new System.Windows.Forms.PictureBox();
            this.GTDLogoBanner = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.OkCommand = new System.Windows.Forms.Button();
            this.ProductTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DriverVersion = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.ShowUserGuideCommand = new System.Windows.Forms.Button();
            DriverVersionLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Integra85Icon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GTDLogoBanner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // DriverVersionLabel
            // 
            DriverVersionLabel.AutoSize = true;
            DriverVersionLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            DriverVersionLabel.Location = new System.Drawing.Point(13, 211);
            DriverVersionLabel.Name = "DriverVersionLabel";
            DriverVersionLabel.Size = new System.Drawing.Size(112, 21);
            DriverVersionLabel.TabIndex = 5;
            DriverVersionLabel.Text = "Driver Version";
            // 
            // Integra85Icon
            // 
            this.Integra85Icon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Integra85Icon.Location = new System.Drawing.Point(324, 144);
            this.Integra85Icon.Name = "Integra85Icon";
            this.Integra85Icon.Size = new System.Drawing.Size(200, 200);
            this.Integra85Icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Integra85Icon.TabIndex = 0;
            this.Integra85Icon.TabStop = false;
            this.Integra85Icon.Tag = "http://www.geminitelescope.com/";
            this.Integra85Icon.Click += new System.EventHandler(this.NavigateToWebPage);
            // 
            // GTDLogoBanner
            // 
            this.GTDLogoBanner.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GTDLogoBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.GTDLogoBanner.Location = new System.Drawing.Point(0, 0);
            this.GTDLogoBanner.Name = "GTDLogoBanner";
            this.GTDLogoBanner.Size = new System.Drawing.Size(536, 138);
            this.GTDLogoBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.GTDLogoBanner.TabIndex = 1;
            this.GTDLogoBanner.TabStop = false;
            this.GTDLogoBanner.Tag = "http://www.geminitelescope.com/";
            this.GTDLogoBanner.Click += new System.EventHandler(this.NavigateToWebPage);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox3.Location = new System.Drawing.Point(324, 350);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(200, 200);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 0;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Tag = "http://tigra-astronomy.com";
            this.pictureBox3.Click += new System.EventHandler(this.NavigateToWebPage);
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
            this.ProductTitle.Text = "Integra 85 Focusing Rotator";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 350);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(303, 147);
            this.label1.TabIndex = 4;
            this.label1.Text = "ASCOM LocalServer Hub\r\nProfessionally produced for\r\nGemini Telescope Design\r\nby\r\n" +
    "Tigra Astronomy";
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
            this.linkLabel1.Location = new System.Drawing.Point(12, 497);
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
            // AboutBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(536, 612);
            this.Controls.Add(this.ShowUserGuideCommand);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.DriverVersion);
            this.Controls.Add(DriverVersionLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ProductTitle);
            this.Controls.Add(this.OkCommand);
            this.Controls.Add(this.GTDLogoBanner);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.Integra85Icon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AboutBox";
            this.Text = "About this software";
            this.Load += new System.EventHandler(this.AboutBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Integra85Icon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GTDLogoBanner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Integra85Icon;
        private System.Windows.Forms.PictureBox GTDLogoBanner;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button OkCommand;
        private System.Windows.Forms.Label ProductTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label DriverVersion;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button ShowUserGuideCommand;
    }
}