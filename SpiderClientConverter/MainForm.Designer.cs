namespace SpiderClientConverter
{
    partial class SpiderClientConverter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpiderClientConverter));
            this.assetsF = new System.Windows.Forms.Button();
            this.outputF = new System.Windows.Forms.Button();
            this.ExportSheets = new System.Windows.Forms.Button();
            this.AssetsPath = new System.Windows.Forms.TextBox();
            this.OutputPath = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Percent = new System.Windows.Forms.Label();
            this.SliceBox = new System.Windows.Forms.CheckBox();
            this.ToSpr = new System.Windows.Forms.CheckBox();
            this.ExportDat = new System.Windows.Forms.Button();
            this.ExSheets = new System.Windows.Forms.CheckBox();
            this.CustomSiganture = new System.Windows.Forms.CheckBox();
            this.sigPanel = new System.Windows.Forms.Panel();
            this.sprHex = new System.Windows.Forms.NumericUpDown();
            this.datHex = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.ChangedToExpire = new System.Windows.Forms.CheckBox();
            this.Corpse = new System.Windows.Forms.CheckBox();
            this.PlayerCorpse = new System.Windows.Forms.CheckBox();
            this.CyclopediaItem = new System.Windows.Forms.CheckBox();
            this.Ammo = new System.Windows.Forms.CheckBox();
            this.ShowOffSocket = new System.Windows.Forms.CheckBox();
            this.Reportable = new System.Windows.Forms.CheckBox();
            this.UpgradeClassification = new System.Windows.Forms.CheckBox();
            this.Wearout = new System.Windows.Forms.CheckBox();
            this.ClockExpire = new System.Windows.Forms.CheckBox();
            this.Expire = new System.Windows.Forms.CheckBox();
            this.ExpireStop = new System.Windows.Forms.CheckBox();
            this.sigPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sprHex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datHex)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // assetsF
            // 
            this.assetsF.Location = new System.Drawing.Point(483, 33);
            this.assetsF.Name = "assetsF";
            this.assetsF.Size = new System.Drawing.Size(30, 20);
            this.assetsF.TabIndex = 0;
            this.assetsF.Text = "...";
            this.assetsF.UseVisualStyleBackColor = true;
            this.assetsF.Click += new System.EventHandler(this.AssetsF_Click);
            // 
            // outputF
            // 
            this.outputF.Location = new System.Drawing.Point(483, 74);
            this.outputF.Name = "outputF";
            this.outputF.Size = new System.Drawing.Size(30, 20);
            this.outputF.TabIndex = 1;
            this.outputF.Text = "...";
            this.outputF.UseVisualStyleBackColor = true;
            this.outputF.Click += new System.EventHandler(this.OutputF_Click);
            // 
            // ExportSheets
            // 
            this.ExportSheets.Location = new System.Drawing.Point(12, 120);
            this.ExportSheets.Name = "ExportSheets";
            this.ExportSheets.Size = new System.Drawing.Size(91, 23);
            this.ExportSheets.TabIndex = 2;
            this.ExportSheets.Text = "Export Sprites";
            this.ExportSheets.UseVisualStyleBackColor = true;
            this.ExportSheets.Click += new System.EventHandler(this.ExportSheets_Click);
            // 
            // AssetsPath
            // 
            this.AssetsPath.Location = new System.Drawing.Point(92, 33);
            this.AssetsPath.Name = "AssetsPath";
            this.AssetsPath.Size = new System.Drawing.Size(385, 20);
            this.AssetsPath.TabIndex = 3;
            // 
            // OutputPath
            // 
            this.OutputPath.Location = new System.Drawing.Point(92, 74);
            this.OutputPath.Name = "OutputPath";
            this.OutputPath.Size = new System.Drawing.Size(385, 20);
            this.OutputPath.TabIndex = 4;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 378);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(465, 23);
            this.progressBar.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Assets Folder:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Output Folder:";
            // 
            // Percent
            // 
            this.Percent.AutoSize = true;
            this.Percent.Location = new System.Drawing.Point(487, 384);
            this.Percent.Name = "Percent";
            this.Percent.Size = new System.Drawing.Size(24, 13);
            this.Percent.TabIndex = 8;
            this.Percent.Text = "0%";
            // 
            // SliceBox
            // 
            this.SliceBox.AutoSize = true;
            this.SliceBox.Location = new System.Drawing.Point(215, 124);
            this.SliceBox.Name = "SliceBox";
            this.SliceBox.Size = new System.Drawing.Size(83, 17);
            this.SliceBox.TabIndex = 9;
            this.SliceBox.Text = "Slice Sheets";
            this.SliceBox.UseVisualStyleBackColor = true;
            // 
            // ToSpr
            // 
            this.ToSpr.AutoSize = true;
            this.ToSpr.Location = new System.Drawing.Point(129, 124);
            this.ToSpr.Name = "ToSpr";
            this.ToSpr.Size = new System.Drawing.Size(80, 17);
            this.ToSpr.TabIndex = 10;
            this.ToSpr.Text = "Export .spr";
            this.ToSpr.UseVisualStyleBackColor = true;
            // 
            // ExportDat
            // 
            this.ExportDat.Location = new System.Drawing.Point(12, 166);
            this.ExportDat.Name = "ExportDat";
            this.ExportDat.Size = new System.Drawing.Size(91, 23);
            this.ExportDat.TabIndex = 11;
            this.ExportDat.Text = "Export Dat";
            this.ExportDat.UseVisualStyleBackColor = true;
            this.ExportDat.Click += new System.EventHandler(this.ExportDat_Click);
            // 
            // ExSheets
            // 
            this.ExSheets.AutoSize = true;
            this.ExSheets.Location = new System.Drawing.Point(129, 147);
            this.ExSheets.Name = "ExSheets";
            this.ExSheets.Size = new System.Drawing.Size(94, 17);
            this.ExSheets.TabIndex = 12;
            this.ExSheets.Text = "Export Sheets";
            this.ExSheets.UseVisualStyleBackColor = true;
            // 
            // CustomSiganture
            // 
            this.CustomSiganture.AutoSize = true;
            this.CustomSiganture.Location = new System.Drawing.Point(129, 170);
            this.CustomSiganture.Name = "CustomSiganture";
            this.CustomSiganture.Size = new System.Drawing.Size(132, 17);
            this.CustomSiganture.TabIndex = 13;
            this.CustomSiganture.Text = "Use Custom Signature";
            this.CustomSiganture.UseVisualStyleBackColor = true;
            this.CustomSiganture.CheckedChanged += new System.EventHandler(this.CustomSiganture_CheckedChanged);
            // 
            // sigPanel
            // 
            this.sigPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sigPanel.Controls.Add(this.sprHex);
            this.sigPanel.Controls.Add(this.datHex);
            this.sigPanel.Controls.Add(this.label4);
            this.sigPanel.Controls.Add(this.label3);
            this.sigPanel.Location = new System.Drawing.Point(308, 124);
            this.sigPanel.Name = "sigPanel";
            this.sigPanel.Size = new System.Drawing.Size(207, 70);
            this.sigPanel.TabIndex = 14;
            this.sigPanel.Visible = false;
            // 
            // sprHex
            // 
            this.sprHex.Hexadecimal = true;
            this.sprHex.Location = new System.Drawing.Point(82, 37);
            this.sprHex.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.sprHex.Name = "sprHex";
            this.sprHex.Size = new System.Drawing.Size(120, 20);
            this.sprHex.TabIndex = 3;
            this.sprHex.ValueChanged += new System.EventHandler(this.SprHex_ValueChanged);
            // 
            // datHex
            // 
            this.datHex.Hexadecimal = true;
            this.datHex.Location = new System.Drawing.Point(82, 11);
            this.datHex.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.datHex.Name = "datHex";
            this.datHex.Size = new System.Drawing.Size(120, 20);
            this.datHex.TabIndex = 2;
            this.datHex.ValueChanged += new System.EventHandler(this.DatHex_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Spr Signature";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Dat Signature";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ExpireStop);
            this.panel1.Controls.Add(this.Expire);
            this.panel1.Controls.Add(this.ClockExpire);
            this.panel1.Controls.Add(this.Wearout);
            this.panel1.Controls.Add(this.UpgradeClassification);
            this.panel1.Controls.Add(this.Reportable);
            this.panel1.Controls.Add(this.ShowOffSocket);
            this.panel1.Controls.Add(this.Ammo);
            this.panel1.Controls.Add(this.CyclopediaItem);
            this.panel1.Controls.Add(this.PlayerCorpse);
            this.panel1.Controls.Add(this.Corpse);
            this.panel1.Controls.Add(this.ChangedToExpire);
            this.panel1.Location = new System.Drawing.Point(12, 223);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(499, 133);
            this.panel1.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 207);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Include These Appearances";
            // 
            // ChangedToExpire
            // 
            this.ChangedToExpire.AutoSize = true;
            this.ChangedToExpire.Location = new System.Drawing.Point(13, 19);
            this.ChangedToExpire.Name = "ChangedToExpire";
            this.ChangedToExpire.Size = new System.Drawing.Size(117, 17);
            this.ChangedToExpire.TabIndex = 1;
            this.ChangedToExpire.Text = "Changed To Expire";
            this.ChangedToExpire.UseVisualStyleBackColor = true;
            // 
            // Corpse
            // 
            this.Corpse.AutoSize = true;
            this.Corpse.Location = new System.Drawing.Point(13, 42);
            this.Corpse.Name = "Corpse";
            this.Corpse.Size = new System.Drawing.Size(60, 17);
            this.Corpse.TabIndex = 2;
            this.Corpse.Text = "Corpse";
            this.Corpse.UseVisualStyleBackColor = true;
            // 
            // PlayerCorpse
            // 
            this.PlayerCorpse.AutoSize = true;
            this.PlayerCorpse.Location = new System.Drawing.Point(13, 65);
            this.PlayerCorpse.Name = "PlayerCorpse";
            this.PlayerCorpse.Size = new System.Drawing.Size(93, 17);
            this.PlayerCorpse.TabIndex = 3;
            this.PlayerCorpse.Text = "Player Corpse";
            this.PlayerCorpse.UseVisualStyleBackColor = true;
            // 
            // CyclopediaItem
            // 
            this.CyclopediaItem.AutoSize = true;
            this.CyclopediaItem.Location = new System.Drawing.Point(13, 88);
            this.CyclopediaItem.Name = "CyclopediaItem";
            this.CyclopediaItem.Size = new System.Drawing.Size(103, 17);
            this.CyclopediaItem.TabIndex = 4;
            this.CyclopediaItem.Text = "Cyclopedia Item";
            this.CyclopediaItem.UseVisualStyleBackColor = true;
            // 
            // Ammo
            // 
            this.Ammo.AutoSize = true;
            this.Ammo.Location = new System.Drawing.Point(185, 19);
            this.Ammo.Name = "Ammo";
            this.Ammo.Size = new System.Drawing.Size(55, 17);
            this.Ammo.TabIndex = 5;
            this.Ammo.Text = "Ammo";
            this.Ammo.UseVisualStyleBackColor = true;
            // 
            // ShowOffSocket
            // 
            this.ShowOffSocket.AutoSize = true;
            this.ShowOffSocket.Location = new System.Drawing.Point(185, 42);
            this.ShowOffSocket.Name = "ShowOffSocket";
            this.ShowOffSocket.Size = new System.Drawing.Size(106, 17);
            this.ShowOffSocket.TabIndex = 6;
            this.ShowOffSocket.Text = "Show Off Socket";
            this.ShowOffSocket.UseVisualStyleBackColor = true;
            // 
            // Reportable
            // 
            this.Reportable.AutoSize = true;
            this.Reportable.Location = new System.Drawing.Point(185, 65);
            this.Reportable.Name = "Reportable";
            this.Reportable.Size = new System.Drawing.Size(79, 17);
            this.Reportable.TabIndex = 7;
            this.Reportable.Text = "Reportable";
            this.Reportable.UseVisualStyleBackColor = true;
            // 
            // UpgradeClassification
            // 
            this.UpgradeClassification.AutoSize = true;
            this.UpgradeClassification.Location = new System.Drawing.Point(185, 88);
            this.UpgradeClassification.Name = "UpgradeClassification";
            this.UpgradeClassification.Size = new System.Drawing.Size(132, 17);
            this.UpgradeClassification.TabIndex = 8;
            this.UpgradeClassification.Text = "Upgrade Classification";
            this.UpgradeClassification.UseVisualStyleBackColor = true;
            // 
            // Wearout
            // 
            this.Wearout.AutoSize = true;
            this.Wearout.Location = new System.Drawing.Point(345, 19);
            this.Wearout.Name = "Wearout";
            this.Wearout.Size = new System.Drawing.Size(68, 17);
            this.Wearout.TabIndex = 9;
            this.Wearout.Text = "Wearout";
            this.Wearout.UseVisualStyleBackColor = true;
            // 
            // ClockExpire
            // 
            this.ClockExpire.AutoSize = true;
            this.ClockExpire.Location = new System.Drawing.Point(345, 42);
            this.ClockExpire.Name = "ClockExpire";
            this.ClockExpire.Size = new System.Drawing.Size(84, 17);
            this.ClockExpire.TabIndex = 10;
            this.ClockExpire.Text = "Clock Expire";
            this.ClockExpire.UseVisualStyleBackColor = true;
            // 
            // Expire
            // 
            this.Expire.AutoSize = true;
            this.Expire.Location = new System.Drawing.Point(345, 65);
            this.Expire.Name = "Expire";
            this.Expire.Size = new System.Drawing.Size(56, 17);
            this.Expire.TabIndex = 11;
            this.Expire.Text = "Expire";
            this.Expire.UseVisualStyleBackColor = true;
            // 
            // ExpireStop
            // 
            this.ExpireStop.AutoSize = true;
            this.ExpireStop.Location = new System.Drawing.Point(345, 88);
            this.ExpireStop.Name = "ExpireStop";
            this.ExpireStop.Size = new System.Drawing.Size(81, 17);
            this.ExpireStop.TabIndex = 12;
            this.ExpireStop.Text = "Expire Stop";
            this.ExpireStop.UseVisualStyleBackColor = true;
            // 
            // SpiderClientConverter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 417);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.sigPanel);
            this.Controls.Add(this.CustomSiganture);
            this.Controls.Add(this.ExSheets);
            this.Controls.Add(this.ExportDat);
            this.Controls.Add(this.ToSpr);
            this.Controls.Add(this.SliceBox);
            this.Controls.Add(this.Percent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.OutputPath);
            this.Controls.Add(this.AssetsPath);
            this.Controls.Add(this.ExportSheets);
            this.Controls.Add(this.outputF);
            this.Controls.Add(this.assetsF);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SpiderClientConverter";
            this.Text = "SpiderClientConverter";
            this.sigPanel.ResumeLayout(false);
            this.sigPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sprHex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datHex)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button assetsF;
        private System.Windows.Forms.Button outputF;
        private System.Windows.Forms.Button ExportSheets;
        private System.Windows.Forms.TextBox AssetsPath;
        private System.Windows.Forms.TextBox OutputPath;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Percent;
        private System.Windows.Forms.CheckBox SliceBox;
        private System.Windows.Forms.CheckBox ToSpr;
        private System.Windows.Forms.Button ExportDat;
        private System.Windows.Forms.CheckBox ExSheets;
        private System.Windows.Forms.CheckBox CustomSiganture;
        private System.Windows.Forms.Panel sigPanel;
        private System.Windows.Forms.NumericUpDown datHex;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown sprHex;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox UpgradeClassification;
        private System.Windows.Forms.CheckBox Reportable;
        private System.Windows.Forms.CheckBox ShowOffSocket;
        private System.Windows.Forms.CheckBox Ammo;
        private System.Windows.Forms.CheckBox CyclopediaItem;
        private System.Windows.Forms.CheckBox PlayerCorpse;
        private System.Windows.Forms.CheckBox Corpse;
        private System.Windows.Forms.CheckBox ChangedToExpire;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox ExpireStop;
        private System.Windows.Forms.CheckBox Expire;
        private System.Windows.Forms.CheckBox ClockExpire;
        private System.Windows.Forms.CheckBox Wearout;
    }
}

