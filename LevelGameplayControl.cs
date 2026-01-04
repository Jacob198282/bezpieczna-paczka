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

        // Maximum number of answer "buttons" (picture boxes) visible on the screen at once.
        private const int MaxAnswerButtons = 4; // Maximum number of answer options supported in the UI.

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

            // Set the level title label text.
            lblLevelTitle.Text = _levelData.LevelTitle;

            // Directly load the first question.
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
            int QuestionNumberOffset = 1; // Offset to convert zero-based index to human-readable number.
            int displayedQuestionNumber = questionIndex + QuestionNumberOffset;
            int totalQuestionsCount = _levelData.Questions.Count;
            lblProgress.Text = $"Pytanie {displayedQuestionNumber} / {totalQuestionsCount}";

            // Load scenario image.
            LoadScenarioImage(currentQuestion.ScenarioImagePath);

            // Create or update answer "buttons" (picture boxes) for the current question.
            DisplayAnswerOptions(currentQuestion.Options);

            // Update score label.
            lblScore.Text = $"Poprawne odpowiedzi: {_correctAnswersCount}";
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

            int optionsCount = options.Count;
            int buttonsToCreate = optionsCount;

            // Limit the number of displayed options by the defined maximum.
            if (buttonsToCreate > MaxAnswerButtons)
            {
                buttonsToCreate = MaxAnswerButtons;
            }

            for (int optionIndex = 0; optionIndex < buttonsToCreate; optionIndex++)
            {
                AnswerOption option = options[optionIndex];

                PictureBox answerPicture = new PictureBox();

                // Store the AnswerOption instance in the Tag property for later retrieval.
                answerPicture.Tag = option;

                // Configure visual and interaction properties of the answer picture.
                answerPicture.BackColor = Color.Transparent;
                answerPicture.Cursor = Cursors.Hand;
                answerPicture.SizeMode = PictureBoxSizeMode.Zoom;

                // At this stage we do not have specific image files for each answer.
                // TODO: Assign an image for this answer option based on AnswerOption data or index.

                // Subscribe to the Click event once per picture box.
                answerPicture.Click += HandleAnswerButtonClick;

                // Let the layout of picture boxes be handled by the panel's layout (e.g., FlowLayoutPanel).
                pnlAnswers.Controls.Add(answerPicture);
            }
        }

        // Handles player's click on one of the answer picture boxes.
        private void HandleAnswerButtonClick(object sender, EventArgs e)
        {
            PictureBox clickedPicture = sender as PictureBox;

            if (clickedPicture == null)
            {
                return;
            }

            AnswerOption selectedOption = clickedPicture.Tag as AnswerOption;

            if (selectedOption == null)
            {
                return;
            }

            // Evaluate the answer and update score.
            if (selectedOption.IsCorrect)
            {
                int correctAnswerIncrement = 1; // Number of points for a single correct answer.
                _correctAnswersCount += correctAnswerIncrement;
            }

            // Show feedback message for the chosen option.
            MessageBox.Show(selectedOption.FeedbackMessage, "Informacja");

            // Placeholder for running the van animation based on the chosen option.
            PlayAnswerAnimation(selectedOption);

            // Proceed to the next question or finish the level.
            GoToNextQuestionOrFinish();
        }

        // Plays the animation of the van according to the selected answer.
        // This is a placeholder method that will be implemented later.
        private void PlayAnswerAnimation(AnswerOption selectedOption)
        {
            // TODO: Implement animation logic using selectedOption.DestinationX, DestinationY and DestinationRotation.
            // At this stage, we only keep the method stub to show the future responsibility.
        }

        // Moves to the next question if available, otherwise completes the level.
        private void GoToNextQuestionOrFinish()
        {
            int lastQuestionIndexOffset = 1; // Offset used to get the last index from the total count.
            int lastQuestionIndex = _levelData.Questions.Count - lastQuestionIndexOffset;

            if (_currentQuestionIndex < lastQuestionIndex)
            {
                int nextQuestionIncrement = 1; // Step to move from current question to the next one.
                _currentQuestionIndex += nextQuestionIncrement;
                LoadQuestion(_currentQuestionIndex);
            }
            else
            {
                CompleteLevel();
            }
        }

        // Calculates final result, checks if the level is passed and raises the LevelCompleted event.
        private void CompleteLevel()
        {
            int totalQuestionsCount = _levelData.Questions.Count;

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

        // Handles click on the "Menu" picture button to request opening the in-level menu.
        private void picMenu_Click(object sender, EventArgs e)
        {
            MenuRequested?.Invoke(this, EventArgs.Empty);
        }

        // Loads graphical assets required by the gameplay screen.
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

    // Contains summary data about the finished level.
    public class LevelCompletedEventArgs : EventArgs
    {
        // Number of questions answered correctly by the player.
        public int CorrectAnswersCount { get; set; }

        // Total number of questions in the level.
        public int TotalQuestionsCount { get; set; }

        // Ratio of correct answers to total questions (0.0 - 1.0).
        public double CorrectRatio { get; set; }

        // Indicates whether the player has passed the level according to PassingThreshold.
        public bool IsPassed { get; set; }
    }
}