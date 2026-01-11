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
    /// Static factory class that manages level data
    /// It ensures each LevelData object is created only once
    /// </summary>
    public static class LevelProvider
    {
        /// <summary>
        /// Dictionary to store created levels. Key: Level ID, Value: LevelData object.
        /// </summary>
        private static readonly Dictionary<int, LevelData> _levelsCache = new Dictionary<int, LevelData>();

        /// <summary>
        /// Returns the requested level data. Creates it if it doesn't exist in cache
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
        /// Internal router to decide which level creation method to call
        /// </summary>
        /// <param name="id">ID of a level which will be loaded</param>
        /// <returns>Points to method that creats an object of type LevelData with questions</returns>
        /// <exception cref="ArgumentException">In case there is no level of such ID</exception>
        private static LevelData CreateLevelById(int id)
        {
            switch (id)
            {
                case 1: // initialize level 1
                    return InitializeLevel1();
                case 2: // initialize level 2
                    return InitializeLevel2();
                case 3: // initialize level 3
                    return InitializeLevel3();

                default:
                    throw new ArgumentException($"Level with ID {id} is not defined in LevelProvider.");
            }
        }

        /// <summary>
        /// Defines the content and questions for Level 1
        /// </summary>
        /// <returns>Level Data object with questions for the learning section</returns>
        private static LevelData InitializeLevel1()
        {
            // Define basic intro and title for level 1
            string title = "Poziom 1: Pierwszy dzień dostawcy";
            string intro = "Witaj w pracy! Twoim zadaniem jest dostarczenie paczek zgodnie z przepisami.\n" +
                          "W związku z tym ważne, żebyś dobrze zapoznał się z zasadami ruchu drogowego, które będą Ci przedstawione w tej grze. " +
                          "Ale zanim do tego dojdziemy przedstawię Ci plan Twojego dnia:\n" +
                          "1. Na początku będziesz zbierał paczki do Twojego samochodu dostawczego. Twoim zadaniem jest zebranie określonej ilości, " +
                          "a następnie dostarczenie ich do odbiorców w kolejnej sekcji. \n" +
                          "Aby zebrać paczkę, musisz tak ustawić wóz, byś na nią najechał. Zrobisz to poruszając się w lewo za pomocą przycisku A " +
                          "lub w prawo za pomocą przycisku D. \n" +
                          "2. Następnie przystąpisz do dostarczania paczek - będą Ci się wyświetlały pytania oraz przyciski z odpowiedziami. " +
                          "Gdy tylko odpowiesz poprawnie na odpowiedź Twoja paczka zostanie pomyślnie dostarczona!\n" +
                          "Im więcej paczek dostarczysz, tym większe moje zadowolenie z Ciebie jako naszego pracownika wystawiona w gwiazdkach\n" +
                          "- 90% dostarczonych paczek = 1 gwiazdka, \n" +
                          "- 95% dostarczonych paczek = 2 gwiazdki, \n" +
                          "- 100% dostarczonych paczek = 3 gwiazdki. \n" +
                          "Zdobywając gwiazdki masz szansę na odblokowanie nowych pojazdów! \n" +
                          "Klikając 'Dalej', pokażą Ci się ZNAKI NAKAZU, z którymi najpierw będziesz się zapoznawać. " +
                          "Następnie klikając 'Rozpocznij grę' zaczniesz zbieranie paczek. \n" +
                          "POWODZENIA!!!";

            LevelData level1 = new LevelData(1, title, intro);

            // Question 1 configuration
            Question q1 = new Question(
                "Zbliżasz się do skrzyżowania ze znakiem widocznym na rysunku. W którę stronę pojedziesz, aby dotrzeć do celu?",
                "1" // Background image filename
            );

            q1.AddOption(new AnswerOption(
                "W prawo",
                true,   // Is correct
                "Znak wskazuje nakaz jazdy w prawo przed znakiem."
            ));

            q1.AddOption(new AnswerOption(
                "W lewo",
                false,  // Is incorrect
                "Błąd! Według znaku nie można skręcić w lewo. Mogło być niebezpiecznie!\n Prawidłowa odpowiedź to W PRAWO"
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
                "Znak wskazuje nakaz jazdy w prawo za znakiem!"
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
                "Muszę pojechać\n w lewo zgodnie ze znakiem",
                true,
                "Znak wskazuje nakaz jazdy w lewo przed znakiem i nie można zawracać."
            ));

            level1.Questions.Add(q3);

            Question q4 = new Question(
                "Miejsce docelowe znajduje się tuż za czerwonym samochodem. Któredy możesz pojechać, po do niego dotrzeć?",
                "4"
            );

            q4.AddOption(new AnswerOption(
                "W prawo",
                false,
                "Błąd! Znak nakazuje skręcić w lewo za znakiem."
            ));

            q4.AddOption(new AnswerOption(
                "Prosto. Wyminiemy czerwony samochodzik.",
                false,
                "Błąd! Znak nakazuje skręcić w lewo za znakiem."
            ));

            q4.AddOption(new AnswerOption(
                "W lewo. Trochę dłuższa droga nie zaszkodzi.",
                true,
                "Znak nakazuje skręcić w lewo za nim."
            ));

            level1.Questions.Add(q4);

            Question q5 = new Question(
                "Chcąc przebić się przez gąszcz zabudowań dojeżdżasz do następującego znaku. Co robisz?",
                "5"
            );

            q5.AddOption(new AnswerOption(
                "Skręcam w lewo",
                false,
                "Błąd! Znak nakazuje jazdę prosto za znakiem"
            ));

            q5.AddOption(new AnswerOption(
                "Jadę prosto",
                true,
                "Za tym znakiem należy jechać prosto"
            ));

            q5.AddOption(new AnswerOption(
                "W prawo. Zrobię parę okrążeń i wtedy dostarczę paczkę",
                false,
                "Błąd! Znak nakazuje jazdę prosto za znakiem"
            ));

            level1.Questions.Add(q5);

            Question q6 = new Question(
                "Przesyłka z kadzidłem do kościoła. Którędy pojedziesz?",
                "6"
            );

            q6.AddOption(new AnswerOption(
                "Prosto lub w lewo,\n gdyby była tam jakaś droga...",
                true,
                "Znak wskazuje dokładnie na jazdę prosto lub w lewo i nie można zawrócić"
            ));

            q6.AddOption(new AnswerOption(
                "W prawo. Muszę \nzostawić znicze na cmentarzu",
                false,
                "Błąd! Za znakiem można jechać tylko prosto lub w lewo"
            ));

            q6.AddOption(new AnswerOption(
                "Do kościoła paczki\n nie dostarczam. Zawracamy",
                false,
                "Błąd! Za znakiem można jechać tylko prosto lub w lewo"
            ));

            q6.AddOption(new AnswerOption(
                "Prosto, aby \nbyło jak najszybciej",
                false,
                "Błąd! Za znakiem można jechać nie tylko prosto, ale też w lewo"
            ));

            level1.Questions.Add(q6);

            Question q7 = new Question(
                "Przed Tobą rozwidlenie wśród wysokich budynków. Którędy pojedziesz, aby dotrzeć do celu?",
                "7"
            );

            q7.AddOption(new AnswerOption(
                "W prawo",
                false,
                "Błąd! Tutaj można jechać nie tylko w prawo, ale też w lewo"
            ));

            q7.AddOption(new AnswerOption(
                "W lewo",
                false,
                "Błąd! Tutaj można jechać nie tylko w lewo, ale też w prawo"
            ));

            q7.AddOption(new AnswerOption(
                "Prosto i wymijamy \nczerwony samochodzik",
                false,
                "Błąd! Tutaj można jechać tylko w prawo albo w lewo"
            ));

            q7.AddOption(new AnswerOption(
                "W prawo lub w lewo",
                true,
                "Znak nakazuje jazdę w prawo lub w lewo"
            ));

            level1.Questions.Add(q7);

            Question q8 = new Question(
                "Uwaga! Przed Tobą roboty drogowe! Którędy możesz je wyminąć?",
                "8"
            );

            q8.AddOption(new AnswerOption(
                "Trzymając się prawej strony",
                true,
                "Znak nakazuje jazdę z prawej strony znaku"
            ));

            q8.AddOption(new AnswerOption(
                "Gaz do dechy i lecimy lewą stroną",
                false,
                "Błąd! Znak nakazuje jazdę z prawej strony znaku"
            ));

            level1.Questions.Add(q8);

            Question q9 = new Question(
                "Skrzyżowanie o ruchu okrężnym - rondo. Kto przejedzie pierwszy? Ty czy czerwony samochodzik?",
                "9"
            );

            q9.AddOption(new AnswerOption(
                "Skoro już jest na tym rondzie to niech jedzie",
                false,
                "Błąd! Rondo bez znaku 'Ustąp pierwszeństwa' traktujemy jak skrzyżowanie równorzędne"
            ));

            q9.AddOption(new AnswerOption(
                "On poczeka, ja wjeżdżam pierwszy",
                true,
                "Rondo bez znaku 'Ustąp pierwszeństwa' traktujemy jak skrzyżowanie równorzędne, więc to Ty masz pierwszeństwo - zasada 'prawej wolnej'"
            ));

            q9.AddOption(new AnswerOption(
                "To nie ma znaczenia",
                false,
                "Błąd! Rondo bez znaku 'Ustąp pierwszeństwa' traktujemy jak skrzyżowanie równorzędne"
            ));

            level1.Questions.Add(q9);

            Question q10 = new Question(
                "A teraz zawracanie na rondzie jak można je wykonać?",
                "10"
            );

            q10.AddOption(new AnswerOption(
                "Objeżdżając całe rondo, \nustępując pierwszeństwa \nnadjeżdżającym samochodom",
                true,
                "Objeżdżając całe rondo pamiętając o ustąpieniu pierwszeństwa nadjeżdżającym samochodom"
            ));

            q10.AddOption(new AnswerOption(
                "Zaraz po wjeździe, \nskręcając w lewo (zamiast w prawo)\n i nie objeżdżając ronda w ogóle",
                false,
                "Błąd! Należy objechać całe rondo ustępując pierwszeństwa nadjeżdzającym samochodom"
            ));

            level1.Questions.Add(q10);


            return level1;
        }

        /// <summary>
        /// Defines the content and questions for Level 2
        /// </summary>
        /// <returns>Level Data object with questions for the learning section</returns>
        private static LevelData InitializeLevel2()
        {
            string title = "Poziom 2: Drugi dzień dostawcy";
            string intro = "Pierwszy dzień już jest za Tobą!\n" +
                "Dzisiaj czekają na Ciebie kolejne wyzwania, a mianowicie ZNAKI OSTRZEGAWCZE.\n" +
                "Są bardzo przydatne na drogach - tak jak znaki nakazu informują nas o tym, dokąd i jak mamy jechać, tak znaki" +
                " ostrzegawcze będą nas ostrzegały przed niebezpieczeństwami, które mogą na nas czyhać, dlatego mijając taki znak " +
                "musimy zachować SZCZEGÓLNĄ OSTROŻNOŚĆ!" +
                "POWODZENIA!!!";

            LevelData level2 = new LevelData(2, title, intro);

            Question q1 = new Question(
                "Dostarczasz paczkę do gospodarstwa rolnego. Co oznacza znak przed Tobą?",
                "1"
            );

            q1.AddOption(new AnswerOption(
                "Gaz do dechy \n i lecimy bokiem w zakręt \njak w 'Tokyo Drift'",
                false,
                "Błąd! Oznacza niebezpieczny zakręt w lewo i należy zachować ostrożność"
            ));

            q1.AddOption(new AnswerOption(
                "Zakręt w lewo. Dzień jak co dzień",
                false,
                "Błąd! Oznacza niebezpieczny zakręt w lewo i należy zachować ostrożność"
            ));

            q1.AddOption(new AnswerOption(
                "Niebezpieczny zakręt w lewo",
                true,
                "Niebezpieczny zakręt w lewo. Należy przy tym zachować szczególną ostrożność"
            ));

            level2.Questions.Add(q1);

            Question q2 = new Question(
                "Dojeżdżasz do skrzyżowania oznaczonego następującym znakiem. Który w kolejności opuścisz skrzyżowanie?",
                "2"
            );

            q2.AddOption(new AnswerOption(
                "Jestem większy jadę pierwszy",
                false,
                "Błąd! Jest to skrzyżowanie równorzędne i obowiązuje zasada prawej wolnej, więc wyjedziesz jako trzeci"
            ));

            q2.AddOption(new AnswerOption(
                "Jako trzeci",
                true,
                "Wyjedziesz jako trzeci zgodnie z zasadą prawej wolnej"
            ));

            q2.AddOption(new AnswerOption(
                "Jestem uprzejmy, ale nie aż tak, więc drugi",
                false,
                "Błąd! Jest to skrzyżowanie równorzędne i obowiązuje zasada prawej wolnej, więc wyjedziesz jako trzeci"
            ));

            q2.AddOption(new AnswerOption(
                "Kto pierwszy ten lepszy",
                false,
                "Błąd! Jest to skrzyżowanie równorzędne i obowiązuje zasada prawej wolnej, więc wyjedziesz jako trzeci"
            ));

            level2.Questions.Add(q2);

            Question q3 = new Question(
                "Dojedziesz do skrzyżowania z główną drogą w okolicy. Jak masz się zachować patrząc na znak przed Tobą?",
                "3"
            );

            q3.AddOption(new AnswerOption(
                "Skręcam w prawo ustępując pierwszeństwa \n i dojeżdżając do celu",
                true,
                "Znak nakazuje skręcić w prawo i ustąpić pierwszeństwa nadjeżdżającym pojazdom"
            ));

            q3.AddOption(new AnswerOption(
                "Skręcam w lewo zajeżdżając \ndo stacji benzynowej",
                false,
                "Błąd! Znak nakazuje skręcić w prawo i ustąpić pierwszeństwa"
            ));

            q3.AddOption(new AnswerOption(
                "Skręcam w prawo",
                false,
                "Błąd! Znak nakazuje skręcić w prawo i ustąpić pierwszeństwa"
            ));

            level2.Questions.Add(q3);

            Question q4 = new Question(
                "Dojeżdżasz do miejsca oznaczonego znakiem położonym obok Twojego pojazdu. Co ono rzeczywiście oznacza?",
                "4"
            );

            q4.AddOption(new AnswerOption(
                "Przejazd kolejowy bez zapór",
                true,
                "Znak wskazuje na przejazd kolejowy nie wyposażony w zapory ani półzapory"
            ));

            q4.AddOption(new AnswerOption(
                "Przejazd przez tory kolejowe",
                false,
                "Błąd! Znak wskazuje na przejazd kolejowy nie wyposażony w zapory ani półzapory"
            ));

            q4.AddOption(new AnswerOption(
                "Stacja kolejowa",
                false,
                "Błąd! Znak wskazuje na przejazd kolejowy nie wyposażony w zapory ani półzapory"
            ));

            q4.AddOption(new AnswerOption(
                "Lokomotywownia Tomka i Przyjaciół ;)",
                false,
                "Błąd! Znak wskazuje na przejazd kolejowy nie wyposażony w zapory ani półzapory"
            ));

            level2.Questions.Add(q4);

            Question q5 = new Question(
                "Zbliżasz się do ronda oznaczonego następującymi znakami. Skręcasz w prawo. Który wjedziesz na skrzyżowanie?",
                "5"
            );

            q5.AddOption(new AnswerOption(
                "Jako pierwszy",
                false,
                "Błąd! Znak wskazuje, że na kierowcy na rondzie mają pierwszeństwo i należy wjechać na rondo, gdy będzie wolna droga"
            ));

            q5.AddOption(new AnswerOption(
                "Jako trzeci",
                true,
                "Znak wskazuje, że na kierowcy na rondzie mają pierwszeństwo i należy wjechać na rondo, gdy będzie wolna droga"
            ));

            q5.AddOption(new AnswerOption(
                "Jako drugi",
                false,
                "Błąd! Znak wskazuje, że na kierowcy na rondzie mają pierwszeństwo i należy wjechać na rondo, gdy będzie wolna droga"
            ));

            q5.AddOption(new AnswerOption(
                "Nie ma to znaczenia - i tak jedziemy na wprost",
                false,
                "Błąd! Znak wskazuje, że na kierowcy na rondzie mają pierwszeństwo i należy wjechać na rondo, gdy będzie wolna droga"
            ));

            level2.Questions.Add(q5);

            return level2;
        }

        /// <summary>
        /// Defines the content and questions for Level 3
        /// </summary>
        /// <returns>Level Data object with questions for the learning section</returns>
        private static LevelData InitializeLevel3()
        {
            string title = "Poziom 3: Trzeci dzień dostawcy";
            string intro = "Gratulacje! Masz już zebrane wystarczające doświadczenie, by przejść do kolejnego dnia Twojej pracy! \n" +
                "Dzisiaj poznasz znaki zakazu - czyli pokazujące, którędy NIE jechać, porównując do znaku nakazu. \n " +
                "Często je spotkasz w okolicy znaków nakazu, ponieważ tam gdzie one każą jechać, to w razie gdybyś je przeoczył, przydałaby się " +
                "informacja, że jednak tędy jechać nie możesz. \n" +
                "Zapoznaj się z nimi na następnej stronie bardzo dokładnie! \n" +
                "POWODZENIA!!!"
                ;

            LevelData level3 = new LevelData(3, title, intro);

            Question q1 = new Question(
                "Potrzebujesz zostawić przesyłkę niedaleko szkoły. Czy możesz skręcić w lewo?",
                "1"
            );

            q1.AddOption(new AnswerOption(
                "Tak",
                false,
                "Błąd! Znak wskazuje na zakaz wjazdu i nie można tamtędy jechać"
            ));

            q1.AddOption(new AnswerOption(
                "Nie",
                true,
                "Znak wskazuje na zakaz wjazdu i nie można tamtędy jechać"
            ));

            q1.AddOption(new AnswerOption(
                "To zależy",
                false,
                "Błąd! Znak wskazuje na zakaz wjazdu i nie można tamtędy jechać"
            ));

            q1.AddOption(new AnswerOption(
                "Kto bogatemu zabroni",
                false,
                "Błąd! Znak wskazuje na zakaz wjazdu i nie można tamtędy jechać"
            ));

            level3.Questions.Add(q1);

            Question q2 = new Question(
                "Czy przed skrzyżowaniem, które jest przed Tobą, należy bezwzględnie się zatrzymać?",
                "2"
            );


            q2.AddOption(new AnswerOption(
                "Tak",
                true,
                "Znak wskazujący STOP nakazuje się zatrzymać przed znakiem lub za nim a przed linią bezwględnego zatrzymania"
            ));

            q2.AddOption(new AnswerOption(
                "No co Ty, w życiu",
                false,
                "Błąd! Znak wskazujący STOP nakazuje się zatrzymać przed znakiem lub za nim a przed linią bezwględnego zatrzymania"
            ));

            q2.AddOption(new AnswerOption(
                "Nie wiem",
                false,
                "Błąd! Znak wskazujący STOP nakazuje się zatrzymać przed znakiem lub za nim a przed linią bezwględnego zatrzymania"
            ));

            q2.AddOption(new AnswerOption(
                "To zależy",
                false,
                "Błąd! Znak wskazujący STOP nakazuje się zatrzymać przed znakiem lub za nim a przed linią bezwględnego zatrzymania"
            ));

            level3.Questions.Add(q2);

            Question q3 = new Question(
                "Pominąłeś miejsce dostawy (znowu). Czy możesz zawrócić na najbliższym skrzyżowaniu?",
                "3"
            );

            q3.AddOption(new AnswerOption(
                "Tak",
                false,
                "Błąd! Znak wskazuje, że na najbliższym skrzyżowaniu nie można zawracać"
            ));

            q3.AddOption(new AnswerOption(
                "Tak z zachowaniem\n szczególnej ostrożności",
                false,
                "Błąd! Znak wskazuje, że na najbliższym skrzyżowaniu nie można zawracać"
            ));

            q3.AddOption(new AnswerOption(
                "Tak z zachowaniem\n szczególnej ostrożności\n i ustępując pierwszeństwa innym",
                false,
                "Błąd! Znak wskazuje, że na najbliższym skrzyżowaniu nie można zawracać"
            ));

            q3.AddOption(new AnswerOption(
                "Nie. Muszę zrobić\n to przy kolejnej okazji",
                true,
                "Znak wskazuje, że na najbliższym skrzyżowaniu nie można zawracać"
            ));

            level3.Questions.Add(q3);

            Question q4 = new Question(
                "Chcesz jak najszybciej dojechać do domu oznaczonego poniżej. Czy możesz wyprzedzić wleczący się traktor przed Tobą?",
                "4"
            );

            q4.AddOption(new AnswerOption(
                "No pewnie, \nileż można za nim jechać",
                false,
                "Błąd! Znak zakazuje wyprzedzania pojazdów wielośladowych"
            ));

            q4.AddOption(new AnswerOption(
                "Tak, ale ostrożnie, \nżeby policja nie zauważyła",
                false,
                "Błąd! Znak zakazuje wyprzedzania pojazdów wielośladowych"
            ));

            q4.AddOption(new AnswerOption(
                "Nie, muszę za nim jechać",
                true,
                "Znak zakazuje wyprzedzania pojazdów wielośladowych"
            ));

            level3.Questions.Add(q4);

            Question q5 = new Question(
                "Dojeżdżasz do wąskiego wiaduktu. Kto przejedzie pierwszy pod nim? Ty czy traktor?",
                "5"
            );

            q5.AddOption(new AnswerOption(
                "Ja",
                false,
                "Błąd! Znak wskazuje, że zabrania kierującym wjazdu lub wejścia na zwężony odcinek jezdni, jeżeli zmusiłoby to kierujących znajdujących się na tym odcinku lub zbliżających się do niego z przeciwnej strony do zatrzymania się lub zmiany toru jazd"
            ));

            q5.AddOption(new AnswerOption(
                "Traktor",
                true,
                "Znak wskazuje, że zabrania kierującym wjazdu lub wejścia na zwężony odcinek jezdni, jeżeli zmusiłoby to kierujących znajdujących się na tym odcinku lub zbliżających się do niego z przeciwnej strony do zatrzymania się lub zmiany toru jazd"
            ));

            q5.AddOption(new AnswerOption(
                "Kto pierwszy ten lepszy",
                false,
                "Błąd! Znak wskazuje, że zabrania kierującym wjazdu lub wejścia na zwężony odcinek jezdni, jeżeli zmusiłoby to kierujących znajdujących się na tym odcinku lub zbliżających się do niego z przeciwnej strony do zatrzymania się lub zmiany toru jazd"
            ));

            level3.Questions.Add(q5);

            return level3;
        }
    }
}