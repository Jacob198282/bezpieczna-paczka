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
    // Struct that defines waypoints where the van should head
    public struct Waypoint
    {
        public Point Position { get; set; } // Position of the van
        public float Rotation { get; set; } // Rotation of the van at a current point

        public Waypoint(int x, int y, float rotation) // Constructor for the waypoint
        {
            Position = new Point(x, y);
            Rotation = rotation;
        }
    }

    /// Represents a single choice available to the player.
    /// It links the text of the answer with the physical result in the game world.
    public class AnswerOption
    {
        // The text displayed on the button
        public string AnswerText { get; set; } // Getter and setter allow to get the value and set the value of the pole

        // Indicates if this specific choice is the correct one according to traffic laws
        public bool IsCorrect { get; set; }

        // --- Animation Parameters for this specific choice ---

        // List of waypoints for animation of the van
        public List<Waypoint> Path {  get; set; }

        // Feedback message shown to the player after the animation
        public string FeedbackMessage { get; set; }

        /// Constructor for the AnswerOption.    
        public AnswerOption(string text, bool isCorrect, List<Waypoint> path, string feedback)
        {
            AnswerText = text;
            IsCorrect = isCorrect;
            Path = path;
            FeedbackMessage = feedback;
        }
    }
}
