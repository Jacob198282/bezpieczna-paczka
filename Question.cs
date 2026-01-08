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
    public class Question
    {
        // The question or description of the situation
        public string QuestionText { get; set; } // getter and setter allow to get the value and set the value of the pole

        // A list of complex answer options instead of just strings
        public List<AnswerOption> Options { get; set; }

        // Number that is required to dynamically build a path to the scenario image
        public string ScenarioImagePath { get; set; }

        /// Constructor for the Question.
        public Question(string text, string imagePath)
        {
            QuestionText = text;
            ScenarioImagePath = imagePath;
            Options = new List<AnswerOption>(); // creates list of answers for the question - list of AnswerOption type objects
        }

        /// Helper method to add an answer option to the question.
        public void AddOption(AnswerOption option)
        {
            Options.Add(option);
        }
    }
}
