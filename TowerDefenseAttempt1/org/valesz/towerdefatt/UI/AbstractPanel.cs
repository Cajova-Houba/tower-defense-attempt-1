using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.UI
{
    /// <summary>
    /// Base class for all panels.
    /// </summary>
    public abstract class AbstractPanel : IHasPosition
    {
        public int PanelWidth { get; set; }
        public int PanelHeight { get; set; }

        public Vector2 Position { get; set; }

        public float X => Position.X;

        public float Y => Position.Y;

        public Point Center => Position.ToPoint() + new Point(PanelWidth / 2, PanelHeight / 2);

        public Color BackgroundColor { get; set; }

        /// <summary>
        /// Font to be used to print text.
        /// </summary>
        public SpriteFont TextFont { get; set; }

        /// <summary>
        /// Game map object to get data from.
        /// </summary>
        public Map GameMap { get; set; }

        protected Texture2D panelBackground;


        protected AbstractPanel(int panelWidth, int panelHeight, Vector2 position, Color backgroundColor, SpriteFont textFont, Map gameMap)
        {
            PanelWidth = panelWidth;
            PanelHeight = panelHeight;
            Position = position;
            BackgroundColor = backgroundColor;
            TextFont = textFont;
            GameMap = gameMap;
        }

        /// <summary>
        /// Initializes the background of this panel.
        /// </summary>
        public virtual void InitPanel(GraphicsDevice graphicsDevice)
        {
            panelBackground = new Texture2D(graphicsDevice, PanelWidth, PanelHeight);
            Color[] data = new Color[PanelWidth * PanelHeight];
            for (int i = 0; i < data.Length; ++i) data[i] = BackgroundColor;
            panelBackground.SetData(data);
        }
    }
}
