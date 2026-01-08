/*
 * Copyright (c) Jacob198282 Gdansk University of Technology
 * MIT License
 * Documentation under https://github.com/Jacob198282/bezpieczna-paczka
 */

using System;
using System.Drawing.Imaging.Effects;
using System.Windows.Forms;

namespace bezpieczna_paczkaApp
{
    // UserControl for the level selection screen
    public partial class LevelSelectControl : UserControl
    {
        // Event to notify the host form when a level is chosen
        public event EventHandler<LevelSelectedEventArgs> LevelSelected;
        // Event to notify the host form that the user wants to go back to the main menu
        public event EventHandler BackClicked;

        // Define custom event arguments to pass the selected level ID (e.g., 1, 2, 3)

        public string projectRoot; // path to the project
        public string graphicsPath; // path to folder with graphics
        public class LevelSelectedEventArgs : EventArgs
        {
            public int LevelId { get; set; }
        }

        public LevelSelectControl(string projectRoot, string graphicsPath)
        {
            InitializeComponent();
            DoubleBuffered = true; // removes flickering of the background
            this.projectRoot = projectRoot;
            this.graphicsPath = graphicsPath;
        }

        // Event handler for level 1 button
        private void picLevel1_Click(object sender, EventArgs e)
        {
            // Pass the chosen level ID
            LevelSelected?.Invoke(this, new LevelSelectedEventArgs { LevelId = 1 });
        }

        // Event handler for level 2 button
        private void picLevel2_Click(object sender, EventArgs e)
        {
            LevelSelected?.Invoke(this, new LevelSelectedEventArgs { LevelId = 2 });
        }

        // Event handler for level 3 button
        private void picLevel3_Click(object sender, EventArgs e)
        {
            LevelSelected?.Invoke(this, new LevelSelectedEventArgs { LevelId = 3 });
        }

        // Loading method for this UserControl
        private void LevelSelectControl_Load(object sender, EventArgs e)
        {
            LoadGraphics(); // Loads images for this UserControl
            // setting transparency for all of the controls, because parent BackColor has changed
            BackColor = Color.FromArgb(70, 0, 0, 0);
            lblVehicles.BackColor = Color.Transparent;
            picVehicle1.BackColor = Color.Transparent;
            picVehicle2.BackColor = Color.Transparent;
            picVehicle3.BackColor = Color.Transparent;
            lblVehicle1.BackColor = Color.Transparent;
            lblVehicle2.BackColor = Color.Transparent;
            lblVehicle3.BackColor = Color.Transparent;
            picBack.BackColor = Color.Transparent;
            picLevel1.BackColor = Color.Transparent;
            picLevel2.BackColor = Color.Transparent;
            picLevel3.BackColor = Color.Transparent;
            picStarsLvl1.BackColor = Color.Transparent;
            picStarsLvl2.BackColor = Color.Transparent;
            picStarsLvl3.BackColor = Color.Transparent;
            picLogo.BackColor = Color.Transparent;
            picUni.BackColor = Color.Transparent;
            picStar.BackColor = Color.Transparent;
            lblTotalStars.BackColor = Color.Transparent;
            tabVehicles.BackColor = Color.Transparent;
            UpdateStarsDisplay();
        }

        // Handles the Click event for the 'Back' picture button
        private void picBack_Click(object sender, EventArgs e)
        {
            // Notify GameWindow to switch back to Main Menu
            BackClicked?.Invoke(this, EventArgs.Empty);
        }

        // Updates the visual display of total stars earned by the player.
        public void UpdateStarsDisplay()
        {
            // Retrieve current progress from the static PlayerProgress class
            int totalStars = PlayerProgress.GetTotalStars();
            int maxStars = PlayerProgress.GetMaxPossibleStars();

            // Format the display string with star symbol and fraction
            lblTotalStars.Text = $" {totalStars} / {maxStars}";

            // Update PictureBoxes of the stars
            UpdateLevelStarsImages();

            // Update PictureBoxes of the vehicles
            UpdateVehiclesDisplay();
        }

        // Updates star images for each level based on player's saved progress.
        private void UpdateLevelStarsImages()
        {
            // Update stars image for each level (1, 2, 3)
            SetStarsImage(picStarsLvl1, PlayerProgress.GetStars(1));
            SetStarsImage(picStarsLvl2, PlayerProgress.GetStars(2));
            SetStarsImage(picStarsLvl3, PlayerProgress.GetStars(3));
        }

        // Updates display of all vehicles based on player progress.
        private void UpdateVehiclesDisplay()
        {
            int currentVehicleID = PlayerProgress.GetCurrentVehicleID();

            SetVehicleDisplay(picVehicle1, lblVehicle1, 1, currentVehicleID);
            SetVehicleDisplay(picVehicle2, lblVehicle2, 2, currentVehicleID);
            SetVehicleDisplay(picVehicle3, lblVehicle3, 3, currentVehicleID);
        }

