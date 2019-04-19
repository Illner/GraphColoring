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
            this.components = new System.ComponentModel.Container();
            this.drawGraphPictureBox = new System.Windows.Forms.PictureBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.graphColoringGroupBox = new System.Windows.Forms.GroupBox();
            this.showMaximumAndMinimumDegreeVerticesCheckBox = new System.Windows.Forms.CheckBox();
            this.showCutVerticesAndBridgesCheckBox = new System.Windows.Forms.CheckBox();
            this.showSimplicialVertexCheckBox = new System.Windows.Forms.CheckBox();
            this.showSpanningTreeCheckBox = new System.Windows.Forms.CheckBox();
            this.namedGraphsComboBox = new System.Windows.Forms.ComboBox();
            this.scheduleAppearanceCheckBox = new System.Windows.Forms.CheckBox();
            this.newGraphButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.colorGraphPlanScheduleButton = new System.Windows.Forms.Button();
            this.algorithmListBox = new System.Windows.Forms.ListBox();
            this.saveGraphButton = new System.Windows.Forms.Button();
            this.loadGraphButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.copyrightStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ColoringProgressProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.drawGraphPanel = new System.Windows.Forms.Panel();
            this.graphModificationEdgeGroupBox = new System.Windows.Forms.GroupBox();
            this.edgeSubdivisionGraphModificationEdgeButton = new System.Windows.Forms.Button();
            this.edgeContractionGraphModificationEdgeButton = new System.Windows.Forms.Button();
            this.edgeDeleteGraphModificationEdgeButton = new System.Windows.Forms.Button();
            this.edgeAddGraphModificationEdgeButton = new System.Windows.Forms.Button();
            this.secondVertexNameGraphModificationEdgeLabel = new System.Windows.Forms.Label();
            this.secondVertexNameGraphModificationEdgeTextBox = new System.Windows.Forms.TextBox();
            this.firstVertexNameGraphModificationEdgeLabel = new System.Windows.Forms.Label();
            this.firstVertexNameGraphModificationEdgeTextBox = new System.Windows.Forms.TextBox();
            this.graphOperationGroupBox = new System.Windows.Forms.GroupBox();
            this.lineGraphGraphOperationButton = new System.Windows.Forms.Button();
            this.complementGraphGraphOperationButton = new System.Windows.Forms.Button();
            this.generateGraphGroupBox = new System.Windows.Forms.GroupBox();
            this.graphDensityGenerateGraphComboBox = new System.Windows.Forms.ComboBox();
            this.graphDensityGenerateGraphLabel = new System.Windows.Forms.Label();
            this.countOfVerticesGenerateGraphLabelNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.countOfVerticesGenerateGraphLabel = new System.Windows.Forms.Label();
            this.generateGraphButton = new System.Windows.Forms.Button();
            this.graphPropertiesGroupBox = new System.Windows.Forms.GroupBox();
            this.SimplicialVertexValueGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.simplicialVertexGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.isChordalValueGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.isChordalGraphPropertiesLabel = new System.Windows.Forms.Label();
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
            this.girthValueGraphPropertiesLabel = new System.Windows.Forms.Label();
            this.girthGraphPropertiesLabel = new System.Windows.Forms.Label();
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
            this.graphModificationVertexGroupBox = new System.Windows.Forms.GroupBox();
            this.newVertexNameGraphModificationVertexLabel = new System.Windows.Forms.Label();
            this.newVertexNameGraphModificationVertexTextBox = new System.Windows.Forms.TextBox();
            this.changeVertexNameGraphModificationVertexButton = new System.Windows.Forms.Button();
            this.vertexExpansionGraphModificationVertexButton = new System.Windows.Forms.Button();
            this.vertexSuppressionGraphModificationVertexButton = new System.Windows.Forms.Button();
            this.vertexContractionGraphModificationVertexButton = new System.Windows.Forms.Button();
            this.vertexDeleteGraphModificationVertexButton = new System.Windows.Forms.Button();
            this.vertexAddGraphModificationVertexButton = new System.Windows.Forms.Button();
            this.vertexNameGraphModificationVertexLabel = new System.Windows.Forms.Label();
            this.vertexNameGraphModificationVertexTextBox = new System.Windows.Forms.TextBox();
            this.graphModificationGroupBox = new System.Windows.Forms.GroupBox();
            this.firstColumnPanel = new System.Windows.Forms.Panel();
            this.secondColumnPanel = new System.Windows.Forms.Panel();
            this.thirdColumnPanel = new System.Windows.Forms.Panel();
            this.ColoringProgressTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.drawGraphPictureBox)).BeginInit();
            this.graphColoringGroupBox.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.drawGraphPanel.SuspendLayout();
            this.graphModificationEdgeGroupBox.SuspendLayout();
            this.graphOperationGroupBox.SuspendLayout();
            this.generateGraphGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.countOfVerticesGenerateGraphLabelNumericUpDown)).BeginInit();
            this.graphPropertiesGroupBox.SuspendLayout();
            this.graphModificationVertexGroupBox.SuspendLayout();
            this.graphModificationGroupBox.SuspendLayout();
            this.firstColumnPanel.SuspendLayout();
            this.secondColumnPanel.SuspendLayout();
            this.thirdColumnPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // drawGraphPictureBox
            // 
            this.drawGraphPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.drawGraphPictureBox.ErrorImage = null;
            this.drawGraphPictureBox.InitialImage = null;
            this.drawGraphPictureBox.Location = new System.Drawing.Point(3, 3);
            this.drawGraphPictureBox.Name = "drawGraphPictureBox";
            this.drawGraphPictureBox.Size = new System.Drawing.Size(29, 26);
            this.drawGraphPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.drawGraphPictureBox.TabIndex = 0;
            this.drawGraphPictureBox.TabStop = false;
            this.drawGraphPictureBox.Click += new System.EventHandler(this.drawGraphPanel_Click);
            // 
            // titleLabel
            // 
            this.titleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.titleLabel.BackColor = System.Drawing.Color.DarkRed;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.titleLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.titleLabel.Location = new System.Drawing.Point(1305, 12);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(271, 68);
            this.titleLabel.TabIndex = 2;
            this.titleLabel.Text = "Graph coloring";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // graphColoringGroupBox
            // 
            this.graphColoringGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphColoringGroupBox.Controls.Add(this.showMaximumAndMinimumDegreeVerticesCheckBox);
            this.graphColoringGroupBox.Controls.Add(this.showCutVerticesAndBridgesCheckBox);
            this.graphColoringGroupBox.Controls.Add(this.showSimplicialVertexCheckBox);
            this.graphColoringGroupBox.Controls.Add(this.showSpanningTreeCheckBox);
            this.graphColoringGroupBox.Controls.Add(this.namedGraphsComboBox);
            this.graphColoringGroupBox.Controls.Add(this.scheduleAppearanceCheckBox);
            this.graphColoringGroupBox.Controls.Add(this.newGraphButton);
            this.graphColoringGroupBox.Controls.Add(this.resetButton);
            this.graphColoringGroupBox.Controls.Add(this.colorGraphPlanScheduleButton);
            this.graphColoringGroupBox.Controls.Add(this.algorithmListBox);
            this.graphColoringGroupBox.Controls.Add(this.saveGraphButton);
            this.graphColoringGroupBox.Controls.Add(this.loadGraphButton);
            this.graphColoringGroupBox.Location = new System.Drawing.Point(3, 3);
            this.graphColoringGroupBox.Name = "graphColoringGroupBox";
            this.graphColoringGroupBox.Size = new System.Drawing.Size(266, 566);
            this.graphColoringGroupBox.TabIndex = 3;
            this.graphColoringGroupBox.TabStop = false;
            // 
            // showMaximumAndMinimumDegreeVerticesCheckBox
            // 
            this.showMaximumAndMinimumDegreeVerticesCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.showMaximumAndMinimumDegreeVerticesCheckBox.AutoSize = true;
            this.showMaximumAndMinimumDegreeVerticesCheckBox.Checked = true;
            this.showMaximumAndMinimumDegreeVerticesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showMaximumAndMinimumDegreeVerticesCheckBox.Location = new System.Drawing.Point(9, 486);
            this.showMaximumAndMinimumDegreeVerticesCheckBox.Name = "showMaximumAndMinimumDegreeVerticesCheckBox";
            this.showMaximumAndMinimumDegreeVerticesCheckBox.Size = new System.Drawing.Size(239, 17);
            this.showMaximumAndMinimumDegreeVerticesCheckBox.TabIndex = 11;
            this.showMaximumAndMinimumDegreeVerticesCheckBox.Text = "Show maximum and minimum degree vertices";
            this.showMaximumAndMinimumDegreeVerticesCheckBox.UseVisualStyleBackColor = true;
            this.showMaximumAndMinimumDegreeVerticesCheckBox.CheckedChanged += new System.EventHandler(this.showCheckBox_CheckedChanged);
            // 
            // showCutVerticesAndBridgesCheckBox
            // 
            this.showCutVerticesAndBridgesCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.showCutVerticesAndBridgesCheckBox.AutoSize = true;
            this.showCutVerticesAndBridgesCheckBox.Checked = true;
            this.showCutVerticesAndBridgesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showCutVerticesAndBridgesCheckBox.Location = new System.Drawing.Point(9, 463);
            this.showCutVerticesAndBridgesCheckBox.Name = "showCutVerticesAndBridgesCheckBox";
            this.showCutVerticesAndBridgesCheckBox.Size = new System.Drawing.Size(169, 17);
            this.showCutVerticesAndBridgesCheckBox.TabIndex = 10;
            this.showCutVerticesAndBridgesCheckBox.Text = "Show cut vertices and bridges";
            this.showCutVerticesAndBridgesCheckBox.UseVisualStyleBackColor = true;
            this.showCutVerticesAndBridgesCheckBox.CheckedChanged += new System.EventHandler(this.showCheckBox_CheckedChanged);
            // 
            // showSimplicialVertexCheckBox
            // 
            this.showSimplicialVertexCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.showSimplicialVertexCheckBox.AutoSize = true;
            this.showSimplicialVertexCheckBox.Checked = true;
            this.showSimplicialVertexCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showSimplicialVertexCheckBox.Location = new System.Drawing.Point(129, 440);
            this.showSimplicialVertexCheckBox.Name = "showSimplicialVertexCheckBox";
            this.showSimplicialVertexCheckBox.Size = new System.Drawing.Size(129, 17);
            this.showSimplicialVertexCheckBox.TabIndex = 9;
            this.showSimplicialVertexCheckBox.Text = "Show simplicial vertex";
            this.showSimplicialVertexCheckBox.UseVisualStyleBackColor = true;
            this.showSimplicialVertexCheckBox.CheckedChanged += new System.EventHandler(this.showCheckBox_CheckedChanged);
            // 
            // showSpanningTreeCheckBox
            // 
            this.showSpanningTreeCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.showSpanningTreeCheckBox.AutoSize = true;
            this.showSpanningTreeCheckBox.Checked = true;
            this.showSpanningTreeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showSpanningTreeCheckBox.Location = new System.Drawing.Point(9, 440);
            this.showSpanningTreeCheckBox.Name = "showSpanningTreeCheckBox";
            this.showSpanningTreeCheckBox.Size = new System.Drawing.Size(120, 17);
            this.showSpanningTreeCheckBox.TabIndex = 8;
            this.showSpanningTreeCheckBox.Text = "Show spanning tree";
            this.showSpanningTreeCheckBox.UseVisualStyleBackColor = true;
            this.showSpanningTreeCheckBox.CheckedChanged += new System.EventHandler(this.showCheckBox_CheckedChanged);
            // 
            // namedGraphsComboBox
            // 
            this.namedGraphsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.namedGraphsComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.namedGraphsComboBox.FormattingEnabled = true;
            this.namedGraphsComboBox.Location = new System.Drawing.Point(7, 144);
            this.namedGraphsComboBox.Name = "namedGraphsComboBox";
            this.namedGraphsComboBox.Size = new System.Drawing.Size(252, 23);
            this.namedGraphsComboBox.TabIndex = 7;
            // 
            // scheduleAppearanceCheckBox
            // 
            this.scheduleAppearanceCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.scheduleAppearanceCheckBox.AutoSize = true;
            this.scheduleAppearanceCheckBox.Location = new System.Drawing.Point(9, 417);
            this.scheduleAppearanceCheckBox.Name = "scheduleAppearanceCheckBox";
            this.scheduleAppearanceCheckBox.Size = new System.Drawing.Size(131, 17);
            this.scheduleAppearanceCheckBox.TabIndex = 1;
            this.scheduleAppearanceCheckBox.Text = "Schedule appearance";
            this.scheduleAppearanceCheckBox.UseVisualStyleBackColor = true;
            this.scheduleAppearanceCheckBox.CheckedChanged += new System.EventHandler(this.scheduleAppearanceCheckBox_CheckedChanged);
            // 
            // newGraphButton
            // 
            this.newGraphButton.BackColor = System.Drawing.Color.DarkRed;
            this.newGraphButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.newGraphButton.ForeColor = System.Drawing.SystemColors.Control;
            this.newGraphButton.Location = new System.Drawing.Point(7, 171);
            this.newGraphButton.Name = "newGraphButton";
            this.newGraphButton.Size = new System.Drawing.Size(253, 57);
            this.newGraphButton.TabIndex = 6;
            this.newGraphButton.Text = "New graph";
            this.newGraphButton.UseVisualStyleBackColor = false;
            this.newGraphButton.Click += new System.EventHandler(this.newGraphButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.resetButton.ForeColor = System.Drawing.Color.DarkRed;
            this.resetButton.Location = new System.Drawing.Point(6, 234);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(253, 57);
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
            this.colorGraphPlanScheduleButton.Location = new System.Drawing.Point(6, 503);
            this.colorGraphPlanScheduleButton.Name = "colorGraphPlanScheduleButton";
            this.colorGraphPlanScheduleButton.Size = new System.Drawing.Size(253, 57);
            this.colorGraphPlanScheduleButton.TabIndex = 4;
            this.colorGraphPlanScheduleButton.Text = "Color graph / Plan schedule";
            this.colorGraphPlanScheduleButton.UseVisualStyleBackColor = false;
            this.colorGraphPlanScheduleButton.Click += new System.EventHandler(this.colorGraphPlanScheduleButton_Click);
            // 
            // algorithmListBox
            // 
            this.algorithmListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.algorithmListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.algorithmListBox.FormattingEnabled = true;
            this.algorithmListBox.IntegralHeight = false;
            this.algorithmListBox.Location = new System.Drawing.Point(7, 297);
            this.algorithmListBox.Name = "algorithmListBox";
            this.algorithmListBox.Size = new System.Drawing.Size(252, 114);
            this.algorithmListBox.TabIndex = 3;
            // 
            // saveGraphButton
            // 
            this.saveGraphButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.saveGraphButton.ForeColor = System.Drawing.Color.DarkRed;
            this.saveGraphButton.Location = new System.Drawing.Point(6, 82);
            this.saveGraphButton.Name = "saveGraphButton";
            this.saveGraphButton.Size = new System.Drawing.Size(253, 56);
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
            this.loadGraphButton.Size = new System.Drawing.Size(253, 57);
            this.loadGraphButton.TabIndex = 0;
            this.loadGraphButton.Text = "Load graph / schedule";
            this.loadGraphButton.UseVisualStyleBackColor = true;
            this.loadGraphButton.Click += new System.EventHandler(this.loadGraphButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyrightStatusLabel,
            this.statusStatusLabel,
            this.ColoringProgressProgressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 819);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1584, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // copyrightStatusLabel
            // 
            this.copyrightStatusLabel.Name = "copyrightStatusLabel";
            this.copyrightStatusLabel.Size = new System.Drawing.Size(1297, 17);
            this.copyrightStatusLabel.Spring = true;
            this.copyrightStatusLabel.Text = "© 2018 - 2019 Petr Illner. All rights reserved";
            // 
            // statusStatusLabel
            // 
            this.statusStatusLabel.AutoSize = false;
            this.statusStatusLabel.Name = "statusStatusLabel";
            this.statusStatusLabel.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.statusStatusLabel.Size = new System.Drawing.Size(272, 17);
            // 
            // ColoringProgressProgressBar
            // 
            this.ColoringProgressProgressBar.Name = "ColoringProgressProgressBar";
            this.ColoringProgressProgressBar.Size = new System.Drawing.Size(100, 16);
            this.ColoringProgressProgressBar.Visible = false;
            // 
            // drawGraphPanel
            // 
            this.drawGraphPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.drawGraphPanel.AutoScroll = true;
            this.drawGraphPanel.BackColor = System.Drawing.Color.White;
            this.drawGraphPanel.Controls.Add(this.drawGraphPictureBox);
            this.drawGraphPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.drawGraphPanel.Location = new System.Drawing.Point(12, 12);
            this.drawGraphPanel.Name = "drawGraphPanel";
            this.drawGraphPanel.Size = new System.Drawing.Size(771, 798);
            this.drawGraphPanel.TabIndex = 5;
            this.drawGraphPanel.Click += new System.EventHandler(this.drawGraphPanel_Click);
            this.drawGraphPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.drawGraphPanel_Paint);
            // 
            // graphModificationEdgeGroupBox
            // 
            this.graphModificationEdgeGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphModificationEdgeGroupBox.Controls.Add(this.edgeSubdivisionGraphModificationEdgeButton);
            this.graphModificationEdgeGroupBox.Controls.Add(this.edgeContractionGraphModificationEdgeButton);
            this.graphModificationEdgeGroupBox.Controls.Add(this.edgeDeleteGraphModificationEdgeButton);
            this.graphModificationEdgeGroupBox.Controls.Add(this.edgeAddGraphModificationEdgeButton);
            this.graphModificationEdgeGroupBox.Controls.Add(this.secondVertexNameGraphModificationEdgeLabel);
            this.graphModificationEdgeGroupBox.Controls.Add(this.secondVertexNameGraphModificationEdgeTextBox);
            this.graphModificationEdgeGroupBox.Controls.Add(this.firstVertexNameGraphModificationEdgeLabel);
            this.graphModificationEdgeGroupBox.Controls.Add(this.firstVertexNameGraphModificationEdgeTextBox);
            this.graphModificationEdgeGroupBox.Location = new System.Drawing.Point(6, 455);
            this.graphModificationEdgeGroupBox.Name = "graphModificationEdgeGroupBox";
            this.graphModificationEdgeGroupBox.Size = new System.Drawing.Size(214, 336);
            this.graphModificationEdgeGroupBox.TabIndex = 9;
            this.graphModificationEdgeGroupBox.TabStop = false;
            this.graphModificationEdgeGroupBox.Text = "Edge";
            // 
            // edgeSubdivisionGraphModificationEdgeButton
            // 
            this.edgeSubdivisionGraphModificationEdgeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.edgeSubdivisionGraphModificationEdgeButton.ForeColor = System.Drawing.Color.DarkRed;
            this.edgeSubdivisionGraphModificationEdgeButton.Location = new System.Drawing.Point(6, 273);
            this.edgeSubdivisionGraphModificationEdgeButton.Name = "edgeSubdivisionGraphModificationEdgeButton";
            this.edgeSubdivisionGraphModificationEdgeButton.Size = new System.Drawing.Size(201, 57);
            this.edgeSubdivisionGraphModificationEdgeButton.TabIndex = 39;
            this.edgeSubdivisionGraphModificationEdgeButton.Text = "Edge subdivision";
            this.edgeSubdivisionGraphModificationEdgeButton.UseVisualStyleBackColor = true;
            this.edgeSubdivisionGraphModificationEdgeButton.Click += new System.EventHandler(this.edgeSubdivisionGraphModificationEdgeButton_Click);
            // 
            // edgeContractionGraphModificationEdgeButton
            // 
            this.edgeContractionGraphModificationEdgeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.edgeContractionGraphModificationEdgeButton.ForeColor = System.Drawing.Color.DarkRed;
            this.edgeContractionGraphModificationEdgeButton.Location = new System.Drawing.Point(6, 210);
            this.edgeContractionGraphModificationEdgeButton.Name = "edgeContractionGraphModificationEdgeButton";
            this.edgeContractionGraphModificationEdgeButton.Size = new System.Drawing.Size(201, 57);
            this.edgeContractionGraphModificationEdgeButton.TabIndex = 38;
            this.edgeContractionGraphModificationEdgeButton.Text = "Edge contraction";
            this.edgeContractionGraphModificationEdgeButton.UseVisualStyleBackColor = true;
            this.edgeContractionGraphModificationEdgeButton.Click += new System.EventHandler(this.edgeContractionGraphModificationEdgeButton_Click);
            // 
            // edgeDeleteGraphModificationEdgeButton
            // 
            this.edgeDeleteGraphModificationEdgeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.edgeDeleteGraphModificationEdgeButton.ForeColor = System.Drawing.Color.DarkRed;
            this.edgeDeleteGraphModificationEdgeButton.Location = new System.Drawing.Point(6, 147);
            this.edgeDeleteGraphModificationEdgeButton.Name = "edgeDeleteGraphModificationEdgeButton";
            this.edgeDeleteGraphModificationEdgeButton.Size = new System.Drawing.Size(201, 57);
            this.edgeDeleteGraphModificationEdgeButton.TabIndex = 37;
            this.edgeDeleteGraphModificationEdgeButton.Text = "Remove edge";
            this.edgeDeleteGraphModificationEdgeButton.UseVisualStyleBackColor = true;
            this.edgeDeleteGraphModificationEdgeButton.Click += new System.EventHandler(this.deleteEdgeGraphModificationEdgeButton_Click);
            // 
            // edgeAddGraphModificationEdgeButton
            // 
            this.edgeAddGraphModificationEdgeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.edgeAddGraphModificationEdgeButton.ForeColor = System.Drawing.Color.DarkRed;
            this.edgeAddGraphModificationEdgeButton.Location = new System.Drawing.Point(6, 84);
            this.edgeAddGraphModificationEdgeButton.Name = "edgeAddGraphModificationEdgeButton";
            this.edgeAddGraphModificationEdgeButton.Size = new System.Drawing.Size(201, 57);
            this.edgeAddGraphModificationEdgeButton.TabIndex = 36;
            this.edgeAddGraphModificationEdgeButton.Text = "Add edge";
            this.edgeAddGraphModificationEdgeButton.UseVisualStyleBackColor = true;
            this.edgeAddGraphModificationEdgeButton.Click += new System.EventHandler(this.edgeAddGraphModificationEdgeButton_Click);
            // 
            // secondVertexNameGraphModificationEdgeLabel
            // 
            this.secondVertexNameGraphModificationEdgeLabel.AutoSize = true;
            this.secondVertexNameGraphModificationEdgeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.secondVertexNameGraphModificationEdgeLabel.Location = new System.Drawing.Point(6, 55);
            this.secondVertexNameGraphModificationEdgeLabel.Name = "secondVertexNameGraphModificationEdgeLabel";
            this.secondVertexNameGraphModificationEdgeLabel.Size = new System.Drawing.Size(137, 16);
            this.secondVertexNameGraphModificationEdgeLabel.TabIndex = 5;
            this.secondVertexNameGraphModificationEdgeLabel.Text = "Second vertex name: ";
            // 
            // secondVertexNameGraphModificationEdgeTextBox
            // 
            this.secondVertexNameGraphModificationEdgeTextBox.Location = new System.Drawing.Point(149, 54);
            this.secondVertexNameGraphModificationEdgeTextBox.Name = "secondVertexNameGraphModificationEdgeTextBox";
            this.secondVertexNameGraphModificationEdgeTextBox.Size = new System.Drawing.Size(51, 20);
            this.secondVertexNameGraphModificationEdgeTextBox.TabIndex = 4;
            // 
            // firstVertexNameGraphModificationEdgeLabel
            // 
            this.firstVertexNameGraphModificationEdgeLabel.AutoSize = true;
            this.firstVertexNameGraphModificationEdgeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.firstVertexNameGraphModificationEdgeLabel.Location = new System.Drawing.Point(6, 24);
            this.firstVertexNameGraphModificationEdgeLabel.Name = "firstVertexNameGraphModificationEdgeLabel";
            this.firstVertexNameGraphModificationEdgeLabel.Size = new System.Drawing.Size(115, 16);
            this.firstVertexNameGraphModificationEdgeLabel.TabIndex = 3;
            this.firstVertexNameGraphModificationEdgeLabel.Text = "First vertex name: ";
            // 
            // firstVertexNameGraphModificationEdgeTextBox
            // 
            this.firstVertexNameGraphModificationEdgeTextBox.Location = new System.Drawing.Point(149, 20);
            this.firstVertexNameGraphModificationEdgeTextBox.Name = "firstVertexNameGraphModificationEdgeTextBox";
            this.firstVertexNameGraphModificationEdgeTextBox.Size = new System.Drawing.Size(51, 20);
            this.firstVertexNameGraphModificationEdgeTextBox.TabIndex = 2;
            // 
            // graphOperationGroupBox
            // 
            this.graphOperationGroupBox.Controls.Add(this.lineGraphGraphOperationButton);
            this.graphOperationGroupBox.Controls.Add(this.complementGraphGraphOperationButton);
            this.graphOperationGroupBox.Location = new System.Drawing.Point(3, 651);
            this.graphOperationGroupBox.Name = "graphOperationGroupBox";
            this.graphOperationGroupBox.Size = new System.Drawing.Size(264, 149);
            this.graphOperationGroupBox.TabIndex = 9;
            this.graphOperationGroupBox.TabStop = false;
            this.graphOperationGroupBox.Text = "Graph operation";
            // 
            // lineGraphGraphOperationButton
            // 
            this.lineGraphGraphOperationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lineGraphGraphOperationButton.ForeColor = System.Drawing.Color.DarkRed;
            this.lineGraphGraphOperationButton.Location = new System.Drawing.Point(6, 82);
            this.lineGraphGraphOperationButton.Name = "lineGraphGraphOperationButton";
            this.lineGraphGraphOperationButton.Size = new System.Drawing.Size(252, 57);
            this.lineGraphGraphOperationButton.TabIndex = 38;
            this.lineGraphGraphOperationButton.Text = "Line graph";
            this.lineGraphGraphOperationButton.UseVisualStyleBackColor = true;
            this.lineGraphGraphOperationButton.Click += new System.EventHandler(this.lineGraphGraphOperationButton_Click);
            // 
            // complementGraphGraphOperationButton
            // 
            this.complementGraphGraphOperationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.complementGraphGraphOperationButton.ForeColor = System.Drawing.Color.DarkRed;
            this.complementGraphGraphOperationButton.Location = new System.Drawing.Point(6, 19);
            this.complementGraphGraphOperationButton.Name = "complementGraphGraphOperationButton";
            this.complementGraphGraphOperationButton.Size = new System.Drawing.Size(252, 57);
            this.complementGraphGraphOperationButton.TabIndex = 37;
            this.complementGraphGraphOperationButton.Text = "Complement graph";
            this.complementGraphGraphOperationButton.UseVisualStyleBackColor = true;
            this.complementGraphGraphOperationButton.Click += new System.EventHandler(this.complementGraphGraphOperationButton_Click);
            // 
            // generateGraphGroupBox
            // 
            this.generateGraphGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.generateGraphGroupBox.Controls.Add(this.graphDensityGenerateGraphComboBox);
            this.generateGraphGroupBox.Controls.Add(this.graphDensityGenerateGraphLabel);
            this.generateGraphGroupBox.Controls.Add(this.countOfVerticesGenerateGraphLabelNumericUpDown);
            this.generateGraphGroupBox.Controls.Add(this.countOfVerticesGenerateGraphLabel);
            this.generateGraphGroupBox.Controls.Add(this.generateGraphButton);
            this.generateGraphGroupBox.Location = new System.Drawing.Point(3, 575);
            this.generateGraphGroupBox.Name = "generateGraphGroupBox";
            this.generateGraphGroupBox.Size = new System.Drawing.Size(265, 149);
            this.generateGraphGroupBox.TabIndex = 7;
            this.generateGraphGroupBox.TabStop = false;
            this.generateGraphGroupBox.Text = "Generate graph";
            // 
            // graphDensityGenerateGraphComboBox
            // 
            this.graphDensityGenerateGraphComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.graphDensityGenerateGraphComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.graphDensityGenerateGraphComboBox.FormattingEnabled = true;
            this.graphDensityGenerateGraphComboBox.Location = new System.Drawing.Point(102, 51);
            this.graphDensityGenerateGraphComboBox.Name = "graphDensityGenerateGraphComboBox";
            this.graphDensityGenerateGraphComboBox.Size = new System.Drawing.Size(156, 23);
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
            this.countOfVerticesGenerateGraphLabelNumericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.countOfVerticesGenerateGraphLabelNumericUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.countOfVerticesGenerateGraphLabelNumericUpDown.Location = new System.Drawing.Point(188, 23);
            this.countOfVerticesGenerateGraphLabelNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.countOfVerticesGenerateGraphLabelNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.countOfVerticesGenerateGraphLabelNumericUpDown.Name = "countOfVerticesGenerateGraphLabelNumericUpDown";
            this.countOfVerticesGenerateGraphLabelNumericUpDown.Size = new System.Drawing.Size(70, 21);
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
            this.generateGraphButton.Location = new System.Drawing.Point(6, 82);
            this.generateGraphButton.Name = "generateGraphButton";
            this.generateGraphButton.Size = new System.Drawing.Size(252, 57);
            this.generateGraphButton.TabIndex = 5;
            this.generateGraphButton.Text = "Generate graph";
            this.generateGraphButton.UseVisualStyleBackColor = true;
            this.generateGraphButton.Click += new System.EventHandler(this.generateGraphButton_Click);
            // 
            // graphPropertiesGroupBox
            // 
            this.graphPropertiesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphPropertiesGroupBox.BackColor = System.Drawing.SystemColors.Control;
            this.graphPropertiesGroupBox.Controls.Add(this.SimplicialVertexValueGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.simplicialVertexGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.isChordalValueGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.isChordalGraphPropertiesLabel);
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
            this.graphPropertiesGroupBox.Controls.Add(this.girthValueGraphPropertiesLabel);
            this.graphPropertiesGroupBox.Controls.Add(this.girthGraphPropertiesLabel);
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
            this.graphPropertiesGroupBox.ForeColor = System.Drawing.Color.Black;
            this.graphPropertiesGroupBox.Location = new System.Drawing.Point(3, 3);
            this.graphPropertiesGroupBox.Name = "graphPropertiesGroupBox";
            this.graphPropertiesGroupBox.Size = new System.Drawing.Size(264, 642);
            this.graphPropertiesGroupBox.TabIndex = 6;
            this.graphPropertiesGroupBox.TabStop = false;
            this.graphPropertiesGroupBox.Text = "Graph property";
            // 
            // SimplicialVertexValueGraphPropertiesLabel
            // 
            this.SimplicialVertexValueGraphPropertiesLabel.AutoSize = true;
            this.SimplicialVertexValueGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.SimplicialVertexValueGraphPropertiesLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.SimplicialVertexValueGraphPropertiesLabel.Location = new System.Drawing.Point(126, 515);
            this.SimplicialVertexValueGraphPropertiesLabel.Name = "SimplicialVertexValueGraphPropertiesLabel";
            this.SimplicialVertexValueGraphPropertiesLabel.Size = new System.Drawing.Size(29, 16);
            this.SimplicialVertexValueGraphPropertiesLabel.TabIndex = 38;
            this.SimplicialVertexValueGraphPropertiesLabel.Text = "       ";
            // 
            // simplicialVertexGraphPropertiesLabel
            // 
            this.simplicialVertexGraphPropertiesLabel.AutoSize = true;
            this.simplicialVertexGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.simplicialVertexGraphPropertiesLabel.Location = new System.Drawing.Point(10, 515);
            this.simplicialVertexGraphPropertiesLabel.Name = "simplicialVertexGraphPropertiesLabel";
            this.simplicialVertexGraphPropertiesLabel.Size = new System.Drawing.Size(111, 16);
            this.simplicialVertexGraphPropertiesLabel.TabIndex = 37;
            this.simplicialVertexGraphPropertiesLabel.Text = "Simplicial vertex: ";
            this.simplicialVertexGraphPropertiesLabel.Click += new System.EventHandler(this.isChordalGraphPropertiesLabel_Click);
            // 
            // isChordalValueGraphPropertiesLabel
            // 
            this.isChordalValueGraphPropertiesLabel.AutoSize = true;
            this.isChordalValueGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.isChordalValueGraphPropertiesLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.isChordalValueGraphPropertiesLabel.Location = new System.Drawing.Point(81, 215);
            this.isChordalValueGraphPropertiesLabel.Name = "isChordalValueGraphPropertiesLabel";
            this.isChordalValueGraphPropertiesLabel.Size = new System.Drawing.Size(29, 16);
            this.isChordalValueGraphPropertiesLabel.TabIndex = 36;
            this.isChordalValueGraphPropertiesLabel.Text = "       ";
            // 
            // isChordalGraphPropertiesLabel
            // 
            this.isChordalGraphPropertiesLabel.AutoSize = true;
            this.isChordalGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.isChordalGraphPropertiesLabel.Location = new System.Drawing.Point(10, 215);
            this.isChordalGraphPropertiesLabel.Name = "isChordalGraphPropertiesLabel";
            this.isChordalGraphPropertiesLabel.Size = new System.Drawing.Size(72, 16);
            this.isChordalGraphPropertiesLabel.TabIndex = 35;
            this.isChordalGraphPropertiesLabel.Text = "Is chordal: ";
            this.isChordalGraphPropertiesLabel.Click += new System.EventHandler(this.isChordalGraphPropertiesLabel_Click);
            // 
            // getGraphPropertiesButton
            // 
            this.getGraphPropertiesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.getGraphPropertiesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.getGraphPropertiesButton.ForeColor = System.Drawing.Color.DarkRed;
            this.getGraphPropertiesButton.Location = new System.Drawing.Point(6, 579);
            this.getGraphPropertiesButton.Name = "getGraphPropertiesButton";
            this.getGraphPropertiesButton.Size = new System.Drawing.Size(252, 57);
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
            this.classValuePropertiesLabel.Location = new System.Drawing.Point(53, 185);
            this.classValuePropertiesLabel.Name = "classValuePropertiesLabel";
            this.classValuePropertiesLabel.Size = new System.Drawing.Size(29, 16);
            this.classValuePropertiesLabel.TabIndex = 33;
            this.classValuePropertiesLabel.Text = "       ";
            // 
            // classPropertiesLabel
            // 
            this.classPropertiesLabel.AutoSize = true;
            this.classPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.classPropertiesLabel.Location = new System.Drawing.Point(10, 185);
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
            this.countOfUsedColorsValueGraphPropertiesLabel.Location = new System.Drawing.Point(165, 553);
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
            this.countOfUsedColorsGraphPropertiesLabel.Location = new System.Drawing.Point(10, 553);
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
            this.isEulerianValueGraphPropertiesLabel.Location = new System.Drawing.Point(84, 290);
            this.isEulerianValueGraphPropertiesLabel.Name = "isEulerianValueGraphPropertiesLabel";
            this.isEulerianValueGraphPropertiesLabel.Size = new System.Drawing.Size(29, 16);
            this.isEulerianValueGraphPropertiesLabel.TabIndex = 29;
            this.isEulerianValueGraphPropertiesLabel.Text = "       ";
            // 
            // isEulerianGraphPropertiesLabel
            // 
            this.isEulerianGraphPropertiesLabel.AutoSize = true;
            this.isEulerianGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.isEulerianGraphPropertiesLabel.Location = new System.Drawing.Point(10, 290);
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
            this.cayleysFormulaValueGraphPropertiesLabel.Location = new System.Drawing.Point(116, 485);
            this.cayleysFormulaValueGraphPropertiesLabel.Name = "cayleysFormulaValueGraphPropertiesLabel";
            this.cayleysFormulaValueGraphPropertiesLabel.Size = new System.Drawing.Size(29, 16);
            this.cayleysFormulaValueGraphPropertiesLabel.TabIndex = 27;
            this.cayleysFormulaValueGraphPropertiesLabel.Text = "       ";
            // 
            // cayleysFormulaGraphPropertiesLabel
            // 
            this.cayleysFormulaGraphPropertiesLabel.AutoSize = true;
            this.cayleysFormulaGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cayleysFormulaGraphPropertiesLabel.Location = new System.Drawing.Point(10, 485);
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
            this.isCyclicValueGraphPropertiesLabel.Location = new System.Drawing.Point(66, 265);
            this.isCyclicValueGraphPropertiesLabel.Name = "isCyclicValueGraphPropertiesLabel";
            this.isCyclicValueGraphPropertiesLabel.Size = new System.Drawing.Size(29, 16);
            this.isCyclicValueGraphPropertiesLabel.TabIndex = 23;
            this.isCyclicValueGraphPropertiesLabel.Text = "       ";
            // 
            // isCyclicGraphPropertiesLabel
            // 
            this.isCyclicGraphPropertiesLabel.AutoSize = true;
            this.isCyclicGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.isCyclicGraphPropertiesLabel.Location = new System.Drawing.Point(10, 265);
            this.isCyclicGraphPropertiesLabel.Name = "isCyclicGraphPropertiesLabel";
            this.isCyclicGraphPropertiesLabel.Size = new System.Drawing.Size(61, 16);
            this.isCyclicGraphPropertiesLabel.TabIndex = 22;
            this.isCyclicGraphPropertiesLabel.Text = "Is cyclic: ";
            this.isCyclicGraphPropertiesLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.isCyclicGraphPropertiesLabel_Click);
            // 
            // girthValueGraphPropertiesLabel
            // 
            this.girthValueGraphPropertiesLabel.AutoSize = true;
            this.girthValueGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.girthValueGraphPropertiesLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.girthValueGraphPropertiesLabel.Location = new System.Drawing.Point(47, 455);
            this.girthValueGraphPropertiesLabel.Name = "girthValueGraphPropertiesLabel";
            this.girthValueGraphPropertiesLabel.Size = new System.Drawing.Size(29, 16);
            this.girthValueGraphPropertiesLabel.TabIndex = 21;
            this.girthValueGraphPropertiesLabel.Text = "       ";
            // 
            // girthGraphPropertiesLabel
            // 
            this.girthGraphPropertiesLabel.AutoSize = true;
            this.girthGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.girthGraphPropertiesLabel.Location = new System.Drawing.Point(10, 455);
            this.girthGraphPropertiesLabel.Name = "girthGraphPropertiesLabel";
            this.girthGraphPropertiesLabel.Size = new System.Drawing.Size(41, 16);
            this.girthGraphPropertiesLabel.TabIndex = 20;
            this.girthGraphPropertiesLabel.Text = "Girth: ";
            this.girthGraphPropertiesLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.girthGraphPropertiesLabel_Click);
            // 
            // countBridgesValueGraphPropertiesLabel
            // 
            this.countBridgesValueGraphPropertiesLabel.AutoSize = true;
            this.countBridgesValueGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.countBridgesValueGraphPropertiesLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.countBridgesValueGraphPropertiesLabel.Location = new System.Drawing.Point(117, 425);
            this.countBridgesValueGraphPropertiesLabel.Name = "countBridgesValueGraphPropertiesLabel";
            this.countBridgesValueGraphPropertiesLabel.Size = new System.Drawing.Size(29, 16);
            this.countBridgesValueGraphPropertiesLabel.TabIndex = 19;
            this.countBridgesValueGraphPropertiesLabel.Text = "       ";
            // 
            // countBridgesGraphPropertiesLabel
            // 
            this.countBridgesGraphPropertiesLabel.AutoSize = true;
            this.countBridgesGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.countBridgesGraphPropertiesLabel.Location = new System.Drawing.Point(10, 425);
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
            this.countCutVerticesValueGraphPropertiesLabel.Location = new System.Drawing.Point(138, 400);
            this.countCutVerticesValueGraphPropertiesLabel.Name = "countCutVerticesValueGraphPropertiesLabel";
            this.countCutVerticesValueGraphPropertiesLabel.Size = new System.Drawing.Size(29, 16);
            this.countCutVerticesValueGraphPropertiesLabel.TabIndex = 17;
            this.countCutVerticesValueGraphPropertiesLabel.Text = "       ";
            // 
            // countCutVerticesGraphPropertiesLabel
            // 
            this.countCutVerticesGraphPropertiesLabel.AutoSize = true;
            this.countCutVerticesGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.countCutVerticesGraphPropertiesLabel.Location = new System.Drawing.Point(10, 400);
            this.countCutVerticesGraphPropertiesLabel.Name = "countCutVerticesGraphPropertiesLabel";
            this.countCutVerticesGraphPropertiesLabel.Size = new System.Drawing.Size(132, 16);
            this.countCutVerticesGraphPropertiesLabel.TabIndex = 16;
            this.countCutVerticesGraphPropertiesLabel.Text = "Count of cut vertices: ";
            this.countCutVerticesGraphPropertiesLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.countBridgesCutVerticesGraphPropertiesLabel_Click);
            // 
            // averageVertexDegreeValueGraphPropertiesLabel
            // 
            this.averageVertexDegreeValueGraphPropertiesLabel.AutoSize = true;
            this.averageVertexDegreeValueGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.averageVertexDegreeValueGraphPropertiesLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.averageVertexDegreeValueGraphPropertiesLabel.Location = new System.Drawing.Point(158, 370);
            this.averageVertexDegreeValueGraphPropertiesLabel.Name = "averageVertexDegreeValueGraphPropertiesLabel";
            this.averageVertexDegreeValueGraphPropertiesLabel.Size = new System.Drawing.Size(28, 13);
            this.averageVertexDegreeValueGraphPropertiesLabel.TabIndex = 15;
            this.averageVertexDegreeValueGraphPropertiesLabel.Text = "       ";
            // 
            // averageVertexDegreeGraphPropertiesLabel
            // 
            this.averageVertexDegreeGraphPropertiesLabel.AutoSize = true;
            this.averageVertexDegreeGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.averageVertexDegreeGraphPropertiesLabel.Location = new System.Drawing.Point(10, 370);
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
            this.minimumVertexDegreeValueGraphPropertiesLabel.Location = new System.Drawing.Point(158, 345);
            this.minimumVertexDegreeValueGraphPropertiesLabel.Name = "minimumVertexDegreeValueGraphPropertiesLabel";
            this.minimumVertexDegreeValueGraphPropertiesLabel.Size = new System.Drawing.Size(29, 16);
            this.minimumVertexDegreeValueGraphPropertiesLabel.TabIndex = 13;
            this.minimumVertexDegreeValueGraphPropertiesLabel.Text = "       ";
            // 
            // minimumVertexDegreeGraphPropertiesLabel
            // 
            this.minimumVertexDegreeGraphPropertiesLabel.AutoSize = true;
            this.minimumVertexDegreeGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.minimumVertexDegreeGraphPropertiesLabel.Location = new System.Drawing.Point(10, 345);
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
            this.maximumVertexDegreeValueGraphPropertiesLabel.Location = new System.Drawing.Point(162, 320);
            this.maximumVertexDegreeValueGraphPropertiesLabel.Name = "maximumVertexDegreeValueGraphPropertiesLabel";
            this.maximumVertexDegreeValueGraphPropertiesLabel.Size = new System.Drawing.Size(29, 16);
            this.maximumVertexDegreeValueGraphPropertiesLabel.TabIndex = 11;
            this.maximumVertexDegreeValueGraphPropertiesLabel.Text = "       ";
            // 
            // maximumVertexDegreeGraphPropertiesLabel
            // 
            this.maximumVertexDegreeGraphPropertiesLabel.AutoSize = true;
            this.maximumVertexDegreeGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.maximumVertexDegreeGraphPropertiesLabel.Location = new System.Drawing.Point(10, 320);
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
            this.isRegularValueGraphPropertiesLabel.Location = new System.Drawing.Point(74, 240);
            this.isRegularValueGraphPropertiesLabel.Name = "isRegularValueGraphPropertiesLabel";
            this.isRegularValueGraphPropertiesLabel.Size = new System.Drawing.Size(29, 16);
            this.isRegularValueGraphPropertiesLabel.TabIndex = 9;
            this.isRegularValueGraphPropertiesLabel.Text = "       ";
            // 
            // isRegularGraphPropertiesLabel
            // 
            this.isRegularGraphPropertiesLabel.AutoSize = true;
            this.isRegularGraphPropertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.isRegularGraphPropertiesLabel.Location = new System.Drawing.Point(10, 240);
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
            // graphModificationVertexGroupBox
            // 
            this.graphModificationVertexGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphModificationVertexGroupBox.Controls.Add(this.newVertexNameGraphModificationVertexLabel);
            this.graphModificationVertexGroupBox.Controls.Add(this.newVertexNameGraphModificationVertexTextBox);
            this.graphModificationVertexGroupBox.Controls.Add(this.changeVertexNameGraphModificationVertexButton);
            this.graphModificationVertexGroupBox.Controls.Add(this.vertexExpansionGraphModificationVertexButton);
            this.graphModificationVertexGroupBox.Controls.Add(this.vertexSuppressionGraphModificationVertexButton);
            this.graphModificationVertexGroupBox.Controls.Add(this.vertexContractionGraphModificationVertexButton);
            this.graphModificationVertexGroupBox.Controls.Add(this.vertexDeleteGraphModificationVertexButton);
            this.graphModificationVertexGroupBox.Controls.Add(this.vertexAddGraphModificationVertexButton);
            this.graphModificationVertexGroupBox.Controls.Add(this.vertexNameGraphModificationVertexLabel);
            this.graphModificationVertexGroupBox.Controls.Add(this.vertexNameGraphModificationVertexTextBox);
            this.graphModificationVertexGroupBox.Location = new System.Drawing.Point(5, 20);
            this.graphModificationVertexGroupBox.Name = "graphModificationVertexGroupBox";
            this.graphModificationVertexGroupBox.Size = new System.Drawing.Size(215, 429);
            this.graphModificationVertexGroupBox.TabIndex = 8;
            this.graphModificationVertexGroupBox.TabStop = false;
            this.graphModificationVertexGroupBox.Text = "Vertex";
            // 
            // newVertexNameGraphModificationVertexLabel
            // 
            this.newVertexNameGraphModificationVertexLabel.AutoSize = true;
            this.newVertexNameGraphModificationVertexLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.newVertexNameGraphModificationVertexLabel.Location = new System.Drawing.Point(7, 364);
            this.newVertexNameGraphModificationVertexLabel.Name = "newVertexNameGraphModificationVertexLabel";
            this.newVertexNameGraphModificationVertexLabel.Size = new System.Drawing.Size(117, 16);
            this.newVertexNameGraphModificationVertexLabel.TabIndex = 42;
            this.newVertexNameGraphModificationVertexLabel.Text = "New vertex name: ";
            // 
            // newVertexNameGraphModificationVertexTextBox
            // 
            this.newVertexNameGraphModificationVertexTextBox.Location = new System.Drawing.Point(130, 363);
            this.newVertexNameGraphModificationVertexTextBox.Name = "newVertexNameGraphModificationVertexTextBox";
            this.newVertexNameGraphModificationVertexTextBox.Size = new System.Drawing.Size(77, 20);
            this.newVertexNameGraphModificationVertexTextBox.TabIndex = 41;
            // 
            // changeVertexNameGraphModificationVertexButton
            // 
            this.changeVertexNameGraphModificationVertexButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.changeVertexNameGraphModificationVertexButton.ForeColor = System.Drawing.Color.DarkRed;
            this.changeVertexNameGraphModificationVertexButton.Location = new System.Drawing.Point(6, 386);
            this.changeVertexNameGraphModificationVertexButton.Name = "changeVertexNameGraphModificationVertexButton";
            this.changeVertexNameGraphModificationVertexButton.Size = new System.Drawing.Size(201, 35);
            this.changeVertexNameGraphModificationVertexButton.TabIndex = 40;
            this.changeVertexNameGraphModificationVertexButton.Text = "Change vertex name";
            this.changeVertexNameGraphModificationVertexButton.UseVisualStyleBackColor = true;
            this.changeVertexNameGraphModificationVertexButton.Click += new System.EventHandler(this.changeVertexNameGraphModificationVertexButton_Click);
            // 
            // vertexExpansionGraphModificationVertexButton
            // 
            this.vertexExpansionGraphModificationVertexButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.vertexExpansionGraphModificationVertexButton.ForeColor = System.Drawing.Color.DarkRed;
            this.vertexExpansionGraphModificationVertexButton.Location = new System.Drawing.Point(6, 297);
            this.vertexExpansionGraphModificationVertexButton.Name = "vertexExpansionGraphModificationVertexButton";
            this.vertexExpansionGraphModificationVertexButton.Size = new System.Drawing.Size(201, 57);
            this.vertexExpansionGraphModificationVertexButton.TabIndex = 39;
            this.vertexExpansionGraphModificationVertexButton.Text = "Vertex expansion";
            this.vertexExpansionGraphModificationVertexButton.UseVisualStyleBackColor = true;
            this.vertexExpansionGraphModificationVertexButton.Click += new System.EventHandler(this.vertexExpansionGraphModificationVertexButton_Click);
            // 
            // vertexSuppressionGraphModificationVertexButton
            // 
            this.vertexSuppressionGraphModificationVertexButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.vertexSuppressionGraphModificationVertexButton.ForeColor = System.Drawing.Color.DarkRed;
            this.vertexSuppressionGraphModificationVertexButton.Location = new System.Drawing.Point(6, 234);
            this.vertexSuppressionGraphModificationVertexButton.Name = "vertexSuppressionGraphModificationVertexButton";
            this.vertexSuppressionGraphModificationVertexButton.Size = new System.Drawing.Size(201, 57);
            this.vertexSuppressionGraphModificationVertexButton.TabIndex = 38;
            this.vertexSuppressionGraphModificationVertexButton.Text = "Vertex suppression";
            this.vertexSuppressionGraphModificationVertexButton.UseVisualStyleBackColor = true;
            this.vertexSuppressionGraphModificationVertexButton.Click += new System.EventHandler(this.vertexSuppressionGraphModificationVertexButton_Click);
            // 
            // vertexContractionGraphModificationVertexButton
            // 
            this.vertexContractionGraphModificationVertexButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.vertexContractionGraphModificationVertexButton.ForeColor = System.Drawing.Color.DarkRed;
            this.vertexContractionGraphModificationVertexButton.Location = new System.Drawing.Point(6, 171);
            this.vertexContractionGraphModificationVertexButton.Name = "vertexContractionGraphModificationVertexButton";
            this.vertexContractionGraphModificationVertexButton.Size = new System.Drawing.Size(201, 57);
            this.vertexContractionGraphModificationVertexButton.TabIndex = 37;
            this.vertexContractionGraphModificationVertexButton.Text = "Vertex contraction";
            this.vertexContractionGraphModificationVertexButton.UseVisualStyleBackColor = true;
            this.vertexContractionGraphModificationVertexButton.Click += new System.EventHandler(this.vertexContractionGraphModificationVertexButton_Click);
            // 
            // vertexDeleteGraphModificationVertexButton
            // 
            this.vertexDeleteGraphModificationVertexButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.vertexDeleteGraphModificationVertexButton.ForeColor = System.Drawing.Color.DarkRed;
            this.vertexDeleteGraphModificationVertexButton.Location = new System.Drawing.Point(6, 108);
            this.vertexDeleteGraphModificationVertexButton.Name = "vertexDeleteGraphModificationVertexButton";
            this.vertexDeleteGraphModificationVertexButton.Size = new System.Drawing.Size(201, 57);
            this.vertexDeleteGraphModificationVertexButton.TabIndex = 36;
            this.vertexDeleteGraphModificationVertexButton.Text = "Remove vertex";
            this.vertexDeleteGraphModificationVertexButton.UseVisualStyleBackColor = true;
            this.vertexDeleteGraphModificationVertexButton.Click += new System.EventHandler(this.vertexDeleteGraphModificationVertexButton_Click);
            // 
            // vertexAddGraphModificationVertexButton
            // 
            this.vertexAddGraphModificationVertexButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.vertexAddGraphModificationVertexButton.ForeColor = System.Drawing.Color.DarkRed;
            this.vertexAddGraphModificationVertexButton.Location = new System.Drawing.Point(6, 45);
            this.vertexAddGraphModificationVertexButton.Name = "vertexAddGraphModificationVertexButton";
            this.vertexAddGraphModificationVertexButton.Size = new System.Drawing.Size(201, 57);
            this.vertexAddGraphModificationVertexButton.TabIndex = 35;
            this.vertexAddGraphModificationVertexButton.Text = "Add vertex";
            this.vertexAddGraphModificationVertexButton.UseVisualStyleBackColor = true;
            this.vertexAddGraphModificationVertexButton.Click += new System.EventHandler(this.vertexAddGraphModificationVertexButton_Click);
            // 
            // vertexNameGraphModificationVertexLabel
            // 
            this.vertexNameGraphModificationVertexLabel.AutoSize = true;
            this.vertexNameGraphModificationVertexLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.vertexNameGraphModificationVertexLabel.Location = new System.Drawing.Point(6, 20);
            this.vertexNameGraphModificationVertexLabel.Name = "vertexNameGraphModificationVertexLabel";
            this.vertexNameGraphModificationVertexLabel.Size = new System.Drawing.Size(89, 16);
            this.vertexNameGraphModificationVertexLabel.TabIndex = 1;
            this.vertexNameGraphModificationVertexLabel.Text = "Vertex name: ";
            // 
            // vertexNameGraphModificationVertexTextBox
            // 
            this.vertexNameGraphModificationVertexTextBox.Location = new System.Drawing.Point(108, 19);
            this.vertexNameGraphModificationVertexTextBox.Name = "vertexNameGraphModificationVertexTextBox";
            this.vertexNameGraphModificationVertexTextBox.Size = new System.Drawing.Size(100, 20);
            this.vertexNameGraphModificationVertexTextBox.TabIndex = 0;
            // 
            // graphModificationGroupBox
            // 
            this.graphModificationGroupBox.Controls.Add(this.graphModificationEdgeGroupBox);
            this.graphModificationGroupBox.Controls.Add(this.graphModificationVertexGroupBox);
            this.graphModificationGroupBox.Location = new System.Drawing.Point(6, 3);
            this.graphModificationGroupBox.Name = "graphModificationGroupBox";
            this.graphModificationGroupBox.Size = new System.Drawing.Size(225, 797);
            this.graphModificationGroupBox.TabIndex = 8;
            this.graphModificationGroupBox.TabStop = false;
            this.graphModificationGroupBox.Text = "Graph modification";
            // 
            // firstColumnPanel
            // 
            this.firstColumnPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.firstColumnPanel.AutoScroll = true;
            this.firstColumnPanel.Controls.Add(this.graphColoringGroupBox);
            this.firstColumnPanel.Controls.Add(this.generateGraphGroupBox);
            this.firstColumnPanel.Location = new System.Drawing.Point(1305, 88);
            this.firstColumnPanel.Name = "firstColumnPanel";
            this.firstColumnPanel.Size = new System.Drawing.Size(271, 727);
            this.firstColumnPanel.TabIndex = 1;
            // 
            // secondColumnPanel
            // 
            this.secondColumnPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.secondColumnPanel.AutoScroll = true;
            this.secondColumnPanel.Controls.Add(this.graphPropertiesGroupBox);
            this.secondColumnPanel.Controls.Add(this.graphOperationGroupBox);
            this.secondColumnPanel.Location = new System.Drawing.Point(1029, 12);
            this.secondColumnPanel.Name = "secondColumnPanel";
            this.secondColumnPanel.Size = new System.Drawing.Size(270, 803);
            this.secondColumnPanel.TabIndex = 9;
            // 
            // thirdColumnPanel
            // 
            this.thirdColumnPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.thirdColumnPanel.AutoScroll = true;
            this.thirdColumnPanel.Controls.Add(this.graphModificationGroupBox);
            this.thirdColumnPanel.Location = new System.Drawing.Point(789, 12);
            this.thirdColumnPanel.Name = "thirdColumnPanel";
            this.thirdColumnPanel.Size = new System.Drawing.Size(234, 803);
            this.thirdColumnPanel.TabIndex = 10;
            // 
            // ColoringProgressTimer
            // 
            this.ColoringProgressTimer.Interval = 500;
            this.ColoringProgressTimer.Tick += new System.EventHandler(this.ColoringProgressTimer_Tick);
            // 
            // GraphColoringForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1584, 841);
            this.Controls.Add(this.thirdColumnPanel);
            this.Controls.Add(this.secondColumnPanel);
            this.Controls.Add(this.firstColumnPanel);
            this.Controls.Add(this.drawGraphPanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.titleLabel);
            this.MinimumSize = new System.Drawing.Size(1366, 880);
            this.Name = "GraphColoringForm";
            this.Text = "Graph coloring | Illner";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.GraphColoringForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.GraphColoringForm_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.drawGraphPictureBox)).EndInit();
            this.graphColoringGroupBox.ResumeLayout(false);
            this.graphColoringGroupBox.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.drawGraphPanel.ResumeLayout(false);
            this.drawGraphPanel.PerformLayout();
            this.graphModificationEdgeGroupBox.ResumeLayout(false);
            this.graphModificationEdgeGroupBox.PerformLayout();
            this.graphOperationGroupBox.ResumeLayout(false);
            this.generateGraphGroupBox.ResumeLayout(false);
            this.generateGraphGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.countOfVerticesGenerateGraphLabelNumericUpDown)).EndInit();
            this.graphPropertiesGroupBox.ResumeLayout(false);
            this.graphPropertiesGroupBox.PerformLayout();
            this.graphModificationVertexGroupBox.ResumeLayout(false);
            this.graphModificationVertexGroupBox.PerformLayout();
            this.graphModificationGroupBox.ResumeLayout(false);
            this.firstColumnPanel.ResumeLayout(false);
            this.secondColumnPanel.ResumeLayout(false);
            this.thirdColumnPanel.ResumeLayout(false);
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
        private System.Windows.Forms.Label girthValueGraphPropertiesLabel;
        private System.Windows.Forms.Label girthGraphPropertiesLabel;
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
        private System.Windows.Forms.GroupBox graphModificationVertexGroupBox;
        private System.Windows.Forms.Button vertexAddGraphModificationVertexButton;
        private System.Windows.Forms.Label vertexNameGraphModificationVertexLabel;
        private System.Windows.Forms.TextBox vertexNameGraphModificationVertexTextBox;
        private System.Windows.Forms.Button vertexDeleteGraphModificationVertexButton;
        private System.Windows.Forms.Button vertexContractionGraphModificationVertexButton;
        private System.Windows.Forms.Button vertexSuppressionGraphModificationVertexButton;
        private System.Windows.Forms.Button vertexExpansionGraphModificationVertexButton;
        private System.Windows.Forms.GroupBox graphModificationEdgeGroupBox;
        private System.Windows.Forms.Label secondVertexNameGraphModificationEdgeLabel;
        private System.Windows.Forms.TextBox secondVertexNameGraphModificationEdgeTextBox;
        private System.Windows.Forms.Label firstVertexNameGraphModificationEdgeLabel;
        private System.Windows.Forms.TextBox firstVertexNameGraphModificationEdgeTextBox;
        private System.Windows.Forms.Button edgeAddGraphModificationEdgeButton;
        private System.Windows.Forms.Button edgeDeleteGraphModificationEdgeButton;
        private System.Windows.Forms.Button edgeContractionGraphModificationEdgeButton;
        private System.Windows.Forms.Button edgeSubdivisionGraphModificationEdgeButton;
        private System.Windows.Forms.GroupBox graphModificationGroupBox;
        private System.Windows.Forms.GroupBox graphOperationGroupBox;
        private System.Windows.Forms.Button lineGraphGraphOperationButton;
        private System.Windows.Forms.Button complementGraphGraphOperationButton;
        private System.Windows.Forms.Panel firstColumnPanel;
        private System.Windows.Forms.Panel secondColumnPanel;
        private System.Windows.Forms.Panel thirdColumnPanel;
        private System.Windows.Forms.Button newGraphButton;
        private System.Windows.Forms.Button changeVertexNameGraphModificationVertexButton;
        private System.Windows.Forms.CheckBox scheduleAppearanceCheckBox;
        private System.Windows.Forms.Label isChordalValueGraphPropertiesLabel;
        private System.Windows.Forms.Label isChordalGraphPropertiesLabel;
        private System.Windows.Forms.Label SimplicialVertexValueGraphPropertiesLabel;
        private System.Windows.Forms.Label simplicialVertexGraphPropertiesLabel;
        private System.Windows.Forms.Label newVertexNameGraphModificationVertexLabel;
        private System.Windows.Forms.TextBox newVertexNameGraphModificationVertexTextBox;
        private System.Windows.Forms.ComboBox namedGraphsComboBox;
        private System.Windows.Forms.CheckBox showCutVerticesAndBridgesCheckBox;
        private System.Windows.Forms.CheckBox showSimplicialVertexCheckBox;
        private System.Windows.Forms.CheckBox showSpanningTreeCheckBox;
        private System.Windows.Forms.CheckBox showMaximumAndMinimumDegreeVerticesCheckBox;
        private System.Windows.Forms.ToolStripProgressBar ColoringProgressProgressBar;
        private System.Windows.Forms.Timer ColoringProgressTimer;
    }
}