using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;
using TowerDefenseAttempt1.org.valesz.towerdefatt.UI.Buttons;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.UI
{
    /// <summary>
    /// Simple panel used to display controls at the beginning of the game.
    /// </summary>
    public class ControlsPanel : AbstractPanel
    {
        private float ResumeButtonX => X + 150;
        private float ResumeButtonY => X + 110;

        private float TextX => X + 20;
        private float TextY => Y + 20;

        private SimpleButton resumeGameButton;
        private readonly float textLineHeight = 20;


        public ControlsPanel(int panelWidth, int panelHeight, Vector2 position, Color backgroundColor, SpriteFont textFont, Map gameMap) 
            : base(panelWidth, panelHeight, position, backgroundColor, textFont, textFont, gameMap)
        {
        }

        public override void InitPanel(GraphicsDevice graphicsDevice)
        {
            base.InitPanel(graphicsDevice);

            resumeGameButton = new SimpleButton(ResumeButtonX, ResumeButtonY, "Resume", TextFont);
            resumeGameButton.Initialize(graphicsDevice);
        }

        /// <summary>
        /// Draws panel using the given sprite batch.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object to use for drawing.</param>
        public void DrawPanel(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(panelBackground, new Vector2(X, Y), Color.White);

            DrawTextLine(spriteBatch, new Vector2(TextX, TextY), "Controls");
            int line = 2;
            DrawTextLine(spriteBatch, new Vector2(TextX, TextY + line++ * textLineHeight), "Buying, placing and selecting towers: Mouse");
            DrawTextLine(spriteBatch, new Vector2(TextX, TextY + line++ * textLineHeight), "Upgrading selected tower: U");
            DrawTextLine(spriteBatch, new Vector2(TextX, TextY + line++ * textLineHeight), "Pause / unpause the game: P, Pause");
            DrawTextLine(spriteBatch, new Vector2(TextX, TextY + line++ * textLineHeight), "Exit game: Esc");

            resumeGameButton.Draw(spriteBatch);
        }

        public bool IsResumeButtonClick(float x, float y)
        {
            return resumeGameButton.IsClick(x, y);
        }

        private void DrawTextLine(SpriteBatch spriteBatch, Vector2 position, string text)
        {
            spriteBatch.DrawString(TextFont, text, position, Color.Black);
        }
    }
}
