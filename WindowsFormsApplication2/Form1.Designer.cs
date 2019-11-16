namespace WindowsFormsApplication2
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
            this.openGLControl = new SharpGL.OpenGLControl();
            this.bt_ColorTable = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.bt_line = new System.Windows.Forms.Button();
            this.bt_Exit = new System.Windows.Forms.Button();
            this.bt_Circle = new System.Windows.Forms.Button();
            this.bt_Rectangle = new System.Windows.Forms.Button();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.bt_Equilateral_Triangle = new System.Windows.Forms.Button();
            this.bt_Regular_Five_Polygon = new System.Windows.Forms.Button();
            this.bt_Regular_Six_Polygon = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // openGLControl
            // 
            this.openGLControl.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.openGLControl.DrawFPS = false;
            this.openGLControl.ForeColor = System.Drawing.Color.Coral;
            this.openGLControl.Location = new System.Drawing.Point(58, 78);
            this.openGLControl.Name = "openGLControl";
            this.openGLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.openGLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl.Size = new System.Drawing.Size(918, 316);
            this.openGLControl.TabIndex = 0;
            this.openGLControl.OpenGLInitialized += new System.EventHandler(this.openGLControl_OpenGLInitialized);
            this.openGLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.openGLControl_OpenGLDraw);
            this.openGLControl.Resized += new System.EventHandler(this.openGLControl_Resized);
            this.openGLControl.Load += new System.EventHandler(this.openGLControl_Load);
            this.openGLControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ctrl_OpenGLControl_MouseDown);
            this.openGLControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ctrl_openGLControl_MouseUp);
            // 
            // bt_ColorTable
            // 
            this.bt_ColorTable.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.bt_ColorTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_ColorTable.Location = new System.Drawing.Point(877, 12);
            this.bt_ColorTable.Name = "bt_ColorTable";
            this.bt_ColorTable.Size = new System.Drawing.Size(99, 46);
            this.bt_ColorTable.TabIndex = 2;
            this.bt_ColorTable.Text = "Color Table";
            this.bt_ColorTable.UseVisualStyleBackColor = false;
            this.bt_ColorTable.Click += new System.EventHandler(this.bt_ColorTable_Click);
            // 
            // bt_line
            // 
            this.bt_line.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.bt_line.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_line.Location = new System.Drawing.Point(58, 13);
            this.bt_line.Name = "bt_line";
            this.bt_line.Size = new System.Drawing.Size(75, 45);
            this.bt_line.TabIndex = 3;
            this.bt_line.Text = "Line";
            this.bt_line.UseVisualStyleBackColor = false;
            this.bt_line.Click += new System.EventHandler(this.bt_Line_Click);
            // 
            // bt_Exit
            // 
            this.bt_Exit.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.bt_Exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_Exit.Location = new System.Drawing.Point(763, 13);
            this.bt_Exit.Name = "bt_Exit";
            this.bt_Exit.Size = new System.Drawing.Size(75, 45);
            this.bt_Exit.TabIndex = 4;
            this.bt_Exit.Text = "Exit";
            this.bt_Exit.UseVisualStyleBackColor = false;
            this.bt_Exit.Click += new System.EventHandler(this.bt_Exit_Click);
            // 
            // bt_Circle
            // 
            this.bt_Circle.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.bt_Circle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_Circle.Location = new System.Drawing.Point(169, 13);
            this.bt_Circle.Name = "bt_Circle";
            this.bt_Circle.Size = new System.Drawing.Size(75, 45);
            this.bt_Circle.TabIndex = 5;
            this.bt_Circle.Text = "Circle";
            this.bt_Circle.UseVisualStyleBackColor = false;
            this.bt_Circle.Click += new System.EventHandler(this.bt_Circle_Click);
            // 
            // bt_Rectangle
            // 
            this.bt_Rectangle.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.bt_Rectangle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_Rectangle.Location = new System.Drawing.Point(280, 13);
            this.bt_Rectangle.Name = "bt_Rectangle";
            this.bt_Rectangle.Size = new System.Drawing.Size(75, 45);
            this.bt_Rectangle.TabIndex = 6;
            this.bt_Rectangle.Text = "Rectangle";
            this.bt_Rectangle.UseVisualStyleBackColor = false;
            this.bt_Rectangle.Click += new System.EventHandler(this.bt_Rectangle_Click);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // bt_Equilateral_Triangle
            // 
            this.bt_Equilateral_Triangle.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.bt_Equilateral_Triangle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_Equilateral_Triangle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bt_Equilateral_Triangle.Location = new System.Drawing.Point(393, 13);
            this.bt_Equilateral_Triangle.Name = "bt_Equilateral_Triangle";
            this.bt_Equilateral_Triangle.Size = new System.Drawing.Size(75, 45);
            this.bt_Equilateral_Triangle.TabIndex = 7;
            this.bt_Equilateral_Triangle.Text = "Equilateral Triangle";
            this.bt_Equilateral_Triangle.UseVisualStyleBackColor = false;
            this.bt_Equilateral_Triangle.Click += new System.EventHandler(this.bt_Equilateral_Triangle_Click);
            // 
            // bt_Regular_Five_Polygon
            // 
            this.bt_Regular_Five_Polygon.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.bt_Regular_Five_Polygon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_Regular_Five_Polygon.Location = new System.Drawing.Point(493, 13);
            this.bt_Regular_Five_Polygon.Name = "bt_Regular_Five_Polygon";
            this.bt_Regular_Five_Polygon.Size = new System.Drawing.Size(75, 59);
            this.bt_Regular_Five_Polygon.TabIndex = 8;
            this.bt_Regular_Five_Polygon.Text = "Regular Five Polygon";
            this.bt_Regular_Five_Polygon.UseVisualStyleBackColor = false;
            this.bt_Regular_Five_Polygon.Click += new System.EventHandler(this.bt_Regular_Five_Polygon_Click);
            // 
            // bt_Regular_Six_Polygon
            // 
            this.bt_Regular_Six_Polygon.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.bt_Regular_Six_Polygon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_Regular_Six_Polygon.Location = new System.Drawing.Point(588, 13);
            this.bt_Regular_Six_Polygon.Name = "bt_Regular_Six_Polygon";
            this.bt_Regular_Six_Polygon.Size = new System.Drawing.Size(75, 59);
            this.bt_Regular_Six_Polygon.TabIndex = 9;
            this.bt_Regular_Six_Polygon.Text = "Regular Six Polygon";
            this.bt_Regular_Six_Polygon.UseVisualStyleBackColor = false;
            this.bt_Regular_Six_Polygon.Click += new System.EventHandler(this.bt_Regular_Six_Polygon_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1118, 522);
            this.Controls.Add(this.bt_Regular_Six_Polygon);
            this.Controls.Add(this.bt_Regular_Five_Polygon);
            this.Controls.Add(this.bt_Equilateral_Triangle);
            this.Controls.Add(this.bt_Rectangle);
            this.Controls.Add(this.bt_Circle);
            this.Controls.Add(this.bt_Exit);
            this.Controls.Add(this.bt_ColorTable);
            this.Controls.Add(this.bt_line);
            this.Controls.Add(this.openGLControl);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SharpGL.OpenGLControl openGLControl;
        private System.Windows.Forms.Button bt_ColorTable;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button bt_line;
        private System.Windows.Forms.Button bt_Exit;
        private System.Windows.Forms.Button bt_Circle;
        private System.Windows.Forms.Button bt_Rectangle;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Button bt_Equilateral_Triangle;
        private System.Windows.Forms.Button bt_Regular_Five_Polygon;
        private System.Windows.Forms.Button bt_Regular_Six_Polygon;
    }
}

