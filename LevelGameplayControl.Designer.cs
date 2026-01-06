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
            lblQuestionText = new Label();
            lblProgress = new Label();
            lblScore = new Label();
            pnlAnswers = new FlowLayoutPanel();
            picMenu = new PictureBox();
            picLogo = new PictureBox();
            picUni = new PictureBox();
            picVan = new PictureBox();
            pnlIntro = new Panel();
            pnlIntroStep2 = new Panel();
            btnStartGameplay = new Button();
            picSignsTutorial = new PictureBox();
            pnlIntroStep1 = new Panel();
            btnNext = new Button();
            lblIntroDescription = new Label();
            lblIntroTitle = new Label();
            pnlScenario = new Panel();
            ((System.ComponentModel.ISupportInitialize)picMenu).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picUni).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picVan).BeginInit();
            pnlIntro.SuspendLayout();
            pnlIntroStep2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picSignsTutorial).BeginInit();
            pnlIntroStep1.SuspendLayout();
            pnlScenario.SuspendLayout();
            SuspendLayout();
            // 
            // lblQuestionText
            // 
            lblQuestionText.BackColor = Color.Transparent;
            lblQuestionText.Font = new Font("Gill Sans Ultra Bold", 20.25F, FontStyle.Italic, GraphicsUnit.Point, 238);
            lblQuestionText.ForeColor = SystemColors.ControlLight;
            lblQuestionText.Location = new Point(173, 0);
            lblQuestionText.Name = "lblQuestionText";
            lblQuestionText.Size = new Size(909, 122);
            lblQuestionText.TabIndex = 1;
            lblQuestionText.Text = "Pytanie";
            lblQuestionText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblProgress
            // 
            lblProgress.BackColor = Color.Transparent;
            lblProgress.Font = new Font("Gill Sans Ultra Bold", 21.75F, FontStyle.Italic, GraphicsUnit.Point, 238);
            lblProgress.ForeColor = SystemColors.ControlLight;
            lblProgress.Location = new Point(20, 623);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(200, 130);
            lblProgress.TabIndex = 2;
            lblProgress.Text = "Paczka \r\n20/20";
            lblProgress.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblScore
            // 
            lblScore.BackColor = Color.Transparent;
            lblScore.Font = new Font("Gill Sans Ultra Bold", 21.75F, FontStyle.Italic, GraphicsUnit.Point, 238);
            lblScore.ForeColor = SystemColors.ControlLight;
            lblScore.Location = new Point(1010, 623);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(250, 130);
            lblScore.TabIndex = 3;
            lblScore.Text = "Dostarczono\r\n20/20";
            lblScore.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlAnswers
            // 
            pnlAnswers.AutoScroll = true;
            pnlAnswers.BackColor = Color.Transparent;
            pnlAnswers.Location = new Point(40, 130);
            pnlAnswers.Name = "pnlAnswers";
            pnlAnswers.Size = new Size(1200, 150);
            pnlAnswers.TabIndex = 5;
            pnlAnswers.WrapContents = false;
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
            picMenu.Click += picMenu_Click;
            // 
            // picLogo
            // 
            picLogo.BackColor = Color.Transparent;
            picLogo.Location = new Point(0, 0);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(125, 125);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 7;
            picLogo.TabStop = false;
            // 
            // picUni
            // 
            picUni.BackColor = Color.Transparent;
            picUni.Location = new Point(1130, 0);
            picUni.Name = "picUni";
            picUni.Size = new Size(150, 125);
            picUni.SizeMode = PictureBoxSizeMode.Zoom;
            picUni.TabIndex = 8;
            picUni.TabStop = false;
            // 
            // picVan
            // 
            picVan.BackColor = Color.Transparent;
            picVan.Location = new Point(227, 290);
            picVan.Name = "picVan";
            picVan.Size = new Size(45, 150);
            picVan.SizeMode = PictureBoxSizeMode.Zoom;
            picVan.TabIndex = 9;
            picVan.TabStop = false;
            // 
            // pnlIntro
            // 
            pnlIntro.Controls.Add(pnlIntroStep2);
            pnlIntro.Controls.Add(pnlIntroStep1);
            pnlIntro.Dock = DockStyle.Fill;
            pnlIntro.Location = new Point(0, 0);
            pnlIntro.Name = "pnlIntro";
            pnlIntro.Size = new Size(1280, 1024);
            pnlIntro.TabIndex = 10;
            // 
            // pnlIntroStep2
            // 
            pnlIntroStep2.BackColor = Color.Green;
            pnlIntroStep2.Controls.Add(btnStartGameplay);
            pnlIntroStep2.Controls.Add(picSignsTutorial);
            pnlIntroStep2.Dock = DockStyle.Fill;
            pnlIntroStep2.Location = new Point(0, 0);
            pnlIntroStep2.Name = "pnlIntroStep2";
            pnlIntroStep2.Size = new Size(1280, 1024);
            pnlIntroStep2.TabIndex = 3;
            pnlIntroStep2.Visible = false;
            // 
            // btnStartGameplay
            // 
            btnStartGameplay.Cursor = Cursors.Hand;
            btnStartGameplay.Font = new Font("Gill Sans Ultra Bold Condensed", 20.25F, FontStyle.Italic, GraphicsUnit.Point, 238);
            btnStartGameplay.Location = new Point(540, 902);
            btnStartGameplay.Name = "btnStartGameplay";
            btnStartGameplay.Size = new Size(200, 100);
            btnStartGameplay.TabIndex = 1;
            btnStartGameplay.Text = "Rozpocznij grę";
            btnStartGameplay.UseVisualStyleBackColor = true;
            btnStartGameplay.Click += btnStartGameplay_Click;
            // 
            // picSignsTutorial
            // 
            picSignsTutorial.Location = new Point(42, 32);
            picSignsTutorial.Name = "picSignsTutorial";
            picSignsTutorial.Size = new Size(1205, 864);
            picSignsTutorial.SizeMode = PictureBoxSizeMode.Zoom;
            picSignsTutorial.TabIndex = 0;
            picSignsTutorial.TabStop = false;
            // 
            // pnlIntroStep1
            // 
            pnlIntroStep1.BackColor = Color.Green;
            pnlIntroStep1.Controls.Add(btnNext);
            pnlIntroStep1.Controls.Add(lblIntroDescription);
            pnlIntroStep1.Controls.Add(lblIntroTitle);
            pnlIntroStep1.Dock = DockStyle.Fill;
            pnlIntroStep1.Location = new Point(0, 0);
            pnlIntroStep1.Name = "pnlIntroStep1";
            pnlIntroStep1.Size = new Size(1280, 1024);
            pnlIntroStep1.TabIndex = 0;
            // 
            // btnNext
            // 
            btnNext.Cursor = Cursors.Hand;
            btnNext.Font = new Font("Gill Sans Ultra Bold Condensed", 20.25F, FontStyle.Italic, GraphicsUnit.Point, 238);
            btnNext.Location = new Point(540, 902);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(200, 100);
            btnNext.TabIndex = 2;
            btnNext.Text = "Dalej";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // lblIntroDescription
            // 
            lblIntroDescription.Font = new Font("Gill Sans Ultra Bold Condensed", 20.25F, FontStyle.Italic, GraphicsUnit.Point, 238);
            lblIntroDescription.ForeColor = SystemColors.ControlLight;
            lblIntroDescription.Location = new Point(42, 100);
            lblIntroDescription.Name = "lblIntroDescription";
            lblIntroDescription.Size = new Size(1196, 775);
            lblIntroDescription.TabIndex = 1;
            lblIntroDescription.Text = "Zasady gry i wstęp";
            // 
            // lblIntroTitle
            // 
            lblIntroTitle.Font = new Font("Gill Sans Ultra Bold Condensed", 27.75F, FontStyle.Italic, GraphicsUnit.Point, 238);
            lblIntroTitle.ForeColor = SystemColors.ControlLight;
            lblIntroTitle.Location = new Point(42, 21);
            lblIntroTitle.Name = "lblIntroTitle";
            lblIntroTitle.Size = new Size(1196, 49);
            lblIntroTitle.TabIndex = 0;
            lblIntroTitle.Text = "Tytuł";
            lblIntroTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlScenario
            // 
            pnlScenario.BackgroundImageLayout = ImageLayout.Zoom;
            pnlScenario.Controls.Add(picVan);
            pnlScenario.Location = new Point(260, 280);
            pnlScenario.Name = "pnlScenario";
            pnlScenario.Size = new Size(740, 740);
            pnlScenario.TabIndex = 2;
            // 
            // LevelGameplayControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Green;
            Controls.Add(pnlIntro);
            Controls.Add(pnlAnswers);
            Controls.Add(picLogo);
            Controls.Add(picUni);
            Controls.Add(picMenu);
            Controls.Add(lblProgress);
            Controls.Add(lblQuestionText);
            Controls.Add(lblScore);
            Controls.Add(pnlScenario);
            Name = "LevelGameplayControl";
            Size = new Size(1280, 1024);
            ((System.ComponentModel.ISupportInitialize)picMenu).EndInit();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            ((System.ComponentModel.ISupportInitialize)picUni).EndInit();
            ((System.ComponentModel.ISupportInitialize)picVan).EndInit();
            pnlIntro.ResumeLayout(false);
            pnlIntroStep2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picSignsTutorial).EndInit();
            pnlIntroStep1.ResumeLayout(false);
            pnlScenario.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Label lblQuestionText;
        private Label lblProgress;
        private Label lblScore;
        private FlowLayoutPanel pnlAnswers;
        private PictureBox picMenu;
        private PictureBox picLogo;
        private PictureBox picUni;
        private PictureBox picVan;
        private Panel pnlIntro;
        private Panel pnlIntroStep1;
        private Button btnNext;
        private Label lblIntroDescription;
        private Label lblIntroTitle;
        private Panel pnlIntroStep2;
        private Button btnStartGameplay;
        private PictureBox picSignsTutorial;
        private Panel pnlScenario;
    }
}
