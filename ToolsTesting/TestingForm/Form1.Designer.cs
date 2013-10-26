namespace TestingForm
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.glControl1 = new OpenTK.GLControl();
            this.tbConsole = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.slmouseCartesian = new System.Windows.Forms.ToolStripStatusLabel();
            this.slZoom = new System.Windows.Forms.ToolStripStatusLabel();
            this.slFPS = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.slLimits = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.bDrag = new System.Windows.Forms.ToolStripButton();
            this.bZoomWindow = new System.Windows.Forms.ToolStripButton();
            this.bZoomExtent = new System.Windows.Forms.ToolStripButton();
            this.bZoomIn = new System.Windows.Forms.ToolStripButton();
            this.bZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.colorToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.lineToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.LeftToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // glControl1
            // 
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControl1.Location = new System.Drawing.Point(3, 3);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(653, 368);
            this.glControl1.TabIndex = 0;
            this.glControl1.VSync = true;
            this.glControl1.Load += new System.EventHandler(this.glControl1_Load);
            this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
            this.glControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseClick);
            this.glControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseDown);
            this.glControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseMove);
            this.glControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseUp);
            this.glControl1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseScroll);
            this.glControl1.Resize += new System.EventHandler(this.glControl1_Resize);
            // 
            // tbConsole
            // 
            this.tbConsole.BackColor = System.Drawing.SystemColors.MenuText;
            this.tbConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbConsole.ForeColor = System.Drawing.SystemColors.Window;
            this.tbConsole.Location = new System.Drawing.Point(3, 377);
            this.tbConsole.Multiline = true;
            this.tbConsole.Name = "tbConsole";
            this.tbConsole.ReadOnly = true;
            this.tbConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbConsole.Size = new System.Drawing.Size(653, 94);
            this.tbConsole.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.glControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbConsole, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(635, 474);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slmouseCartesian,
            this.slZoom,
            this.slFPS,
            this.toolStripStatusLabel1,
            this.slLimits});
            this.statusStrip1.Location = new System.Drawing.Point(0, 499);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(687, 24);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // slmouseCartesian
            // 
            this.slmouseCartesian.AutoSize = false;
            this.slmouseCartesian.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.slmouseCartesian.Name = "slmouseCartesian";
            this.slmouseCartesian.Size = new System.Drawing.Size(170, 19);
            this.slmouseCartesian.Text = "0,0";
            // 
            // slZoom
            // 
            this.slZoom.AutoSize = false;
            this.slZoom.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.slZoom.Name = "slZoom";
            this.slZoom.Size = new System.Drawing.Size(100, 19);
            this.slZoom.Text = "Zoom";
            // 
            // slFPS
            // 
            this.slFPS.AutoSize = false;
            this.slFPS.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.slFPS.Name = "slFPS";
            this.slFPS.Size = new System.Drawing.Size(50, 19);
            this.slFPS.Text = "FPS";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(52, 19);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = " ";
            // 
            // slLimits
            // 
            this.slLimits.AutoSize = false;
            this.slLimits.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.slLimits.Name = "slLimits";
            this.slLimits.Size = new System.Drawing.Size(300, 19);
            this.slLimits.Text = "( , )( , )";
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.tableLayoutPanel1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(635, 474);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // toolStripContainer1.LeftToolStripPanel
            // 
            this.toolStripContainer1.LeftToolStripPanel.Controls.Add(this.toolStrip1);
            this.toolStripContainer1.LeftToolStripPanel.Controls.Add(this.toolStrip2);
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(687, 499);
            this.toolStripContainer1.TabIndex = 4;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bDrag,
            this.bZoomWindow,
            this.bZoomExtent,
            this.bZoomIn,
            this.bZoomOut});
            this.toolStrip1.Location = new System.Drawing.Point(0, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(28, 121);
            this.toolStrip1.TabIndex = 0;
            // 
            // bDrag
            // 
            this.bDrag.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bDrag.Image = ((System.Drawing.Image)(resources.GetObject("bDrag.Image")));
            this.bDrag.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bDrag.Name = "bDrag";
            this.bDrag.Size = new System.Drawing.Size(26, 19);
            this.bDrag.Text = "D";
            this.bDrag.ToolTipText = "Drag by Mouse";
            this.bDrag.Click += new System.EventHandler(this.bDrag_Click);
            // 
            // bZoomWindow
            // 
            this.bZoomWindow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bZoomWindow.Image = ((System.Drawing.Image)(resources.GetObject("bZoomWindow.Image")));
            this.bZoomWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bZoomWindow.Name = "bZoomWindow";
            this.bZoomWindow.Size = new System.Drawing.Size(26, 19);
            this.bZoomWindow.Text = "Zw";
            this.bZoomWindow.ToolTipText = "Zoom by Window";
            this.bZoomWindow.Click += new System.EventHandler(this.bZoomWindow_Click);
            // 
            // bZoomExtent
            // 
            this.bZoomExtent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bZoomExtent.Image = ((System.Drawing.Image)(resources.GetObject("bZoomExtent.Image")));
            this.bZoomExtent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bZoomExtent.Name = "bZoomExtent";
            this.bZoomExtent.Size = new System.Drawing.Size(26, 19);
            this.bZoomExtent.Text = "Zx";
            this.bZoomExtent.ToolTipText = "Zoom to Extent";
            this.bZoomExtent.Click += new System.EventHandler(this.bZoomExtent_Click);
            // 
            // bZoomIn
            // 
            this.bZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("bZoomIn.Image")));
            this.bZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bZoomIn.Name = "bZoomIn";
            this.bZoomIn.Size = new System.Drawing.Size(26, 19);
            this.bZoomIn.Text = "Z+";
            this.bZoomIn.ToolTipText = "Zoom In";
            this.bZoomIn.Click += new System.EventHandler(this.bZoomIn_Click);
            // 
            // bZoomOut
            // 
            this.bZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("bZoomOut.Image")));
            this.bZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bZoomOut.Name = "bZoomOut";
            this.bZoomOut.Size = new System.Drawing.Size(26, 19);
            this.bZoomOut.Text = "Z-";
            this.bZoomOut.ToolTipText = "Zoom Out";
            this.bZoomOut.Click += new System.EventHandler(this.bZoomOut_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorToolStripButton,
            this.lineToolStripButton});
            this.toolStrip2.Location = new System.Drawing.Point(28, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(24, 57);
            this.toolStrip2.TabIndex = 1;
            // 
            // colorToolStripButton
            // 
            this.colorToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.colorToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("colorToolStripButton.Image")));
            this.colorToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.colorToolStripButton.Name = "colorToolStripButton";
            this.colorToolStripButton.Size = new System.Drawing.Size(22, 20);
            this.colorToolStripButton.Text = "Seleccionar Color";
            this.colorToolStripButton.Click += new System.EventHandler(this.ColorToolStripButtonClick);
            // 
            // lineToolStripButton
            // 
            this.lineToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.lineToolStripButton.Image = global::TestingForm.Properties.Resources.LineButton;
            this.lineToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.lineToolStripButton.Name = "lineToolStripButton";
            this.lineToolStripButton.Size = new System.Drawing.Size(30, 20);
            this.lineToolStripButton.Text = "Dibujar Linea";
            this.lineToolStripButton.Click += new System.EventHandler(this.lineToolStripButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 523);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form1";
            this.Text = "Basic CAD";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.LeftToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.LeftToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenTK.GLControl glControl1;
        private System.Windows.Forms.TextBox tbConsole;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStripStatusLabel slmouseCartesian;
        private System.Windows.Forms.ToolStripStatusLabel slZoom;
        private System.Windows.Forms.ToolStripStatusLabel slFPS;
        private System.Windows.Forms.ToolStripStatusLabel slLimits;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton bZoomWindow;
        private System.Windows.Forms.ToolStripButton bZoomExtent;
        private System.Windows.Forms.ToolStripButton bZoomIn;
        private System.Windows.Forms.ToolStripButton bZoomOut;
        private System.Windows.Forms.ToolStripButton bDrag;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton colorToolStripButton;
        private System.Windows.Forms.ToolStripButton lineToolStripButton;
    }
}

