using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;
using TowerDefenseAttempt1.org.valesz.towerdefatt.UI.Theme;

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

        /// <summary>
        /// Game map object to get data from.
        /// </summary>
        public Map GameMap { get; set; }

        protected Texture2D panelBackground;

        protected ITheme Theme { get; private set; }


        protected AbstractPanel(int panelWidth, int panelHeight, Vector2 position, Map gameMap, ITheme theme)
        {
            PanelWidth = panelWidth;
            PanelHeight = panelHeight;
            Position = position;
            GameMap = gameMap;
            Theme = theme;
        }

        /// <summary>
        /// Initializes the background of this panel.
        /// </summary>
        public virtual void InitPanel(GraphicsDevice graphicsDevice)
        {
            panelBackground = new Texture2D(graphicsDevice, PanelWidth, PanelHeight);
            Color[] data = new Color[PanelWidth * PanelHeight];
            for (int i = 0; i < data.Length; ++i) data[i] = Theme.BackgroundColor;
            panelBackground.SetData(data);
        }
    }
}
