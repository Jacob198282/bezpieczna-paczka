using System;
using System.Windows.Forms;

namespace bezpieczna_paczkaApp
{
    // UserControl for the main menu screen.
    public partial class MainMenuControl : UserControl
    {
        // Public event to notify the host form (GameWindow) when the level selection button is clicked.
        public event EventHandler SelectLevelClicked;

        public MainMenuControl()
        {
            InitializeComponent();
            
            DoubleBuffered = true; // removes flickering of the background
        }

        // Handles the Click event for the 'Select Level' button.
        private void picSelectLevel_Click(object sender, EventArgs e)
        {
            // Notify the GameWindow to switch screens.
            SelectLevelClicked?.Invoke(this, EventArgs.Empty);
        }

        // Handles the Click event for the 'Exit Game' button.
        private void picExitGame_Click(object sender, EventArgs e)
        {
            // Safely exits the application.
            Application.Exit();
        }
        private void MainMenuConrol_Load(object sender, EventArgs e)
        {
            // You can add any initialization code here that needs to run
            // after the components are loaded.
            // For example, loading high scores, user settings, etc.
            // This is the best place to load images from files.
            LoadGraphics();
        }


        /// Loads all graphical assets from the 'graphics' folder.

        private void LoadGraphics()
        {
            // Get the directory where the .exe file is running
            string basePath = Application.StartupPath;
            string graphicsPath = Path.Combine(basePath, "graphics");

            try
            {
                // Load the image for the Select Level button
                string selectLevelPath = Path.Combine(graphicsPath, "wybierz-poziom.png");
                picSelectLevel.Image = Image.FromFile(selectLevelPath);

                // Load the image for the logo
                string logoPath = Path.Combine(graphicsPath, "bezpieczna-paczka-logo-nobg.png");
                picLogo.Image = Image.FromFile(logoPath);

                // Load the image for the Exit Button
                string exitPath = Path.Combine(graphicsPath, "wyjdz-z-gry.png");
                picExitGame.Image = Image.FromFile(exitPath);
            }
            catch (FileNotFoundException ex)
            {
                // If a file is missing, show a helpful error message instead of crashing.
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