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
    /// <summary>
    /// Class responsible for showing menu during gameplay
    /// </summary>
    public partial class GameMenuControl : UserControl
    {
        /// <summary>
        /// Events to notify the parent container about user choices
        /// </summary>
        public event EventHandler? ResumeClicked;
        public event EventHandler? ExitToLevelSelectClicked;

        /// <summary>
        /// path to the project
        /// </summary>
        public string projectRoot;
        /// <summary>
        /// path to folder with graphics
        /// </summary>
        public string graphicsPath;

        /// <summary>
        /// Game menu contructor
        /// </summary>
        /// <param name="projectRoot">File Path used for loading graphics to the project root</param>
        /// <param name="graphicsPath">Combined root path and graphics folder used for loading graphics</param>
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

        /// <summary>
        /// Handler for the resume button
        /// </summary>
        /// <param name="sender">PictureBox picResume that sends an event</param>
        /// <param name="e">Arguments of an event</param>
        private void picResume_Click(object sender, EventArgs e)
        {
            // Fire the event to close the menu
            ResumeClicked?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Handler for select Level button
        /// </summary>
        /// <param name="sender">PictureBox picLevelSelect that sends an event</param>
        /// <param name="e">Arguments of an event</param>
        private void picSelectLevel_Click(object sender, EventArgs e)
        {
            // Fire the event to go back to level selection
            ExitToLevelSelectClicked?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Handler for exit game button
        /// </summary>
        /// <param name="sender">PictureBox picExit that sends an event</param>
        /// <param name="e">Arguments of an event</param>
        private void picExit_Click(object sender, EventArgs e)
        {
            // Close the entire application
            Application.Exit();
        }

        /// <summary>
        /// Method for loading graphics for the game menu
        /// </summary>
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

        /// <summary>
        /// Loading method for GameMenuControl User Control
        /// </summary>
        /// <param name="sender">GameMenuControl object that sends load event</param>
        /// <param name="e">Arguments of an event</param>
        private void GameMenuControl_Load(object sender, EventArgs e)
        {
            LoadGraphics();
            BackColor = Color.FromArgb(70, 0, 0, 0); // Setting transparency mode for the in game menu
        }
    }
}
