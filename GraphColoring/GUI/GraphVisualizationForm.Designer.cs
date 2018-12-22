namespace GraphColoring.GUI
{
    partial class GraphVisualizationForm
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
            this.graphVisualizationPictureBox = new System.Windows.Forms.PictureBox();
            this.graphVisualizationPanel = new System.Windows.Forms.Panel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.graphVisualizationPictureBox)).BeginInit();
            this.graphVisualizationPanel.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // graphVisualizationPictureBox
            // 
            this.graphVisualizationPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphVisualizationPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.graphVisualizationPictureBox.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.graphVisualizationPictureBox.Location = new System.Drawing.Point(3, 3);
            this.graphVisualizationPictureBox.Name = "graphVisualizationPictureBox";
            this.graphVisualizationPictureBox.Size = new System.Drawing.Size(770, 405);
            this.graphVisualizationPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.graphVisualizationPictureBox.TabIndex = 0;
            this.graphVisualizationPictureBox.TabStop = false;
            this.graphVisualizationPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GraphVisualizationForm_MouseDown);
            this.graphVisualizationPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GraphVisualizationForm_MouseMove);
            this.graphVisualizationPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GraphVisualizationForm_MouseUp);
            this.graphVisualizationPictureBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.GraphVisualizationForm_MouseWheel);
            // 
            // graphVisualizationPanel
            // 
            this.graphVisualizationPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphVisualizationPanel.BackColor = System.Drawing.Color.White;
            this.graphVisualizationPanel.Controls.Add(this.graphVisualizationPictureBox);
            this.graphVisualizationPanel.Location = new System.Drawing.Point(12, 27);
            this.graphVisualizationPanel.Name = "graphVisualizationPanel";
            this.graphVisualizationPanel.Size = new System.Drawing.Size(776, 411);
            this.graphVisualizationPanel.TabIndex = 1;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveImageToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 24);
            this.menuStrip.TabIndex = 3;
            this.menuStrip.Text = "menuStrip1";
            // 
            // saveImageToolStripMenuItem
            // 
            this.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            this.saveImageToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.saveImageToolStripMenuItem.Text = "Save";
            this.saveImageToolStripMenuItem.Click += new System.EventHandler(this.saveImageToolStripMenuItem_Click);
            // 
            // GraphVisualizationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.graphVisualizationPanel);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "GraphVisualizationForm";
            this.Text = "Graph coloring - graph visialization | Illner";
            ((System.ComponentModel.ISupportInitialize)(this.graphVisualizationPictureBox)).EndInit();
            this.graphVisualizationPanel.ResumeLayout(false);
            this.graphVisualizationPanel.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox graphVisualizationPictureBox;
        private System.Windows.Forms.Panel graphVisualizationPanel;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
    }
}