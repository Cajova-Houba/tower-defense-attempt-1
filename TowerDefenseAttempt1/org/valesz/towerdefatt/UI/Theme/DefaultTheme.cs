using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.UI.Theme
{
    public class DefaultTheme : ITheme
    {
        public Color BackgroundColor => Color.LightGray;

        public int LineHeight => 15;

        public float MarginTop => 20;

        public float MarginLeft => 20;

        public Color FontColor => Color.Black;

        public int BigLineHeight => 20;

        public SpriteFont TextFont { get; private set; }

        public SpriteFont BoldTextFont { get; private set; }

        public DefaultTheme(SpriteFont textFont, SpriteFont boldFont)
        {
            TextFont = textFont;
            BoldTextFont = boldFont;
        }
    }
}
