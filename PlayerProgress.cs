/*
 * Copyright (c) Jacob198282 Gdansk University of Technology
 * MIT License
 * Documentation under https://github.com/Jacob198282/bezpieczna-paczka
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace bezpieczna_paczkaApp
{
    /// <summary>
    /// Static class responsible for storing and managing player's progress across all game levels.
    /// This class acts as a centralized storage for stars earned in each level.
    /// </summary>
    public static class PlayerProgress
    {
        /// <summary>
        /// Total number of levels available in the game
        /// Used for calculating the maximum possible stars a player can earn
        /// Update this value when adding new levels to the game
        /// </summary>
        public const int TotalLevelsCount = 3;

        /// <summary>
        /// Maximum number of stars that can be earned per single level
        /// Based on the scoring system: 90% = 1 star, 95% = 2 stars, 100% = 3 stars
        /// </summary>
        public const int MaxStarsPerLevel = 3;

        /// <summary>
        /// Stars required to unlock vehicle 1 (available from start).
        /// </summary>
        public const int VEHICLE_1_THRESHOLD = 0;

        /// <summary>
        /// Stars required to unlock vehicle 2.
        /// </summary>
        public const int VEHICLE_2_THRESHOLD = 3;

        /// <summary>
        /// Stars required to unlock vehicle 3.
        /// </summary>
        public const int VEHICLE_3_THRESHOLD = 9;

        /// <summary>
        /// Total number of vehicles in the game.
        /// </summary>
        public const int TotalVehiclesCount = 3;

        /// <summary>
        /// Path to the progress save file
        /// </summary>
        private static string? saveFilePath; // It can be null

        /// <summary>
        /// Currently selected vehicle ID (player's choice among unlocked vehicles)
        /// </summary>
        private static int selectedVehicleID = 1;


        /// <summary>
        /// Dictionary storing the best star count achieved for each level
        /// </summary>
        private static readonly Dictionary<int, int> starsPerLevel = new Dictionary<int, int>();

        /// <summary>
        /// Records the number of stars earned for a specific level
        /// Implements "best score" logic - only updates if the new score is higher than previously saved
        /// This ensures that replaying a level with a worse result does not overwrite a better previous score
        /// </summary>
        /// <param name="levelID">ID of the level that the player completed</param>
        /// <param name="stars">Number of the stars that player collected</param>
        public static void SetStars(int levelID, int stars)
        {
            // Ensure the stars value is within valID bounds (0 to MaxStarsPerLevel)
            // Math.Clamp prevents invalID values from being stored
            stars = Math.Clamp(stars, 0, MaxStarsPerLevel);

            // Check if this level has been played before
            bool levelExistsInProgress = starsPerLevel.ContainsKey(levelID);

            // Only update the score if:
            // This is the first time completing this level or
            // The new score is better than the previous best
            if (!levelExistsInProgress || starsPerLevel[levelID] < stars)
            {
                starsPerLevel[levelID] = stars;
            }
        }

        /// <summary>
        /// Sets the path where progress file will be saved.
        /// Must be called before LoadProgress() or SaveProgress().
        /// </summary>
        /// <param name="projectRoot">Root path of the project</param>
        public static void Initialize(string projectRoot)
        {
            string configPath = Path.Combine(projectRoot, "res", "config");

            // Create config directory if it doesn't exist
            if (!Directory.Exists(configPath))
            {
                Directory.CreateDirectory(configPath);
            }

            saveFilePath = Path.Combine(configPath, "progress.txt");
        }

        /// <summary>
        /// Saves current progress to file.
        /// Format: one line per level as "level_X=Y" where X is level ID and Y is stars.
        /// </summary>
        public static void SaveProgress()
        {
            if (string.IsNullOrEmpty(saveFilePath))
            {
                System.Diagnostics.Debug.WriteLine("SaveProgress: path not initialized");
                return;
            }

            try
            {
                List<string> lines = new List<string>();

                // Write each level's stars
                foreach (var entry in starsPerLevel)
                {
                    lines.Add($"level_{entry.Key}={entry.Value}");
                }

                // Write selected vehicle
                lines.Add($"vehicle={selectedVehicleID}");

                File.WriteAllLines(saveFilePath, lines);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to save progress: {ex.Message}");
            }
        }

        /// <summary>
        /// Loads progress from file. If file doesn't exist, starts with empty progress.
        /// </summary>
        public static void LoadProgress()
        {
            if (string.IsNullOrEmpty(saveFilePath))
            {
                System.Diagnostics.Debug.WriteLine("LoadProgress: path not initialized");
                return;
            }

            // Clear existing data
            starsPerLevel.Clear();
            selectedVehicleID = 1;

            // If no save file, start fresh
            if (!File.Exists(saveFilePath))
            {
                return;
            }

            try
            {
                string[] lines = File.ReadAllLines(saveFilePath);

                foreach (string line in lines)
                {
                    // Parse format: "level_X=Y"
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    string[] parts = line.Split('=');
                    if (parts.Length != 2)
                    {
                        continue;
                    }

                    // Extract level ID from "level_X"
                    string keyPart = parts[0].Trim();
                    string valuePart = parts[1].Trim();

                    if (keyPart.StartsWith("level_"))
                    {
                        string idString = keyPart.Substring(6); // Remove "level_" prefix
                        if (int.TryParse(idString, out int levelId) &&
                            int.TryParse(valuePart, out int stars))
                        {
                            starsPerLevel[levelId] = Math.Clamp(stars, 0, MaxStarsPerLevel);
                        }
                    }
                    else if (keyPart == "vehicle")
                    {
                        if (int.TryParse(valuePart, out int vehicleID))
                        {
                            selectedVehicleID = Math.Clamp(vehicleID, 1, TotalVehiclesCount);
                        }
                    }
                    else
                    { 
                        continue; 
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to load progress: {ex.Message}");
            }
        }

        /// <summary>
        /// Clears all saved progress, resetting the game to initial state
        /// Useful for implementing a "New Game" or "Reset Progress" feature
        /// </summary>
        public static void ResetProgress()
        {
            starsPerLevel.Clear();
            selectedVehicleID = 1;

            SaveProgress();
        }

        /// <summary>
        /// Retrieves the number of stars earned for a specific level
        /// </summary>
        /// <param name="levelID">ID of the level, from which the number of collected stars will be counted</param>
        /// <returns>Number of stars</returns>
        public static int GetStars(int levelID)
        {
            // Use ContainsKey check to safely return 0 for levels not yet played
            if (starsPerLevel.ContainsKey(levelID))
            {
                return starsPerLevel[levelID];
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Calculates the sum of all stars earned across all completed levels
        /// Used for displaying overall player progress in the level selection screen
        /// </summary>
        /// <returns>Number of total stars collected from all the levels</returns>
        public static int GetTotalStars()
        {
            // LINQ Sum() efficiently adds all values in the dictionary
            return starsPerLevel.Values.Sum();
        }

        /// <summary>
        /// Calculates the theoretical maximum stars achievable in the entire game
        /// Used for displaying progress in format "X / Y" (e.g., "5 / 9")
        /// </summary>
        /// <returns>Maximum achievable number of stars</returns>
        public static int GetMaxPossibleStars()
        {
            return TotalLevelsCount * MaxStarsPerLevel;
        }

        /// <summary>
        /// Checks whether a specific level has been successfully completed
        /// A level is consIDered "completed" if the player earned at least 1 star
        /// </summary>
        /// <param name="levelID"></param>
        /// <returns>True or false whether the level is completed or not</returns>
        public static bool IsLevelCompleted(int levelID)
        {
            // Level is completed if it exists in dictionary AND has at least 1 star
            return starsPerLevel.ContainsKey(levelID) && starsPerLevel[levelID] > 0;
        }

        /// <summary>
        /// Checks if a specific level is unlocked.
        /// Level 1 is always unlocked.
        /// Other levels require previous level to be completed.
        /// </summary>
        /// <param name="levelID">Level ID to check (1, 2, or 3)</param>
        /// <returns>True if level is unlocked</returns>
        public static bool IsLevelUnlocked(int levelID)
        {
            if (levelID <= 1)
            {
                // Level 1 is always unlocked
                return true;
            }

            // Check if previous level is completed
            int previousLevelID = levelID - 1;
            return IsLevelCompleted(previousLevelID);
        }

        /// <summary>
        /// Returns the ID of the best unlocked vehicle based on total stars.
        /// </summary>
        /// <returns>Vehicle ID (1, 2, or 3)</returns>
        public static int GetCurrentVehicleID()
        {
            // Check if selected vehicle is still unlocked
            if (IsVehicleUnlocked(selectedVehicleID))
            {
                return selectedVehicleID;
            }

            int totalStars = GetTotalStars();

            if (totalStars >= VEHICLE_3_THRESHOLD)
            {
                return 3;
            }
            else if (totalStars >= VEHICLE_2_THRESHOLD)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// Sets the currently selected vehicle if it is unlocked.
        /// </summary>
        /// <param name="vehicleID">Vehicle ID to select (1, 2, or 3)</param>
        /// <returns>True if vehicle was selected, false if locked</returns>
        public static bool SetCurrentVehicleID(int vehicleID)
        {
            // Only allow selecting unlocked vehicles
            if (!IsVehicleUnlocked(vehicleID))
            {
                return false;
            }

            selectedVehicleID = vehicleID;
            SaveProgress();
            return true;
        }

        /// <summary>
        /// Checks if a specific vehicle is unlocked.
        /// </summary>
        /// <param name="vehicleID">Vehicle ID to check (1, 2, or 3)</param>
        /// <returns>True if vehicle is unlocked</returns>
        public static bool IsVehicleUnlocked(int vehicleID)
        {
            int totalStars = GetTotalStars();
            int threshold = GetVehicleThreshold(vehicleID);

            return totalStars >= threshold;
        }

        /// <summary>
        /// Returns the star threshold required to unlock a specific vehicle.
        /// </summary>
        /// <param name="vehicleID">Vehicle ID (1, 2, or 3)</param>
        /// <returns>Number of stars required</returns>
        public static int GetVehicleThreshold(int vehicleID)
        {
            switch (vehicleID)
            {
                case 1:
                    return VEHICLE_1_THRESHOLD;
                case 2:
                    return VEHICLE_2_THRESHOLD;
                case 3:
                    return VEHICLE_3_THRESHOLD;
                default:
                    return int.MaxValue;
            }
        }
    }
}