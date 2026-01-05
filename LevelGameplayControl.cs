using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace bezpieczna_paczkaApp
{
    public partial class LevelGameplayControl : UserControl
    {
        // Event raised when the level is completed (all questions answered).
        public event EventHandler<LevelCompletedEventArgs> LevelCompleted;

        // Event raised when the player wants to open the in-level menu (pause/options).
        public event EventHandler MenuRequested;

        // Data for the current level (title, intro, questions).
        private readonly LevelData _levelData;

        // Index of the currently displayed question (zero-based).
        private int _currentQuestionIndex;

        // Number of correctly answered questions in this level.
        private int _correctAnswersCount;

        // Total number of question in this level.
        private int totalQuestionsCount;

        // Maximum number of answer "buttons" (picture boxes) visible on the screen at once.
        private const int MaxAnswerButtons = 4; // Maximum number of answer options supported in the UI.

        // Font for the answer buttons, buttons and labels
        public Font buttonFont = new Font("Gill Sans Ultra Bold", 20.25F, FontStyle.Italic, GraphicsUnit.Point, 238);

        public LevelGameplayControl(LevelData levelData)
        {
            if (levelData == null)
            {
                throw new ArgumentNullException(nameof(levelData));
            }

            _levelData = levelData;

            InitializeComponent();     // Designer-created UI (labels, picture boxes, panels).
            LoadGraphics();            // Load graphical assets such as the menu button image.
            InitializeRuntimeState();  // Bind level data and load the first question.
        }

        // Initializes runtime state such as labels, question index and score.
        private void InitializeRuntimeState()
        {
            _currentQuestionIndex = 0;
            _correctAnswersCount = 0;

            totalQuestionsCount = _levelData.Questions.Count; // setting total number of questions provided by LevelData

            // Setup the intro
            SetupIntro();
        }

        /// <summary>
        /// Prepares the initial state of the intro overlay.
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
        /// Handler for the 'Next' button in the first part of the intro.
        /// </summary>
        private void btnNext_Click(object sender, EventArgs e)
        {
            // 1. Hide the text panel
            pnlIntroStep1.Visible = false;

            // 2. Load the tutorial image based on the level ID
            // We can use a naming convention like 'signs_level_1.png'
            string graphicsPath = Path.Combine(Application.StartupPath, "graphics");
            string tutorialImgName = $"znaki_poziom_{_levelData.LevelID}.png"; // Example dynamic name
            string fullPath = Path.Combine(graphicsPath, tutorialImgName);

            if (File.Exists(fullPath))
            {
                picSignsTutorial.Image = Image.FromFile(fullPath);
            }

            // 3. Show the tutorial panel
            pnlIntroStep2.Visible = true;
        }

        /// <summary>
        /// Handler for the final button that starts the actual game.
        /// </summary>
        private void btnStartGameplay_Click(object sender, EventArgs e)
        {
            // Hide the whole intro overlay
            pnlIntro.Visible = false;
            //_currentQuestionIndex = 0;
            LoadQuestion(_currentQuestionIndex);
        }

        // Loads a single question by index and updates all UI elements accordingly.
        private void LoadQuestion(int questionIndex)
        {
            // Defensive check to avoid out-of-range errors.
            if (questionIndex < 0 || questionIndex >= _levelData.Questions.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(questionIndex));
            }

            Question currentQuestion = _levelData.Questions[questionIndex];

            // Update question text label.
            lblQuestionText.Text = currentQuestion.QuestionText;

            // Update progress label: "Pytanie X / N".
            int displayedQuestionNumber = questionIndex + 1; // Offset to convert zero-based index to human-readable number.
            lblProgress.Text = $"Paczka\n{displayedQuestionNumber} / {totalQuestionsCount}";

            // Load scenario image.
            LoadScenarioImage(currentQuestion.ScenarioImagePath);

            // Create or update answer "buttons" (picture boxes) for the current question.
            DisplayAnswerOptions(currentQuestion.Options);

            // Update score label.
            lblScore.Text = $"Dostarczono {_correctAnswersCount}";
        }

        // Loads the scenario image from the given file path into the PictureBox.
        private void LoadScenarioImage(string imagePath)
        {
            // Dispose previous image to free resources, if needed.
            if (picScenario.Image != null)
            {
                picScenario.Image.Dispose();
                picScenario.Image = null;
            }

            if (string.IsNullOrWhiteSpace(imagePath))
            {
                // If no image is provided, we simply do not display anything.
                return;
            }

            try
            {
                picScenario.Image = Image.FromFile(imagePath);
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

        // Creates or updates answer "buttons" using PictureBox controls according to the provided list of options.
        private void DisplayAnswerOptions(List<AnswerOption> options)
        {
            // Clear any existing controls in the answers panel.
            pnlAnswers.Controls.Clear();

            int buttonsToCreate = options.Count; // check how many answers are for this question

            // Limit the number of displayed options by the defined maximum.
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
                btn.Font = buttonFont; // Using publicly created font instead of creating each time new font
                btn.BackColor = Color.WhiteSmoke;
                btn.FlatStyle = FlatStyle.Flat;
                btn.Cursor = Cursors.Hand;
                btn.Margin = new Padding(0, 5, 0, 5); // Add spacing between buttons

                // Storing the whole AnswerOption object in the Tag for the click event
                btn.Tag = option;

                // --- Event Handling ---
                btn.Click += HandleAnswerButtonClick;

                // Add the button to the FlowLayoutPanel
                pnlAnswers.Controls.Add(btn);
            }
        }

        // Handles player's click on one of the answer picture boxes.
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
                lblScore.Text = $"Dostarczono: {_correctAnswersCount}"; // Immediatelly update score label
                answerMessage = "Poprawna odpowiedź!";
            }
            else
            {
                answerMessage = "Niepoprawna odpowiedź!";
            }

                // Show feedback message for the chosen option.
                MessageBox.Show(selectedOption.FeedbackMessage, answerMessage);

            // Placeholder for running the van animation based on the chosen option
            PlayAnswerAnimation(selectedOption);

            // Proceed to the next question or finish the level
            GoToNextQuestionOrFinish();
        }

        // Plays the animation of the van according to the selected answer
        // This is a placeholder method that will be implemented later
        private void PlayAnswerAnimation(AnswerOption selectedOption)
        {
            // TODO: Implement animation logic using selectedOption.DestinationX, DestinationY and DestinationRotation
            // At this stage, we only keep the method stub to show the future responsibility
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

            if (totalQuestionsCount <= 0)
            {
                return;
            }

            double correctRatio = (double)_correctAnswersCount / totalQuestionsCount;

            bool isPassed = correctRatio >= _levelData.PassingThreshold;

            LevelCompletedEventArgs eventArgs = new LevelCompletedEventArgs
            {
                CorrectAnswersCount = _correctAnswersCount,
                TotalQuestionsCount = totalQuestionsCount,
                CorrectRatio = correctRatio,
                IsPassed = isPassed
            };

            LevelCompleted?.Invoke(this, eventArgs);
        }

        // Handles click on the "Menu" picture button to request opening the in-level menu
        private void picMenu_Click(object sender, EventArgs e)
        {
            MenuRequested?.Invoke(this, EventArgs.Empty);
        }

        // Loads graphical assets required by the gameplay screen
        private void LoadGraphics()
        {
            string basePath = Application.StartupPath;
            string graphicsPath = Path.Combine(basePath, "graphics");

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
    }
}