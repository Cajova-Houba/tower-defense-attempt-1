using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.UI.Labels
{
    public class PriceLabel: NumberLabel { 

        public PriceLabel(float floatVal) : base(string.Format(CultureInfo.GetCultureInfo("en-US"), "{0:C}", floatVal))
        {
        }
    }
}
