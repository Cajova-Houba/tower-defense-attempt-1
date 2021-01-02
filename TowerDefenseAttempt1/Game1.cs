using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Base;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Spawn;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Tower;

namespace TowerDefenseAttempt1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        /// <summary>
        /// All textures used throughout the game.
        /// 
        /// Texture map -> texture
        /// </summary>
        Dictionary<string, Texture2D> textures;

        SpriteFont scoreFont;

        Texture2D grassTexture;

        /// <summary>
        /// Side panel for displaying game stats and buying towers.
        /// </summary>
        Texture2D sidePanel;
        float sidePanelX = 630;
        float sidePanelY = 20;

        /// <summary>
        /// Sets to true when the left mouse button is presed (used to track click: press-release).
        /// </summary>
        bool leftMouseClick;

        Map gameMap;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;   
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            gameMap = new Map();
            gameMap.StartNewMap(new DefaultBase(100,100), new WaveSpawner(new List<ISpawnPoint> {
                new DefaultSpawnPoint(550, 30),
                new DefaultSpawnPoint(545, 38),
                new DefaultSpawnPoint(510, 83),
                new DefaultSpawnPoint(479, 155),
                new DefaultSpawnPoint(480, 160),
                new DefaultSpawnPoint(495, 170),
                new DefaultSpawnPoint(510, 270),
                new DefaultSpawnPoint(515, 280),
                new DefaultSpawnPoint(505, 301)
            }));
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            InitTextureMap(gameMap);

            scoreFont = Content.Load<SpriteFont>("assets/fonts/score");
            grassTexture = Content.Load<Texture2D>("assets/ground/grass");

            InitSidePanel();
        }

        private void InitSidePanel()
        {
            sidePanel = new Texture2D(GraphicsDevice, 150, 400);
            Color[] data = new Color[150 * 400];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.LightGray;
            sidePanel.SetData(data);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            HandleMouse();

            // TODO: Add your update logic here
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

            base.Update(gameTime);
        }

        /// <summary>
        /// Checks for mous click and places tower if possible.
        /// </summary>
        private void HandleMouse()
        {
            MouseState state = Mouse.GetState();
            
            // selecting tower from shop
            if (state.LeftButton == ButtonState.Pressed && state.X > sidePanelX)
            {
                leftMouseClick = true;
            } else if (state.LeftButton == ButtonState.Released && leftMouseClick && state.X > sidePanelX)
            {
                leftMouseClick = false;
                gameMap.SelectTowerFromShop(state.X, state.Y);
            } 

            // placing selected tower
            else if (gameMap.SelectedShopTower != null)
            {
                if (state.LeftButton == ButtonState.Pressed &&  state.X < sidePanelX)
                {
                    leftMouseClick = true;
                } else if (state.LeftButton == ButtonState.Released && leftMouseClick && state.X < sidePanelX)
                {
                    leftMouseClick = false;
                    gameMap.BuyTower(gameMap.SelectedShopTower.Clone(state.X-gameMap.SelectedShopTower.Center.X, state.Y - gameMap.SelectedShopTower.Center.Y));
                }
            }
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
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Draws side panel with game stats and tower 'shop'.
        /// </summary>
        private void DrawSidePanel()
        {
            float lineHeight = 15;
            float textHeightBase = 30;
            float text1XBase = sidePanelX + 10;
            float text2XBase = sidePanelX + 80;

            _spriteBatch.Draw(sidePanel, new Vector2(sidePanelX, sidePanelY), Color.White);
            _spriteBatch.DrawString(scoreFont, "Score", new Vector2(text1XBase, textHeightBase), Color.Black);
            _spriteBatch.DrawString(scoreFont, gameMap.Score.ToString(), new Vector2(text2XBase, textHeightBase), Color.Black);


            _spriteBatch.DrawString(scoreFont, "Kills", new Vector2(text1XBase, textHeightBase + lineHeight), Color.Black);
            _spriteBatch.DrawString(scoreFont, gameMap.EnemiesKilled.ToString(), new Vector2(text2XBase, textHeightBase + lineHeight), Color.Black);

            _spriteBatch.DrawString(scoreFont, "Enemies", new Vector2(text1XBase, textHeightBase + lineHeight*2), Color.Black);
            _spriteBatch.DrawString(scoreFont, gameMap.Enemies.Count.ToString(), new Vector2(text2XBase, textHeightBase + lineHeight*2), Color.Black);

            _spriteBatch.DrawString(scoreFont, "Money", new Vector2(text1XBase, textHeightBase + lineHeight*3), Color.Black);
            _spriteBatch.DrawString(scoreFont, gameMap.Money.ToString(), new Vector2(text2XBase, textHeightBase + lineHeight*3), Color.Black);

            _spriteBatch.DrawString(scoreFont, "Base HP", new Vector2(text1XBase, textHeightBase + lineHeight*4), Color.Black);
            _spriteBatch.DrawString(scoreFont, gameMap.Base.Hp.ToString(), new Vector2(text2XBase, textHeightBase + lineHeight*4), Color.Black);

            _spriteBatch.DrawString(scoreFont, "Towers", new Vector2(text1XBase, textHeightBase + lineHeight * 6), Color.Black);
            float tX = text1XBase;
            for(int i = 0; i < gameMap.AvailableTowers.Count; i++)
            {
                float tY = textHeightBase + lineHeight * 7 + 5 + i * 70;
                ITower tower = gameMap.AvailableTowers[i];
                tower.Position = new Vector2(tX, tY);
                _spriteBatch.Draw(textures[tower.TextureName], tower.Position, Color.White);
                _spriteBatch.DrawString(scoreFont, "x" + tower.Price.ToString(), new Vector2(tX + 70, tY + 27), Color.Black);
            }
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
