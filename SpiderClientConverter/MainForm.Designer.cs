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
            this.ExportSheets.Text = "Export Sheets";
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
            this.SliceBox.Location = new System.Drawing.Point(118, 124);
            this.SliceBox.Name = "SliceBox";
            this.SliceBox.Size = new System.Drawing.Size(85, 17);
            this.SliceBox.TabIndex = 9;
            this.SliceBox.Text = "Slice Sheets";
            this.SliceBox.UseVisualStyleBackColor = true;
            // 
            // ToSpr
            // 
            this.ToSpr.AutoSize = true;
            this.ToSpr.Location = new System.Drawing.Point(209, 124);
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
            // SpiderClientConverter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 463);
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
    }
}

