namespace GraphColoring.GUI
{
    partial class GraphColoringForm
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

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            this.drawGraphPictureBox = new System.Windows.Forms.PictureBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.graphColoringGroupBox = new System.Windows.Forms.GroupBox();
            this.colorGraphPlanScheduleButton = new System.Windows.Forms.Button();
            this.algorithmListBox = new System.Windows.Forms.ListBox();
            this.saveGraphButton = new System.Windows.Forms.Button();
            this.loadGraphButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.copyrightStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.drawGraphPictureBox)).BeginInit();
            this.graphColoringGroupBox.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // drawGraphPictureBox
            // 
            this.drawGraphPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.drawGraphPictureBox.BackColor = System.Drawing.Color.White;
            this.drawGraphPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.drawGraphPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drawGraphPictureBox.ErrorImage = null;
            this.drawGraphPictureBox.InitialImage = null;
            this.drawGraphPictureBox.Location = new System.Drawing.Point(12, 12);
            this.drawGraphPictureBox.Name = "drawGraphPictureBox";
            this.drawGraphPictureBox.Size = new System.Drawing.Size(604, 611);
            this.drawGraphPictureBox.TabIndex = 0;
            this.drawGraphPictureBox.TabStop = false;
            // 
            // titleLabel
            // 
            this.titleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.titleLabel.BackColor = System.Drawing.Color.DarkRed;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.titleLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.titleLabel.Location = new System.Drawing.Point(622, 12);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(272, 68);
            this.titleLabel.TabIndex = 2;
            this.titleLabel.Text = "Graph coloring";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // graphColoringGroupBox
            // 
            this.graphColoringGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphColoringGroupBox.Controls.Add(this.colorGraphPlanScheduleButton);
            this.graphColoringGroupBox.Controls.Add(this.algorithmListBox);
            this.graphColoringGroupBox.Controls.Add(this.saveGraphButton);
            this.graphColoringGroupBox.Controls.Add(this.loadGraphButton);
            this.graphColoringGroupBox.Location = new System.Drawing.Point(622, 96);
            this.graphColoringGroupBox.Name = "graphColoringGroupBox";
            this.graphColoringGroupBox.Size = new System.Drawing.Size(272, 527);
            this.graphColoringGroupBox.TabIndex = 3;
            this.graphColoringGroupBox.TabStop = false;
            // 
            // colorGraphPlanScheduleButton
            // 
            this.colorGraphPlanScheduleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.colorGraphPlanScheduleButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.colorGraphPlanScheduleButton.ForeColor = System.Drawing.Color.DarkRed;
            this.colorGraphPlanScheduleButton.Location = new System.Drawing.Point(6, 464);
            this.colorGraphPlanScheduleButton.Name = "colorGraphPlanScheduleButton";
            this.colorGraphPlanScheduleButton.Size = new System.Drawing.Size(260, 57);
            this.colorGraphPlanScheduleButton.TabIndex = 4;
            this.colorGraphPlanScheduleButton.Text = "Color graph / Plan schedule";
            this.colorGraphPlanScheduleButton.UseVisualStyleBackColor = true;
            this.colorGraphPlanScheduleButton.Click += new System.EventHandler(this.colorGraphPlanScheduleButton_Click);
            // 
            // algorithmListBox
            // 
            this.algorithmListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.algorithmListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.algorithmListBox.FormattingEnabled = true;
            this.algorithmListBox.IntegralHeight = false;
            this.algorithmListBox.ItemHeight = 16;
            this.algorithmListBox.Location = new System.Drawing.Point(7, 144);
            this.algorithmListBox.Name = "algorithmListBox";
            this.algorithmListBox.Size = new System.Drawing.Size(259, 314);
            this.algorithmListBox.TabIndex = 3;
            // 
            // saveGraphButton
            // 
            this.saveGraphButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.saveGraphButton.ForeColor = System.Drawing.Color.DarkRed;
            this.saveGraphButton.Location = new System.Drawing.Point(6, 82);
            this.saveGraphButton.Name = "saveGraphButton";
            this.saveGraphButton.Size = new System.Drawing.Size(260, 56);
            this.saveGraphButton.TabIndex = 1;
            this.saveGraphButton.Text = "Save colored graph / planned schedule";
            this.saveGraphButton.UseVisualStyleBackColor = true;
            this.saveGraphButton.Click += new System.EventHandler(this.saveGraphButton_Click);
            // 
            // loadGraphButton
            // 
            this.loadGraphButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.loadGraphButton.ForeColor = System.Drawing.Color.DarkRed;
            this.loadGraphButton.Location = new System.Drawing.Point(6, 19);
            this.loadGraphButton.Name = "loadGraphButton";
            this.loadGraphButton.Size = new System.Drawing.Size(260, 57);
            this.loadGraphButton.TabIndex = 0;
            this.loadGraphButton.Text = "Load graph / schedule";
            this.loadGraphButton.UseVisualStyleBackColor = true;
            this.loadGraphButton.Click += new System.EventHandler(this.loadGraphButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyrightStatusLabel,
            this.statusStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 626);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(904, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // copyrightStatusLabel
            // 
            this.copyrightStatusLabel.Name = "copyrightStatusLabel";
            this.copyrightStatusLabel.Size = new System.Drawing.Size(617, 17);
            this.copyrightStatusLabel.Spring = true;
            this.copyrightStatusLabel.Text = "© 2018 Petr Illner. All rights reserved";
            // 
            // statusStatusLabel
            // 
            this.statusStatusLabel.AutoSize = false;
            this.statusStatusLabel.Name = "statusStatusLabel";
            this.statusStatusLabel.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.statusStatusLabel.Size = new System.Drawing.Size(272, 17);
            // 
            // GraphColoringForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(904, 648);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.graphColoringGroupBox);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.drawGraphPictureBox);
            this.MinimumSize = new System.Drawing.Size(920, 687);
            this.Name = "GraphColoringForm";
            this.Text = "Graph coloring | Illner";
            ((System.ComponentModel.ISupportInitialize)(this.drawGraphPictureBox)).EndInit();
            this.graphColoringGroupBox.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox drawGraphPictureBox;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.GroupBox graphColoringGroupBox;
        private System.Windows.Forms.Button colorGraphPlanScheduleButton;
        private System.Windows.Forms.ListBox algorithmListBox;
        private System.Windows.Forms.Button saveGraphButton;
        private System.Windows.Forms.Button loadGraphButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel copyrightStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel statusStatusLabel;
    }
}