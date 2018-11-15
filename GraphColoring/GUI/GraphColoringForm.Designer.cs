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
            this.resetButton = new System.Windows.Forms.Button();
            this.colorGraphPlanScheduleButton = new System.Windows.Forms.Button();
            this.algorithmListBox = new System.Windows.Forms.ListBox();
            this.saveGraphButton = new System.Windows.Forms.Button();
            this.loadGraphButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.copyrightStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.drawGraphPanel = new System.Windows.Forms.Panel();
            this.graphPropertiesGroupBox = new System.Windows.Forms.GroupBox();
            this.getGraphPropertiesButton = new System.Windows.Forms.Button();
            this.classValuePropertiesLabel = new System.Windows.Forms.Label();
            this.classPropertiesLabel = new System.Windows.Forms.Label();
            this.countOfUsedColorsValueGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.countOfUsedColorsGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.isEulerianValueGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.isEulerianGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.cayleysFormulaValueGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.cayleysFormulaGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.circuitRankValueGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.circuitRankGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.isCyclicValueGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.isCyclicGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.grithValueGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.grithGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.countBridgesValueGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.countBridgesGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.countCutVerticesValueGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.countCutVerticesGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.averageVertexDegreeValueGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.averageVertexDegreeGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.minimumVertexDegreeValueGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.minimumVertexDegreeGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.maximumVertexDegreeValueGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.maximumVertexDegreeGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.isRegularValueGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.isRegularGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.countComponentValueGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.countComponentGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.isConnectedValueGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.isConnectedGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.countEdgesValueGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.countEdgesGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.countVerticesValueGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.countVerticesGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.generateGraphGroupBox = new System.Windows.Forms.GroupBox();
            this.graphDensityGenerateGraphComboBox = new System.Windows.Forms.ComboBox();
            this.graphDensityGenerateGraphLabel = new System.Windows.Forms.Label();
            this.countOfVerticesGenerateGraphLabelNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.countOfVerticesGenerateGraphLabel = new System.Windows.Forms.Label();
            this.generateGraphButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.drawGraphPictureBox)).BeginInit();
            this.graphColoringGroupBox.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.drawGraphPanel.SuspendLayout();
            this.graphPropertiesGroupBox.SuspendLayout();
            this.generateGraphGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.countOfVerticesGenerateGraphLabelNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // drawGraphPictureBox
            // 
            this.drawGraphPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.drawGraphPictureBox.ErrorImage = null;
            this.drawGraphPictureBox.InitialImage = null;
            this.drawGraphPictureBox.Location = new System.Drawing.Point(3, 3);
            this.drawGraphPictureBox.Name = "drawGraphPictureBox";
            this.drawGraphPictureBox.Size = new System.Drawing.Size(600, 612);
            this.drawGraphPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.drawGraphPictureBox.TabIndex = 0;
            this.drawGraphPictureBox.TabStop = false;
            // 
            // titleLabel
            // 
            this.titleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.titleLabel.BackColor = System.Drawing.Color.DarkRed;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.titleLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.titleLabel.Location = new System.Drawing.Point(902, 12);
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
            this.graphColoringGroupBox.Controls.Add(this.resetButton);
            this.graphColoringGroupBox.Controls.Add(this.colorGraphPlanScheduleButton);
            this.graphColoringGroupBox.Controls.Add(this.algorithmListBox);
            this.graphColoringGroupBox.Controls.Add(this.saveGraphButton);
            this.graphColoringGroupBox.Controls.Add(this.loadGraphButton);
            this.graphColoringGroupBox.Location = new System.Drawing.Point(902, 96);
            this.graphColoringGroupBox.Name = "graphColoringGroupBox";
            this.graphColoringGroupBox.Size = new System.Drawing.Size(272, 385);
            this.graphColoringGroupBox.TabIndex = 3;
            this.graphColoringGroupBox.TabStop = false;
            // 
            // resetButton
            // 
            this.resetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.resetButton.ForeColor = System.Drawing.Color.DarkRed;
            this.resetButton.Location = new System.Drawing.Point(6, 144);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(260, 57);
            this.resetButton.TabIndex = 5;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // colorGraphPlanScheduleButton
            // 
            this.colorGraphPlanScheduleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.colorGraphPlanScheduleButton.BackColor = System.Drawing.Color.DarkRed;
            this.colorGraphPlanScheduleButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.colorGraphPlanScheduleButton.ForeColor = System.Drawing.SystemColors.Control;
            this.colorGraphPlanScheduleButton.Location = new System.Drawing.Point(6, 322);
            this.colorGraphPlanScheduleButton.Name = "colorGraphPlanScheduleButton";
            this.colorGraphPlanScheduleButton.Size = new System.Drawing.Size(260, 57);
            this.colorGraphPlanScheduleButton.TabIndex = 4;
            this.colorGraphPlanScheduleButton.Text = "Color graph / Plan schedule";
            this.colorGraphPlanScheduleButton.UseVisualStyleBackColor = false;
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
            this.algorithmListBox.Location = new System.Drawing.Point(7, 207);
            this.algorithmListBox.Name = "algorithmListBox";
            this.algorithmListBox.Size = new System.Drawing.Size(259, 109);
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 639);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1184, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // copyrightStatusLabel
            // 
            this.copyrightStatusLabel.Name = "copyrightStatusLabel";
            this.copyrightStatusLabel.Size = new System.Drawing.Size(897, 17);
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
            // drawGraphPanel
            // 
            this.drawGraphPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.drawGraphPanel.AutoScroll = true;
            this.drawGraphPanel.BackColor = System.Drawing.Color.White;
            this.drawGraphPanel.Controls.Add(this.drawGraphPictureBox);
            this.drawGraphPanel.Location = new System.Drawing.Point(12, 12);
            this.drawGraphPanel.Name = "drawGraphPanel";
            this.drawGraphPanel.Size = new System.Drawing.Size(606, 618);
            this.drawGraphPanel.TabIndex = 5;
            // 
            // graphPropertiesGroupBox
            // 
            this.graphPropertiesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphPropertiesGroupBox.Controls.Add(this.getGraphPropertiesButton);
            this.graphPropertiesGroupBox.Controls.Add(this.classValuePropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.classPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.countOfUsedColorsValueGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.countOfUsedColorsGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.isEulerianValueGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.isEulerianGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.cayleysFormulaValueGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.cayleysFormulaGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.circuitRankValueGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.circuitRankGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.isCyclicValueGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.isCyclicGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.grithValueGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.grithGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.countBridgesValueGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.countBridgesGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.countCutVerticesValueGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.countCutVerticesGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.averageVertexDegreeValueGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.averageVertexDegreeGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.minimumVertexDegreeValueGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.minimumVertexDegreeGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.maximumVertexDegreeValueGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.maximumVertexDegreeGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.isRegularValueGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.isRegularGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.countComponentValueGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.countComponentGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.isConnectedValueGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.isConnectedGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.countEdgesValueGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.countEdgesGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.countVerticesValueGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.countVerticesGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Location = new System.Drawing.Point(624, 12);
            this.graphPropertiesGroupBox.Name = "graphPropertiesGroupBox";
            this.graphPropertiesGroupBox.Size = new System.Drawing.Size(272, 618);
            this.graphPropertiesGroupBox.TabIndex = 6;
            this.graphPropertiesGroupBox.TabStop = false;
            this.graphPropertiesGroupBox.Text = "Graph property";
            // 
            // getGraphPropertiesButton
            // 
            this.getGraphPropertiesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.getGraphPropertiesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.getGraphPropertiesButton.ForeColor = System.Drawing.Color.DarkRed;
            this.getGraphPropertiesButton.Location = new System.Drawing.Point(6, 555);
            this.getGraphPropertiesButton.Name = "getGraphPropertiesButton";
            this.getGraphPropertiesButton.Size = new System.Drawing.Size(260, 57);
            this.getGraphPropertiesButton.TabIndex = 34;
            this.getGraphPropertiesButton.Text = "Get graph properties";
            this.getGraphPropertiesButton.UseVisualStyleBackColor = true;
            this.getGraphPropertiesButton.Click += new System.EventHandler(this.getGraphPropertiesButton_Click);
            // 
            // classValuePropertiesLabel
            // 
            this.classValuePropertiesLabel.AutoSize = true;
            this.classValuePropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.classValuePropertiesLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.classValuePropertiesLabel.Location = new System.Drawing.Point(53, 188);
            this.classValuePropertiesLabel.Name = "classValuePropertiesLabel";
            this.classValuePropertiesLabel.Size = new System.Drawing.Size(29, 16);
            this.classValuePropertiesLabel.TabIndex = 33;
            this.classValuePropertiesLabel.Text = "       ";
            // 
            // classPropertiesLabel
            // 
            this.classPropertiesLabel.AutoSize = true;
            this.classPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.classPropertiesLabel.Location = new System.Drawing.Point(10, 188);
            this.classPropertiesLabel.Name = "classPropertiesLabel";
            this.classPropertiesLabel.Size = new System.Drawing.Size(48, 16);
            this.classPropertiesLabel.TabIndex = 32;
            this.classPropertiesLabel.Text = "Class: ";
            this.classPropertiesLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.classPropertiesLabel_Click);
            // 
            // countOfUsedColorsValueGraphPropertiesLabel
            // 
            this.countOfUsedColorsValueGraphPropertiesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.countOfUsedColorsValueGraphPropertiesLabel.AutoSize = true;
            this.countOfUsedColorsValueGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.countOfUsedColorsValueGraphPropertiesLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.countOfUsedColorsValueGraphPropertiesLabel.Location = new System.Drawing.Point(165, 529);
            this.countOfUsedColorsValueGraphPropertiesLabel.Name = "countOfUsedColorsValueGraphPropertiesLabel";
            this.countOfUsedColorsValueGraphPropertiesLabel.Size = new System.Drawing.Size(44, 20);
            this.countOfUsedColorsValueGraphPropertiesLabel.TabIndex = 31;
            this.countOfUsedColorsValueGraphPropertiesLabel.Text = "       ";
            // 
            // countOfUsedColorsGraphPropertiesLabel
            // 
            this.countOfUsedColorsGraphPropertiesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.countOfUsedColorsGraphPropertiesLabel.AutoSize = true;
            this.countOfUsedColorsGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.countOfUsedColorsGraphPropertiesLabel.Location = new System.Drawing.Point(10, 529);
            this.countOfUsedColorsGraphPropertiesLabel.Name = "countOfUsedColorsGraphPropertiesLabel";
            this.countOfUsedColorsGraphPropertiesLabel.Size = new System.Drawing.Size(163, 20);
            this.countOfUsedColorsGraphPropertiesLabel.TabIndex = 30;
            this.countOfUsedColorsGraphPropertiesLabel.Text = "Count of used colors: ";
            // 
            // isEulerianValueGraphPropertiesLabel
            // 
            this.isEulerianValueGraphPropertiesLabel.AutoSize = true;
            this.isEulerianValueGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.isEulerianValueGraphPropertiesLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.isEulerianValueGraphPropertiesLabel.Location = new System.Drawing.Point(81, 483);
            this.isEulerianValueGraphPropertiesLabel.Name = "isEulerianValueGraphPropertiesLabel";
            this.isEulerianValueGraphPropertiesLabel.Size = new System.Drawing.Size(29, 16);
            this.isEulerianValueGraphPropertiesLabel.TabIndex = 29;
            this.isEulerianValueGraphPropertiesLabel.Text = "       ";
            // 
            // isEulerianGraphPropertiesLabel
            // 
            this.isEulerianGraphPropertiesLabel.AutoSize = true;
            this.isEulerianGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.isEulerianGraphPropertiesLabel.Location = new System.Drawing.Point(10, 483);
            this.isEulerianGraphPropertiesLabel.Name = "isEulerianGraphPropertiesLabel";
            this.isEulerianGraphPropertiesLabel.Size = new System.Drawing.Size(75, 16);
            this.isEulerianGraphPropertiesLabel.TabIndex = 28;
            this.isEulerianGraphPropertiesLabel.Text = "Is eulerian: ";
            this.isEulerianGraphPropertiesLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.isEulerianGraphPropertiesLabel_Click);
            // 
            // cayleysFormulaValueGraphPropertiesLabel
            // 
            this.cayleysFormulaValueGraphPropertiesLabel.AutoSize = true;
            this.cayleysFormulaValueGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cayleysFormulaValueGraphPropertiesLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.cayleysFormulaValueGraphPropertiesLabel.Location = new System.Drawing.Point(116, 453);
            this.cayleysFormulaValueGraphPropertiesLabel.Name = "cayleysFormulaValueGraphPropertiesLabel";
            this.cayleysFormulaValueGraphPropertiesLabel.Size = new System.Drawing.Size(29, 16);
            this.cayleysFormulaValueGraphPropertiesLabel.TabIndex = 27;
            this.cayleysFormulaValueGraphPropertiesLabel.Text = "       ";
            // 
            // cayleysFormulaGraphPropertiesLabel
            // 
            this.cayleysFormulaGraphPropertiesLabel.AutoSize = true;
            this.cayleysFormulaGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cayleysFormulaGraphPropertiesLabel.Location = new System.Drawing.Point(10, 453);
            this.cayleysFormulaGraphPropertiesLabel.Name = "cayleysFormulaGraphPropertiesLabel";
            this.cayleysFormulaGraphPropertiesLabel.Size = new System.Drawing.Size(110, 16);
            this.cayleysFormulaGraphPropertiesLabel.TabIndex = 26;
            this.cayleysFormulaGraphPropertiesLabel.Text = "Cayleys formula: ";
            this.cayleysFormulaGraphPropertiesLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cayleysFormulaGraphPropertiesLabel_Click);
            // 
            // circuitRankValueGraphPropertiesLabel
            // 
            this.circuitRankValueGraphPropertiesLabel.AutoSize = true;
            this.circuitRankValueGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.circuitRankValueGraphPropertiesLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.circuitRankValueGraphPropertiesLabel.Location = new System.Drawing.Point(84, 80);
            this.circuitRankValueGraphPropertiesLabel.Name = "circuitRankValueGraphPropertiesLabel";
            this.circuitRankValueGraphPropertiesLabel.Size = new System.Drawing.Size(29, 16);
            this.circuitRankValueGraphPropertiesLabel.TabIndex = 25;
            this.circuitRankValueGraphPropertiesLabel.Text = "       ";
            // 
            // circuitRankGraphPropertiesLabel
            // 
            this.circuitRankGraphPropertiesLabel.AutoSize = true;
            this.circuitRankGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.circuitRankGraphPropertiesLabel.Location = new System.Drawing.Point(10, 80);
            this.circuitRankGraphPropertiesLabel.Name = "circuitRankGraphPropertiesLabel";
            this.circuitRankGraphPropertiesLabel.Size = new System.Drawing.Size(79, 16);
            this.circuitRankGraphPropertiesLabel.TabIndex = 24;
            this.circuitRankGraphPropertiesLabel.Text = "Circuit rank: ";
            // 
            // isCyclicValueGraphPropertiesLabel
            // 
            this.isCyclicValueGraphPropertiesLabel.AutoSize = true;
            this.isCyclicValueGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.isCyclicValueGraphPropertiesLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.isCyclicValueGraphPropertiesLabel.Location = new System.Drawing.Point(66, 243);
            this.isCyclicValueGraphPropertiesLabel.Name = "isCyclicValueGraphPropertiesLabel";
            this.isCyclicValueGraphPropertiesLabel.Size = new System.Drawing.Size(29, 16);
            this.isCyclicValueGraphPropertiesLabel.TabIndex = 23;
            this.isCyclicValueGraphPropertiesLabel.Text = "       ";
            // 
            // isCyclicGraphPropertiesLabel
            // 
            this.isCyclicGraphPropertiesLabel.AutoSize = true;
            this.isCyclicGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.isCyclicGraphPropertiesLabel.Location = new System.Drawing.Point(10, 243);
            this.isCyclicGraphPropertiesLabel.Name = "isCyclicGraphPropertiesLabel";
            this.isCyclicGraphPropertiesLabel.Size = new System.Drawing.Size(61, 16);
            this.isCyclicGraphPropertiesLabel.TabIndex = 22;
            this.isCyclicGraphPropertiesLabel.Text = "Is cyclic: ";
            this.isCyclicGraphPropertiesLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.isCyclicGraphPropertiesLabel_Click);
            // 
            // grithValueGraphPropertiesLabel
            // 
            this.grithValueGraphPropertiesLabel.AutoSize = true;
            this.grithValueGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.grithValueGraphPropertiesLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.grithValueGraphPropertiesLabel.Location = new System.Drawing.Point(47, 423);
            this.grithValueGraphPropertiesLabel.Name = "grithValueGraphPropertiesLabel";
            this.grithValueGraphPropertiesLabel.Size = new System.Drawing.Size(29, 16);
            this.grithValueGraphPropertiesLabel.TabIndex = 21;
            this.grithValueGraphPropertiesLabel.Text = "       ";
            // 
            // grithGraphPropertiesLabel
            // 
            this.grithGraphPropertiesLabel.AutoSize = true;
            this.grithGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.grithGraphPropertiesLabel.Location = new System.Drawing.Point(10, 423);
            this.grithGraphPropertiesLabel.Name = "grithGraphPropertiesLabel";
            this.grithGraphPropertiesLabel.Size = new System.Drawing.Size(41, 16);
            this.grithGraphPropertiesLabel.TabIndex = 20;
            this.grithGraphPropertiesLabel.Text = "Grith: ";
            this.grithGraphPropertiesLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.grithGraphPropertiesLabel_Click);
            // 
            // countBridgesValueGraphPropertiesLabel
            // 
            this.countBridgesValueGraphPropertiesLabel.AutoSize = true;
            this.countBridgesValueGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.countBridgesValueGraphPropertiesLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.countBridgesValueGraphPropertiesLabel.Location = new System.Drawing.Point(117, 393);
            this.countBridgesValueGraphPropertiesLabel.Name = "countBridgesValueGraphPropertiesLabel";
            this.countBridgesValueGraphPropertiesLabel.Size = new System.Drawing.Size(29, 16);
            this.countBridgesValueGraphPropertiesLabel.TabIndex = 19;
            this.countBridgesValueGraphPropertiesLabel.Text = "       ";
            // 
            // countBridgesGraphPropertiesLabel
            // 
            this.countBridgesGraphPropertiesLabel.AutoSize = true;
            this.countBridgesGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.countBridgesGraphPropertiesLabel.Location = new System.Drawing.Point(10, 393);
            this.countBridgesGraphPropertiesLabel.Name = "countBridgesGraphPropertiesLabel";
            this.countBridgesGraphPropertiesLabel.Size = new System.Drawing.Size(111, 16);
            this.countBridgesGraphPropertiesLabel.TabIndex = 18;
            this.countBridgesGraphPropertiesLabel.Text = "Count of bridges: ";
            this.countBridgesGraphPropertiesLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.countBridgesCutVerticesGraphPropertiesLabel_Click);
            // 
            // countCutVerticesValueGraphPropertiesLabel
            // 
            this.countCutVerticesValueGraphPropertiesLabel.AutoSize = true;
            this.countCutVerticesValueGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.countCutVerticesValueGraphPropertiesLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.countCutVerticesValueGraphPropertiesLabel.Location = new System.Drawing.Point(138, 363);
            this.countCutVerticesValueGraphPropertiesLabel.Name = "countCutVerticesValueGraphPropertiesLabel";
            this.countCutVerticesValueGraphPropertiesLabel.Size = new System.Drawing.Size(29, 16);
            this.countCutVerticesValueGraphPropertiesLabel.TabIndex = 17;
            this.countCutVerticesValueGraphPropertiesLabel.Text = "       ";
            // 
            // countCutVerticesGraphPropertiesLabel
            // 
            this.countCutVerticesGraphPropertiesLabel.AutoSize = true;
            this.countCutVerticesGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.countCutVerticesGraphPropertiesLabel.Location = new System.Drawing.Point(10, 363);
            this.countCutVerticesGraphPropertiesLabel.Name = "countCutVerticesGraphPropertiesLabel";
            this.countCutVerticesGraphPropertiesLabel.Size = new System.Drawing.Size(132, 16);
            this.countCutVerticesGraphPropertiesLabel.TabIndex = 16;
            this.countCutVerticesGraphPropertiesLabel.Text = "Count of cut vertices: ";
            this.countCutVerticesGraphPropertiesLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.countBridgesCutVerticesGraphPropertiesLabel_Click);
            // 
            // averageVertexDegreeValueGraphPropertiesLabel
            // 
            this.averageVertexDegreeValueGraphPropertiesLabel.AutoSize = true;
            this.averageVertexDegreeValueGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.averageVertexDegreeValueGraphPropertiesLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.averageVertexDegreeValueGraphPropertiesLabel.Location = new System.Drawing.Point(155, 333);
            this.averageVertexDegreeValueGraphPropertiesLabel.Name = "averageVertexDegreeValueGraphPropertiesLabel";
            this.averageVertexDegreeValueGraphPropertiesLabel.Size = new System.Drawing.Size(29, 16);
            this.averageVertexDegreeValueGraphPropertiesLabel.TabIndex = 15;
            this.averageVertexDegreeValueGraphPropertiesLabel.Text = "       ";
            // 
            // averageVertexDegreeGraphPropertiesLabel
            // 
            this.averageVertexDegreeGraphPropertiesLabel.AutoSize = true;
            this.averageVertexDegreeGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.averageVertexDegreeGraphPropertiesLabel.Location = new System.Drawing.Point(10, 333);
            this.averageVertexDegreeGraphPropertiesLabel.Name = "averageVertexDegreeGraphPropertiesLabel";
            this.averageVertexDegreeGraphPropertiesLabel.Size = new System.Drawing.Size(152, 16);
            this.averageVertexDegreeGraphPropertiesLabel.TabIndex = 14;
            this.averageVertexDegreeGraphPropertiesLabel.Text = "Average vertex degree: ";
            this.averageVertexDegreeGraphPropertiesLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.maximumMinimumAverageVertexDegreeGraphPropertiesLabel_Click);
            // 
            // minimumVertexDegreeValueGraphPropertiesLabel
            // 
            this.minimumVertexDegreeValueGraphPropertiesLabel.AutoSize = true;
            this.minimumVertexDegreeValueGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.minimumVertexDegreeValueGraphPropertiesLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.minimumVertexDegreeValueGraphPropertiesLabel.Location = new System.Drawing.Point(158, 303);
            this.minimumVertexDegreeValueGraphPropertiesLabel.Name = "minimumVertexDegreeValueGraphPropertiesLabel";
            this.minimumVertexDegreeValueGraphPropertiesLabel.Size = new System.Drawing.Size(29, 16);
            this.minimumVertexDegreeValueGraphPropertiesLabel.TabIndex = 13;
            this.minimumVertexDegreeValueGraphPropertiesLabel.Text = "       ";
            // 
            // minimumVertexDegreeGraphPropertiesLabel
            // 
            this.minimumVertexDegreeGraphPropertiesLabel.AutoSize = true;
            this.minimumVertexDegreeGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.minimumVertexDegreeGraphPropertiesLabel.Location = new System.Drawing.Point(10, 303);
            this.minimumVertexDegreeGraphPropertiesLabel.Name = "minimumVertexDegreeGraphPropertiesLabel";
            this.minimumVertexDegreeGraphPropertiesLabel.Size = new System.Drawing.Size(153, 16);
            this.minimumVertexDegreeGraphPropertiesLabel.TabIndex = 12;
            this.minimumVertexDegreeGraphPropertiesLabel.Text = "Minimum vertex degree: ";
            this.minimumVertexDegreeGraphPropertiesLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.maximumMinimumAverageVertexDegreeGraphPropertiesLabel_Click);
            // 
            // maximumVertexDegreeValueGraphPropertiesLabel
            // 
            this.maximumVertexDegreeValueGraphPropertiesLabel.AutoSize = true;
            this.maximumVertexDegreeValueGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.maximumVertexDegreeValueGraphPropertiesLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.maximumVertexDegreeValueGraphPropertiesLabel.Location = new System.Drawing.Point(162, 273);
            this.maximumVertexDegreeValueGraphPropertiesLabel.Name = "maximumVertexDegreeValueGraphPropertiesLabel";
            this.maximumVertexDegreeValueGraphPropertiesLabel.Size = new System.Drawing.Size(29, 16);
            this.maximumVertexDegreeValueGraphPropertiesLabel.TabIndex = 11;
            this.maximumVertexDegreeValueGraphPropertiesLabel.Text = "       ";
            // 
            // maximumVertexDegreeGraphPropertiesLabel
            // 
            this.maximumVertexDegreeGraphPropertiesLabel.AutoSize = true;
            this.maximumVertexDegreeGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.maximumVertexDegreeGraphPropertiesLabel.Location = new System.Drawing.Point(10, 273);
            this.maximumVertexDegreeGraphPropertiesLabel.Name = "maximumVertexDegreeGraphPropertiesLabel";
            this.maximumVertexDegreeGraphPropertiesLabel.Size = new System.Drawing.Size(157, 16);
            this.maximumVertexDegreeGraphPropertiesLabel.TabIndex = 10;
            this.maximumVertexDegreeGraphPropertiesLabel.Text = "Maximum vertex degree: ";
            this.maximumVertexDegreeGraphPropertiesLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.maximumMinimumAverageVertexDegreeGraphPropertiesLabel_Click);
            // 
            // isRegularValueGraphPropertiesLabel
            // 
            this.isRegularValueGraphPropertiesLabel.AutoSize = true;
            this.isRegularValueGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.isRegularValueGraphPropertiesLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.isRegularValueGraphPropertiesLabel.Location = new System.Drawing.Point(74, 213);
            this.isRegularValueGraphPropertiesLabel.Name = "isRegularValueGraphPropertiesLabel";
            this.isRegularValueGraphPropertiesLabel.Size = new System.Drawing.Size(29, 16);
            this.isRegularValueGraphPropertiesLabel.TabIndex = 9;
            this.isRegularValueGraphPropertiesLabel.Text = "       ";
            // 
            // isRegularGraphPropertiesLabel
            // 
            this.isRegularGraphPropertiesLabel.AutoSize = true;
            this.isRegularGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.isRegularGraphPropertiesLabel.Location = new System.Drawing.Point(10, 213);
            this.isRegularGraphPropertiesLabel.Name = "isRegularGraphPropertiesLabel";
            this.isRegularGraphPropertiesLabel.Size = new System.Drawing.Size(69, 16);
            this.isRegularGraphPropertiesLabel.TabIndex = 8;
            this.isRegularGraphPropertiesLabel.Text = "Is regular: ";
            this.isRegularGraphPropertiesLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.isRegularGraphPropertiesLabel_Click);
            // 
            // countComponentValueGraphPropertiesLabel
            // 
            this.countComponentValueGraphPropertiesLabel.AutoSize = true;
            this.countComponentValueGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.countComponentValueGraphPropertiesLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.countComponentValueGraphPropertiesLabel.Location = new System.Drawing.Point(143, 110);
            this.countComponentValueGraphPropertiesLabel.Name = "countComponentValueGraphPropertiesLabel";
            this.countComponentValueGraphPropertiesLabel.Size = new System.Drawing.Size(29, 16);
            this.countComponentValueGraphPropertiesLabel.TabIndex = 7;
            this.countComponentValueGraphPropertiesLabel.Text = "       ";
            // 
            // countComponentGraphPropertiesLabel
            // 
            this.countComponentGraphPropertiesLabel.AutoSize = true;
            this.countComponentGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.countComponentGraphPropertiesLabel.Location = new System.Drawing.Point(10, 110);
            this.countComponentGraphPropertiesLabel.Name = "countComponentGraphPropertiesLabel";
            this.countComponentGraphPropertiesLabel.Size = new System.Drawing.Size(139, 16);
            this.countComponentGraphPropertiesLabel.TabIndex = 6;
            this.countComponentGraphPropertiesLabel.Text = "Count of components: ";
            // 
            // isConnectedValueGraphPropertiesLabel
            // 
            this.isConnectedValueGraphPropertiesLabel.AutoSize = true;
            this.isConnectedValueGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.isConnectedValueGraphPropertiesLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.isConnectedValueGraphPropertiesLabel.Location = new System.Drawing.Point(94, 140);
            this.isConnectedValueGraphPropertiesLabel.Name = "isConnectedValueGraphPropertiesLabel";
            this.isConnectedValueGraphPropertiesLabel.Size = new System.Drawing.Size(29, 16);
            this.isConnectedValueGraphPropertiesLabel.TabIndex = 5;
            this.isConnectedValueGraphPropertiesLabel.Text = "       ";
            // 
            // isConnectedGraphPropertiesLabel
            // 
            this.isConnectedGraphPropertiesLabel.AutoSize = true;
            this.isConnectedGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.isConnectedGraphPropertiesLabel.Location = new System.Drawing.Point(10, 140);
            this.isConnectedGraphPropertiesLabel.Name = "isConnectedGraphPropertiesLabel";
            this.isConnectedGraphPropertiesLabel.Size = new System.Drawing.Size(90, 16);
            this.isConnectedGraphPropertiesLabel.TabIndex = 4;
            this.isConnectedGraphPropertiesLabel.Text = "Is connected: ";
            // 
            // countEdgesValueGraphPropertiesLabel
            // 
            this.countEdgesValueGraphPropertiesLabel.AutoSize = true;
            this.countEdgesValueGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.countEdgesValueGraphPropertiesLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.countEdgesValueGraphPropertiesLabel.Location = new System.Drawing.Point(108, 50);
            this.countEdgesValueGraphPropertiesLabel.Name = "countEdgesValueGraphPropertiesLabel";
            this.countEdgesValueGraphPropertiesLabel.Size = new System.Drawing.Size(29, 16);
            this.countEdgesValueGraphPropertiesLabel.TabIndex = 3;
            this.countEdgesValueGraphPropertiesLabel.Text = "       ";
            // 
            // countEdgesGraphPropertiesLabel
            // 
            this.countEdgesGraphPropertiesLabel.AutoSize = true;
            this.countEdgesGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.countEdgesGraphPropertiesLabel.Location = new System.Drawing.Point(10, 50);
            this.countEdgesGraphPropertiesLabel.Name = "countEdgesGraphPropertiesLabel";
            this.countEdgesGraphPropertiesLabel.Size = new System.Drawing.Size(104, 16);
            this.countEdgesGraphPropertiesLabel.TabIndex = 2;
            this.countEdgesGraphPropertiesLabel.Text = "Count of edges: ";
            // 
            // countVerticesValueGraphPropertiesLabel
            // 
            this.countVerticesValueGraphPropertiesLabel.AutoSize = true;
            this.countVerticesValueGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.countVerticesValueGraphPropertiesLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.countVerticesValueGraphPropertiesLabel.Location = new System.Drawing.Point(116, 20);
            this.countVerticesValueGraphPropertiesLabel.Name = "countVerticesValueGraphPropertiesLabel";
            this.countVerticesValueGraphPropertiesLabel.Size = new System.Drawing.Size(29, 16);
            this.countVerticesValueGraphPropertiesLabel.TabIndex = 1;
            this.countVerticesValueGraphPropertiesLabel.Text = "       ";
            // 
            // countVerticesGraphPropertiesLabel
            // 
            this.countVerticesGraphPropertiesLabel.AutoSize = true;
            this.countVerticesGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.countVerticesGraphPropertiesLabel.Location = new System.Drawing.Point(10, 20);
            this.countVerticesGraphPropertiesLabel.Name = "countVerticesGraphPropertiesLabel";
            this.countVerticesGraphPropertiesLabel.Size = new System.Drawing.Size(112, 16);
            this.countVerticesGraphPropertiesLabel.TabIndex = 0;
            this.countVerticesGraphPropertiesLabel.Text = "Count of vertices: ";
            // 
            // generateGraphGroupBox
            // 
            this.generateGraphGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.generateGraphGroupBox.Controls.Add(this.graphDensityGenerateGraphComboBox);
            this.generateGraphGroupBox.Controls.Add(this.graphDensityGenerateGraphLabel);
            this.generateGraphGroupBox.Controls.Add(this.countOfVerticesGenerateGraphLabelNumericUpDown);
            this.generateGraphGroupBox.Controls.Add(this.countOfVerticesGenerateGraphLabel);
            this.generateGraphGroupBox.Controls.Add(this.generateGraphButton);
            this.generateGraphGroupBox.Location = new System.Drawing.Point(902, 487);
            this.generateGraphGroupBox.Name = "generateGraphGroupBox";
            this.generateGraphGroupBox.Size = new System.Drawing.Size(272, 143);
            this.generateGraphGroupBox.TabIndex = 7;
            this.generateGraphGroupBox.TabStop = false;
            this.generateGraphGroupBox.Text = "Generate graph";
            // 
            // graphDensityGenerateGraphComboBox
            // 
            this.graphDensityGenerateGraphComboBox.FormattingEnabled = true;
            this.graphDensityGenerateGraphComboBox.Location = new System.Drawing.Point(146, 53);
            this.graphDensityGenerateGraphComboBox.Name = "graphDensityGenerateGraphComboBox";
            this.graphDensityGenerateGraphComboBox.Size = new System.Drawing.Size(120, 21);
            this.graphDensityGenerateGraphComboBox.TabIndex = 9;
            // 
            // graphDensityGenerateGraphLabel
            // 
            this.graphDensityGenerateGraphLabel.AutoSize = true;
            this.graphDensityGenerateGraphLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.graphDensityGenerateGraphLabel.Location = new System.Drawing.Point(6, 54);
            this.graphDensityGenerateGraphLabel.Name = "graphDensityGenerateGraphLabel";
            this.graphDensityGenerateGraphLabel.Size = new System.Drawing.Size(97, 16);
            this.graphDensityGenerateGraphLabel.TabIndex = 8;
            this.graphDensityGenerateGraphLabel.Text = "Graph density: ";
            // 
            // countOfVerticesGenerateGraphLabelNumericUpDown
            // 
            this.countOfVerticesGenerateGraphLabelNumericUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.countOfVerticesGenerateGraphLabelNumericUpDown.Location = new System.Drawing.Point(146, 25);
            this.countOfVerticesGenerateGraphLabelNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.countOfVerticesGenerateGraphLabelNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.countOfVerticesGenerateGraphLabelNumericUpDown.Name = "countOfVerticesGenerateGraphLabelNumericUpDown";
            this.countOfVerticesGenerateGraphLabelNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.countOfVerticesGenerateGraphLabelNumericUpDown.TabIndex = 7;
            this.countOfVerticesGenerateGraphLabelNumericUpDown.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // countOfVerticesGenerateGraphLabel
            // 
            this.countOfVerticesGenerateGraphLabel.AutoSize = true;
            this.countOfVerticesGenerateGraphLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.countOfVerticesGenerateGraphLabel.Location = new System.Drawing.Point(6, 25);
            this.countOfVerticesGenerateGraphLabel.Name = "countOfVerticesGenerateGraphLabel";
            this.countOfVerticesGenerateGraphLabel.Size = new System.Drawing.Size(112, 16);
            this.countOfVerticesGenerateGraphLabel.TabIndex = 6;
            this.countOfVerticesGenerateGraphLabel.Text = "Count of vertices: ";
            // 
            // generateGraphButton
            // 
            this.generateGraphButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.generateGraphButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.generateGraphButton.ForeColor = System.Drawing.Color.DarkRed;
            this.generateGraphButton.Location = new System.Drawing.Point(6, 80);
            this.generateGraphButton.Name = "generateGraphButton";
            this.generateGraphButton.Size = new System.Drawing.Size(260, 57);
            this.generateGraphButton.TabIndex = 5;
            this.generateGraphButton.Text = "Generate graph";
            this.generateGraphButton.UseVisualStyleBackColor = true;
            this.generateGraphButton.Click += new System.EventHandler(this.generateGraphButton_Click);
            // 
            // GraphColoringForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.generateGraphGroupBox);
            this.Controls.Add(this.graphPropertiesGroupBox);
            this.Controls.Add(this.drawGraphPanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.graphColoringGroupBox);
            this.Controls.Add(this.titleLabel);
            this.MinimumSize = new System.Drawing.Size(1200, 700);
            this.Name = "GraphColoringForm";
            this.Text = "Graph coloring | Illner";
            ((System.ComponentModel.ISupportInitialize)(this.drawGraphPictureBox)).EndInit();
            this.graphColoringGroupBox.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.drawGraphPanel.ResumeLayout(false);
            this.drawGraphPanel.PerformLayout();
            this.graphPropertiesGroupBox.ResumeLayout(false);
            this.graphPropertiesGroupBox.PerformLayout();
            this.generateGraphGroupBox.ResumeLayout(false);
            this.generateGraphGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.countOfVerticesGenerateGraphLabelNumericUpDown)).EndInit();
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
        private System.Windows.Forms.Panel drawGraphPanel;
        private System.Windows.Forms.GroupBox graphPropertiesGroupBox;
        private System.Windows.Forms.Label maximumVertexDegreeValueGraphPropertiesLabel;
        private System.Windows.Forms.Label maximumVertexDegreeGraphPropertiesLabel;
        private System.Windows.Forms.Label isRegularValueGraphPropertiesLabel;
        private System.Windows.Forms.Label isRegularGraphPropertiesLabel;
        private System.Windows.Forms.Label countComponentValueGraphPropertiesLabel;
        private System.Windows.Forms.Label countComponentGraphPropertiesLabel;
        private System.Windows.Forms.Label isConnectedValueGraphPropertiesLabel;
        private System.Windows.Forms.Label isConnectedGraphPropertiesLabel;
        private System.Windows.Forms.Label countEdgesValueGraphPropertiesLabel;
        private System.Windows.Forms.Label countEdgesGraphPropertiesLabel;
        private System.Windows.Forms.Label countVerticesValueGraphPropertiesLabel;
        private System.Windows.Forms.Label countVerticesGraphPropertiesLabel;
        private System.Windows.Forms.Label isEulerianValueGraphPropertiesLabel;
        private System.Windows.Forms.Label isEulerianGraphPropertiesLabel;
        private System.Windows.Forms.Label cayleysFormulaValueGraphPropertiesLabel;
        private System.Windows.Forms.Label cayleysFormulaGraphPropertiesLabel;
        private System.Windows.Forms.Label circuitRankValueGraphPropertiesLabel;
        private System.Windows.Forms.Label circuitRankGraphPropertiesLabel;
        private System.Windows.Forms.Label isCyclicValueGraphPropertiesLabel;
        private System.Windows.Forms.Label isCyclicGraphPropertiesLabel;
        private System.Windows.Forms.Label grithValueGraphPropertiesLabel;
        private System.Windows.Forms.Label grithGraphPropertiesLabel;
        private System.Windows.Forms.Label countBridgesValueGraphPropertiesLabel;
        private System.Windows.Forms.Label countBridgesGraphPropertiesLabel;
        private System.Windows.Forms.Label countCutVerticesValueGraphPropertiesLabel;
        private System.Windows.Forms.Label averageVertexDegreeValueGraphPropertiesLabel;
        private System.Windows.Forms.Label averageVertexDegreeGraphPropertiesLabel;
        private System.Windows.Forms.Label minimumVertexDegreeValueGraphPropertiesLabel;
        private System.Windows.Forms.Label minimumVertexDegreeGraphPropertiesLabel;
        protected System.Windows.Forms.Label countCutVerticesGraphPropertiesLabel;
        private System.Windows.Forms.Label countOfUsedColorsValueGraphPropertiesLabel;
        private System.Windows.Forms.Label countOfUsedColorsGraphPropertiesLabel;
        private System.Windows.Forms.Button getGraphPropertiesButton;
        private System.Windows.Forms.Label classValuePropertiesLabel;
        private System.Windows.Forms.Label classPropertiesLabel;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.GroupBox generateGraphGroupBox;
        private System.Windows.Forms.ComboBox graphDensityGenerateGraphComboBox;
        private System.Windows.Forms.Label graphDensityGenerateGraphLabel;
        private System.Windows.Forms.NumericUpDown countOfVerticesGenerateGraphLabelNumericUpDown;
        private System.Windows.Forms.Label countOfVerticesGenerateGraphLabel;
        private System.Windows.Forms.Button generateGraphButton;
    }
}