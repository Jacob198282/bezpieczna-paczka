/*
 * Copyright (c) Jacob198282 Gdansk University of Technology
 * MIT License
 * Documentation under https://github.com/Jacob198282/bezpieczna-paczka
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bezpieczna_paczkaApp
{
    public partial class GameMenuControl : UserControl
    {
        // Events to notify the parent container about user choices
        public event EventHandler? ResumeClicked;
        public event EventHandler? ExitToLevelSelectClicked;

        public string projectRoot; // path to the project
        public string graphicsPath; // path to folder with graphics
        public GameMenuControl(string projectRoot, string graphicsPath)
        {
            // Initialize component
            InitializeComponent();
            DoubleBuffered = true; // anti-flickering 
            // Load graphics fot the in-game menu
            Dock = DockStyle.Fill;

            this.projectRoot = projectRoot;
            this.graphicsPath = graphicsPath;

            picExit.BackColor = Color.Transparent;
            picResume.BackColor = Color.Transparent;
            picLevelSelect.BackColor = Color.Transparent;
        }
        private void picResume_Click(object sender, EventArgs e)
        {
            // Fire the event to close the menu
            ResumeClicked?.Invoke(this, EventArgs.Empty);
        }

        private void picSelectLevel_Click(object sender, EventArgs e)
        {
            // Fire the event to go back to level selection
            ExitToLevelSelectClicked?.Invoke(this, EventArgs.Empty);
        }

        private void picExit_Click(object sender, EventArgs e)
        {
            // Close the entire application
            Application.Exit();
        }

        private void LoadGraphics()
        {
            try
            {
                // Load image for the back button
                string menuButtonPath = Path.Combine(graphicsPath, "powrot.png");
                ResourceHelper.LoadPictureBoxImage(picResume, menuButtonPath);            
                
                // Load image for level select menu button
                string logoPath = Path.Combine(graphicsPath, "wybierz-poziom.png");
                ResourceHelper.LoadPictureBoxImage(picLevelSelect, logoPath);

                // Load image for the exit button
                string uniPath = Path.Combine(graphicsPath, "wyjdz-z-gry.png");
                ResourceHelper.LoadPictureBoxImage(picExit, uniPath);
            }
            catch (Exception ex) // Other errors
            {
                System.Diagnostics.Debug.WriteLine($"Failed to load image: {ex.Message}");

                MessageBox.Show(
                    $"Wystąpił nieoczekiwany błąd przy wczytywaniu grafiki przycisku menu:\n{ex.Message}",
                    "Błąd Krytyczny");
            }
        }

        // Loading method for GameMenuControl User Control
        private void GameMenuControl_Load(object sender, EventArgs e)
        {
            LoadGraphics();
            BackColor = Color.FromArgb(70, 0, 0, 0); // Setting transparency mode for the in game menu
        }
    }
}
