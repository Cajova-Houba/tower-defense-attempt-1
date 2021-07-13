using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Configuration;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Controls;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core.Statistics;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Statistics;
using TowerDefenseAttempt1.org.valesz.towerdefatt.UI;

namespace TowerDefenseAttempt1
{
    public class Game1 : Game
    {
        private const int WIDTH = 1000;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        /// <summary>
        /// All textures used throughout the game.
        /// 
        /// Texture map -> texture
        /// </summary>
        Dictionary<string, Texture2D> textures;

        SpriteFont scoreFont, scoreFontBold;

        Texture2D grassTexture;

        /// <summary>
        /// Side panel for displaying game stats and buying towers.
        /// </summary>
        float sidePanelX = 750;
        float sidePanelY = 20;
        int sidePanelW = 225;
        int sidePanelH = 430;
        StatsDisplaySidePanel statsDisplaySidePanel;
        GameOverPanel gameOverPanel;
        ControlsPanel controlsPanel;


        Map gameMap;

        KeyboardHandler keyboardHandler;
        MouseHandler mouseHandler;
        GameState gameState;

        IStatsCollector statsCollector;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = WIDTH;
            _graphics.ApplyChanges();

            statsCollector = new CsvStatsCollector();

            keyboardHandler = new KeyboardHandler();
            mouseHandler = new MouseHandler();
            gameState = new GameState();
            gameState.Pause();

            // TODO: Add your initialization logic here
            gameMap = new Map() { StatsCollector = statsCollector };
            ConfigUtils.StartNewMapWithDefaultConfiguration(gameMap);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            InitTextureMap(gameMap);

            scoreFont = Content.Load<SpriteFont>("assets/fonts/score");
            scoreFontBold = Content.Load<SpriteFont>("assets/fonts/scoreBold");
            grassTexture = Content.Load<Texture2D>("assets/ground/grass");

            InitSidePanel();
            InitGameOverPanel();
            InitControlsPanel();

        }

        private void InitControlsPanel()
        {
            controlsPanel = new ControlsPanel(400, 200, new Vector2(150, 100), Color.LightGray, scoreFont, gameMap);
            controlsPanel.InitPanel(GraphicsDevice);
        }

        private void InitGameOverPanel()
        {
            gameOverPanel = new GameOverPanel(400, 100, 100, 100, Color.LightGray, scoreFont, gameMap);
            gameOverPanel.InitPanel(GraphicsDevice);
        }

        private void InitSidePanel()
        {
            statsDisplaySidePanel = new StatsDisplaySidePanel(sidePanelW, sidePanelH, sidePanelX, sidePanelY, 
                Color.LightGray, 
                15, 30, 
                10, 85, 
                scoreFont, scoreFontBold, gameMap);
            statsDisplaySidePanel.InitPanel(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (keyboardHandler.ShouldExit())
            {
                Exit();
            }

            mouseHandler.HandleMouse(gameState, gameMap, sidePanelX, gameOverPanel, controlsPanel);
            keyboardHandler.HandleKeyboard(gameState, gameMap);

            // TODO: Add your update logic here
            if (!gameMap.GameLost() && !gameState.IsPaused())
            {
                foreach(IHasAI enemy in gameMap.Enemies)
                {
                    enemy.UpdateState(gameMap);
                }

                foreach(IHasAI tower in gameMap.Towers)
                {
                    tower.UpdateState(gameMap);
                }

                for(int i = gameMap.Enemies.Count -1; i >= 0; i--)
                {
                    if (gameMap.Enemies[i].Hp == 0)
                    {
                        gameMap.AddScore(gameMap.Enemies[i].Value);
                        gameMap.IncrementKillCounter();
                        gameMap.Enemies.RemoveAt(i);
                    }
                }
                gameMap.SpawnEnemies();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);
            _spriteBatch.Draw(grassTexture, 
                new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), 
                new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), 
                Color.White);
            _spriteBatch.Draw(textures[gameMap.Base.TextureName], gameMap.Base.Position, Color.White);

            foreach (ITower tower in gameMap.Towers)
            {
                _spriteBatch.Draw(textures[tower.TextureName], tower.Position, Color.White);
                if (tower.Shot != null)
                {
                    DrawLine(tower.Shot[0], tower.Shot[1]);
                }
            }

            foreach (IHasTexture enemy in gameMap.Enemies) 
            {
                _spriteBatch.Draw(textures[enemy.TextureName], enemy.Position, Color.White);
            }
            

            DrawSidePanel();

            DrawControlsPanel();

            DrawGameOverPanel();
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawControlsPanel()
        {
            if (gameState.IsPaused())
            {
                controlsPanel.DrawPanel(_spriteBatch);
            }
        }

        /// <summary>
        /// Draw panel on the end of the game.
        /// </summary>
        private void DrawGameOverPanel()
        {
            if (gameMap.GameLost())
            {
                statsCollector.FinalizeStatistics();
                gameOverPanel.DrawPanel(_spriteBatch);
            }
        }

        /// <summary>
        /// Draws side panel with game stats and tower 'shop'.
        /// </summary>
        private void DrawSidePanel()
        {
            statsDisplaySidePanel.DrawPanel(_spriteBatch, textures);
        }

        /// <summary>
        /// Draws a black line from point point1 to point point2.
        /// </summary>
        /// <param name="point1">Starting point of the line.</param>
        /// <param name="point2">Ending point of the line. </param>
        private void DrawLine(Point point1, Point point2)
        {
            // how to draw a line
            // 1. know the lngth of a line and draw it as a rectangle with height of 1
            // 2. rotate it by angle phi (can be calculated using trigonometry)
            Vector2 distanceVector = (point2 - point1).ToVector2();
            float len = distanceVector.Length();
            float alpha = (float)Math.Asin(distanceVector.Y / len);

            // 2nd and 3rd quadrants
            if ((distanceVector.X < 0 && distanceVector.Y > 0) || 
                (distanceVector.X < 0 && distanceVector.Y < 0)
                )
            {
                alpha = (float)(Math.PI - alpha);
            }

            _spriteBatch.Draw(textures["assets/attack/shot"], new Rectangle(point1, new Point((int)len, 1)), null, Color.White, alpha, new Vector2(0f, 0f), SpriteEffects.None, 1f);
        }

        /// <summary>
        /// Loads all textures needed by the objects in the given game map. Textures are stored in the
        /// "textures" dictionary.
        /// </summary>
        /// <param name="gameMap">Map to laod textures for.</param>
        private void InitTextureMap(Map gameMap)
        {
            textures = new Dictionary<string, Texture2D>();
            textures.Add("assets/attack/shot", Content.Load<Texture2D>("assets/attack/shot"));

            foreach (string textureName in gameMap.GetAllTextures())
            {
                textures.Add(textureName, Content.Load<Texture2D>(textureName));
            }
        }
    }
}
