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
    /// Represents a single choice available to the player.
    /// It links the text of the answer with the physical result in the game world.
    /// </summary>
    public class AnswerOption
    {
        /// <summary>
        /// The text displayed on the button
        /// </summary>
        public string AnswerText { get; set; } // Getter and setter allow to get the value and set the value of the pole

        /// <summary>
        /// Indicates if this specific choice is the correct one according to traffic laws
        /// </summary>
        public bool IsCorrect { get; set; }

        /// <summary>
        /// Feedback message shown to the player
        /// </summary>
        public string FeedbackMessage { get; set; }

        /// <summary>
        /// Constructor for the AnswerOption.  
        /// </summary>
        /// <param name="text">Text of the answer</param>
        /// <param name="isCorrect">Bool value determining whether answer is correct or not</param>
        /// <param name="feedback">Feedback message shown after answering the question</param>
        public AnswerOption(string text, bool isCorrect, string feedback)
        {
            AnswerText = text;
            IsCorrect = isCorrect;
            FeedbackMessage = feedback;
        }
    }
}
