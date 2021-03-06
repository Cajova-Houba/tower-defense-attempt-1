﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;
using TowerDefenseAttempt1.org.valesz.towerdefatt.UI.Buttons;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.UI
{
    public class GameOverPanel : AbstractPanel
    {
        public string GameOverMessage => "You lost and the game is over.";

        private float AgainButtonX => X + 170;
        private float AgainButtonY => Y + 60;

        protected Texture2D againButtonBackground;
        private SimpleButton againButton;

        public GameOverPanel(int panelWidth, int panelHeight, float x, float y, Color backgroundColor, SpriteFont textFont, Map gameMap) 
            : base(panelWidth, panelHeight, new Vector2(x,y), backgroundColor, textFont, textFont, gameMap)
        {
        }

        public override void InitPanel(GraphicsDevice graphicsDevice)
        {
            base.InitPanel(graphicsDevice);
            againButton = new SimpleButton(AgainButtonX, AgainButtonY, "Retry", TextFont);
            againButton.Initialize(graphicsDevice);
        }

        /// <summary>
        /// Draws panel using the given sprite batch.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object to use for drawing.</param>
        public void DrawPanel(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(panelBackground, new Vector2(X, Y), Color.White);

            spriteBatch.DrawString(TextFont, GameOverMessage, new Vector2(X + 100, Y + 25), Color.Black);
            againButton.Draw(spriteBatch);
        }

        /// <summary>
        /// Returns true if the retry button is under given coordinates.
        /// </summary>
        /// <param name="clickX"></param>
        /// <param name="clickY"></param>
        /// <returns></returns>
        public bool RetryButtonClicked(float clickX, float clickY)
        {
            return againButton.IsClick(clickX, clickY);
        }
    }
}
