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
            this.button1 = new System.Windows.Forms.Button();
            this.sigPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sprHex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datHex)).BeginInit();
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
            this.AssetsPath.Size = new System.Drawing.Size(294, 20);
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
            this.progressBar.Location = new System.Drawing.Point(16, 428);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(465, 23);
            this.progressBar.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Assets Folder:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Output Folder:";
            // 
            // Percent
            // 
            this.Percent.AutoSize = true;
            this.Percent.Location = new System.Drawing.Point(491, 434);
            this.Percent.Name = "Percent";
            this.Percent.Size = new System.Drawing.Size(21, 13);
            this.Percent.TabIndex = 8;
            this.Percent.Text = "0%";
            // 
            // SliceBox
            // 
            this.SliceBox.AutoSize = true;
            this.SliceBox.Location = new System.Drawing.Point(229, 124);
            this.SliceBox.Name = "SliceBox";
            this.SliceBox.Size = new System.Drawing.Size(85, 17);
            this.SliceBox.TabIndex = 9;
            this.SliceBox.Text = "Slice Sheets";
            this.SliceBox.UseVisualStyleBackColor = true;
            // 
            // ToSpr
            // 
            this.ToSpr.AutoSize = true;
            this.ToSpr.Location = new System.Drawing.Point(329, 124);
            this.ToSpr.Name = "ToSpr";
            this.ToSpr.Size = new System.Drawing.Size(76, 17);
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
            this.ExSheets.Location = new System.Drawing.Point(129, 124);
            this.ExSheets.Name = "ExSheets";
            this.ExSheets.Size = new System.Drawing.Size(92, 17);
            this.ExSheets.TabIndex = 12;
            this.ExSheets.Text = "Export Sheets";
            this.ExSheets.UseVisualStyleBackColor = true;
            // 
            // CustomSiganture
            // 
            this.CustomSiganture.AutoSize = true;
            this.CustomSiganture.Location = new System.Drawing.Point(129, 170);
            this.CustomSiganture.Name = "CustomSiganture";
            this.CustomSiganture.Size = new System.Drawing.Size(131, 17);
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
            this.sigPanel.Location = new System.Drawing.Point(129, 193);
            this.sigPanel.Name = "sigPanel";
            this.sigPanel.Size = new System.Drawing.Size(299, 88);
            this.sigPanel.TabIndex = 14;
            this.sigPanel.Visible = false;
            // 
            // sprHex
            // 
            this.sprHex.Hexadecimal = true;
            this.sprHex.Location = new System.Drawing.Point(139, 46);
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
            this.datHex.Location = new System.Drawing.Point(139, 20);
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
            this.label4.Location = new System.Drawing.Point(34, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Spr Signature";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Dat Signature";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(392, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 21);
            this.button1.TabIndex = 15;
            this.button1.Text = "Auto Select Folder";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SpiderClientConverter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 463);
            this.Controls.Add(this.button1);
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
        private System.Windows.Forms.Button button1;
    }
}

