/*
 * Copyright (c) Jacob198282 Gdansk University of Technology
 * MIT License
 * Documentation under https://github.com/Jacob198282/bezpieczna-paczka
 */

using System;
using System.Windows.Forms;

namespace bezpieczna_paczkaApp
{
    // UserControl for the main menu screen
    public partial class MainMenuControl : UserControl
    {
        // Public event to notify the host form (GameWindow) when the level selection button is clicked
        public event EventHandler? SelectLevelClicked;

        public string projectRoot; // path to the project
        public string graphicsPath; // path to folder with graphics
        public MainMenuControl(string projectRoot, string graphicsPath)
        {
            InitializeComponent();
            
            DoubleBuffered = true; // Removes flickering of the background

            this.projectRoot = projectRoot;
            this.graphicsPath = graphicsPath;
        }

        // Handles the Click event for the 'Select Level' button
        private void picSelectLevel_Click(object sender, EventArgs e)
        {
            // Notify the GameWindow to switch screens.
            SelectLevelClicked?.Invoke(this, EventArgs.Empty);
        }

        // Handles the Click event for the 'Exit Game' button
        private void picExitGame_Click(object sender, EventArgs e)
        {
            // Safely exits the application
            Application.Exit();
        }
        
        // Loading method for this UserControl
        private void MainMenuConrol_Load(object sender, EventArgs e)
        {
            LoadGraphics(); // loading graphics for this UserControl
        }


        /// Loads all graphical assets from the 'graphics' folder

        private void LoadGraphics()
        {
            try
            {
                // Load the image for the Select Level button
                string selectLevelPath = Path.Combine(graphicsPath, "wybierz-poziom.png");
                ResourceHelper.LoadPictureBoxImage(picSelectLevel, selectLevelPath);

                // Load the image for the logo
                string logoPath = Path.Combine(graphicsPath, "bezpieczna-paczka-logo-nobg.png");
                ResourceHelper.LoadPictureBoxImage(picLogo, logoPath);

                // Load the image for the Exit Button
                string exitPath = Path.Combine(graphicsPath, "wyjdz-z-gry.png");
                ResourceHelper.LoadPictureBoxImage(picExitGame, exitPath);

                // Load image for the university logo
                string uniPath = Path.Combine(graphicsPath, "pg_logo_czarne.png");
                ResourceHelper.LoadPictureBoxImage(picUni, uniPath);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to load image: {ex.Message}");
                
                // Catch other potential errors (e.g., file is not an image)
                MessageBox.Show($"Wystąpił nieoczekiwany błąd przy wczytywaniu grafiki:\n{ex.Message}", "Błąd Krytyczny");
                Application.Exit();
            }
        }
    }
}