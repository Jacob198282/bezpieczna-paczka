namespace bezpieczna_paczkaApp
{
    partial class MainMenuControl
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
            picLogo = new PictureBox();
            picSelectLevel = new PictureBox();
            picExitGame = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picSelectLevel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picExitGame).BeginInit();
            SuspendLayout();
            // 
            // picLogo
            // 
            picLogo.BackColor = Color.Transparent;
            picLogo.Location = new Point(440, 100);
            picLogo.Margin = new Padding(4, 3, 4, 3);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(400, 400);
            picLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            picLogo.TabIndex = 0;
            picLogo.TabStop = false;
            // 
            // picSelectLevel
            // 
            picSelectLevel.BackColor = Color.Transparent;
            picSelectLevel.Cursor = Cursors.Hand;
            picSelectLevel.Location = new Point(174, 577);
            picSelectLevel.Margin = new Padding(4, 3, 4, 3);
            picSelectLevel.Name = "picSelectLevel";
            picSelectLevel.Size = new Size(290, 115);
            picSelectLevel.SizeMode = PictureBoxSizeMode.Zoom;
            picSelectLevel.TabIndex = 1;
            picSelectLevel.TabStop = false;
            picSelectLevel.Click += picSelectLevel_Click;
            // 
            // picExitGame
            // 
            picExitGame.BackColor = Color.Transparent;
            picExitGame.Cursor = Cursors.Hand;
            picExitGame.Location = new Point(790, 577);
            picExitGame.Margin = new Padding(4, 3, 4, 3);
            picExitGame.Name = "picExitGame";
            picExitGame.Size = new Size(292, 115);
            picExitGame.SizeMode = PictureBoxSizeMode.Zoom;
            picExitGame.TabIndex = 2;
            picExitGame.TabStop = false;
            picExitGame.Click += picExitGame_Click;
            // 
            // MainMenuControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(picExitGame);
            Controls.Add(picSelectLevel);
            Controls.Add(picLogo);
            DoubleBuffered = true;
            Name = "MainMenuControl";
            Size = new Size(1280, 1024);
            Load += MainMenuConrol_Load;
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            ((System.ComponentModel.ISupportInitialize)picSelectLevel).EndInit();
            ((System.ComponentModel.ISupportInitialize)picExitGame).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private PictureBox picLogo;
        private PictureBox picSelectLevel;
        private PictureBox picExitGame;
    }

}
