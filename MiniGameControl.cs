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
        // Event raised when minigame is completed (target score reached)
        public event EventHandler? MinigameCompleted;

        // Event raised when player wants to open menu
        public event EventHandler? MenuRequested;

        // Game area boundaries
        private const int GAME_AREA_LEFT = 194;
        private const int GAME_AREA_RIGHT = 1086;
        private const int GAME_AREA_BOTTOM = 900;

        // Player settings
        private const int PLAYER_WIDTH = 60;
        private const int PLAYER_HEIGHT = 150;
        private const int PLAYER_SPEED = 15;
        private const int PLAYER_Y = 750;

        // Package settings
        private const int PACKAGE_WIDTH = 50;
        private const int PACKAGE_HEIGHT = 50;
        private const float PACKAGE_SPEED = 5f;
        private const int SPAWN_INTERVAL_MS = 1000;

        // Target score to complete minigame
        private const int TARGET_SCORE = 10;

        // Background scroll speed
        private const float BACKGROUND_SCROLL_SPEED = 3f;

        // Player position
        private float _playerX;

        // Current score
        private int _score;

        // Active packages on screen
        private List<Package> _packages;

        // Game timer for animation loop
        private System.Windows.Forms.Timer? _gameTimer;

        // Timer for spawning new packages
        private System.Windows.Forms.Timer? _spawnTimer;

        // Random generator for package positions
        private Random _random;

        // Background scroll offset
        private float _backgroundOffset;

        // Loaded images
        private Image? _playerImage;
        private Image? _packageImage;
        private Image? _backgroundImage;

        // Movement flags
        private bool _moveLeft;
        private bool _moveRight;

        // Paths
        private string _graphicsPath;

        // Current vehicle ID
        private int _vehicleID;

        public MiniGameControl(string graphicsPath)
        {
            InitializeComponent();

            _graphicsPath = graphicsPath;
            _packages = new List<Package>();
            _random = new Random();
            _score = 0;
            _backgroundOffset = 0;
            _moveLeft = false;
            _moveRight = false;

            // Get current vehicle
            _vehicleID = PlayerProgress.GetCurrentVehicleID();

            // Center player horizontally in game area
            _playerX = (GAME_AREA_LEFT + GAME_AREA_RIGHT) / 2 - PLAYER_WIDTH / 2;

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
            _score = 0;
            _packages.Clear();
            _backgroundOffset = 0;
            _playerX = (GAME_AREA_LEFT + GAME_AREA_RIGHT) / 2 - PLAYER_WIDTH / 2;

            // Load graphics
            LoadGraphics();

            // Update display
            UpdateScoreDisplay();

            // Initialize game timer (60 FPS)
            if (_gameTimer == null)
            {
                _gameTimer = new System.Windows.Forms.Timer();
                _gameTimer.Interval = 16; // ~60 FPS
                _gameTimer.Tick += GameLoop;
            }

            // Initialize spawn timer
            if (_spawnTimer == null)
            {
                _spawnTimer = new System.Windows.Forms.Timer();
                _spawnTimer.Interval = SPAWN_INTERVAL_MS;
                _spawnTimer.Tick += SpawnPackage;
            }

            // Start timers
            _gameTimer.Start();
            _spawnTimer.Start();

            // Focus for keyboard input
            Focus();
        }

        /// <summary>
        /// Stops the minigame.
        /// </summary>
        public void StopGame()
        {
            _gameTimer?.Stop();
            _spawnTimer?.Stop();
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
            if (_score >= TARGET_SCORE)
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
            if (_moveLeft)
            {
                _playerX -= PLAYER_SPEED;
            }
            if (_moveRight)
            {
                _playerX += PLAYER_SPEED;
            }

            // Clamp to game area
            float minX = GAME_AREA_LEFT;
            float maxX = GAME_AREA_RIGHT - PLAYER_WIDTH;
            _playerX = Math.Clamp(_playerX, minX, maxX);
        }

        /// <summary>
        /// Updates background scroll offset.
        /// </summary>
        private void UpdateBackground()
        {
            _backgroundOffset += BACKGROUND_SCROLL_SPEED;

            // Reset when scrolled full height
            if (_backgroundImage != null && _backgroundOffset >= _backgroundImage.Height)
            {
                _backgroundOffset = 0;
            }
        }

        /// <summary>
        /// Updates all package positions.
        /// </summary>
        private void UpdatePackages()
        {
            foreach (var package in _packages)
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
            RectangleF playerBounds = new RectangleF(_playerX, PLAYER_Y, PLAYER_WIDTH, PLAYER_HEIGHT);

            foreach (var package in _packages)
            {
                if (!package.IsCollected && package.CollidesWith(playerBounds))
                {
                    package.IsCollected = true;
                    _score++;
                    UpdateScoreDisplay();
                }
            }
        }

        /// <summary>
        /// Removes collected and off-screen packages from list.
        /// </summary>
        private void CleanupPackages()
        {
            _packages.RemoveAll(p => p.IsCollected || p.IsOffScreen);
        }

        /// <summary>
        /// Spawns a new package at random X position.
        /// </summary>
        private void SpawnPackage(object? sender, EventArgs e)
        {
            // Random X within game area
            int minX = GAME_AREA_LEFT;
            int maxX = GAME_AREA_RIGHT - PACKAGE_WIDTH;
            float x = _random.Next(minX, maxX);

            // Start above screen
            float y = -PACKAGE_HEIGHT;

            Package package = new Package(x, y, PACKAGE_WIDTH, PACKAGE_HEIGHT, PACKAGE_SPEED);
            _packages.Add(package);
        }

        /// <summary>
        /// Updates the score label.
        /// </summary>
        private void UpdateScoreDisplay()
        {
            lblScore.Text = $"ZEBRANO: {_score} / {TARGET_SCORE}";
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
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            {
                _moveLeft = true;
            }
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                _moveRight = true;
            }
        }

        /// <summary>
        /// Handles key up events for player movement.
        /// </summary>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            {
                _moveLeft = false;
            }
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                _moveRight = false;
            }
        }

        /// <summary>
        /// Custom painting for the minigame.
        /// </summary>
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
        private void DrawBackground(Graphics g)
        {
            if (_backgroundImage == null)
            {
                // Fallback solid color
                g.Clear(Color.Gray);
                return;
            }

            // Draw two copies of background for seamless scrolling
            int y1 = (int)_backgroundOffset;
            int y2 = y1 - _backgroundImage.Height;

            g.DrawImage(_backgroundImage, 0, y1, Width, _backgroundImage.Height);
            g.DrawImage(_backgroundImage, 0, y2, Width, _backgroundImage.Height);
        }

        /// <summary>
        /// Draws all packages.
        /// </summary>
        private void DrawPackages(Graphics g)
        {
            foreach (var package in _packages)
            {
                if (_packageImage != null)
                {
                    g.DrawImage(_packageImage, package.X, package.Y, package.Width, package.Height);
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
        private void DrawPlayer(Graphics g)
        {
            if (_playerImage != null)
            {
                g.DrawImage(_playerImage, _playerX, PLAYER_Y, PLAYER_WIDTH, PLAYER_HEIGHT);
            }
            else
            {
                // Fallback rectangle
                g.FillRectangle(Brushes.Blue, _playerX, PLAYER_Y, PLAYER_WIDTH, PLAYER_HEIGHT);
            }
        }

        /// <summary>
        /// Handles click on menu button. Pauses game and requests menu.
        /// </summary>
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
            _gameTimer?.Start();
            _spawnTimer?.Start();
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
            string vehiclePath = Path.Combine(_graphicsPath, "vehicles", $"pojazd{_vehicleID}.png");
            _playerImage = ResourceHelper.LoadImageWithoutLock(vehiclePath);

            // Load package image
            string packagePath = Path.Combine(_graphicsPath, "minigame", "paczka.png");
            _packageImage = ResourceHelper.LoadImageWithoutLock(packagePath);

            // Load background
            string bgPath = Path.Combine(_graphicsPath, "minigame", "minigra-tlo.png");
            _backgroundImage = ResourceHelper.LoadImageWithoutLock(bgPath);

            // Load menu button
            string menuPath = Path.Combine(_graphicsPath, "menu.png");
            ResourceHelper.LoadPictureBoxImage(picMenu, menuPath);
        }

        /// <summary>
        /// Disposes loaded images.
        /// </summary>
        private void DisposeImages()
        {
            if (_playerImage != null)
            {
                _playerImage.Dispose();
                _playerImage = null;
            }
            if (_packageImage != null)
            {
                _packageImage.Dispose();
                _packageImage = null;
            }
            if (_backgroundImage != null)
            {
                _backgroundImage.Dispose();
                _backgroundImage = null;
            }

            // Dispose menu button image
            ResourceHelper.DisposePictureBoxImage(picMenu);
        }
    }
}