/*
 * Copyright (c) Jacob198282 Gdansk University of Technology
 * MIT License
 * Documentation under https://github.com/Jacob198282/bezpieczna-paczka
 */

using System;
using System.Windows.Forms;

namespace bezpieczna_paczkaApp
{

    /// The main welcome screen for the 'Bezpieczna Paczka' game
    /// It displays the logo and navigation buttons

    public partial class GameWindow
    {

        /// Constructor for the Welcome Form
        private MainMenuControl mainMenu;
        private LevelSelectControl levelSelect;
        private GameMenuControl gameMenu;
        public string projectRoot; // path to the project
        public string graphicsPath; // path to folder with graphics

        public GameWindow()
        {
            // This method is required for the designer support
            // It initializes all components placed on the form (defined in GameWindow.Designer.cs)
            InitializeComponent();
            projectRoot = Path.GetFullPath(Path.Combine(Application.StartupPath, "..", "..", "..")); // Go back three folders
            graphicsPath = Path.Combine(projectRoot, "res", "graphics");
            DoubleBuffered = true; // Removes flickering from the screen

            // Initialization of user controls
            mainMenu = new MainMenuControl(projectRoot,graphicsPath);
            levelSelect = new LevelSelectControl(projectRoot, graphicsPath);
            gameMenu = new GameMenuControl(projectRoot, graphicsPath);

            // Configuration and adding controls to the panel
            SetupControl(mainMenu);
            SetupControl(levelSelect);
            SetupControl(gameMenu);

            // Exception handling by user controls
            mainMenu.SelectLevelClicked += HandleSelectLevelClicked;
            levelSelect.LevelSelected += HandleLevelSelected;
            levelSelect.BackClicked += HandleBackClicked;
            gameMenu.ExitToLevelSelectClicked += HandleExitToLevelSelect;
            gameMenu.ResumeClicked += HandleResumeClicked;

            mainMenu.Visible = true; // At first this user control must be visible
            levelSelect.Visible = false;
            gameMenu.Visible = false;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                // Flag 0x02000000 is WS_EX_COMPOSITED
                // Window and child-controls (UserControls) will be drawn
                // in one cycle, which eliminates flickering
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

        /// Handler for the event raised by MainMenuControl when 'Select Level' is clicked

        private void HandleSelectLevelClicked(object sender, EventArgs e)
        {
            SwitchUserControl(mainMenu, levelSelect); // switch controls
        }

        /// Handler for the event raised by LevelSelectControl when 'Back' is clicked
        private void HandleBackClicked(object sender, EventArgs e)
        {
            // Call the existing method to switch back to the main menu view
            SwitchUserControl(levelSelect, mainMenu); // switch controls
        }

        /// Handler for the event raised by LevelSelectControl when a level is chosen

        private void HandleLevelSelected(object sender, LevelSelectControl.LevelSelectedEventArgs e)
        {
            // Get the pre-loaded level data from the provider
            LevelData data = LevelProvider.GetLevel(e.LevelId);

            // Create the gameplay control with this data
            LevelGameplayControl gameplay = new LevelGameplayControl(data, projectRoot, graphicsPath);

            gameplay.MenuRequested += HandleMenuClicked;
            gameplay.LevelCompleted += HandleLevelCompleted;

            // Setup controls that will be visible during the game
            SetupControl(gameplay);

            // Switch the view
            SwitchUserControl(levelSelect, gameplay);
        }

        /// <summary>
        /// Handler for menu button being clicked during gameplay
        /// </summary>
        private void HandleMenuClicked(object sender, EventArgs e)
        {
            gameMenu.BringToFront();
            gameMenu.Visible = true;
        }

        /// Handles the event raised when a player completes all questions in a level
        private void HandleLevelCompleted(object sender, LevelCompletedEventArgs e)
        {
            // Setting best score of the level
            PlayerProgress.SetStars(e.LevelID, e.StarsEarned);

            string resultMessage;
            string messageTitle;

            if (e.IsPassed)
            {
                // player passed
                resultMessage = $"Gratulacje! Ukonczyles poziom!\n\n" +
                               $"Poprawne odpowiedzi: {e.CorrectAnswersCount}/{e.TotalQuestionsCount}\n" +
                               $"Zdobyte gwiazdki: {e.StarsEarned}/{PlayerProgress.MaxStarsPerLevel - e.StarsEarned}";

                messageTitle = "Poziom ukonczony!";
            }
            else
            {
                // Player did not pass - show encouraging message to try again
                resultMessage = $"Niestety, nie udalo sie ukonczyc poziomu.\n\n" +
                               $"Poprawne odpowiedzi: {e.CorrectAnswersCount}/{e.TotalQuestionsCount}\n" +
                               $"Sprobuj ponownie!";

                messageTitle = "Sprobuj ponownie";
            }

            // Display the result dialog to the player
            MessageBox.Show(resultMessage, messageTitle);

            // This ensures the total stars display is current when player returns
            levelSelect.UpdateStarsDisplay();

            SwitchUserControl((UserControl)sender, levelSelect);
        }

        /// Handler for exiting the game and entering select level menu
        private void HandleExitToLevelSelect(object sender, EventArgs e)
        {
            gameMenu.Visible = false;
            // Find the current gameplay control in the container and switch from it
            if (gameMenu != null)
            {
                SwitchUserControl(gameMenu, levelSelect);
            }
        }

        /// Handler for back button in the in-game menu
        private void HandleResumeClicked(object sender, EventArgs e)
        {
            gameMenu.Visible = false;
        }

        // Function for loading stuff for the GameWindow
        private void GameWindow_Load(object sender, EventArgs e)
        {
            LoadBackground();
        }

        // function for loading background from the local directory ./graphics/
        private void LoadBackground()
        {
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
