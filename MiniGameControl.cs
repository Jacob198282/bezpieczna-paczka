using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Copyright (c) Jacob198282 Gdansk University of Technology
 * MIT License
 * Documentation under https://github.com/Jacob198282/bezpieczna-paczka
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace bezpieczna_paczkaApp
{
    /// <summary>
    /// UserControl for the package collection minigame.
    /// Player moves left/right to collect falling packages.
    /// </summary>
    public partial class MiniGameControl : UserControl
    {
        /// <summary>
        /// Event raised when minigame is completed (target score reached)
        /// </summary>
        public event EventHandler? MinigameCompleted;

        /// <summary>
        /// Event raised when player wants to open menu
        /// </summary>
        public event EventHandler? MenuRequested;

        /// <summary>
        /// Game area boundaries
        /// </summary>
        private const int GAME_AREA_LEFT = 194;
        private const int GAME_AREA_RIGHT = 1086;
        private const int GAME_AREA_BOTTOM = 900;

        /// <summary>
        /// Player settings
        /// </summary>
        private const int PLAYER_WIDTH = 60;
        private const int PLAYER_HEIGHT = 150;
        private const int PLAYER_SPEED = 15;
        private const int PLAYER_Y = 750;

        /// <summary>
        /// Package settings
        /// </summary>
        private const int PACKAGE_WIDTH = 50;
        private const int PACKAGE_HEIGHT = 50;
        private const float PACKAGE_SPEED = 5f;
        private const int SPAWN_INTERVAL_MS = 1000;

        /// <summary>
        /// Target score to complete minigame
        /// </summary>
        private const int TARGET_SCORE = 10;

        /// <summary>
        /// Background scroll speed
        /// </summary>
        private const float BACKGROUND_SCROLL_SPEED = 3f;

        /// <summary>
        /// Player position
        /// </summary>
        private float playerX;

        /// <summary>
        /// Current score
        /// </summary>
        private int score;

        /// <summary>
        /// Active packages on screen
        /// </summary>
        private List<Package> packages;

        /// <summary>
        /// Game timer for animation loop
        /// </summary>
        private System.Windows.Forms.Timer? gameTimer;

        /// <summary>
        /// Timer for spawning new packages
        /// </summary>
        private System.Windows.Forms.Timer? spawnTimer;

        /// <summary>
        /// Random generator for package positions
        /// </summary>
        private Random random;

        /// <summary>
        /// Background scroll offset
        /// </summary>
        private float backgroundOffset;

        /// <summary>
        /// Loaded images
        /// </summary>
        private Image? playerImage;
        private Image? packageImage;
        private Image? backgroundImage;

        /// <summary>
        /// Movement flags
        /// </summary>
        private bool moveLeft;
        private bool moveRight;

        /// <summary>
        /// Paths
        /// </summary>
        private string graphicsPath;

        /// <summary>
        /// Current vehicle ID
        /// </summary>
        private int vehicleID;

        /// <summary>
        /// MiniGameControl constructor
        /// </summary>
        /// <param name="graphicsPath">Combined root path and graphics folder used for loading graphics</param>
        public MiniGameControl(string graphicsPath)
        {
            InitializeComponent();

            this.graphicsPath = graphicsPath;
            packages = new List<Package>();
            random = new Random();
            score = 0;
            backgroundOffset = 0;
            moveLeft = false;
            moveRight = false;

            // Get current vehicle
            vehicleID = PlayerProgress.GetCurrentVehicleID();

            // Center player horizontally in game area
            playerX = (GAME_AREA_LEFT + GAME_AREA_RIGHT) / 2 - PLAYER_WIDTH / 2;

            // Enable double buffering for smooth rendering
            DoubleBuffered = true;

            // Enable key events
            SetStyle(ControlStyles.Selectable, true);
            TabStop = true;
        }

        /// <summary>
        /// Initializes and starts the minigame.
        /// </summary>
        public void StartGame()
        {
            // Reset state
            score = 0;
            packages.Clear();
            backgroundOffset = 0;
            playerX = (GAME_AREA_LEFT + GAME_AREA_RIGHT) / 2 - PLAYER_WIDTH / 2;

            // Load graphics
            LoadGraphics();

            // Update display
            UpdateScoreDisplay();

            // Initialize game timer (60 FPS)
            if (gameTimer == null)
            {
                gameTimer = new System.Windows.Forms.Timer();
                gameTimer.Interval = 16; // ~60 FPS
                gameTimer.Tick += GameLoop;
            }

            // Initialize spawn timer
            if (spawnTimer == null)
            {
                spawnTimer = new System.Windows.Forms.Timer();
                spawnTimer.Interval = SPAWN_INTERVAL_MS;
                spawnTimer.Tick += SpawnPackage;
            }

            // Start timers
            gameTimer.Start();
            spawnTimer.Start();

            // Focus for keyboard input
            Focus();
        }

        /// <summary>
        /// Stops the minigame.
        /// </summary>
        public void StopGame()
        {
            gameTimer?.Stop();
            spawnTimer?.Stop();
        }

        /// <summary>
        /// Main game loop - called every frame.
        /// </summary>
        private void GameLoop(object? sender, EventArgs e)
        {
            // Update player position based on input
            UpdatePlayerPosition();

            // Update background scroll
            UpdateBackground();

            // Update all packages
            UpdatePackages();

            // Check collisions
            CheckCollisions();

            // Remove off-screen and collected packages
            CleanupPackages();

            // Check win condition
            if (score >= TARGET_SCORE)
            {
                CompleteMinigame();
                return;
            }

            // Redraw
            Invalidate();
        }

        /// <summary>
        /// Updates player position based on input flags.
        /// </summary>
        private void UpdatePlayerPosition()
        {
            if (moveLeft)
            {
                playerX -= PLAYER_SPEED;
            }
            if (moveRight)
            {
                playerX += PLAYER_SPEED;
            }

            // Clamp to game area
            float minX = GAME_AREA_LEFT;
            float maxX = GAME_AREA_RIGHT - PLAYER_WIDTH;
            playerX = Math.Clamp(playerX, minX, maxX);
        }

        /// <summary>
        /// Updates background scroll offset.
        /// </summary>
        private void UpdateBackground()
        {
            backgroundOffset += BACKGROUND_SCROLL_SPEED;

            // Reset when scrolled full height
            if (backgroundImage != null && backgroundOffset >= backgroundImage.Height)
            {
                backgroundOffset = 0;
            }
        }

        /// <summary>
        /// Updates all package positions.
        /// </summary>
        private void UpdatePackages()
        {
            foreach (var package in packages)
            {
                package.Update();

                // Mark as off-screen if below game area
                if (package.Y > GAME_AREA_BOTTOM)
                {
                    package.IsOffScreen = true;
                }
            }
        }

        /// <summary>
        /// Checks collisions between player and packages.
        /// </summary>
        private void CheckCollisions()
        {
            RectangleF playerBounds = new RectangleF(playerX, PLAYER_Y, PLAYER_WIDTH, PLAYER_HEIGHT);

            foreach (var package in packages)
            {
                if (!package.IsCollected && package.CollidesWith(playerBounds))
                {
                    package.IsCollected = true;
                    score++;
                    UpdateScoreDisplay();
                }
            }
        }

        /// <summary>
        /// Removes collected and off-screen packages from list.
        /// </summary>
        private void CleanupPackages()
        {
            packages.RemoveAll(p => p.IsCollected || p.IsOffScreen);
        }

        /// <summary>
        /// Spawns a new package at random X position.
        /// </summary>
        /// <param name="sender">Spawn timer in the start method</param>
        /// <param name="e">Event arguments</param>
        private void SpawnPackage(object? sender, EventArgs e)
        {
            // Random X within game area
            int minX = GAME_AREA_LEFT;
            int maxX = GAME_AREA_RIGHT - PACKAGE_WIDTH;
            float x = random.Next(minX, maxX);

            // Start above screen
            float y = -PACKAGE_HEIGHT;

            Package package = new Package(x, y, PACKAGE_WIDTH, PACKAGE_HEIGHT, PACKAGE_SPEED);
            packages.Add(package);
        }

        /// <summary>
        /// Updates the score label.
        /// </summary>
        private void UpdateScoreDisplay()
        {
            lblScore.Text = $"ZEBRANO: {score}/{TARGET_SCORE}";
        }

        /// <summary>
        /// Completes the minigame and raises event.
        /// </summary>
        private void CompleteMinigame()
        {
            StopGame();
            MessageBox.Show("Gratulacje! Zebrałeś wszystkie przesyłki! \n Teraz należy je dostarczyć!");
            MinigameCompleted?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Handles key down events for player movement.
        /// </summary>
        /// <param name="e">Key event arguments</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            {
                moveLeft = true;
            }
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                moveRight = true;
            }
        }

        /// <summary>
        /// Handles key up events for player movement.
        /// </summary>
        /// <param name="e">Key event arguments</param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            {
                moveLeft = false;
            }
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                moveRight = false;
            }
        }

        /// <summary>
        /// Custom painting for the minigame.
        /// </summary>
        /// <param name="e">Paint event arguments</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            // Draw scrolling background
            DrawBackground(g);

            // Draw packages
            DrawPackages(g);

            // Draw player
            DrawPlayer(g);
        }

        /// <summary>
        /// Draws scrolling background.
        /// </summary>
        /// <param name="g">Graphics object on which the background image is drawn</param>
        private void DrawBackground(Graphics g)
        {
            if (backgroundImage == null)
            {
                // Fallback solid color
                g.Clear(Color.Gray);
                return;
            }

            // Draw two copies of background for seamless scrolling
            int y1 = (int)backgroundOffset;
            int y2 = y1 - backgroundImage.Height;

            g.DrawImage(backgroundImage, 0, y1, Width, backgroundImage.Height);
            g.DrawImage(backgroundImage, 0, y2, Width, backgroundImage.Height);
        }

        /// <summary>
        /// Draws all packages.
        /// </summary>
        /// <param name="g">Graphics object on which the package image is drawn</param>
        private void DrawPackages(Graphics g)
        {
            foreach (var package in packages)
            {
                if (packageImage != null)
                {
                    g.DrawImage(packageImage, package.X, package.Y, package.Width, package.Height);
                }
                else
                {
                    // Fallback rectangle
                    g.FillRectangle(Brushes.Brown, package.X, package.Y, package.Width, package.Height);
                }
            }
        }

        /// <summary>
        /// Draws player vehicle.
        /// </summary>
        /// <param name="g">Graphics object on which player's vehicle is drawn</param>
        private void DrawPlayer(Graphics g)
        {
            if (playerImage != null)
            {
                g.DrawImage(playerImage, playerX, PLAYER_Y, PLAYER_WIDTH, PLAYER_HEIGHT);
            }
            else
            {
                // Fallback rectangle
                g.FillRectangle(Brushes.Blue, playerX, PLAYER_Y, PLAYER_WIDTH, PLAYER_HEIGHT);
            }
        }

        /// <summary>
        /// Handles click on menu button. Pauses game and requests menu.
        /// </summary>
        /// <param name="sender">PictureBox as a menu button</param>
        /// <param name="e">Event arguments</param>
        private void picMenu_Click(object? sender, EventArgs e)
        {
            // Pause the game
            StopGame();

            // Request menu
            MenuRequested?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Resumes the minigame after returning from menu.
        /// </summary>
        public void ResumeGame()
        {
            gameTimer?.Start();
            spawnTimer?.Start();
            Focus();
        }

        /// <summary>
        /// Loads all graphics for the minigame.
        /// </summary>
        private void LoadGraphics()
        {
            // Dispose previous images
            DisposeImages();

            // Load player vehicle
            string vehiclePath = Path.Combine(graphicsPath, "vehicles", $"pojazd{vehicleID}.png");
            playerImage = ResourceHelper.LoadImageWithoutLock(vehiclePath);

            // Load package image
            string packagePath = Path.Combine(graphicsPath, "minigame", "paczka.png");
            packageImage = ResourceHelper.LoadImageWithoutLock(packagePath);

            // Load background
            string bgPath = Path.Combine(graphicsPath, "minigame", "minigra-tlo.png");
            backgroundImage = ResourceHelper.LoadImageWithoutLock(bgPath);

            // Load menu button
            string menuPath = Path.Combine(graphicsPath, "menu.png");
            ResourceHelper.LoadPictureBoxImage(picMenu, menuPath);
        }

        /// <summary>
        /// Disposes loaded images.
        /// </summary>
        private void DisposeImages()
        {
            if (playerImage != null)
            {
                playerImage.Dispose();
                playerImage = null;
            }
            if (packageImage != null)
            {
                packageImage.Dispose();
                packageImage = null;
            }
            if (backgroundImage != null)
            {
                backgroundImage.Dispose();
                backgroundImage = null;
            }

            // Dispose menu button image
            ResourceHelper.DisposePictureBoxImage(picMenu);
        }
    }
}