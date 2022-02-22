using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;
using TowerDefenseAttempt1.org.valesz.towerdefatt.UI.Buttons;
using TowerDefenseAttempt1.org.valesz.towerdefatt.UI.Theme;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.UI
{
    /// <summary>
    /// Simple panel used to display controls at the beginning of the game.
    /// </summary>
    public class ControlsPanel : AbstractPanel
    {
        private float ResumeButtonX => X + 150;
        private float ResumeButtonY => X + 110;

        private readonly float TextX;
        private readonly float TextY;

        private SimpleButton resumeGameButton;
        private string[] textLines = new string[] {
            "Controls",
            "Buying, placing and selecting towers: Mouse",
            "Upgrading selected tower: U",
            "Pause / unpause the game: P, Pause",
            "Exit game: Esc"
        };

        public ControlsPanel(int panelWidth, int panelHeight, Vector2 position, Map gameMap, ITheme theme) 
            : base(panelWidth, panelHeight, position, gameMap, theme)
        {
            TextX = X + theme.MarginLeft;
            TextY = Y + theme.MarginTop;
        }

        public override void InitPanel(GraphicsDevice graphicsDevice)
        {
            base.InitPanel(graphicsDevice);

            resumeGameButton = new SimpleButton(ResumeButtonX, ResumeButtonY, "Resume", Theme.TextFont);
            resumeGameButton.Initialize(graphicsDevice);
        }

        /// <summary>
        /// Draws panel using the given sprite batch.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object to use for drawing.</param>
        public void DrawPanel(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(panelBackground, new Vector2(X, Y), Color.White);

            for(int i = 0; i < textLines.Length; i++)
            {
                DrawTextLine(spriteBatch, TextX, TextY, i, textLines[i]);
            }

            resumeGameButton.Draw(spriteBatch);
        }

        public bool IsResumeButtonClick(float x, float y)
        {
            return resumeGameButton.IsClick(x, y);
        }

        private void DrawTextLine(SpriteBatch spriteBatch, float x, float y, int lineNumber, string text)
        {
            spriteBatch.DrawString(Theme.TextFont, text, new Vector2(x, y + lineNumber * Theme.BigLineHeight), Theme.FontColor);
        }
    }
}
