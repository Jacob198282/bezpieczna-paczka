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
using System.Drawing;

namespace bezpieczna_paczkaApp
{
    /// <summary>
    /// Represents a falling package in the minigame.
    /// </summary>
    public class Package
    {
        // Position of the package (top-left corner)
        public float X { get; set; }
        public float Y { get; set; }

        // Size of the package
        public int Width { get; set; }
        public int Height { get; set; }

        // Falling speed in pixels per frame
        public float Speed { get; set; }

        // Flag indicating if package was collected
        public bool IsCollected { get; set; }

        // Flag indicating if package left the screen
        public bool IsOffScreen { get; set; }

        /// <summary>
        /// Creates a new package at specified position.
        /// </summary>
        /// <param name="x">Starting X position</param>
        /// <param name="y">Starting Y position (usually negative, above screen)</param>
        /// <param name="width">Package width</param>
        /// <param name="height">Package height</param>
        /// <param name="speed">Falling speed</param>
        public Package(float x, float y, int width, int height, float speed)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Speed = speed;
            IsCollected = false;
            IsOffScreen = false;
        }

        /// <summary>
        /// Updates package position (moves it down).
        /// </summary>
        public void Update()
        {
            Y += Speed;
        }

        /// <summary>
        /// Returns the bounding rectangle of the package.
        /// </summary>
        public RectangleF GetBounds()
        {
            return new RectangleF(X, Y, Width, Height);
        }

        /// <summary>
        /// Checks if this package collides with a rectangle (player vehicle).
        /// </summary>
        /// <param name="playerBounds">Player's bounding rectangle</param>
        /// <returns>True if collision detected</returns>
        public bool CollidesWith(RectangleF playerBounds)
        {
            return GetBounds().IntersectsWith(playerBounds);
        }
    }
}