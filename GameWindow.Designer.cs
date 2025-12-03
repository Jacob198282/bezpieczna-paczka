namespace bezpieczna_paczkaApp
{
    // This 'partial' class definition splits the GameWindow class across two files.
    // This file is for the designer-generated code.
    partial class GameWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameWindow));
            picLogo = new PictureBox();
            picSelectLevel = new PictureBox();
            picExitGame = new PictureBox();
            picLevel1 = new PictureBox();
            picLevel2 = new PictureBox();
            picLevel3 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picSelectLevel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picExitGame).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picLevel1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picLevel2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picLevel3).BeginInit();
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
            picLevel1.Visible = false;
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
            picLevel2.Visible = false;
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
            picLevel3.Visible = false;
            picLevel3.Click += picLevel3_Click;
            // 
            // GameWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1278, 1022);
            Controls.Add(picExitGame);
            Controls.Add(picSelectLevel);
            Controls.Add(picLogo);
            Controls.Add(picLevel1);
            Controls.Add(picLevel2);
            Controls.Add(picLevel3);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "GameWindow";
            StartPosition = FormStartPosition.CenterScreen;
            Load += GameWindow_Load;
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            ((System.ComponentModel.ISupportInitialize)picSelectLevel).EndInit();
            ((System.ComponentModel.ISupportInitialize)picExitGame).EndInit();
            ((System.ComponentModel.ISupportInitialize)picLevel1).EndInit();
            ((System.ComponentModel.ISupportInitialize)picLevel2).EndInit();
            ((System.ComponentModel.ISupportInitialize)picLevel3).EndInit();
            ResumeLayout(false);

        }

        #endregion

        // Declare the components so the logic file (GameWindow.cs) can access them
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.PictureBox picSelectLevel;
        private System.Windows.Forms.PictureBox picExitGame;
        private System.Windows.Forms.PictureBox picLevel1; // Picture box for Level 1 selection button.
        private System.Windows.Forms.PictureBox picLevel2; // Picture box for Level 2 selection button.
        private System.Windows.Forms.PictureBox picLevel3; // Picture box for Level 3 selection button.
        // END ADDITION
    }
}