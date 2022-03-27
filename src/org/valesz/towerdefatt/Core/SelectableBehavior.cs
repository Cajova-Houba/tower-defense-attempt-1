using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseAttempt1.src.org.valesz.towerdefatt.Core
{
    public class SelectableBehavior
    {
        protected const string DEFAULT_ANIMATION = "default";
        protected const string SELECTED_ANIMATION = "selected";

        private bool selected = false;

        public bool IsSelected()
        {
            return selected;
        }

        public void Deselect()
        {
            selected = false;
        }

        public void Select()
        {
            selected = true;
        }

        public void ChangeSelect()
        {
            selected = !selected;
        }

        public void UpdateAimation(GenericVisibleObject target)
        {
            target.GetAnimationNode().Animation = selected ? SELECTED_ANIMATION : DEFAULT_ANIMATION;
        }
    }
}
