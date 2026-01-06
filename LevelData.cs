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
    /// Contains all data for a specific game level, including introduction and a set of questions.
    public class LevelData
    {
        // The title of the level 
        public string LevelTitle { get; set; }

        // The introductory text explaining the plot and rules for this specific part of the story
        public string IntroText { get; set; }

        // A collection of all questions/scenarios assigned to this level
        public List<Question> Questions { get; set; }

        // Required score to pass the level (at least one star)
        // Expressed as a percentage, where 1.0 is 100%
        public double PassingThreshold = 0.90;

        // identification of the current level
        public int LevelID { get; set; }

        /// Constructor for the LevelData class.
        public LevelData(int id, string title, string intro)
        {
            LevelID = id;
            LevelTitle = title; // title of the level
            IntroText = intro; // introduction to the level
            Questions = new List<Question>();
        }
    }
}