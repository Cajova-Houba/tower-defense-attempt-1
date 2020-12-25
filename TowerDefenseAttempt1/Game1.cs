using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Base;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;
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
            gameMap.StartNewMap(new DefaultBase(100,100));

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

            uint enemiesToSpawn = 0;
            for(int i = gameMap.Enemies.Count -1; i >= 0; i--)
            {
                if (gameMap.Enemies[i].Hp == 0)
                {
                    gameMap.AddScore(gameMap.Enemies[i].Value);
                    enemiesToSpawn++;
                    gameMap.Enemies.RemoveAt(i);
                }
            }
            
            for(int i = 0; i< enemiesToSpawn; i++)
            {
                gameMap.SpawnEnemy();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Checks for mous click and places tower if possible.
        /// </summary>
        private void HandleMouse()
        {
            MouseState state = Mouse.GetState();
            
            if (state.LeftButton == ButtonState.Pressed)
            {
                gameMap.PlaceTower(new DefaultTower(state.X, state.Y));
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
            foreach (IHasTexture enemy in gameMap.Enemies) 
            {
                _spriteBatch.Draw(textures[enemy.TextureName], enemy.Position, Color.White);
            }
            foreach (ITower tower in gameMap.Towers) 
            {
                _spriteBatch.Draw(textures[tower.TextureName], tower.Position, Color.White);
                if (tower.Shot != null)
                {
                    DrawLine(tower.Shot[0], tower.Shot[1]);
                }
            }

            _spriteBatch.DrawString(scoreFont, gameMap.Score.ToString(), new Vector2(600, 15), Color.Black);
            _spriteBatch.End();

            base.Draw(gameTime);
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
