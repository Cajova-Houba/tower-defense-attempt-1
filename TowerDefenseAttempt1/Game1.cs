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

            base.Update(gameTime);
        }

        private void HandleMouse()
        {
            MouseState state = Mouse.GetState();
            
            if (state.LeftButton == ButtonState.Pressed && gameMap.Towers.Count == 0)
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
            _spriteBatch.Begin();

            _spriteBatch.Draw(textures[gameMap.Base.TextureName], gameMap.Base.Position, Color.White);
            foreach (IHasTexture tower in gameMap.Towers) 
            {
                _spriteBatch.Draw(textures[tower.TextureName], tower.Position, Color.White);
            }
            foreach (IHasTexture enemy in gameMap.Enemies) 
            {
                _spriteBatch.Draw(textures[enemy.TextureName], enemy.Position, Color.White);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void InitTextureMap(Map gameMap)
        {
            textures = new Dictionary<string, Texture2D>();

            foreach(string textureName in gameMap.GetAllTextures())
            {
                textures.Add(textureName, Content.Load<Texture2D>(textureName));
            }
        }
    }
}
