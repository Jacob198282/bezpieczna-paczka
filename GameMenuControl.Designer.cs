namespace bezpieczna_paczkaApp
{
    partial class GameMenuControl
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
            picLevelSelect = new PictureBox();
            picResume = new PictureBox();
            picExit = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picLevelSelect).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picResume).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picExit).BeginInit();
            SuspendLayout();
            // 
            // picLevelSelect
            // 
            picLevelSelect.Cursor = Cursors.Hand;
            picLevelSelect.Location = new Point(495, 480);
            picLevelSelect.Name = "picLevelSelect";
            picLevelSelect.Size = new Size(290, 120);
            picLevelSelect.SizeMode = PictureBoxSizeMode.Zoom;
            picLevelSelect.TabIndex = 0;
            picLevelSelect.TabStop = false;
            picLevelSelect.Click += picSelectLevel_Click;
            // 
            // picResume
            // 
            picResume.Cursor = Cursors.Hand;
            picResume.Location = new Point(495, 320);
            picResume.Name = "picResume";
            picResume.Size = new Size(290, 120);
            picResume.SizeMode = PictureBoxSizeMode.Zoom;
            picResume.TabIndex = 1;
            picResume.TabStop = false;
            picResume.Click += picResume_Click;
            // 
            // picExit
            // 
            picExit.Cursor = Cursors.Hand;
            picExit.Location = new Point(495, 640);
            picExit.Name = "picExit";
            picExit.Size = new Size(290, 120);
            picExit.SizeMode = PictureBoxSizeMode.Zoom;
            picExit.TabIndex = 2;
            picExit.TabStop = false;
            picExit.Click += picExit_Click;
            // 
            // GameMenuControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(picExit);
            Controls.Add(picResume);
            Controls.Add(picLevelSelect);
            Name = "GameMenuControl";
            Size = new Size(1280, 1024);
            ((System.ComponentModel.ISupportInitialize)picLevelSelect).EndInit();
            ((System.ComponentModel.ISupportInitialize)picResume).EndInit();
            ((System.ComponentModel.ISupportInitialize)picExit).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox picLevelSelect;
        private PictureBox picResume;
        private PictureBox picExit;
    }
}
