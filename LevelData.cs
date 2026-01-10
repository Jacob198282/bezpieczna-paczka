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
    /// Contains all data for a specific game level, including introduction and a set of questions.
    /// </summary>
    public class LevelData
    {
        /// <summary>
        /// The title of the level 
        /// </summary>
        public string LevelTitle { get; set; }

        /// <summary>
        /// The introductory text explaining the plot and rules for this specific part of the story
        /// </summary>
        public string IntroText { get; set; }

        /// <summary>
        /// A collection of all questions/scenarios assigned to this level
        /// </summary>
        public List<Question> Questions { get; set; }

        /// <summary>
        /// Required score to pass the level (at least one star)
        /// Expressed as a percentage, where 1.0 is 100%
        /// </summary>
        public double PassingThreshold = 0.90;

        /// <summary>
        /// identification of the current level
        /// </summary>
        public int LevelID { get; set; }

        /// <summary>
        /// Constructor for the LevelData class.
        /// </summary>
        /// <param name="id">Which level the player is playing</param>
        /// <param name="title">Title of the level - displayed at the beginning</param>
        /// <param name="intro">Intro of the level - displayed at the beginning</param>
        public LevelData(int id, string title, string intro)
        {
            LevelID = id;
            LevelTitle = title; // title of the level
            IntroText = intro; // introduction to the level
            Questions = new List<Question>();
        }
    }
}