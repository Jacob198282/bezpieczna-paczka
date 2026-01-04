using System;
using System.Windows.Forms;

namespace bezpieczna_paczkaApp
{

    /// The main welcome screen for the 'Bezpieczna Paczka' game
    /// It displays the logo and navigation buttons.

    public partial class GameWindow
    {

        /// Constructor for the Welcome Form.
        private MainMenuControl mainMenu;
        private LevelSelectControl levelSelect;

        public GameWindow()
        {
            // This method is required for the designer support.
            // It initializes all components placed on the form (defined in GameWindow.Designer.cs).
            InitializeComponent();
            DoubleBuffered = true; // removes flickering from the screen

            // initialization of user controls
            mainMenu = new MainMenuControl();
            levelSelect = new LevelSelectControl();

            // configuration and adding controls to the panel
            SetupControl(mainMenu);
            SetupControl(levelSelect);

            // exception handling by user controls
            mainMenu.SelectLevelClicked += HandleSelectLevelClicked;
            levelSelect.LevelSelected += HandleLevelSelected;
            levelSelect.BackClicked += HandleBackClicked;

            mainMenu.Visible = true; // at first this user control must be visible
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                // Flag 0x02000000 is WS_EX_COMPOSITED
                // Window and child-controls (UserControls) will be drawn
                // in one cycle, which eliminates flickering.
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        // helper function for user control switching
        private void SetupControl(UserControl control)
        {
            control.Dock = DockStyle.Fill;
            control.Visible = false; // hidden by default
            pnlContainer.Controls.Add(control);
        }

        // function for switching between User Controls
        private void SwitchUserControl(UserControl presentControl, UserControl newControl)
        {
            presentControl.Visible = false;
            newControl.Visible = true;
        }

        // --- Event Handlers for UserControl events ---

        /// Handler for the event raised by MainMenuControl when 'Select Level' is clicked.

        private void HandleSelectLevelClicked(object sender, EventArgs e)
        {
            SwitchUserControl(mainMenu, levelSelect); // switch controls
        }

        /// Handler for the event raised by LevelSelectControl when 'Back' is clicked.
        private void HandleBackClicked(object sender, EventArgs e)
        {
            // Call the existing method to switch back to the main menu view.
            SwitchUserControl(levelSelect, mainMenu); // switch controls
        }

        /// Handler for the event raised by LevelSelectControl when a level is chosen.

        private void HandleLevelSelected(object sender, LevelSelectControl.LevelSelectedEventArgs e)
        {
            // Get the pre-loaded level data from the provider
            LevelData data = LevelProvider.GetLevel(e.LevelId);

            // Create the gameplay control with this data
            LevelGameplayControl gameplay = new LevelGameplayControl(data);

            // Setup controls that will be visible during the game
            SetupControl(gameplay);

            // Switch the view
            SwitchUserControl(levelSelect, gameplay);
        }

        // Function for loading stuff for the GameWindow
        private void GameWindow_Load(object sender, EventArgs e)
        {
            LoadBackground();
        }

        // function for loading background from the local directory ./graphics/
        private void LoadBackground()
        {
            string basePath = Application.StartupPath;
            string graphicsPath = Path.Combine(basePath, "graphics");

            try
            {
                string backgroundPath = Path.Combine(graphicsPath, "tlo-startowe.png");
                BackgroundImage = Image.FromFile(backgroundPath);
                BackgroundImageLayout = ImageLayout.Stretch; // Size to fit
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show($"B³¹d wczytywania t³a: Nie znaleziono pliku!\\n{ex.Message}", "B³¹d Pliku");
            }
        }
    }
}
