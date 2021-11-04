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
        public uint Hp { get; protected set; }

        public uint MaxHp { get; protected set; }

        public bool IsDead => Hp == 0;

        public AbstractLivingObject(uint maxHp, float x, float y) : base(x,y)
        {
            MaxHp = maxHp;
            Hp = MaxHp;
        }

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
