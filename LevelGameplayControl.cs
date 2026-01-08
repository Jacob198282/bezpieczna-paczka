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
    public partial class LevelGameplayControl : UserControl
    {
        // Event raised when the level is completed (all questions answered)
        public event EventHandler<LevelCompletedEventArgs> LevelCompleted;

        // Event raised when the player wants to open the in-level menu (pause/options)
        public event EventHandler MenuRequested;

        // Data for the current level (title, intro, questions)
        private readonly LevelData _levelData;

        // Index of the currently displayed question (zero-based)
        private int _currentQuestionIndex;

        // Number of correctly answered questions in this level
        private int _correctAnswersCount;

        // Total number of question in this level
        private int totalQuestionsCount;

        // Maximum number of answer "buttons" (picture boxes) visible on the screen at once
        private const int MaxAnswerButtons = 4; // Maximum number of answer options supported in the UI

        // Font for the answer buttons, buttons and labels
        public Font buttonFont = new Font("Gill Sans Ultra Bold", 20.25F, FontStyle.Italic, GraphicsUnit.Point, 238);

        public Font answerFont = new Font("Gill Sans MT Condensed", 18F, FontStyle.Regular, GraphicsUnit.Point, 238);

        public string projectRoot; // path to the project
        public string graphicsPath; // path to folder with graphics
        public string scenarioPath; // path to folder containing scenario pictures

        // ID of the vehicle used in current gameplay session
        public int currentVehicleID;

        public LevelGameplayControl(LevelData levelData, string projectRoot, string graphicsPath)
        {
            if (levelData == null)
            {
                throw new ArgumentNullException(nameof(levelData));
            }

            this.projectRoot = projectRoot;
            this.graphicsPath = graphicsPath;

            scenarioPath = Path.Combine(graphicsPath, "scenario"); // Setting path to folder with scenario images

            _levelData = levelData;

            currentVehicleID = PlayerProgress.GetCurrentVehicleID();

            InitializeComponent();     // Designer-created UI (labels, picture boxes, panels)
            LoadGraphics();            // Load graphical assets such as the menu button image
            InitializeRuntimeState();  // Bind level data and load the first question
        }

        // Initializes runtime state such as labels, question index and score
        private void InitializeRuntimeState()
        {
            _currentQuestionIndex = 0;
            _correctAnswersCount = 0;

            totalQuestionsCount = _levelData.Questions.Count; // setting total number of questions provided by LevelData

            // Setup the intro
            SetupIntro();
        }

        /// <summary>
        /// Prepares the initial state of the intro overlay
        /// </summary>
        private void SetupIntro()
        {
            // Step 1: Text Intro
            lblIntroTitle.Text = _levelData.LevelTitle;
            lblIntroDescription.Text = _levelData.IntroText;

            // Ensure the first step is visible and second is hidden
            pnlIntroStep1.Visible = true;
            pnlIntroStep2.Visible = false;
            pnlIntro.Visible = true;
        }

        /// <summary>
        /// Handler for the 'Next' button in the first part of the intro
        /// </summary>
        private void btnNext_Click(object sender, EventArgs e)
        {
            // Hiding the text panel
            pnlIntroStep1.Visible = false;

            // Loading the tutorial image based on the level ID
            // Using naming convention 'znaki_poziom_{what level the player is playing}.png'
            //string projectRoot = Path.GetFullPath(Path.Combine(Application.StartupPath, "..", "..", "..")); // Go back three folders
            //string graphicsPath = Path.Combine(projectRoot, "res", "graphics");
            string tutorialImgName = $"znaki_poziom_{_levelData.LevelID}.png"; // Dynamic name using parameter of _levelData
            string fullPath = Path.Combine(graphicsPath, tutorialImgName);

            if (File.Exists(fullPath))
            {
                picSignsTutorial.Image = Image.FromFile(fullPath);
            }

            // Showing the tutorial panel with the image
            pnlIntroStep2.Visible = true;
        }

        /// <summary>
        /// Handler for the final button that starts the actual game.
        /// </summary>
        private void btnStartGameplay_Click(object sender, EventArgs e)
        {
            // Hide the whole intro overlay
            pnlIntro.Visible = false;
            LoadQuestion(_currentQuestionIndex);
        }

        // Loads a single question by index and updates all UI elements accordingly
        private void LoadQuestion(int questionIndex)
        {
            // Defensive check to avoid out-of-range errors
            if (questionIndex < 0 || questionIndex >= _levelData.Questions.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(questionIndex));
            }

            //_lastRotation = 0f;

            Question currentQuestion = _levelData.Questions[questionIndex];

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
            lblScore.Text = $"Dostarczono {_correctAnswersCount}";
        }

        // Loads the scenario image from the given file path into the PictureBox
        private void LoadScenarioImage(string scenarioID)
        {
            // Dispose previous image to free resources, if needed
            if (pnlScenario.BackgroundImage != null)
            {
                pnlScenario.BackgroundImage.Dispose();
                pnlScenario.BackgroundImage = null;
            }

            if (string.IsNullOrWhiteSpace(scenarioID))
            {
                // If no image is provided, we simply do not display anything
                return;
            }

            try
            {
                // Getting the name for the folder depending on what level is actually on
                string imagePath = BuildScenarioPath(scenarioID);
                pnlScenario.BackgroundImage = Image.FromFile(imagePath);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(
                    $"Błąd wczytywania grafiki pytania: Nie znaleziono pliku!\n{ex.Message}",
                    "Błąd Pliku");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Wystąpił nieoczekiwany błąd przy wczytywaniu grafiki pytania:\n{ex.Message}",
                    "Błąd Krytyczny");
            }
        }

        /// <summary>
        /// Builds full path to scenario image based on scenario ID and current vehicle.
        /// </summary>
        /// <param name="scenarioId">Scenario identifier (e.g., "1", "2", "3")</param>
        /// <returns>Full path like "scenario/level_1/1_pojazd_1.png"</returns>
        private string BuildScenarioPath(string scenarioID)
        {
            // Build filename: {scenarioId}_pojazd_{vehicleId}.png
            string fileName = $"{scenarioID}_pojazd_{currentVehicleID}.png";

            // Build folder path based on level ID
            string folderName = $"level_{_levelData.LevelID}";
            string scenarioLevelPath = Path.Combine(scenarioPath, folderName); // Setting path to folder containing scenarios for this level

            // Return complete path
            return Path.Combine(scenarioLevelPath, fileName);
        }

        // Creates or updates answer "buttons" using PictureBox controls according to the provided list of options
        private void DisplayAnswerOptions(List<AnswerOption> options)
        {
            // Clear any existing controls in the answers panel
            pnlAnswers.Controls.Clear();

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
                //btn.Anchor = AnchorStyles.Left;
                //btn.Anchor = AnchorStyles.Right;
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

        // Handles player's click on one of the answer picture boxes
        private void HandleAnswerButtonClick(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender; // Casting sender to a Button type to retrieve the attached AnswerOption

            if (clickedButton == null)
            {
                return;
            }

            AnswerOption selectedOption = (AnswerOption)clickedButton.Tag; // Retrieving attached AnswerOption

            if (selectedOption == null)
            {
                return;
            }

            string answerMessage; // String variable holding feedback message after answering the question

            // Evaluate the answer and update score
            if (selectedOption.IsCorrect)
            {
                _correctAnswersCount++;
                lblScore.Text = $"Dostarczono {_correctAnswersCount}"; // Immediatelly update score label
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

        // Moves to the next question if available, otherwise completes the level
        private void GoToNextQuestionOrFinish()
        {
            int lastQuestionIndex = totalQuestionsCount - 1; // Offset used to get the last index from the total count

            if (_currentQuestionIndex < lastQuestionIndex)
            {
                _currentQuestionIndex++; // Step to move from current question to the next one
                LoadQuestion(_currentQuestionIndex);
            }
            else
            {
                CompleteLevel();
            }
        }

        // Calculates final result, checks if the level is passed and raises the LevelCompleted event
        private void CompleteLevel()
        {
            // Somehow incompleted level
            if (totalQuestionsCount <= 0)
            {
                return;
            }

            // Calculate the ratio of the answers
            double correctRatio = (double)_correctAnswersCount / totalQuestionsCount;

            // Check if the player passed the test
            bool isPassed = correctRatio >= _levelData.PassingThreshold;

            // Calculate number of stars collected by player
            int starsEarned = CalculateStars(correctRatio);

            LevelCompletedEventArgs eventArgs = new LevelCompletedEventArgs
            {
                CorrectAnswersCount = _correctAnswersCount,
                TotalQuestionsCount = totalQuestionsCount,
                CorrectRatio = correctRatio,
                StarsEarned = starsEarned,
                IsPassed = isPassed,
                LevelID = _levelData.LevelID
            };

            // Raise event
            LevelCompleted?.Invoke(this, eventArgs);
        }

        /// Calculates the number of stars earned based on the player's correct answer ratio
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

        // Handles click on the "Menu" picture button to request opening the in-level menu
        private void picMenu_Click(object sender, EventArgs e)
        {
            MenuRequested?.Invoke(this, EventArgs.Empty);
        }

        // Loads graphical assets required by the gameplay screen
        private void LoadGraphics()
        {
            try
            {
                // Load image for the menu button
                string menuButtonPath = Path.Combine(graphicsPath, "menu.png");
                picMenu.Image = Image.FromFile(menuButtonPath);

                // Load image for the game logo
                string logoPath = Path.Combine(graphicsPath, "bezpieczna-paczka-logo-nobg.png");
                picLogo.Image = Image.FromFile(logoPath);

                // Load image for the university logo
                string uniPath = Path.Combine(graphicsPath, "pg_logo_czarne.png");
                picUni.Image = Image.FromFile(uniPath);

                // Load van image
                string vanPath = Path.Combine(graphicsPath, "pojazd1.png");
                picVan.Image = Image.FromFile(vanPath);
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

    // Contains summary data about the finished level
    public class LevelCompletedEventArgs : EventArgs
    {
        // Number of questions answered correctly by the player
        public int CorrectAnswersCount { get; set; }

        // Total number of questions in the level
        public int TotalQuestionsCount { get; set; }

        // Ratio of correct answers to total questions (0.0 - 1.0)
        public double CorrectRatio { get; set; }

        // Indicates whether the player has passed the level according to PassingThreshold
        public bool IsPassed { get; set; }

        /// The number of stars earned based on the player's performance
        public int StarsEarned { get; set; }

        /// The unique identifier of the level that was completed
        public int LevelID { get; set; }
    }
}