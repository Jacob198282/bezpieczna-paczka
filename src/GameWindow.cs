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
    /// The main welcome screen for the 'Bezpieczna Paczka' game
    /// It displays the logos and navigation buttons
    /// </summary>
    public partial class GameWindow
    {
        private MainMenuControl mainMenu;
        private LevelSelectControl levelSelect;
        private GameMenuControl gameMenu;
        private LevelGameplayControl? currentGameplay; // Nullable - it can not exist
        /// <summary>
        /// path to the project
        /// </summary>
        public string projectRoot;
        /// <summary>
        /// path to folder with graphics
        /// </summary>
        public string graphicsPath; 

        /// <summary>
        /// Constructor for the GameWindow form
        /// </summary>
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

            PlayerProgress.Initialize(projectRoot);
            PlayerProgress.LoadProgress();
        }

        /// <summary>
        /// Method for stopping flickering of the applications while changing UserControls
        /// </summary>
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

        /// <summary>
        /// Helper function for user control switching
        /// </summary>
        /// <param name="control">UserControl object which controls must be added to the pnlContainer</param>
        private void SetupControl(UserControl control)
        {
            control.Dock = DockStyle.Fill;
            control.Visible = false; // hidden by default
            pnlContainer.Controls.Add(control);
        }

        /// <summary>
        /// Function for switching between User Controls
        /// </summary>
        /// <param name="presentControl">Actually visible UserControl and the one to be hide now</param>
        /// <param name="newControl">userControl that is wanted to be visible now</param>
        private void SwitchUserControl(UserControl presentControl, UserControl newControl)
        {
            presentControl.Visible = false;
            newControl.Visible = true;
        }

        // --- Event Handlers for UserControl events ---

        /// <summary>
        /// Handler for the event raised by MainMenuControl when 'Select Level' is clicked
        /// </summary>
        /// <param name="sender">PictureBox that after click sends an event to switch from main menu to level select menu</param>
        /// <param name="e">Event arguments</param>
        private void HandleSelectLevelClicked(object? sender, EventArgs e)
        {
            SwitchUserControl(mainMenu, levelSelect); // switch controls
        }

        /// <summary>
        /// Handler for the event raised by LevelSelectControl when 'Back' is clicked
        /// </summary>
        /// <param name="sender">PictureBox that after click sends an event to switch from level select menu to main menu</param>
        /// <param name="e"></param>
        private void HandleBackClicked(object? sender, EventArgs e)
        {
            // Call the existing method to switch back to the main menu view
            SwitchUserControl(levelSelect, mainMenu); // switch controls
        }

        /// <summary>
        /// Handler for the event raised by LevelSelectControl when a level is chosen
        /// </summary>
        /// <param name="sender">PictureBox responsible for showing which level the player will play, e.g. Level 1, Level 2...</param>
        /// <param name="e">Event arguments</param>
        private void HandleLevelSelected(object? sender, LevelSelectControl.LevelSelectedEventArgs e)
        {
            CleanupCurrentGameplay();

            // Get the pre-loaded level data from the provider
            LevelData data = LevelProvider.GetLevel(e.LevelId);

            // Create the gameplay control with this data
            currentGameplay = new LevelGameplayControl(data, projectRoot, graphicsPath);

            currentGameplay.MenuRequested += HandleMenuClicked;
            currentGameplay.LevelCompleted += HandleLevelCompleted;

            // Setup controls that will be visible during the game
            SetupControl(currentGameplay);

            // Switch the view
            SwitchUserControl(levelSelect, currentGameplay);
        }

        /// <summary>
        /// Handler for menu button being clicked during gameplay
        /// </summary>
        /// <param name="sender">PictureBox responsible for menu button</param>
        /// <param name="e">Event arguments</param>
        private void HandleMenuClicked(object? sender, EventArgs e)
        {
            gameMenu.BringToFront();
            gameMenu.Visible = true;
        }

        /// <summary>
        /// Handles the event raised when a player completes all questions in a level
        /// </summary>
        /// <param name="sender">Method inside LevelGameplayControl class that completes the level</param>
        /// <param name="e">Event arugments</param>
        private void HandleLevelCompleted(object? sender, LevelCompletedEventArgs e)
        {
            // Setting best score of the level
            PlayerProgress.SetStars(e.LevelID, e.StarsEarned);

            PlayerProgress.SaveProgress();

            string resultMessage;
            string messageTitle;

            if (e.IsPassed)
            {
                // Player passed
                resultMessage = $"Gratulacje! Ukoñczy³eœ poziom!\n\n" +
                               $"Poprawne odpowiedzi: {e.CorrectAnswersCount}/{e.TotalQuestionsCount}\n" +
                               $"Zdobyte gwiazdki: {e.StarsEarned}/{PlayerProgress.MaxStarsPerLevel}";

                messageTitle = "Poziom ukoñczony!";
            }
            else
            {
                // Player did not pass - show encouraging message to try again
                resultMessage = $"Niestety, nie uda³o sie ukoñczyæ poziomu.\n\n" +
                               $"Poprawne odpowiedzi: {e.CorrectAnswersCount}/{e.TotalQuestionsCount}\n" +
                               $"Spróbuj ponownie!";

                messageTitle = "Spróbuj ponownie";
            }

            // Display the result dialog to the player
            MessageBox.Show(resultMessage, messageTitle);

            // This ensures the total stars display is current when player returns
            levelSelect.UpdateStarsDisplay();

            CleanupCurrentGameplay();

            // Cast sender to UserControl if not null
            if (sender is UserControl senderControl)
            {
                SwitchUserControl(senderControl, levelSelect);
            }
            else
            {
                levelSelect.Visible = true;
            }
        }

        /// <summary>
        /// Removes and disposes the current gameplay control if it exists.
        /// </summary>
        private void CleanupCurrentGameplay()
        {
            if (currentGameplay != null)
            {
                // Stop timer before disposal
                currentGameplay.StopTimer();

                // Remove event handlers to prevent memory leaks
                currentGameplay.MenuRequested -= HandleMenuClicked;
                currentGameplay.LevelCompleted -= HandleLevelCompleted;

                // Remove from container
                pnlContainer.Controls.Remove(currentGameplay);

                // Dispose resources
                currentGameplay.Dispose();
                currentGameplay = null;
            }
        }

        /// <summary>
        /// Handler for exiting the game and entering select level menu
        /// </summary>
        /// <param name="sender">PictureBox in LevelSelectControl responsible for exiting from game to level select menu</param>
        /// <param name="e">Event arguments</param>
        private void HandleExitToLevelSelect(object? sender, EventArgs e)
        {
            gameMenu.Visible = false;

            CleanupCurrentGameplay();

            if (gameMenu != null)
            {
                SwitchUserControl(gameMenu, levelSelect);
            }
        }

        /// <summary>
        /// Handler for back button in the in-game menu
        /// </summary>
        /// <param name="sender">PictureBox used as back button</param>
        /// <param name="e">Event arguments</param>
        private void HandleResumeClicked(object? sender, EventArgs e)
        {
            gameMenu.Visible = false;

            // Resume timer in current gameplay
            if (currentGameplay != null)
            {
                currentGameplay.ResumeMinigameOrTimer();
            }
        }

        /// <summary>
        /// Function for loading stuff for the GameWindow
        /// </summary>
        /// <param name="sender">GameWindow form</param>
        /// <param name="e">Event arguments</param>
        private void GameWindow_Load(object? sender, EventArgs e)
        {
            LoadBackground();
        }

        /// <summary>
        /// Function for loading background from the local directory ./graphics/
        /// </summary>
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