        /// <summary>
        /// Sets image and status label for a single vehicle.
        /// </summary>
        /// <param name="picBox">PictureBox to update</param>
        /// <param name="lblStatus">Status label to update</param>
        /// <param name="vehicleID">Vehicle ID (1, 2, or 3)</param>
        /// <param name="currentVehicleId">Currently active vehicle ID</param>
        private void SetVehicleDisplay(PictureBox picBox, Label lblStatus, int vehicleID, int currentVehicleID)
        {
            // Checking with the method of the PlayerProgress class if the vehicle is unlocked
            bool isUnlocked = PlayerProgress.IsVehicleUnlocked(vehicleID); 
            bool isActive = (vehicleID == currentVehicleID); // If the active vehicle is the same as current, then it is true

            // Build image path
            string imageName = $"pojazd{vehicleID}.png"; // Name of the image of the vehicle
            string imagePath = Path.Combine(graphicsPath, "vehicles" , imageName); // Path to the image of the vehicle

            // Set status label
            if (isUnlocked)
            {
                lblStatus.Text = "DOSTĘPNY";
                lblStatus.ForeColor = Color.White;
                // Load vehicle image
                try
                {
                    if (picBox.Image != null)
                    {
                        picBox.Image.Dispose();
                        picBox.Image = null;
                    }

                    picBox.Image = Image.FromFile(imagePath);
                }
                catch (FileNotFoundException ex) // If the file has not been found
                {
                    MessageBox.Show($"Błąd wczytywania grafiki: Nie znaleziono pliku!\n{ex.Message}", "Błąd Pliku");
                    Application.Exit(); // Exit if graphics are missing
                }
                catch (Exception ex) // Other error
                {
                    MessageBox.Show($"Wystąpił nieoczekiwany błąd przy wczytywaniu grafiki:\n{ex.Message}", "Błąd Krytyczny");
                    Application.Exit();
                }

                if(isActive)
                {
                    lblStatus.Text = "AKTYWNY";
                    lblStatus.ForeColor = Color.Gold;
                }
            }
            else
            {
                int threshold = PlayerProgress.GetVehicleThreshold(vehicleID);
                // Method for setting conjugation of the word GWIAZDKA depending of the number of stars - currently working for numbers less than 12
                if (threshold > 4 || threshold == 0)
                {
                    lblStatus.Text = $"{threshold} GWIAZDEK";
                }
                else if(threshold == 1)
                {
                    lblStatus.Text = $"{threshold} GWIAZDKA";
                }
                else
                {
                    lblStatus.Text = $"{threshold} GWIAZDKI";
                }
                lblStatus.ForeColor = Color.Silver;
            }
        }

        // Loads the appropriate star image into a PictureBox based on star count.
        private void SetStarsImage(PictureBox pictureBox, int starCount)
        {
            // Clamp value to valid range - in case the provided value is invalid
            starCount = Math.Clamp(starCount, 0, 3);

            // Build path to the appropriate star image
            string imageName = $"stars_{starCount}.png";
            string imagePath = Path.Combine(graphicsPath, "stars", imageName);

            try
            {
                // Dispose previous image to free memory
                if (pictureBox.Image != null)
                {
                    pictureBox.Image.Dispose();
                    pictureBox.Image = null;
                }

                // Load new image
                pictureBox.Image = Image.FromFile(imagePath);
            }
            catch (FileNotFoundException ex) // If the file has not been found
            {
                MessageBox.Show($"Błąd wczytywania grafiki: Nie znaleziono pliku!\n{ex.Message}", "Błąd Pliku");
                Application.Exit(); // Exit if graphics are missing
            }
            catch (Exception ex) // Other error
            {
                MessageBox.Show($"Wystąpił nieoczekiwany błąd przy wczytywaniu grafiki:\n{ex.Message}", "Błąd Krytyczny");
                Application.Exit();
            }
        }

        // Method for loading graphics
        private void LoadGraphics()
        {
            try
            {
                // Load the images for the Level Selection buttons
                string level1Path = Path.Combine(graphicsPath, "poziom1.png");
                picLevel1.Image = Image.FromFile(level1Path);

                string level2Path = Path.Combine(graphicsPath, "poziom2.png");
                picLevel2.Image = Image.FromFile(level2Path);

                string level3Path = Path.Combine(graphicsPath, "poziom3.png");
                picLevel3.Image = Image.FromFile(level3Path);

                // Load the image for the Back Button
                string backPath = Path.Combine(graphicsPath, "powrot.png");
                picBack.Image = Image.FromFile(backPath);

                // Load image for the university logo
                string uniPath = Path.Combine(graphicsPath, "pg_logo_czarne.png");
                picUni.Image = Image.FromFile(uniPath);

                // Load image for game logo
                string logoPath = Path.Combine(graphicsPath, "bezpieczna-paczka-logo-nobg.png");
                picLogo.Image = Image.FromFile(logoPath);

                // Load image for star image
                string starPath = Path.Combine(graphicsPath, "stars", "star.png");
                picStar.Image = Image.FromFile(starPath);
            }
            catch (FileNotFoundException ex)
            {
                // If a file is missing, show a helpful error message instead of crashing
                MessageBox.Show($"Błąd wczytywania grafiki: Nie znaleziono pliku!\n{ex.Message}", "Błąd Pliku");
                Application.Exit(); // Exit if graphics are missing
            }
            catch (Exception ex)
            {
                // Catch other potential errors (e.g., file is not an image)
                MessageBox.Show($"Wystąpił nieoczekiwany błąd przy wczytywaniu grafiki:\n{ex.Message}", "Błąd Krytyczny");
                Application.Exit();
            }
        }
    }
}
