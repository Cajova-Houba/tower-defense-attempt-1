using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.UI.Labels
{
    /// <summary>
    /// Label for numeric values.
    /// </summary>
    public class NumberLabel
    {
        private readonly string value;

        public NumberLabel(string value)
        {
            this.value = value;
        }

        public NumberLabel(float floatVal): this(string.Format(CultureInfo.GetCultureInfo("en-US"), "{0:0.##}", floatVal))
        {
        }

        public NumberLabel(uint uintVal): this(string.Format(CultureInfo.GetCultureInfo("en-US"), "{0}", uintVal))
        {
        }

        /// <summary>
        /// Get the string value to print.
        /// </summary>
        /// <returns>Printed label.</returns>
        public virtual string Get()
        {
            return value;
        }
    }
}
