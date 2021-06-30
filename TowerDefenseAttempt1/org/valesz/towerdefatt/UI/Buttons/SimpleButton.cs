using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.UI.Buttons
{
    /// <summary>
    /// Simple button with given dimensions, position, color and label.
    /// </summary>
    public class SimpleButton
    {
        /// <summary>
        /// Margin on both sides.
        /// </summary>
        private readonly int sideMargin = 20;
        private readonly int topMargin = 5;

        /// <summary>
        /// Width to be used for one character.
        /// </summary>
        private readonly int charWidth = 10;

        private float x, y;
        /// <summary>
        /// The width is calculated from text.
        /// </summary>
        private int width, height;
        private string text;
        private Color backgroundColor;
        private SpriteFont textFont;
        private Texture2D background;

        /// <summary>
        /// Button must be initialized before it can be drawn.
        /// </summary>
        private bool initialized = false;

        /// <summary>
        /// Default values for everything possible.
        /// </summary>
        /// <param name="x">Top left X.</param>
        /// <param name="y">Top left Y.</param>
        /// <param name="text">Button caption.</param>
        public SimpleButton(float x, float y, string text, SpriteFont textFont) : this(x, y, 30, text, Color.Silver, textFont)
        {

        }

        public SimpleButton(float x, float y, int height, string text, Color backgroundColor, SpriteFont textFont)
        {
            this.x = x;
            this.y = y;
            this.width = CalculateWidth(text);
            this.height = height;
            this.text = text;
            this.backgroundColor = backgroundColor;
            this.textFont = textFont;
        }

        /// <summary>
        /// Initializes itself using given graphics device. Especially sets the background object.
        /// </summary>
        public void Initialize(GraphicsDevice graphicsDevice)
        {
            background = new Texture2D(graphicsDevice, width, height);
            Color[] data = new Color[width * height];
            for (int i = 0; i < data.Length; ++i) data[i] = backgroundColor;
            background.SetData(data);
            initialized = true;
        }


        /// <summary>
        /// Draws itself using given sprite batch.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch used by this component to draw itself.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!initialized)
            {
                throw new Exception("Button not initialized. Call Initialize() before drawing.");
            }

            spriteBatch.Draw(background, new Vector2(x, y), Color.White);
            spriteBatch.DrawString(textFont, text, new Vector2(x + sideMargin, y+topMargin), Color.Black);
        }

        /// <summary>
        /// Returns true if the button button is under given coordinates.
        /// </summary>
        /// <param name="clickX">Mouse click X</param>
        /// <param name="clickY">Mouse click Y</param>
        /// <returns>True if the button was clicked on</returns>
        public bool IsClick(float clickX, float clickY)
        {
            return clickX >= x && clickX <= x + width
                && clickY >= y && clickY <= y + height;
        }


        private int CalculateWidth(string text)
        {
            return sideMargin + text.Length * charWidth + sideMargin;
        }

    }
}
