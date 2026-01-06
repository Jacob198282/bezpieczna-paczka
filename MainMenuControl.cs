using System;
using System.Windows.Forms;

namespace bezpieczna_paczkaApp
{
    // UserControl for the main menu screen
    public partial class MainMenuControl : UserControl
    {
        // Public event to notify the host form (GameWindow) when the level selection button is clicked
        public event EventHandler SelectLevelClicked;

        public MainMenuControl()
        {
            InitializeComponent();
            
            DoubleBuffered = true; // Removes flickering of the background
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
            string projectRoot = Path.GetFullPath(Path.Combine(Application.StartupPath, "..", "..","..")); // Go back three folders
            string graphicsPath = Path.Combine(projectRoot, "res", "graphics");

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

                // Load image for the university logo
                string uniPath = Path.Combine(graphicsPath, "pg_logo_czarne.png");
                picUni.Image = Image.FromFile(uniPath);
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