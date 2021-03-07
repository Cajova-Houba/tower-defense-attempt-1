using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.UI
{
    public class GameOverPanel : AbstractPanel
    {
        public string GameOverMessage => "You lost and the game is over.";

        private float AgainButtonX => X + 170;
        private float AgainButtonY => Y + 67;

        private int againButtonWidth = 80;
        private int againButtonHeight = 30;
        protected Texture2D againButtonBackground;

        public GameOverPanel(int panelWidth, int panelHeight, float x, float y, Color backgroundColor, SpriteFont textFont, Map gameMap) 
            : base(panelWidth, panelHeight, new Vector2(x,y), backgroundColor, textFont, textFont, gameMap)
        {
        }

        public override void InitPanel(GraphicsDevice graphicsDevice)
        {
            base.InitPanel(graphicsDevice);
            againButtonBackground = new Texture2D(graphicsDevice, againButtonWidth, againButtonHeight);
            Color[] data = new Color[againButtonWidth * againButtonHeight];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Silver;
            againButtonBackground.SetData(data);
        }

        /// <summary>
        /// Draws panel using the given sprite batch.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object to use for drawing.</param>
        public void DrawPanel(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(panelBackground, new Vector2(X, Y), Color.White);

            spriteBatch.DrawString(TextFont, GameOverMessage, new Vector2(X + 100, Y + 25), Color.Black);
            DrawAgainButton(spriteBatch);
        }

        /// <summary>
        /// Returns true if the retry button is under given coordinates.
        /// </summary>
        /// <param name="clickX"></param>
        /// <param name="clickY"></param>
        /// <returns></returns>
        public bool RetryButtonClicked(float clickX, float clickY)
        {
            return clickX >= AgainButtonX && clickX <= AgainButtonX + againButtonWidth
                && clickY >= AgainButtonY && clickY <= AgainButtonY + againButtonHeight;
        }

        /// <summary>
        /// Draws button used to restart the game.
        /// </summary>
        /// <param name="spriteBatch">Object used for drawing.</param>
        private void DrawAgainButton(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(againButtonBackground, new Vector2(X + 150, Y + 60), Color.White);
            spriteBatch.DrawString(TextFont, "Retry", new Vector2(AgainButtonX, AgainButtonY), Color.Black);
        }
    }
}
