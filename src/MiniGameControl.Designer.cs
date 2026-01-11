using System.IO.Packaging;

namespace bezpieczna_paczkaApp
{
    partial class MiniGameControl
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
                StopGame();

                if (gameTimer != null)
                {
                    gameTimer.Dispose();
                    gameTimer = null;
                }

                if (spawnTimer != null)
                {
                    spawnTimer.Dispose();
                    spawnTimer = null;
                }

                DisposeImages();
                packages.Clear();

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
            lblScore = new Label();
            picMenu = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picMenu).BeginInit();
            SuspendLayout();
            // 
            // lblScore
            // 
            lblScore.BackColor = Color.Transparent;
            lblScore.Font = new Font("Gill Sans Ultra Bold", 36F, FontStyle.Italic, GraphicsUnit.Point, 238);
            lblScore.ForeColor = Color.Gold;
            lblScore.Location = new Point(370, 15);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(540, 61);
            lblScore.TabIndex = 0;
            lblScore.Text = "ZEBRANO: 10/10";
            // 
            // picMenu
            // 
            picMenu.BackColor = Color.Transparent;
            picMenu.Cursor = Cursors.Hand;
            picMenu.Location = new Point(1086, 902);
            picMenu.Name = "picMenu";
            picMenu.Size = new Size(168, 50);
            picMenu.SizeMode = PictureBoxSizeMode.Zoom;
            picMenu.TabIndex = 7;
            picMenu.TabStop = false;
            picMenu.Click += picMenu_Click;
            // 
            // MiniGameControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gray;
            Controls.Add(picMenu);
            Controls.Add(lblScore);
            Name = "MiniGameControl";
            Size = new Size(1280, 1024);
            ((System.ComponentModel.ISupportInitialize)picMenu).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label lblScore;
        private PictureBox picMenu;
    }
}
