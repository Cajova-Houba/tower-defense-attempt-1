using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Core.Abstract
{
    /// <summary>
    /// Abstract representation of an object that is visible and has hp.
    /// </summary>
    public abstract class AbstractLivingObject : AbstractVisibleObject, IHasHp
    {
        public uint Hp { get; private set; }

        public abstract uint MaxHp { get; }

        public void TakeHit(uint damage)
        {
            if (Hp < damage)
            {
                Hp = 0;
            }
            else
            {
                Hp -= damage;
            }
        }
    }
}
