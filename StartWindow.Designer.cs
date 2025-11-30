namespace bezpieczna_paczkaApp
{
    // This 'partial' class definition splits the StartWindow class across two files.
    // This file is for the designer-generated code.
    partial class StartWindow
    {
        
        /// Required designer variable.
        
        private System.ComponentModel.IContainer components = null;

        
        /// Clean up any resources being used.
        
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


        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartWindow));
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
            picLogo.Image = (Image)resources.GetObject("picLogo.Image");
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
            picSelectLevel.Image = (Image)resources.GetObject("picSelectLevel.Image");
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
            picExitGame.Image = (Image)resources.GetObject("picExitGame.Image");
            picExitGame.Location = new Point(790, 577);
            picExitGame.Margin = new Padding(4, 3, 4, 3);
            picExitGame.Name = "picExitGame";
            picExitGame.Size = new Size(292, 115);
            picExitGame.SizeMode = PictureBoxSizeMode.Zoom;
            picExitGame.TabIndex = 2;
            picExitGame.TabStop = false;
            picExitGame.Click += picExitGame_Click;
            // 
            // StartWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1278, 1022);
            ControlBox = false;
            Controls.Add(picExitGame);
            Controls.Add(picSelectLevel);
            Controls.Add(picLogo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "StartWindow";
            StartPosition = FormStartPosition.CenterScreen;
            Load += StartWindow_Load;
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            ((System.ComponentModel.ISupportInitialize)picSelectLevel).EndInit();
            ((System.ComponentModel.ISupportInitialize)picExitGame).EndInit();
            ResumeLayout(false);

        }

        #endregion

        // Declare the components so the logic file (StartWindow.cs) can access them
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.PictureBox picSelectLevel;
        private System.Windows.Forms.PictureBox picExitGame;
    }
}