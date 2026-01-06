using System;
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
        public class LevelSelectedEventArgs : EventArgs
        {
            public int LevelId { get; set; }
        }

        public LevelSelectControl()
        {
            InitializeComponent();
            DoubleBuffered = true; // removes flickering of the background
        }

        private void picLevel1_Click(object sender, EventArgs e)
        {
            // Pass the chosen level ID
            LevelSelected?.Invoke(this, new LevelSelectedEventArgs { LevelId = 1 });
        }

        private void picLevel2_Click(object sender, EventArgs e)
        {
            LevelSelected?.Invoke(this, new LevelSelectedEventArgs { LevelId = 2 });
        }

        private void picLevel3_Click(object sender, EventArgs e)
        {
            LevelSelected?.Invoke(this, new LevelSelectedEventArgs { LevelId = 3 });
        }

        // Loading method for this UserControl
        private void LevelSelectControl_Load(object sender, EventArgs e)
        {

            LoadGraphics(); // Loads images for this UserControl
        }

        /// Handles the Click event for the 'Back' picture button
        private void picBack_Click(object sender, EventArgs e)
        {
            // Notify GameWindow to switch back to Main Menu
            BackClicked?.Invoke(this, EventArgs.Empty);
        }

        /// Loads all graphical assets from the 'graphics' folder

        private void LoadGraphics()
        {
            string projectRoot = Path.GetFullPath(Path.Combine(Application.StartupPath, "..", "..","..")); // Go back three folders
            string graphicsPath = Path.Combine(projectRoot, "res", "graphics");

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
