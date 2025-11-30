using System;
using System.Windows.Forms;

namespace bezpieczna_paczkaApp
{
    
    /// The main welcome screen for the 'Bezpieczna Paczka' game
    /// It displays the logo and navigation buttons.
    
    public partial class StartWindow : Form
    {
        
        /// Constructor for the Welcome Form.
        
        public StartWindow()
        {
            // This method is required for the designer support.
            // It initializes all components placed on the form (defined in StartWindow.Designer.cs).
            InitializeComponent();
        }

        
        /// Handles the Click event for the 'Select Level' picture button.
        
        private void picSelectLevel_Click(object sender, EventArgs e)
        {
            // Placeholder: This is where we will navigate to the level selection screen.
            MessageBox.Show("Przejœcie do ekranu wyboru poziomu (do implementacji).");

            // Example of how you would open a new form and close this one:
            // LevelSelectWindow levelSelect = new LevelSelectWindow();
            // levelSelect.Show();
            // this.Hide(); // or this.Close() if we don't need to come back.
        }

        
        /// Handles the Click event for the 'Exit Game' picture button.
        
        private void picExitGame_Click(object sender, EventArgs e)
        {
            // Safely exits the application.
            Application.Exit();
        }

        
        /// Optional: Actions to perform when the form loads.
        
        private void StartWindow_Load(object sender, EventArgs e)
        {
            // You can add any initialization code here that needs to run
            // after the components are loaded.
            // For example, loading high scores, user settings, etc.
            // This is the best place to load images from files.
            LoadGraphics();
        }

        /// <summary>
        /// Loads all graphical assets from the 'graphics' folder.
        /// </summary>
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

                // string backgroundPath = Path.Combine(graphicsPath, "tlo-startowe.png");
                // this.BackgroundImage = Image.FromFile(backgroundPath);
            }
            catch (FileNotFoundException ex)
            {
                // If a file is missing, show a helpful error message instead of crashing.
                MessageBox.Show($"B³¹d wczytywania grafiki: Nie znaleziono pliku!\n{ex.Message}", "B³¹d Pliku");
                Application.Exit(); // Exit if graphics are missing
            }
            catch (Exception ex)
            {
                // Catch other potential errors (e.g., file is not an image)
                MessageBox.Show($"Wyst¹pi³ nieoczekiwany b³¹d przy wczytywaniu grafiki:\n{ex.Message}", "B³¹d Krytyczny");
                Application.Exit();
            }
        }
    }
}
