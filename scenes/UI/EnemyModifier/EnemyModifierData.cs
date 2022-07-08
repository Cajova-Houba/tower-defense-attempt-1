using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EnemyModifier;

namespace TowerDefenseAttempt1.scenes.UI.EnemyModifier
{
    /// <summary>
    /// Immutable class used to represent data of enemy modifier.
    /// </summary>
    public class EnemyModifierData
    {
        public readonly ModifierType modifierType;
        public readonly uint value;

        public EnemyModifierData(ModifierType modifierType, uint value)
        {
            this.modifierType = modifierType;
            this.value = value;
        }
    }
}
