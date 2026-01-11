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
    /// <summary>
    /// UserControl for the level selection screen
    /// </summary>
    public partial class LevelSelectControl : UserControl
    {
        /// <summary>
        /// Event to notify the host form when a level is chosen
        /// </summary>
        public event EventHandler<LevelSelectedEventArgs>? LevelSelected;
        /// <summary>
        /// Event to notify the host form that the user wants to go back to the main menu
        /// </summary>
        public event EventHandler? BackClicked;

        /// <summary>
        /// path to the project
        /// </summary>
        public string projectRoot;
        /// <summary>
        /// path to folder with graphics
        /// </summary>
        public string graphicsPath; 

        /// <summary>
        /// Define custom event arguments to pass the selected level ID (e.g., 1, 2, 3)
        /// </summary>
        public class LevelSelectedEventArgs : EventArgs
        {
            /// <summary>
            /// ID of level that is being chosen
            /// </summary>
            public int LevelId { get; set; }
        }

        /// <summary>
        /// LevelSelectControl constructor
        /// </summary>
        /// <param name="projectRoot">File Path used for loading graphics to the project root</param>
        /// <param name="graphicsPath">Combined root path and graphics folder used for loading graphics</param>
        public LevelSelectControl(string projectRoot, string graphicsPath)
        {
            InitializeComponent();
            DoubleBuffered = true; // removes flickering of the background
            this.projectRoot = projectRoot;
            this.graphicsPath = graphicsPath;
        }

        /// <summary>
        /// Event handler for level 1 button
        /// </summary>
        /// <param name="sender">PictureBox as Level 1 button</param>
        /// <param name="e">Event arguments</param>
        private void picLevel1_Click(object sender, EventArgs e)
        {
            // Pass the chosen level ID
            if (PlayerProgress.IsLevelUnlocked(1))
            {
                LevelSelected?.Invoke(this, new LevelSelectedEventArgs { LevelId = 1 });
            }
        }

        /// <summary>
        /// Event handler for level 2 button
        /// </summary>
        /// <param name="sender">PictureBox as Level 2 button</param>
        /// <param name="e">Event arguments</param>
        private void picLevel2_Click(object sender, EventArgs e)
        {
            if (PlayerProgress.IsLevelUnlocked(2))
            {
                LevelSelected?.Invoke(this, new LevelSelectedEventArgs { LevelId = 2 });
            }
            else
            {
                MessageBox.Show("Najpierw ukończ Poziom 1!", "Poziom zablokowany");
            }
        }

        /// <summary>
        /// Event handler for level 3 button
        /// </summary>
        /// <param name="sender">PictureBox as Level 3 button</param>
        /// <param name="e">Event arguments</param>
        private void picLevel3_Click(object sender, EventArgs e)
        {
            if (PlayerProgress.IsLevelUnlocked(3))
            {
                LevelSelected?.Invoke(this, new LevelSelectedEventArgs { LevelId = 3 });
            }
            else
            {
                MessageBox.Show("Najpierw ukończ Poziom 2!", "Poziom zablokowany");
            }
        }

        /// <summary>
        /// Loading method for this UserControl
        /// </summary>
        /// <param name="sender">LevelSelectControl object</param>
        /// <param name="e">Event arguments</param>
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
            picReset.BackColor = Color.Transparent;
            lblTotalStars.BackColor = Color.Transparent;
            tabVehicles.BackColor = Color.Transparent;
            UpdateStarsDisplay();
        }

        /// <summary>
        /// Handles the Click event for the 'Back' picture button
        /// </summary>
        /// <param name="sender">PictureBox as a back button</param>
        /// <param name="e">Event arguments</param>
        private void picBack_Click(object sender, EventArgs e)
        {
            // Notify GameWindow to switch back to Main Menu
            BackClicked?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Handles click on reset button. Clears all progress after confirmation.
        /// </summary>
        /// <param name="sender">PictureBox as a reset button</param>
        /// <param name="e">Event arguments</param>
        private void picReset_Click(object sender, EventArgs e)
        {
            // Ask for confirmation
            DialogResult result = MessageBox.Show(
                "Czy na pewno chcesz zresetować postęp gry?\nTa operacja jest nieodwracalna!",
                "Potwierdzenie resetu",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                // Clear all progress
                PlayerProgress.ResetProgress();

                // Refresh display
                UpdateStarsDisplay();

                MessageBox.Show(
                    "Postęp gry został zresetowany.",
                    "Reset wykonany");
            }
        }

        /// <summary>
        /// Handles click on vehicle 1 picture.
        /// </summary>
        /// <param name="sender">PictureBox with 1st vehicle</param>
        /// <param name="e">Event arguments</param>
        private void picVehicle1_Click(object sender, EventArgs e)
        {
            SelectVehicle(1);
        }

        /// <summary>
        /// Handles click on vehicle 2 picture.
        /// </summary>
        /// <param name="sender">PictureBox with 2nd vehicle</param>
        /// <param name="e">Event arguments</param>
        private void picVehicle2_Click(object sender, EventArgs e)
        {
            SelectVehicle(2);
        }

        /// <summary>
        /// Handles click on vehicle 3 picture.
        /// </summary>
        /// <param name="sender">PictureBox with 3rd vehicle</param>
        /// <param name="e">Event arguments</param>
        private void picVehicle3_Click(object sender, EventArgs e)
        {
            SelectVehicle(3);
        }

        /// <summary>
        /// Attempts to select a vehicle. Only works if vehicle is unlocked.
        /// </summary>
        /// <param name="vehicleID">Vehicle ID to select</param>
        private void SelectVehicle(int vehicleID)
        {
            // Try to set the vehicle as current
            bool success = PlayerProgress.SetCurrentVehicleID(vehicleID);

            if (success)
            {
                // Refresh display to show new active vehicle
                UpdateVehiclesDisplay();
            }
        }

        /// <summary>
        /// Updates the visual display of total stars earned by the player.
        /// </summary>
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

            // Update level buttons (locked/unlocked)
            UpdateLevelButtonsDisplay();
        }

        /// <summary>
        /// Updates star images for each level based on player's saved progress.
        /// </summary>
        private void UpdateLevelStarsImages()
        {
            // Update stars image for each level (1, 2, 3)
            SetStarsImage(picStarsLvl1, PlayerProgress.GetStars(1));
            SetStarsImage(picStarsLvl2, PlayerProgress.GetStars(2));
            SetStarsImage(picStarsLvl3, PlayerProgress.GetStars(3));
        }

        /// <summary>
        /// Updates display of all vehicles based on player progress.
        /// </summary>
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
        /// <param name="currentVehicleID">Currently active vehicle ID</param>
        private void SetVehicleDisplay(PictureBox picBox, Label lblStatus, int vehicleID, int currentVehicleID)
        {
            // Checking with the method of the PlayerProgress class if the vehicle is unlocked
            bool isUnlocked = PlayerProgress.IsVehicleUnlocked(vehicleID);
            bool isActive = (vehicleID == currentVehicleID); // If the active vehicle is the same as current, then it is true

            // Build image path
            string imageName = $"pojazd{vehicleID}.png"; // Name of the image of the vehicle
            string imagePath = Path.Combine(graphicsPath, "vehicles", imageName); // Path to the image of the vehicle

            // Set status label
            if (isUnlocked)
            {
                // Set cursor to hand for clickable vehicles
                picBox.Cursor = Cursors.Hand;

                // Load vehicle image
                try
                {
                    ResourceHelper.DisposePictureBoxImage(picBox);

                    picBox.Image = null;
                    ResourceHelper.LoadPictureBoxImage(picBox, imagePath);
                    picBox.Refresh();

                }
                catch (Exception ex) // Other error
                {
                    System.Diagnostics.Debug.WriteLine($"Failed to load image: {ex.Message}");

                    MessageBox.Show($"Wystąpił nieoczekiwany błąd przy wczytywaniu grafiki:\n{ex.Message}", "Błąd Krytyczny");
                    Application.Exit();
                }

                // Set label under vehicle whether it is active or not
                if (isActive)
                {
                    lblStatus.Text = "AKTYWNY";
                    lblStatus.ForeColor = Color.Gold;
                }
                else
                {
                    lblStatus.Text = "DOSTĘPNY";
                    lblStatus.ForeColor = Color.White;
                }
            }
            else
            {
                // Set cursor to default for locked vehicles
                picBox.Cursor = Cursors.Default;
                // If the image after reset is still active
                ResourceHelper.DisposePictureBoxImage(picBox);

                int threshold = PlayerProgress.GetVehicleThreshold(vehicleID);
                // Method for setting conjugation of the word GWIAZDKA depending of the number of stars - currently working for numbers less than 12
                if (threshold > 4 || threshold == 0)
                {
                    lblStatus.Text = $"{threshold} GWIAZDEK";
                }
                else if (threshold == 1)
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

        /// <summary>
        /// Loads the appropriate star image into a PictureBox based on star count.
        /// </summary>
        /// <param name="pictureBox">PictureBox that displays star images</param>
        /// <param name="starCount">Used to load proper image according to the number of the stars collected by the player</param>
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
                ResourceHelper.DisposePictureBoxImage(pictureBox);

                // Load new image
                ResourceHelper.LoadPictureBoxImage(pictureBox, imagePath);
            }
            catch (Exception ex) // Other error
            {
                System.Diagnostics.Debug.WriteLine($"Failed to load image: {ex.Message}");

                MessageBox.Show($"Wystąpił nieoczekiwany błąd przy wczytywaniu grafiki:\n{ex.Message}", "Błąd Krytyczny");
                Application.Exit();
            }
        }

        /// <summary>
        /// Updates the visual state of level buttons based on unlock status.
        /// </summary>
        private void UpdateLevelButtonsDisplay()
        {
            SetLevelButtonDisplay(picLevel1, 1);
            SetLevelButtonDisplay(picLevel2, 2);
            SetLevelButtonDisplay(picLevel3, 3);
        }

        /// <summary>
        /// Sets the visual state of a single level button.
        /// </summary>
        /// <param name="picBox">PictureBox to update</param>
        /// <param name="levelID">Level ID (1, 2, or 3)</param>
        private void SetLevelButtonDisplay(PictureBox picBox, int levelID)
        {
            bool isUnlocked = PlayerProgress.IsLevelUnlocked(levelID);

            // Load normal image
            string imagePath = Path.Combine(graphicsPath, $"poziom{levelID}.png");
            ResourceHelper.LoadPictureBoxImage(picBox, imagePath);

            if (isUnlocked)
            {
                picBox.Cursor = Cursors.Hand;
            }
            else
            {
                picBox.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Method for loading graphics
        /// </summary>
        private void LoadGraphics()
        {
            try
            {
                // Load the image for the Back Button
                string backPath = Path.Combine(graphicsPath, "powrot.png");
                ResourceHelper.LoadPictureBoxImage(picBack, backPath);

                // Load image for the university logo
                string uniPath = Path.Combine(graphicsPath, "pg_logo_czarne.png");
                ResourceHelper.LoadPictureBoxImage(picUni, uniPath);

                // Load image for game logo
                string logoPath = Path.Combine(graphicsPath, "bezpieczna-paczka-logo-nobg.png");
                ResourceHelper.LoadPictureBoxImage(picLogo, logoPath);

                // Load image for star image
                string starPath = Path.Combine(graphicsPath, "stars", "star.png");
                ResourceHelper.LoadPictureBoxImage(picStar, starPath);

                // Load the image for the Reset button
                string resetPath = Path.Combine(graphicsPath, "reset.png");
                ResourceHelper.LoadPictureBoxImage(picReset, resetPath);
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
