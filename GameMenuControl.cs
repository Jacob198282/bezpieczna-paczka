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
        public event EventHandler ResumeClicked;
        public event EventHandler ExitToLevelSelectClicked;
        public GameMenuControl()
        {
            // Initialize component
            InitializeComponent();
            DoubleBuffered = true; // anti-flickering 
            // Load graphics fot the in-game menu
            Dock = DockStyle.Fill;
            LoadGraphics();
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
            string basePath = Application.StartupPath;
            string graphicsPath = Path.Combine(basePath, "graphics");

            try
            {
                // Load image for the back button
                string menuButtonPath = Path.Combine(graphicsPath, "powrot.png");
                picResume.Image = Image.FromFile(menuButtonPath);

                // Load image for level select menu button
                string logoPath = Path.Combine(graphicsPath, "wybierz-poziom.png");
                picLevelSelect.Image = Image.FromFile(logoPath);

                // Load image for the exit button
                string uniPath = Path.Combine(graphicsPath, "wyjdz-z-gry.png");
                picExit.Image = Image.FromFile(uniPath);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(
                    $"Błąd wczytywania grafiki przycisku menu: Nie znaleziono pliku!\n{ex.Message}",
                    "Błąd Pliku");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Wystąpił nieoczekiwany błąd przy wczytywaniu grafiki przycisku menu:\n{ex.Message}",
                    "Błąd Krytyczny");
            }
        }
    }
}
