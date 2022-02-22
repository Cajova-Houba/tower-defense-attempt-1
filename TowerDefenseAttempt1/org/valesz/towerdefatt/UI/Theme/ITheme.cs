using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.UI.Theme
{
    /// <summary>
    /// Base interface for theme configuration.
    /// </summary>
    public interface ITheme
    {
        /// <summary>
        /// Color to be used as a background for given panel.
        /// </summary>
        Color BackgroundColor { get; }

        /// <summary>
        /// Text line height.
        /// </summary>
        int LineHeight { get; }

        /// <summary>
        /// Top margin for panels. Content should not start at y=0 but at y=MarginTop.
        /// </summary>
        float MarginTop { get; }

        /// <summary>
        /// Left margin for panels. Content should not start at x=0 but at x=MarginLeft.
        /// </summary>
        float MarginLeft { get; }

        /// <summary>
        /// Default font color to be used.
        /// </summary>
        Color FontColor { get; }

        /// <summary>
        /// Same as LineHeight but for bigger lines.
        /// </summary>
        int BigLineHeight { get; }

        /// <summary>
        /// Font to be used to print text.
        /// </summary>
        SpriteFont TextFont { get; }

        /// <summary>
        /// Same as TextFont but bold.
        /// </summary>
        SpriteFont BoldTextFont { get; }
    }
}
