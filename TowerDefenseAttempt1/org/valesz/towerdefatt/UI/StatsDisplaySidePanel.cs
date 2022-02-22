using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core.GameShop;
using TowerDefenseAttempt1.org.valesz.towerdefatt.UI.Labels;
using TowerDefenseAttempt1.org.valesz.towerdefatt.UI.Theme;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.UI
{
    /// <summary>
    /// Simple panel which displays stats as lines data in two columns.
    /// </summary>
    public class StatsDisplaySidePanel : AbstractPanel
    {

        /// <summary>
        /// Where to start drawing the content of the second column. Relative to the left side of the panel.
        /// </summary>
        public float Column2Base { get; set; }


        public StatsDisplaySidePanel(int panelWidth, int panelHeight, float x, float y, float column2Base, Map gameMap, ITheme theme)
            :base(panelWidth, panelHeight, new Vector2(x,y), gameMap, theme)
        {
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
            int cntr = 0;
            DrawTextLine(spriteBatch, "Money", new PriceLabel(GameMap.Money).Get(), cntr++, true);
            cntr++;

            DrawTextLine(spriteBatch, "Score", new NumberLabel(GameMap.Score).Get(), cntr++);
            DrawTextLine(spriteBatch, "Kills", new NumberLabel(GameMap.EnemiesKilled).Get(), cntr++);
            DrawTextLine(spriteBatch, "Enemies", new NumberLabel(GameMap.Enemies.Count).Get(), cntr++);
            DrawTextLine(spriteBatch, "Base HP", new NumberLabel(GameMap.Base.Hp).Get(), cntr++);
            cntr++;

            DrawShop(spriteBatch, "Items", cntr++, textures);

            DrawSelectedItem(spriteBatch, 18);
        }

        /// <summary>
        /// Draws text in two columns.
        /// </summary>
        /// <param name="spriteBatch">Object to be used for drawing.</param>
        /// <param name="column1">Contents of the column 1.</param>
        /// <param name="column2">Contents of the column 2.</param>
        /// <param name="lineNumber">Number of the line (used for spacing). Starts at 0.</param>
        private void DrawTextLine(SpriteBatch spriteBatch, string column1, string column2, int lineNumber, bool bold = false)
        {
            SpriteFont font = bold ? Theme.BoldTextFont : Theme.TextFont;
            spriteBatch.DrawString(font, column1, new Vector2(X + Theme.MarginLeft, Y + Theme.MarginTop + lineNumber* Theme.LineHeight), Theme.FontColor);
            spriteBatch.DrawString(font, column2, new Vector2(X + Column2Base, Y + Theme.MarginTop + lineNumber* Theme.LineHeight), Theme.FontColor);
        }

        /// <summary>
        /// Draws towers available in the shop.
        /// </summary>
        /// <param name="spriteBatch">Object to be used for drawing.</param>
        /// <param name="heading">Heading of the shop.</param>
        /// <param name="lineNumber">Line number where to print heading. Will be used to print rest of the shop too.</param>
        /// <param name="textures">Dictionary which contains textures used to draw towers.</param>
        private void DrawShop(SpriteBatch spriteBatch, string heading, int lineNumber, Dictionary<string, Texture2D> textures)
        {
            DrawTextLine(spriteBatch, heading, "", lineNumber);
            int lineCounter = 0;
            foreach (IShopItem shopItem in GameMap.Shop.AvailableTowers)
            {
                float tY = Y + Theme.MarginTop + Theme.LineHeight * (lineNumber + 1) + 5 + lineCounter * 70;
                DrawShopItem(spriteBatch, shopItem, tY, textures);
                lineCounter++;
            }
            foreach (IShopItem shopItem in GameMap.Shop.AvailableObstacles)
            {
                float tY = Y + Theme.MarginTop + Theme.LineHeight * (lineNumber + 1) + 5 + lineCounter * 70;
                DrawShopItem(spriteBatch, shopItem, tY, textures);
                lineCounter++;
            }
        }

        private void DrawShopItem(SpriteBatch spriteBatch, IShopItem shopItem, float tY, Dictionary<string, Texture2D> textures)
        {
            shopItem.Position = new Vector2(X + Theme.MarginLeft, tY);
            spriteBatch.Draw(textures[shopItem.TextureName], shopItem.Position, Color.White);
            spriteBatch.DrawString(Theme.TextFont, new PriceLabel(shopItem.Price).Get(), new Vector2(X + Theme.MarginLeft + 70, tY + 27), Theme.FontColor);
        }

        /// <summary>
        /// Draws item that is selected on the map.
        /// </summary>
        private void DrawSelectedItem(SpriteBatch spriteBatch, int lineNumber)
        {
            DrawTextLine(spriteBatch, "Selected", "", lineNumber);
            if (GameMap.SelectedItem == null)
            {
                return;
            }

            if (GameMap.SelectedItem is ITower)
            {
                ITower tower = (ITower)GameMap.SelectedItem;
                DrawTextLine(spriteBatch, "Attack", new NumberLabel(tower.Damage).Get(), lineNumber+1);
                DrawTextLine(spriteBatch, "Speed", new NumberLabel(tower.AttackSpeed).Get(), lineNumber+2);
                DrawTextLine(spriteBatch, "(U)pgrade", new PriceLabel(tower.UpgradePrice).Get(), lineNumber+3);
            } else if (GameMap.SelectedItem is IObstacle)
            {
                IObstacle obstacle = (IObstacle)GameMap.SelectedItem;
                DrawTextLine(spriteBatch, "HP", obstacle.Hp.ToString(), lineNumber + 1);
                DrawTextLine(spriteBatch, "(U)pgrade", new PriceLabel(obstacle.UpgradePrice).Get(), lineNumber + 3);
            }
        }
    }
}
