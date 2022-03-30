using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseAttempt1.src.org.valesz.towerdefatt.Core.Util
{
    /// <summary>
    /// Constants shared by objects in game.
    /// </summary>
    public class GameConstants
    {
        public const string LEFT_MOUSE_CLICK = "ui_mouse_left";

        public const string UPGRADE_ACTION = "upgrade_action";

        public const string ENEMIES_GROUP = "enemies";
        public const string SELECTABLE_GROUP = "selectable";

        /// <summary>
        /// The Level node is always the root of level scene.
        /// </summary>
        public const string LEVEL_NODE = "/root/Level";
    }
}
