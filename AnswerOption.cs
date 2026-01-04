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
        public string AnswerText { get; set; } // getter and setter allow to get the value and set the value of the pole

        // Indicates if this specific choice is the correct one according to traffic laws
        public bool IsCorrect { get; set; }

        // --- Animation Parameters for this specific choice ---

        // Final X position of the delivery van if this answer is selected
        public int DestinationX { get; set; }

        // Final Y position of the delivery van if this answer is selected
        public int DestinationY { get; set; }

        // Final rotation angle of the van (e.g., 90 for a right turn)
        public float DestinationRotation { get; set; }

        // Feedback message shown to the player after the animation (e.g., "Correct! You yielded priority.")
        public string FeedbackMessage { get; set; }

        /// Constructor for the AnswerOption.    
        public AnswerOption(string text, bool isCorrect, int x, int y, float rotation, string feedback)
        {
            AnswerText = text;
            IsCorrect = isCorrect;
            DestinationX = x;
            DestinationY = y;
            DestinationRotation = rotation;
            FeedbackMessage = feedback;
        }
    }
}
