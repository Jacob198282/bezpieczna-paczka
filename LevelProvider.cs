/*
 * Copyright (c) Jacob198282 Gdansk University of Technology
 * MIT License
 * Documentation under https://github.com/Jacob198282/bezpieczna-paczka
 */

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
            // Define basic intro and title for level 1
            string title = "Poziom 1: Pierwszy Dzień Dostawcy";
            string intro = "Witaj w pracy! Twoim zadaniem jest dostarczenie paczek zgodnie z przepisami.\n" +
                          "Pamiętaj, że bezpieczeństwo jest ważniejsze niż pośpiech.";

            LevelData level1 = new LevelData(1, title, intro);

            // --- Question 1 Configuration ---
            Question q1 = new Question(
                "Zbliżasz się do skrzyżowania równorzędnego. Z Twojej prawej nadjeżdża rowerzysta. Co robisz?",
                "1.png" // Background image filename
            );

            q1.AddOption(new AnswerOption(
                "Ustępuję pierwszeństwa rowerzyście",
                true,   // Is correct
                new List<Waypoint>
                { 
                    new Waypoint(227,290,0f),
                    new Waypoint(227,339,90f),
                    new Waypoint(356,291,0f),
                    new Waypoint(437,182,-90f),
                    new Waypoint(438,85,0f),
                    new Waypoint(384,44,-90f),
                    new Waypoint(1,43,0f),
                    new Waypoint(15,5,-90f),
                    new Waypoint(15,238,0f),
                    new Waypoint(167,290,-90f)
                },
                "Brawo! Na skrzyżowaniu równorzędnym obowiązuje zasada prawej ręki."
            ));

            q1.AddOption(new AnswerOption(
                "Przejeżdżam pierwszy, jestem większy",
                false,  // Is incorrect
                new List<Waypoint>
                {
                    new Waypoint(227,290,0f),
                    new Waypoint(227,339,-90f)
                },
                "Błąd! Rowerzysta miał pierwszeństwo. Mogło dojść do kolizji!"
            ));

            level1.Questions.Add(q1);

            return level1;
        }
    }
}