using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.UI
{
    /// <summary>
    /// Simple panel which displays stats as lines data in two columns.
    /// </summary>
    public class StatsDisplaySidePanel : AbstractPanel
    {

        /// <summary>
        /// Height of one line item.
        /// </summary>
        public float LineHeight { get; set; }

        /// <summary>
        /// Where to start drawing content on the panel. Relative to the top of the panel.
        /// </summary>
        public float ContentTopBase { get; set; }

        /// <summary>
        /// Where to start drawing the content of the first column. Relative to the left side of the panel.
        /// </summary>
        public float Column1Base { get; set; }

        /// <summary>
        /// Where to start drawing the content of the second column. Relative to the left side of the panel.
        /// </summary>
        public float Column2Base { get; set; }


        public StatsDisplaySidePanel(int panelWidth, int panelHeight, float x, float y, Color backgroundColor, float lineHeight, float contentTopBase, float column1Base, float column2Base, SpriteFont font, Map gameMap)
            :base(panelWidth, panelHeight, new Vector2(x,y), backgroundColor, font, gameMap)
        {
            LineHeight = lineHeight;
            ContentTopBase = contentTopBase;
            Column1Base = column1Base;
            Column2Base = column2Base;
        }

        /// <summary>
        /// Drwas the panel using the provided sprite batch.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object to use for drawing.</param>
        /// <param name="textures">Dictionary containing textures.</param>
        public void DrawPanel(SpriteBatch spriteBatch, Dictionary<string, Texture2D> textures)
        {
            spriteBatch.Draw(panelBackground, new Vector2(X, Y), Color.White);
            DrawTextLine(spriteBatch, "Score", GameMap.Score.ToString(), 0);
            DrawTextLine(spriteBatch, "Kills", GameMap.EnemiesKilled.ToString(), 1);
            DrawTextLine(spriteBatch, "Enemies", GameMap.Enemies.Count.ToString(), 2);
            DrawTextLine(spriteBatch, "Money", GameMap.Money.ToString(), 3);
            DrawTextLine(spriteBatch, "Base HP", GameMap.Base.Hp.ToString(), 4);

            DrawTowerShop(spriteBatch, "Towers", 6, textures);

            DrawSelectedMapTower(spriteBatch, GameMap.SelectedMapTower, 18);
        }

        /// <summary>
        /// Draws text in two columns.
        /// </summary>
        /// <param name="spriteBatch">Object to be used for drawing.</param>
        /// <param name="column1">Contents of the column 1.</param>
        /// <param name="column2">Contents of the column 2.</param>
        /// <param name="lineNumber">Number of the line (used for spacing). Starts at 0.</param>
        private void DrawTextLine(SpriteBatch spriteBatch, string column1, string column2, int lineNumber)
        {
            spriteBatch.DrawString(TextFont, column1, new Vector2(X + Column1Base, Y + ContentTopBase + lineNumber*LineHeight), Color.Black);
            spriteBatch.DrawString(TextFont, column2, new Vector2(X + Column2Base, Y + ContentTopBase + lineNumber*LineHeight), Color.Black);
        }

        /// <summary>
        /// Draws towers available in the shop.
        /// </summary>
        /// <param name="spriteBatch">Object to be used for drawing.</param>
        /// <param name="heading">Heading of the shop.</param>
        /// <param name="lineNumber">Line number where to print heading. Will be used to print rest of the shop too.</param>
        /// <param name="textures">Dictionary which contains textures used to draw towers.</param>
        private void DrawTowerShop(SpriteBatch spriteBatch, string heading, int lineNumber, Dictionary<string, Texture2D> textures)
        {
            DrawTextLine(spriteBatch, heading, "", lineNumber);
            for (int i = 0; i < GameMap.AvailableTowers.Count; i++)
            {
                float tY = Y + ContentTopBase + LineHeight * (lineNumber + 1) + 5 + i * 70;
                ITower tower = GameMap.AvailableTowers[i];
                tower.Position = new Vector2(X + Column1Base, tY);
                spriteBatch.Draw(textures[tower.TextureName], tower.Position, Color.White);
                spriteBatch.DrawString(TextFont, "x" + tower.Price.ToString(), new Vector2(X + Column1Base + 70, tY + 27), Color.Black);
            }
        }

        /// <summary>
        /// Draws tower that is selected on the map.
        /// </summary>
        private void DrawSelectedMapTower(SpriteBatch spriteBatch, ITower tower, int lineNumber)
        {
            DrawTextLine(spriteBatch, "Selected tower", "", lineNumber);
            if (GameMap.SelectedMapTower != null)
            {
                DrawTextLine(spriteBatch, "Attack", tower.Damage.ToString(), lineNumber+1);
                DrawTextLine(spriteBatch, "Speed", tower.AttackSpeed.ToString(), lineNumber+2);
                DrawTextLine(spriteBatch, "(U)pgrade", "x"+tower.UpgradePrice.ToString(), lineNumber+3);
            }
        }
    }
}
