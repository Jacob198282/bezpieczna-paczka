/*
 * Copyright (c) Jacob198282 Gdansk University of Technology
 * MIT License
 * Documentation under https://github.com/Jacob198282/bezpieczna-paczka
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bezpieczna_paczkaApp
{
    /// <summary>
    /// UserControl responsible for showing the game
    /// </summary>
    public partial class LevelGameplayControl : UserControl
    {
        /// <summary>
        /// Event raised when the level is completed (all questions answered)
        /// </summary>
        public event EventHandler<LevelCompletedEventArgs>? LevelCompleted;

        /// <summary>
        /// Event raised when the player wants to open the in-level menu (pause/options)
        /// </summary>
        public event EventHandler? MenuRequested;

        /// <summary>
        /// Data for the current level (title, intro, questions)
        /// </summary>
        private readonly LevelData levelData;

        /// <summary>
        /// Index of the currently displayed question (zero-based)
        /// </summary>
        private int currentQuestionIndex;

        /// <summary>
        /// Number of correctly answered questions in this level
        /// </summary>
        private int correctAnswersCount;

        /// <summary>
        /// Total number of question in this level
        /// </summary>
        private int totalQuestionsCount;

        /// <summary>
        /// Maximum number of answer "buttons" (picture boxes) visible on the screen at once
        /// </summary>
        private const int MaxAnswerButtons = 4; // Maximum number of answer options supported in the UI

        /// <summary>
        /// Font for the buttons and labels
        /// </summary>
        public Font buttonFont;
        /// <summary>
        /// Font for answer buttons
        /// </summary>
        public Font answerFont;

        /// <summary>
        /// path to the project
        /// </summary>
        public string projectRoot;
        /// <summary>
        /// path to folder with graphics
        /// </summary>
        public string graphicsPath;
        /// <summary>
        /// path to folder containing scenario pictures
        /// </summary>
        public string scenarioPath;

        /// <summary>
        /// ID of the vehicle used in current gameplay session
        /// </summary>
        public int currentVehicleID;

        /// <summary>
        /// Time limit for each question in seconds
        /// </summary>
        private const int QUESTION_TIME_LIMIT = 30;

        /// <summary>
        /// Timer that counts down for each question
        /// </summary>
        private System.Windows.Forms.Timer? questionTimer;

        /// <summary>
        /// Seconds remaining for current question
        /// </summary>
        private int secondsLeft;

        /// <summary>
        /// Minigame control for package collection
        /// </summary>
        private MiniGameControl? minigame;

        /// <summary>
        /// LevelGameplayControl constructor
        /// </summary>
        /// <param name="level">Which level will be played - with the details provided by the LevelData object</param>
        /// <param name="projectRoot">File Path used for loading graphics to the project root</param>
        /// <param name="graphicsPath">Combined root path and graphics folder used for loading graphics</param>
        /// <exception cref="ArgumentNullException">In case of trying to load null LevelData object</exception>
        public LevelGameplayControl(LevelData level, string projectRoot, string graphicsPath)
        {
            if (level == null)
            {
                throw new ArgumentNullException(nameof(level));
            }

            this.projectRoot = projectRoot;
            this.graphicsPath = graphicsPath;

            scenarioPath = Path.Combine(graphicsPath, "scenario"); // Setting path to folder with scenario images

            levelData = level;

            // Initialize fonts
            buttonFont = new Font("Gill Sans Ultra Bold", 20.25F, FontStyle.Italic, GraphicsUnit.Point, 238);
            answerFont = new Font("Gill Sans MT Condensed", 20F, FontStyle.Regular, GraphicsUnit.Point, 238);

            currentVehicleID = PlayerProgress.GetCurrentVehicleID();

            InitializeComponent();     // Designer-created UI (labels, picture boxes, panels)
            LoadGraphics();            // Load graphical assets such as the menu button image
            InitializeRuntimeState();  // Bind level data and load the first question
        }

        /// <summary>
        /// Initializes runtime state such as labels, question index and score
        /// </summary>
        private void InitializeRuntimeState()
        {
            currentQuestionIndex = 0;
            correctAnswersCount = 0;

            totalQuestionsCount = levelData.Questions.Count; // setting total number of questions provided by LevelData

            // Setup the intro
            SetupIntro();
        }

        /// <summary>
        /// Prepares the initial state of the intro overlay
        /// </summary>
        private void SetupIntro()
        {
            // Step 1: Text Intro
            lblIntroTitle.Text = levelData.LevelTitle;
            lblIntroDescription.Text = levelData.IntroText;

            // Ensure the first step is visible and second is hidden
            pnlIntroStep1.Visible = true;
            pnlIntroStep2.Visible = false;
            pnlIntro.Visible = true;
        }

        /// <summary>
        /// Handler for the 'Next' button in the first part of the intro
        /// </summary>
        /// <param name="sender">Button Next</param>
        /// <param name="e">Event arguments</param>
        private void btnNext_Click(object? sender, EventArgs e)
        {
            // Hiding the text panel
            pnlIntroStep1.Visible = false;

            // Loading the tutorial image based on the level ID
            // Using naming convention 'znaki_poziom_{what level the player is playing}.png'
            string tutorialImgName = $"znaki_poziom_{levelData.LevelID}.png"; // Dynamic name using parameter of _levelData
            string fullPath = Path.Combine(graphicsPath, tutorialImgName);

            // Dispose previous tutorial image if exists
            ResourceHelper.DisposePictureBoxImage(picSignsTutorial);

            ResourceHelper.LoadPictureBoxImage(picSignsTutorial, fullPath);

            // Showing the tutorial panel with the image
            pnlIntroStep2.Visible = true;
        }

        /// <summary>
        /// Handler for the final button that starts the actual game.
        /// </summary>
        /// <param name="sender">Button Start Gameplay</param>
        /// <param name="e">Event arguments</param>
        private void btnStartGameplay_Click(object? sender, EventArgs e)
        {
            // Hide the whole intro overlay
            pnlIntro.Visible = false;
            StartMinigame();
        }

        /// <summary>
        /// Loads a single question by index and updates all UI elements accordingly
        /// </summary>
        /// <param name="questionIndex">Index of the intended question to load</param>
        /// <exception cref="ArgumentOutOfRangeException">In case the index is out of range</exception>
        private void LoadQuestion(int questionIndex)
        {
            // Defensive check to avoid out-of-range errors
            if (questionIndex < 0 || questionIndex >= levelData.Questions.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(questionIndex));
            }

            Question currentQuestion = levelData.Questions[questionIndex];

            // Update question text label
            lblQuestionText.Text = currentQuestion.QuestionText;

            // Update progress label: "Pytanie X / N"
            int displayedQuestionNumber = questionIndex + 1; // Offset to convert zero-based index to human-readable number
            lblProgress.Text = $"Paczka\n{displayedQuestionNumber} / {totalQuestionsCount}";

            // Load scenario image
            LoadScenarioImage(currentQuestion.ScenarioImagePath);

            // Create or update answer "buttons" (picture boxes) for the current question
            DisplayAnswerOptions(currentQuestion.Options);

            // Update score label
            lblScore.Text = $"Dostarczono {correctAnswersCount}";

            // Start countdown timer for this question
            StartTimer();
        }

        /// <summary>
        /// Loads the scenario image from the given file path into the PictureBox
        /// </summary>
        /// <param name="scenarioID">ID of the scenario image needed to build a proper path</param>
        private void LoadScenarioImage(string scenarioID)
        {
            // Dispose previous image to free resources, if needed
            ResourceHelper.DisposePanelBackground(pnlScenario);

            if (string.IsNullOrWhiteSpace(scenarioID))
            {
                // If no image is provided, we simply do not display anything
                return;
            }

            try
            {
                // Getting the name for the folder depending on what level is actually on
                string imagePath = BuildScenarioPath(scenarioID);
                ResourceHelper.LoadPanelBackground(pnlScenario, imagePath);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to load image: {ex.Message}");

                MessageBox.Show(
                    $"Wystąpił nieoczekiwany błąd przy wczytywaniu grafiki pytania:\n{ex.Message}",
                    "Błąd Krytyczny");
            }
        }

        /// <summary>
        /// Builds full path to scenario image based on scenario ID and current vehicle.
        /// </summary>
        /// <param name="scenarioID">Scenario identifier (e.g., "1", "2", "3")</param>
        /// <returns>Full path like "scenario/level_1/1_pojazd_1.png"</returns>
        private string BuildScenarioPath(string scenarioID)
        {
            // Build filename: {scenarioId}_pojazd_{vehicleId}.png
            string fileName = $"{scenarioID}_pojazd_{currentVehicleID}.png";

            // Build folder path based on level ID
            string folderName = $"level_{levelData.LevelID}";
            string scenarioLevelPath = Path.Combine(scenarioPath, folderName); // Setting path to folder containing scenarios for this level

            // Return complete path
            return Path.Combine(scenarioLevelPath, fileName);
        }

        /// <summary>
        /// Creates or updates answer "buttons" using PictureBox controls according to the provided list of options
        /// </summary>
        /// <param name="options">List of the answers to be displayed</param>
        private void DisplayAnswerOptions(List<AnswerOption> options)
        {
            // Clear any existing controls in the answers panel
            CleanupAnswerButtons();

            int buttonsToCreate = options.Count; // check how many answers are for this question

            // Limit the number of displayed options by the defined maximum
            if (buttonsToCreate > MaxAnswerButtons)
            {
                buttonsToCreate = MaxAnswerButtons;
            }

            foreach (AnswerOption option in options)
            {
                // Create a new Button instance
                Button btn = new Button();

                // --- Visual Settings ---
                btn.Text = option.AnswerText;
                btn.Width = 300; // Fixed width, so that up to four buttons could fit in one pnlAnswers panel
                btn.Height = 140; // Fixed height for better touch/click target
                btn.Font = answerFont; // Using publicly created font instead of creating each time new font
                btn.BackColor = Color.WhiteSmoke;
                btn.FlatStyle = FlatStyle.Flat;
                btn.Cursor = Cursors.Hand;
                btn.Margin = new Padding(0, 5, 0, 5); // Add spacing between buttons
                btn.AutoSize = true; // 
                btn.AutoSizeMode = AutoSizeMode.GrowOnly;
                btn.Anchor = AnchorStyles.Top;
                btn.Anchor = AnchorStyles.Bottom;

                // Storing the whole AnswerOption object in the Tag for the click event
                btn.Tag = option;

                // --- Event Handling ---
                btn.Click += HandleAnswerButtonClick;

                // Add the button to the FlowLayoutPanel
                pnlAnswers.Controls.Add(btn);
            }
        }

        /// <summary>
        /// Handles player's click on one of the answer picture boxes
        /// </summary>
        /// <param name="sender">Created answer button</param>
        /// <param name="e">Event arguments</param>
        private void HandleAnswerButtonClick(object? sender, EventArgs e)
        {
            // Stop timer immediately when answer is clicked
            StopTimer();
            
            Button clickedButton;
            AnswerOption selectedOption;
            
            // Casting sender to a Button type to retrieve the attached AnswerOption
            if (sender is Button senderButton)
            {
                clickedButton = senderButton;
            }
            else
            {
                return;
            }

            // If clickedButton is null
            if (clickedButton == null) 
            {
                return;
            }

            // Retrieving attached AnswerOption
            if (clickedButton.Tag is AnswerOption clickedOption)
            {
                selectedOption = clickedOption;
            }
            else
            {
                return;
            }

            if (selectedOption == null)
            {
                return;
            }

            string answerMessage; // String variable holding feedback message after answering the question

            // Evaluate the answer and update score
            if (selectedOption.IsCorrect)
            {
                correctAnswersCount++;
                lblScore.Text = $"Dostarczono {correctAnswersCount}"; // Immediatelly update score label
                answerMessage = "Poprawna odpowiedź!";
            }
            else
            {
                answerMessage = "Niepoprawna odpowiedź!";
            }

                // Show feedback message for the chosen option
                MessageBox.Show(selectedOption.FeedbackMessage, answerMessage);

            // Proceed to the next question or finish the level
            GoToNextQuestionOrFinish();
        }

        /// <summary>
        /// Moves to the next question if available, otherwise completes the level
        /// </summary>
        private void GoToNextQuestionOrFinish()
        {
            int lastQuestionIndex = totalQuestionsCount - 1; // Offset used to get the last index from the total count

            if (currentQuestionIndex < lastQuestionIndex)
            {
                currentQuestionIndex++; // Step to move from current question to the next one
                LoadQuestion(currentQuestionIndex);
            }
            else
            {
                CompleteLevel();
            }
        }

        /// <summary>
        /// Calculates final result, checks if the level is passed and raises the LevelCompleted event
        /// </summary>
        private void CompleteLevel()
        {
            // Somehow incompleted level
            if (totalQuestionsCount <= 0)
            {
                return;
            }

            // Calculate the ratio of the answers
            double correctRatio = (double)correctAnswersCount / totalQuestionsCount;

            // Check if the player passed the test
            bool isPassed = correctRatio >= levelData.PassingThreshold;

            // Calculate number of stars collected by player
            int starsEarned = CalculateStars(correctRatio);

            LevelCompletedEventArgs eventArgs = new LevelCompletedEventArgs
            {
                CorrectAnswersCount = correctAnswersCount,
                TotalQuestionsCount = totalQuestionsCount,
                CorrectRatio = correctRatio,
                StarsEarned = starsEarned,
                IsPassed = isPassed,
                LevelID = levelData.LevelID
            };

            // Raise event
            LevelCompleted?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// Calculates the number of stars earned based on the player's correct answer ratio
        /// </summary>
        /// <param name="correctRatio">Ratio of correct answers as a player's score</param>
        /// <returns>Ratio of the correct answers</returns>
        private int CalculateStars(double correctRatio)
        {
            // Check thresholds from highest to lowest
            // This order ensures we award the maximum applicable stars

            if (correctRatio >= 1.0)
            {
                // Perfect score - all questions answered correctly
                return 3;
            }
            else if (correctRatio >= 0.95)
            {
                // Excellent performance - 95% or more correct
                return 2;
            }
            else if (correctRatio >= 0.90)
            {
                // Good performance - 90% or more correct (minimum to pass)
                return 1;
            }
            else
            {
                // Below passing threshold - level not completed successfully
                return 0;
            }
        }

        /// <summary>
        /// Handles click on the "Menu" picture button to request opening the in-level menu
        /// </summary>
        /// <param name="sender">PictureBox as a menu button</param>
        /// <param name="e">Event arguments</param>
        private void picMenu_Click(object? sender, EventArgs e)
        {
            // Pause timer when opening menu
            StopTimer();

            MenuRequested?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Creates and starts the minigame.
        /// </summary>
        private void StartMinigame()
        {
            // Create minigame if not exists
            if (minigame == null)
            {
                minigame = new MiniGameControl(graphicsPath);
                minigame.Dock = DockStyle.Fill;
                minigame.MinigameCompleted += OnMinigameCompleted;
                minigame.MenuRequested += OnMinigameMenuRequested;
                Controls.Add(minigame);
            }

            // Bring to front and start
            minigame.BringToFront();
            minigame.Visible = true;
            minigame.StartGame();
        }

        /// <summary>
        /// Called when minigame is completed. Starts the question - answer phase.
        /// </summary>
        /// <param name="sender">MiniGame UserControl's methods responsible for counting points</param>
        /// <param name="e">Event arguments</param>
        private void OnMinigameCompleted(object? sender, EventArgs e)
        {
            // Hide minigame
            if (minigame != null)
            {
                minigame.Visible = false;
            }

            // Start questions
            LoadQuestion(currentQuestionIndex);
        }

        /// <summary>
        /// Called when menu is requested from minigame.
        /// </summary>
        /// <param name="sender">PictureBox as a menu button</param>
        /// <param name="e">Event arguments</param>
        private void OnMinigameMenuRequested(object? sender, EventArgs e)
        {
            // Forward the menu request to GameWindow
            MenuRequested?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Cleans up minigame resources.
        /// </summary>
        private void CleanupMinigame()
        {
            if (minigame != null)
            {
                minigame.StopGame();
                minigame.MinigameCompleted -= OnMinigameCompleted;
                minigame.MenuRequested -= OnMinigameMenuRequested;
                Controls.Remove(minigame);
                minigame.Dispose();
                minigame = null;
            }
        }

        /// <summary>
        /// Starts or restarts the countdown timer for current question.
        /// </summary>
        public void StartTimer()
        {
            // Initialize timer if not exists
            if (questionTimer == null)
            {
                questionTimer = new System.Windows.Forms.Timer();
                questionTimer.Interval = 1000; // 1 second
                questionTimer.Tick += OnTimerTick;
            }

            // Reset countdown
            secondsLeft = QUESTION_TIME_LIMIT;
            UpdateTimerDisplay();

            // Start counting
            questionTimer.Start();
        }

        /// <summary>
        /// Stops the countdown timer.
        /// </summary>
        public void StopTimer()
        {
            if (questionTimer != null)
            {
                questionTimer.Stop();
            }
        }

        /// <summary>
        /// Resumes the countdown timer without resetting time.
        /// </summary>
        public void ResumeTimer()
        {
            if (questionTimer != null && secondsLeft > 0)
            {
                questionTimer.Start();
            }
        }

        /// <summary>
        /// Resumes minigame if active, otherwise resumes question timer.
        /// </summary>
        public void ResumeMinigameOrTimer()
        {
            if (minigame != null && minigame.Visible)
            {
                minigame.ResumeGame();
            }
            else
            {
                ResumeTimer();
            }
        }

        /// <summary>
        /// Called every second by the timer. Updates display and handles timeout.
        /// </summary>
        /// <param name="sender">Timer object</param>
        /// <param name="e">Event arguments</param>
        private void OnTimerTick(object? sender, EventArgs e)
        {
            secondsLeft--;

            if (secondsLeft <= 0)
            {
                // Time expired - treat as wrong answer
                OnTimeExpired();
            }
            else
            {
                UpdateTimerDisplay();
            }
        }

        /// <summary>
        /// Updates the timer label text and color based on remaining time.
        /// </summary>
        private void UpdateTimerDisplay()
        {
            lblTimer.Text = $"Czas: {secondsLeft}s";

            // Change color to red when less than 10 seconds
            if (secondsLeft <= 10)
            {
                lblTimer.ForeColor = Color.Red;
            }
            else
            {
                lblTimer.ForeColor = SystemColors.ControlLight;
            }
        }

        /// <summary>
        /// Called when time runs out. Treats as incorrect answer and moves to next question.
        /// </summary>
        private void OnTimeExpired()
        {
            StopTimer();

            // Get current question
            Question currentQuestion = levelData.Questions[currentQuestionIndex];

            // Find correct answer
            string correctAnswerText = "Brak poprawnej odpowiedzi";
            foreach (AnswerOption option in currentQuestion.Options)
            {
                if (option.IsCorrect)
                {
                    correctAnswerText = option.AnswerText;
                    break;
                }
            }

            // Show timeout message with correct answer
            MessageBox.Show(
                $"Czas minął! Nie udzielono odpowiedzi.\n\nPoprawna odpowiedź:\n{correctAnswerText}",
                "Koniec czasu");

            // Move to next question (no points awarded)
            GoToNextQuestionOrFinish();
        }

        /// <summary>
        /// Loads graphical assets required by the gameplay screen
        /// </summary>
        private void LoadGraphics()
        {
            try
            {
                // Dispose previous images before loading new ones
                ResourceHelper.DisposePictureBoxImage(picMenu);
                ResourceHelper.DisposePictureBoxImage(picLogo);
                ResourceHelper.DisposePictureBoxImage(picUni);
                ResourceHelper.DisposePictureBoxImage(picBoss);

                // Load image for the menu button
                string menuButtonPath = Path.Combine(graphicsPath, "menu.png");
                ResourceHelper.LoadPictureBoxImage(picMenu, menuButtonPath);

                // Load image for the game logo
                string logoPath = Path.Combine(graphicsPath, "bezpieczna-paczka-logo-nobg.png");
                ResourceHelper.LoadPictureBoxImage(picLogo, logoPath);

                // Load image for the university logo
                string uniPath = Path.Combine(graphicsPath, "pg_logo_bialy.png");
                ResourceHelper.LoadPictureBoxImage(picUni, uniPath);

                string bossPath = Path.Combine(graphicsPath, "szef.png");
                ResourceHelper.LoadPictureBoxImage(picBoss, bossPath);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to load image: {ex.Message}");

                MessageBox.Show(
                    $"Wystąpił nieoczekiwany błąd przy wczytywaniu grafiki przycisku menu:\n{ex.Message}",
                    "Błąd Krytyczny");
            }
        }

        /// <summary>
        /// Removes event handlers and disposes all dynamically created answer buttons.
        /// </summary>
        private void CleanupAnswerButtons()
        {
            if (pnlAnswers == null)
            {
                return;
            }

            // Iterate through all controls and clean them up
            foreach (Control ctrl in pnlAnswers.Controls)
            {
                if (ctrl is Button btn)
                {
                    // Remove event handler to prevent memory leak
                    btn.Click -= HandleAnswerButtonClick;
                    btn.Tag = null;
                }
            }

            // Clear all controls from panel
            pnlAnswers.Controls.Clear();
        }
    }

    /// <summary>
    /// Contains summary data about the finished level
    /// </summary>
    public class LevelCompletedEventArgs : EventArgs
    {
        /// <summary>
        /// Number of questions answered correctly by the player
        /// </summary>
        public int CorrectAnswersCount { get; set; }

        /// <summary>
        /// Total number of questions in the level
        /// </summary>
        public int TotalQuestionsCount { get; set; }

        /// <summary>
        /// Ratio of correct answers to total questions (0.0 - 1.0)
        /// </summary>
        public double CorrectRatio { get; set; }

        /// <summary>
        /// Indicates whether the player has passed the level according to PassingThreshold
        /// </summary>
        public bool IsPassed { get; set; }

        /// <summary>
        /// The number of stars earned based on the player's performance
        /// </summary>
        public int StarsEarned { get; set; }

        /// <summary>
        /// The unique identifier of the level that was completed
        /// </summary>
        public int LevelID { get; set; }
    }
}