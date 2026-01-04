namespace bezpieczna_paczkaApp
{
    partial class LevelGameplayControl
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
            lblLevelTitle = new Label();
            lblQuestionText = new Label();
            lblProgress = new Label();
            lblScore = new Label();
            picScenario = new PictureBox();
            pnlAnswers = new FlowLayoutPanel();
            picMenu = new PictureBox();
            picLogo = new PictureBox();
            picUni = new PictureBox();
            picVan = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picScenario).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picMenu).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picUni).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picVan).BeginInit();
            SuspendLayout();
            // 
            // lblLevelTitle
            // 
            lblLevelTitle.AutoSize = true;
            lblLevelTitle.BackColor = Color.Transparent;
            lblLevelTitle.Font = new Font("Gill Sans Ultra Bold Condensed", 36F, FontStyle.Italic, GraphicsUnit.Point, 0);
            lblLevelTitle.ForeColor = SystemColors.ControlLight;
            lblLevelTitle.Location = new Point(589, 21);
            lblLevelTitle.Name = "lblLevelTitle";
            lblLevelTitle.Size = new Size(134, 67);
            lblLevelTitle.TabIndex = 0;
            lblLevelTitle.Text = "Tytuł";
            lblLevelTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblQuestionText
            // 
            lblQuestionText.AutoSize = true;
            lblQuestionText.BackColor = Color.Transparent;
            lblQuestionText.Font = new Font("Gill Sans Ultra Bold Condensed", 36F, FontStyle.Italic, GraphicsUnit.Point, 238);
            lblQuestionText.ForeColor = SystemColors.ControlLight;
            lblQuestionText.Location = new Point(570, 155);
            lblQuestionText.Name = "lblQuestionText";
            lblQuestionText.Size = new Size(184, 67);
            lblQuestionText.TabIndex = 1;
            lblQuestionText.Text = "Pytanie";
            lblQuestionText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblProgress
            // 
            lblProgress.AutoSize = true;
            lblProgress.BackColor = Color.Transparent;
            lblProgress.Font = new Font("Gill Sans Ultra Bold Condensed", 36F, FontStyle.Italic, GraphicsUnit.Point, 238);
            lblProgress.ForeColor = SystemColors.ControlLight;
            lblProgress.Location = new Point(20, 623);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(166, 67);
            lblProgress.TabIndex = 2;
            lblProgress.Text = "Postęp";
            lblProgress.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblScore
            // 
            lblScore.AutoSize = true;
            lblScore.BackColor = Color.Transparent;
            lblScore.Font = new Font("Gill Sans Ultra Bold Condensed", 36F, FontStyle.Italic, GraphicsUnit.Point, 238);
            lblScore.ForeColor = SystemColors.ControlLight;
            lblScore.Location = new Point(1031, 623);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(158, 67);
            lblScore.TabIndex = 3;
            lblScore.Text = "Wynik";
            lblScore.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // picScenario
            // 
            picScenario.BackColor = Color.Transparent;
            picScenario.Location = new Point(295, 378);
            picScenario.Name = "picScenario";
            picScenario.Size = new Size(709, 574);
            picScenario.SizeMode = PictureBoxSizeMode.Zoom;
            picScenario.TabIndex = 4;
            picScenario.TabStop = false;
            // 
            // pnlAnswers
            // 
            pnlAnswers.BackColor = Color.Transparent;
            pnlAnswers.Location = new Point(295, 225);
            pnlAnswers.Name = "pnlAnswers";
            pnlAnswers.Size = new Size(709, 117);
            pnlAnswers.TabIndex = 5;
            // 
            // picMenu
            // 
            picMenu.BackColor = Color.Transparent;
            picMenu.Cursor = Cursors.Hand;
            picMenu.Location = new Point(1070, 902);
            picMenu.Name = "picMenu";
            picMenu.Size = new Size(168, 50);
            picMenu.SizeMode = PictureBoxSizeMode.Zoom;
            picMenu.TabIndex = 6;
            picMenu.TabStop = false;
            // 
            // picLogo
            // 
            picLogo.BackColor = Color.Transparent;
            picLogo.Location = new Point(42, 21);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(125, 125);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 7;
            picLogo.TabStop = false;
            // 
            // picUni
            // 
            picUni.Location = new Point(1088, 21);
            picUni.Name = "picUni";
            picUni.Size = new Size(150, 125);
            picUni.SizeMode = PictureBoxSizeMode.Zoom;
            picUni.TabIndex = 8;
            picUni.TabStop = false;
            // 
            // picVan
            // 
            picVan.Location = new Point(554, 733);
            picVan.Name = "picVan";
            picVan.Size = new Size(200, 200);
            picVan.SizeMode = PictureBoxSizeMode.Zoom;
            picVan.TabIndex = 9;
            picVan.TabStop = false;
            // 
            // LevelGameplayControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(picVan);
            Controls.Add(picUni);
            Controls.Add(picLogo);
            Controls.Add(picMenu);
            Controls.Add(pnlAnswers);
            Controls.Add(lblProgress);
            Controls.Add(lblLevelTitle);
            Controls.Add(lblQuestionText);
            Controls.Add(lblScore);
            Controls.Add(picScenario);
            Name = "LevelGameplayControl";
            Size = new Size(1280, 1024);
            ((System.ComponentModel.ISupportInitialize)picScenario).EndInit();
            ((System.ComponentModel.ISupportInitialize)picMenu).EndInit();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            ((System.ComponentModel.ISupportInitialize)picUni).EndInit();
            ((System.ComponentModel.ISupportInitialize)picVan).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblLevelTitle;
        private Label lblQuestionText;
        private Label lblProgress;
        private Label lblScore;
        private PictureBox picScenario;
        private FlowLayoutPanel pnlAnswers;
        private PictureBox picMenu;
        private PictureBox picLogo;
        private PictureBox picUni;
        private PictureBox picVan;
    }
}
