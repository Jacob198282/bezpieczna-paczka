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
                "Zbliżasz się do skrzyżowania ze znakiem widocznym na rysunku. W którę stronę pojedziesz, aby dotrzeć do celu?",
                "1" // Background image filename
            );

            q1.AddOption(new AnswerOption(
                "W prawo",
                true,   // Is correct
                "Brawo! Znak wskazuje nakaz jazdy w prawo przed znakiem."
            ));

            q1.AddOption(new AnswerOption(
                "W lewo",
                false,  // Is incorrect
                "Błąd! Według znaku nie można skręcić w lewo. Mogło być niebezpiecznie!\n Prawidłowa odpowiedź to w prawo"
            ));

            level1.Questions.Add(q1);

            Question q2 = new Question(
                "Przed tobą budynek ratusza i chciałbyś dojechać do budynku szkoły. Gdyby istniała inna droga, mógłbyś skręcić w lewo lub pojechać prosto?",
                "2"
            );

            q2.AddOption(new AnswerOption(
                "Mogę jechać prosto. \nTrzeba coś załatwić w ratuszu.",
                false,
                "Błąd! Mogłeś trafić na autokar! \nPrawidłowa odpowiedź to w prawo!"
            ));

            q2.AddOption(new AnswerOption(
                "Mogę jechać w lewo. \nMoże później przyjadę do szkoły.",
                false,
                "Błąd! \nPrawidłowa odpowiedź to w prawo!"
            ));

            q2.AddOption(new AnswerOption(
                "W prawo. \nTrzeba dostarczyć paczki jak najszybciej.",
                true,
                "Brawo! Znak wskazuje nakaz jazdy w prawo za znakiem!"
            ));

            level1.Questions.Add(q2);

            Question q3 = new Question(
                "Przejechałeś miejsce dostawy. Czy możesz zawrócić na tym skrzyżowaniu?",
                "3"
            );

            q3.AddOption(new AnswerOption(
                "Nie ma problemu, \ntak będzie najszybciej! ",
                false,
                "Błąd! Widząc ten znak nie można zawracać na tym skrzyżowaniu!"
            ));

            q3.AddOption(new AnswerOption(
                "Muszę pojechać\n w prawo zgodnie ze znakiem",
                true,
                "Brawo! Znak wskazuje nakaz jazdy w lewo przed znakiem i nie można zawracać."
            ));

            level1.Questions.Add(q3);

            return level1;
        }
    }
}