using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Controls
{
    /// <summary>
    /// Contains code for handling keyboard input.
    /// </summary>
    public class KeyboardHandler
    {
        const Keys TOWER_UPGRADE_KEY = Keys.U;
        const Keys EXIT_KEY = Keys.Escape;


        /// <summary>
        /// Checks if the exit keys are pressed.
        /// </summary>
        /// <returns>True if the exit keys are pressed and the game should exit.</returns>
        public bool ShouldExit()
        {
            return GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(EXIT_KEY);
        }

        public void HandleKeyboard(GameState gameState, Map gameMap)
        {
            if (Keyboard.GetState().IsKeyDown(TOWER_UPGRADE_KEY))
            {
                if (gameState.CanUpdateTower())
                {
                    gameMap.UpgradeSelectedTower();
                    gameState.UpdateTowerUpgradeTimer();
                }
            }
            else if (Keyboard.GetState().IsKeyUp(TOWER_UPGRADE_KEY))
            {
                // upgrade key release = reset the timer so that the upgrades
                // can be purchased every key 'click'
                gameState.ResetTowerUpgradeTimer();
            }
        }
    }
}
