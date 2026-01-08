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
    /// Represents a single choice available to the player.
    /// It links the text of the answer with the physical result in the game world.
    public class AnswerOption
    {
        // The text displayed on the button
        public string AnswerText { get; set; } // Getter and setter allow to get the value and set the value of the pole

        // Indicates if this specific choice is the correct one according to traffic laws
        public bool IsCorrect { get; set; }

        // Feedback message shown to the player after the animation
        public string FeedbackMessage { get; set; }

        /// Constructor for the AnswerOption.    
        public AnswerOption(string text, bool isCorrect, string feedback)
        {
            AnswerText = text;
            IsCorrect = isCorrect;
            FeedbackMessage = feedback;
        }
    }
}
