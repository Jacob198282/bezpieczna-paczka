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
using System.Windows.Forms;

namespace bezpieczna_paczkaApp
{
    /// <summary>
    /// Static helper class for managing graphical resources.
    /// Provides methods for safe disposal and loading of images.
    /// </summary>
    public static class ResourceHelper
    {
        /// <summary>
        /// Loads image from file without keeping file handle open.
        /// This prevents memory leaks caused by Image.FromFile().
        /// </summary>
        /// <param name="imagePath">Full path to image file</param>
        /// <returns>Loaded image or null if failed</returns>
        public static Image? LoadImageWithoutLock(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath) || !File.Exists(imagePath))
            {
                MessageBox.Show(
                $"Błąd wczytywania grafiki przycisku menu: Nie znaleziono pliku!\n{imagePath}",
                "Błąd Pliku");

                return null;
            }

            try
            {
                byte[] imageData = File.ReadAllBytes(imagePath);
                using (var ms = new MemoryStream(imageData))
                {
                    return new Bitmap(ms); // Copy data to memory and file is closed immediately, unlocking resources
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to load image: {ex.Message}");

                MessageBox.Show(
                $"Wystąpił nieoczekiwany błąd przy wczytywaniu grafiki przycisku menu:\n{ex.Message}",
                "Błąd Krytyczny");
            }

            // Fallback return if something unexpected happens
            return null;
        }

        /// <summary>
        /// Safely disposes image from a PictureBox and sets it to null.
        /// </summary>
        /// <param name="pictureBox">PictureBox to clean</param>
        public static void DisposePictureBoxImage(PictureBox pictureBox)
        {
            if (pictureBox != null && pictureBox.Image != null)
            {
                pictureBox.Image.Dispose();
                pictureBox.Image = null;
            }
        }

        /// <summary>
        /// Safely disposes background image from a Panel and sets it to null.
        /// </summary>
        /// <param name="panel">Panel to clean</param>
        public static void DisposePanelBackground(Panel panel)
        {
            if (panel != null && panel.BackgroundImage != null)
            {
                panel.BackgroundImage.Dispose();
                panel.BackgroundImage = null;
            }
        }

        /// <summary>
        /// Loads image into PictureBox, disposing previous image first.
        /// </summary>
        /// <param name="pictureBox">Target PictureBox</param>
        /// <param name="imagePath">Full path to image file</param>
        /// <returns>True if loaded successfully</returns>
        public static bool LoadPictureBoxImage(PictureBox pictureBox, string imagePath)
        {
            if (pictureBox == null || string.IsNullOrEmpty(imagePath))
            {
                return false;
            }

            // Dispose previous image
            DisposePictureBoxImage(pictureBox);

            Image? newImage = LoadImageWithoutLock(imagePath);

            if (newImage != null)
            {
                pictureBox.Image = newImage;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Loads background image into Panel, disposing previous image first.
        /// </summary>
        /// <param name="panel">Target Panel</param>
        /// <param name="imagePath">Full path to image file</param>
        /// <returns>True if loaded successfully</returns>
        public static bool LoadPanelBackground(Panel panel, string imagePath)
        {
            if (panel == null || string.IsNullOrEmpty(imagePath))
            {
                return false;
            }

            // Dispose previous image
            DisposePanelBackground(panel);

            Image? newImage = LoadImageWithoutLock(imagePath);

            if (newImage != null)
            {
                panel.BackgroundImage = newImage;
                return true;
            }

            return false;
        }
    }
}