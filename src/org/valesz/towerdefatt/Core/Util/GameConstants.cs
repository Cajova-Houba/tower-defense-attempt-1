using Godot;
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

        internal const string NO_ACTION = "";
        public const string UPGRADE_ACTION = "upgrade_action";
        internal const string SHOP_TOWER_ACTION = "shop_tower_action";
        internal const string SHOP_OBSTACLE_ACTION = "shop_obstacle_action";

        public const string ENEMIES_GROUP = "enemies";
        public const string SELECTABLE_GROUP = "selectable";

        /// <summary>
        /// Group for all items bought by player.
        /// </summary>
        public const string PLAYER_ITEMS = "itemsBoughtByPlayer";

        /// <summary>
        /// The Level node is always the root of level scene.
        /// </summary>
        public const string LEVEL_NODE = "/root/Level";

        /// <summary>
        /// Base node in the level.
        /// </summary>
        public const string BASE_NODE = LEVEL_NODE+"/Base";
        internal static string[] ACTIONS = new string[] { 
            UPGRADE_ACTION,
            SHOP_TOWER_ACTION,
            SHOP_OBSTACLE_ACTION
        };
    }
}
