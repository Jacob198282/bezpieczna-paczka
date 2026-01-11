/*
 * Copyright (c) Jacob198282 Gdansk University of Technology
 * MIT License
 * Documentation under https://github.com/Jacob198282/bezpieczna-paczka
 */

using System;
using System.Windows.Forms;

namespace bezpieczna_paczkaApp
{
    /// <summary>
    /// UserControl for the main menu screen
    /// </summary>
    public partial class MainMenuControl : UserControl
    {
        /// <summary>
        /// Public event to notify the host form (GameWindow) when the level selection button is clicked
        /// </summary>
        public event EventHandler? SelectLevelClicked;

        /// <summary>
        /// path to the project
        /// </summary>
        public string projectRoot;

        /// <summary>
        /// path to folder with graphics
        /// </summary>
        public string graphicsPath; 

        /// <summary>
        /// MainMenuControl constructor
        /// </summary>
        /// <param name="projectRoot">File Path used for loading graphics to the project root</param>
        /// <param name="graphicsPath">Combined root path and graphics folder used for loading graphics</param>
        public MainMenuControl(string projectRoot, string graphicsPath)
        {
            InitializeComponent();
            
            DoubleBuffered = true; // Removes flickering of the background

            this.projectRoot = projectRoot;
            this.graphicsPath = graphicsPath;
        }

        /// <summary>
        /// Handles the Click event for the 'Select Level' button
        /// </summary>
        /// <param name="sender">PictureBox as a Select Level button</param>
        /// <param name="e">Event arguments</param>
        private void picSelectLevel_Click(object sender, EventArgs e)
        {
            // Notify the GameWindow to switch screens.
            SelectLevelClicked?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Handles the Click event for the 'Exit Game' button
        /// </summary>
        /// <param name="sender">PictureBox as a Exit Game button</param>
        /// <param name="e">Event arguments</param>
        private void picExitGame_Click(object sender, EventArgs e)
        {
            // Safely exits the application
            Application.Exit();
        }

        /// <summary>
        /// Loading method for this UserControl
        /// </summary>
        /// <param name="sender">MainMenuControl object</param>
        /// <param name="e">Event arguments</param>
        private void MainMenuConrol_Load(object sender, EventArgs e)
        {
            LoadGraphics(); // loading graphics for this UserControl
        }

        /// <summary>
        /// Loads all graphical assets from the 'graphics' folder
        /// </summary>
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