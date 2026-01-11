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
    /// Class which defines the structure and parameters of the question created in the LevelProvider class
    /// </summary>
    public class Question
    {
        /// <summary>
        /// The question or description of the situation
        /// </summary>
        public string QuestionText { get; set; } // getter and setter allow to get the value and set the value of the pole

        /// <summary>
        /// A list of complex answer options instead of just strings
        /// </summary>
        public List<AnswerOption> Options { get; set; }

        /// <summary>
        /// Number that is required to dynamically build a path to the scenario image
        /// </summary>
        public string ScenarioImagePath { get; set; }

        /// <summary>
        /// Constructor for the Question.
        /// </summary>
        /// <param name="text">Description of the question</param>
        /// <param name="imagePath">Path to the scenario image</param>
        public Question(string text, string imagePath)
        {
            QuestionText = text;
            ScenarioImagePath = imagePath;
            Options = new List<AnswerOption>(); // creates list of answers for the question - list of AnswerOption type objects
        }

        /// <summary>
        /// Helper method to add an answer option to the question.
        /// </summary>
        /// <param name="option">AnswerOption object</param>
        public void AddOption(AnswerOption option)
        {
            Options.Add(option);
        }
    }
}
