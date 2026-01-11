namespace bezpieczna_paczkaApp
{
    // This 'partial' class definition splits the GameWindow class across two files.
    // This file is for the designer-generated code.
    partial class GameWindow : Form
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


        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.

        private void InitializeComponent()
        {
            pnlContainer = new Panel();
            SuspendLayout();
            // 
            // pnlContainer
            // 
            pnlContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlContainer.BackColor = Color.Transparent;
            pnlContainer.Location = new Point(0, 0);
            pnlContainer.Margin = new Padding(0);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.Size = new Size(1280, 1024);
            pnlContainer.TabIndex = 0;
            // 
            // GameWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1278, 1022);
            Controls.Add(pnlContainer);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "GameWindow";
            StartPosition = FormStartPosition.CenterScreen;
            Load += GameWindow_Load;
            ResumeLayout(false);

        }

        #endregion

        private Panel pnlContainer;
    }
}