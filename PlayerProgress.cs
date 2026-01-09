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
        /// Total number of levels available in the game
        /// Used for calculating the maximum possible stars a player can earn
        /// Update this value when adding new levels to the game
        public const int TotalLevelsCount = 3;

        /// Maximum number of stars that can be earned per single level
        /// Based on the scoring system: 90% = 1 star, 95% = 2 stars, 100% = 3 stars
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

        // Path to the progress save file
        private static string? _saveFilePath; // It can be null

        // Currently selected vehicle ID (player's choice among unlocked vehicles)
        private static int _selectedVehicleID = 1;


        /// Dictionary storing the best star count achieved for each level
        private static readonly Dictionary<int, int> _starsPerLevel = new Dictionary<int, int>();

        /// Records the number of stars earned for a specific level
        /// Implements "best score" logic - only updates if the new score is higher than previously saved
        /// This ensures that replaying a level with a worse result does not overwrite a better previous score
        public static void SetStars(int levelID, int stars)
        {
            // Ensure the stars value is within valID bounds (0 to MaxStarsPerLevel)
            // Math.Clamp prevents invalID values from being stored
            stars = Math.Clamp(stars, 0, MaxStarsPerLevel);

            // Check if this level has been played before
            bool levelExistsInProgress = _starsPerLevel.ContainsKey(levelID);

            // Only update the score if:
            // This is the first time completing this level or
            // The new score is better than the previous best
            if (!levelExistsInProgress || _starsPerLevel[levelID] < stars)
            {
                _starsPerLevel[levelID] = stars;
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

            _saveFilePath = Path.Combine(configPath, "progress.txt");
        }

        /// <summary>
        /// Saves current progress to file.
        /// Format: one line per level as "level_X=Y" where X is level ID and Y is stars.
        /// </summary>
        public static void SaveProgress()
        {
            if (string.IsNullOrEmpty(_saveFilePath))
            {
                System.Diagnostics.Debug.WriteLine("SaveProgress: path not initialized");
                return;
            }

            try
            {
                List<string> lines = new List<string>();

                // Write each level's stars
                foreach (var entry in _starsPerLevel)
                {
                    lines.Add($"level_{entry.Key}={entry.Value}");
                }

                // Write selected vehicle
                lines.Add($"vehicle={_selectedVehicleID}");

                File.WriteAllLines(_saveFilePath, lines);
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
            if (string.IsNullOrEmpty(_saveFilePath))
            {
                System.Diagnostics.Debug.WriteLine("LoadProgress: path not initialized");
                return;
            }

            // Clear existing data
            _starsPerLevel.Clear();
            _selectedVehicleID = 1;

            // If no save file, start fresh
            if (!File.Exists(_saveFilePath))
            {
                return;
            }

            try
            {
                string[] lines = File.ReadAllLines(_saveFilePath);

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
                            _starsPerLevel[levelId] = Math.Clamp(stars, 0, MaxStarsPerLevel);
                        }
                    }
                    else if (keyPart == "vehicle")
                    {
                        if (int.TryParse(valuePart, out int vehicleID))
                        {
                            _selectedVehicleID = Math.Clamp(vehicleID, 1, TotalVehiclesCount);
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

        /// Clears all saved progress, resetting the game to initial state
        /// Useful for implementing a "New Game" or "Reset Progress" feature
        public static void ResetProgress()
        {
            _starsPerLevel.Clear();
            _selectedVehicleID = 1;

            SaveProgress();
        }

        /// Retrieves the number of stars earned for a specific level
        public static int GetStars(int levelID)
        {
            // Use ContainsKey check to safely return 0 for levels not yet played
            if (_starsPerLevel.ContainsKey(levelID))
            {
                return _starsPerLevel[levelID];
            }
            else
            {
                return 0;
            }
        }

        /// Calculates the sum of all stars earned across all completed levels
        /// Used for displaying overall player progress in the level selection screen
        public static int GetTotalStars()
        {
            // LINQ Sum() efficiently adds all values in the dictionary
            return _starsPerLevel.Values.Sum();
        }

        /// Calculates the theoretical maximum stars achievable in the entire game
        /// Used for displaying progress in format "X / Y" (e.g., "5 / 9")
        public static int GetMaxPossibleStars()
        {
            return TotalLevelsCount * MaxStarsPerLevel;
        }

        /// Checks whether a specific level has been successfully completed
        /// A level is consIDered "completed" if the player earned at least 1 star
        public static bool IsLevelCompleted(int levelID)
        {
            // Level is completed if it exists in dictionary AND has at least 1 star
            return _starsPerLevel.ContainsKey(levelID) && _starsPerLevel[levelID] > 0;
        }

        /// <summary>
        /// Returns the ID of the best unlocked vehicle based on total stars.
        /// </summary>
        /// <returns>Vehicle ID (1, 2, or 3)</returns>
        public static int GetCurrentVehicleID()
        {
            // Check if selected vehicle is still unlocked
            if (IsVehicleUnlocked(_selectedVehicleID))
            {
                return _selectedVehicleID;
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

            _selectedVehicleID = vehicleID;
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