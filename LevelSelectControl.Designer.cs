namespace bezpieczna_paczkaApp
{
    partial class LevelSelectControl : UserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            picLevel1 = new PictureBox();
            picLevel2 = new PictureBox();
            picLevel3 = new PictureBox();
            picBack = new PictureBox();
            picUni = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picLevel1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picLevel2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picLevel3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picBack).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picUni).BeginInit();
            SuspendLayout();
            // 
            // picLevel1
            // 
            picLevel1.BackColor = Color.Transparent;
            picLevel1.Cursor = Cursors.Hand;
            picLevel1.Location = new Point(50, 200);
            picLevel1.Name = "picLevel1";
            picLevel1.Size = new Size(290, 115);
            picLevel1.SizeMode = PictureBoxSizeMode.Zoom;
            picLevel1.TabIndex = 3;
            picLevel1.TabStop = false;
            picLevel1.Click += picLevel1_Click;
            // 
            // picLevel2
            // 
            picLevel2.BackColor = Color.Transparent;
            picLevel2.Cursor = Cursors.Hand;
            picLevel2.Location = new Point(480, 200);
            picLevel2.Name = "picLevel2";
            picLevel2.Size = new Size(290, 115);
            picLevel2.SizeMode = PictureBoxSizeMode.Zoom;
            picLevel2.TabIndex = 4;
            picLevel2.TabStop = false;
            picLevel2.Click += picLevel2_Click;
            // 
            // picLevel3
            // 
            picLevel3.BackColor = Color.Transparent;
            picLevel3.Cursor = Cursors.Hand;
            picLevel3.Location = new Point(910, 200);
            picLevel3.Name = "picLevel3";
            picLevel3.Size = new Size(290, 115);
            picLevel3.SizeMode = PictureBoxSizeMode.Zoom;
            picLevel3.TabIndex = 5;
            picLevel3.TabStop = false;
            picLevel3.Click += picLevel3_Click;
            // 
            // picBack
            // 
            picBack.BackColor = Color.Transparent;
            picBack.Cursor = Cursors.Hand;
            picBack.Location = new Point(480, 500);
            picBack.Name = "picBack";
            picBack.Size = new Size(290, 115);
            picBack.SizeMode = PictureBoxSizeMode.Zoom;
            picBack.TabIndex = 6;
            picBack.TabStop = false;
            picBack.Click += picBack_Click;
            // 
            // picUni
            // 
            picUni.Location = new Point(560, 850);
            picUni.Name = "picUni";
            picUni.Size = new Size(170, 150);
            picUni.SizeMode = PictureBoxSizeMode.Zoom;
            picUni.TabIndex = 10;
            picUni.TabStop = false;
            // 
            // LevelSelectControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(picUni);
            Controls.Add(picBack);
            Controls.Add(picLevel1);
            Controls.Add(picLevel2);
            Controls.Add(picLevel3);
            DoubleBuffered = true;
            Name = "LevelSelectControl";
            Size = new Size(1280, 1024);
            Load += LevelSelectControl_Load;
            ((System.ComponentModel.ISupportInitialize)picLevel1).EndInit();
            ((System.ComponentModel.ISupportInitialize)picLevel2).EndInit();
            ((System.ComponentModel.ISupportInitialize)picLevel3).EndInit();
            ((System.ComponentModel.ISupportInitialize)picBack).EndInit();
            ((System.ComponentModel.ISupportInitialize)picUni).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private PictureBox picLevel1; // Picture box for Level 1 selection button.
        private PictureBox picLevel2; // Picture box for Level 2 selection button.
        private PictureBox picLevel3; // Picture box for Level 3 selection button.
        private PictureBox picBack;
        private PictureBox picUni;
    }
}
