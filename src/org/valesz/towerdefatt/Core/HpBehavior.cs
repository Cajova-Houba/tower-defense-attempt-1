using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseAttempt1.src.org.valesz.towerdefatt.Core
{
    public class HpBehavior
    {
        public uint Hp { get; protected set; }

        public bool IsDead => Hp == 0;

        public HpBehavior(uint maxHp)
        {
            Init(maxHp);
        }

        public void Init(uint maxHp)
        {
            Hp = maxHp;
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
