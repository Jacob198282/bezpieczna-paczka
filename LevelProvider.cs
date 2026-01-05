using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bezpieczna_paczkaApp
{
    /// <summary>
    /// Static factory class that manages level data.
    /// It ensures each LevelData object is created only once (Caching/Singleton pattern).
    /// </summary>
    public static class LevelProvider
    {
        // Dictionary to store created levels. Key: Level ID, Value: LevelData object.
        private static readonly Dictionary<int, LevelData> _levelsCache = new Dictionary<int, LevelData>();

        /// <summary>
        /// Returns the requested level data. Creates it if it doesn't exist in cache.
        /// </summary>
        /// <param name="levelId">The ID of the level to retrieve (e.g., 1, 2, 3)</param>
        /// <returns>Pre-loaded LevelData object</returns>
        public static LevelData GetLevel(int levelId)
        {
            // Check if we already created this level before
            if (!_levelsCache.ContainsKey(levelId))
            {
                // If not, create it and add to our memory (cache)
                _levelsCache[levelId] = CreateLevelById(levelId);
            }

            return _levelsCache[levelId];
        }

        /// <summary>
        /// Internal router to decide which level creation method to call.
        /// </summary>
        private static LevelData CreateLevelById(int id)
        {
            switch (id)
            {
                case 1: // initialize level 1
                    return InitializeLevel1();
                case 2: // initialize level 2

                case 3: // initialize level 3


                default:
                    throw new ArgumentException($"Level with ID {id} is not defined in LevelProvider.");
            }
        }

        /// <summary>
        /// Defines the content, questions, and animations for Level 1.
        /// </summary>
        private static LevelData InitializeLevel1()
        {
            // Define basic intro and title
            string title = "Poziom 1: Pierwszy Dzień Dostawcy";
            string intro = "Witaj w pracy! Twoim zadaniem jest dostarczenie paczek zgodnie z przepisami.\n" +
                          "Pamiętaj, że bezpieczeństwo jest ważniejsze niż pośpiech.";

            LevelData level1 = new LevelData(1, title, intro);

            // --- Question 1 Configuration ---
            // Let's assume the van starts at the bottom of a T-junction
            Question q1 = new Question(
                "Zbliżasz się do skrzyżowania równorzędnego. Z Twojej prawej nadjeżdża rowerzysta. Co robisz?",
                "" // Background image filename
            );

            // Correct Option: Yield priority
            // Animation: Van stays at a safe distance or moves slightly then stops
            q1.AddOption(new AnswerOption(
                "Ustępuję pierwszeństwa rowerzyście",
                true,   // Is correct
                600,    // Target X (Stay in lane)
                500,    // Target Y (Stop before intersection)
                0f,     // No rotation
                "Brawo! Na skrzyżowaniu równorzędnym obowiązuje zasada prawej ręki."
            ));

            // Incorrect Option: Drive through
            // Animation: Van drives to the middle of the intersection (potential crash scenario)
            int crashX = 600;
            int crashY = 300;
            q1.AddOption(new AnswerOption(
                "Przejeżdżam pierwszy, jestem większy",
                false,  // Is incorrect
                crashX,
                crashY,
                0f,
                "Błąd! Rowerzysta miał pierwszeństwo. Mogło dojść do kolizji!"
            ));

            level1.Questions.Add(q1);

            return level1;
        }
    }
}