using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Configuration
{
    /// <summary>
    /// Common class containing the names of all textures used throughout the game. By using this class, we can ceep all the names in one place and not have
    /// them scattered around the codebase.
    /// </summary>
    public class Textures
    {
        public const string DEFAULT_OBSTACLE = "assets/obstacles/default";
        public const string DEFAULT_OBSTACLE_SELECTED = "assets/obstacles/default_selected";

        public const string DEFAULT_ENEMY = "assets/enemies/default";
        public const string DEFAULT_ENEMY_HURT = "assets/enemies/default_hurt";

        public const string DEFAULT_TOWER = "assets/towers/default";
        public const string DEFAULT_TOWER_SELECTED = "assets/towers/default_selected";
        public const string DEFAULT_TOWER_SHOT_SELECTED = "assets/towers/default_shot_selected";
        public const string DEFAULT_TOWER_SHOT = "assets/towers/default_shot";

    }
}
