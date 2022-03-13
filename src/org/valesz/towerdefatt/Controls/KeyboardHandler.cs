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
        /// <summary>
        /// How long to wait before registering key presses again.
        /// </summary>
        //const long keyPressCoolDown = 500;
        //const Keys UPGRADE_ITEM_KEY = Keys.U;
        //const Keys EXIT_KEY = Keys.Escape;
        //const Keys PAUSE_KEY = Keys.Pause;
        //const Keys PAUSE_KEY_2 = Keys.P;


        ///// <summary>
        ///// A map of key => last press timestamp.
        ///// </summary>
        //Dictionary<Keys, long> coolDowns;

        //public KeyboardHandler()
        //{
        //    coolDowns = new Dictionary<Keys, long>();
        //}

        ///// <summary>
        ///// Checks if the exit keys are pressed.
        ///// </summary>
        ///// <returns>True if the exit keys are pressed and the game should exit.</returns>
        //public bool ShouldExit()
        //{
        //    return GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(EXIT_KEY);
        //}

        //public void HandleKeyboard(GameState gameState, Map gameMap)
        //{
        //    if (gameState.IsPaused())
        //    {
        //        if (PauseKeyPressed())
        //        {
        //            gameState.UnPause();
        //            RegisterPauseKeyPresses();
        //        }

        //        return;
        //    }

        //    if (PauseKeyPressed())
        //    {
        //        gameState.Pause();
        //        RegisterPauseKeyPresses();
        //    } 
        //    else if (Keyboard.GetState().IsKeyDown(UPGRADE_ITEM_KEY))
        //    {
        //        if (gameState.CanBuyUpgrade())
        //        {
        //            gameMap.UpgradeSelected();
        //            gameState.UpdateUpgradeTimer();
        //        }
        //    }
        //    else if (Keyboard.GetState().IsKeyUp(UPGRADE_ITEM_KEY))
        //    {
        //        // upgrade key release = reset the timer so that the upgrades
        //        // can be purchased every key 'click'
        //        gameState.ResetUpgradeTimer();
        //    }

        //}

        ///// <summary>
        ///// Checks if the given key has cooled down and can be pressed again.
        ///// </summary>
        ///// <param name="key"> Key to check</param>
        ///// <returns>True if the key has cooled down.</returns>
        //private bool CheckCoolDown(Keys key)
        //{
        //    if (!coolDowns.ContainsKey(key))
        //    {
        //        return true;
        //    }

        //    long now = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        //    return now - coolDowns[key] > keyPressCoolDown;
        //}

        //private void RegisterPauseKeyPresses()
        //{
        //    RegisterKeyPress(PAUSE_KEY);
        //    RegisterKeyPress(PAUSE_KEY_2);
        //}

        //private void RegisterKeyPress(Keys key)
        //{
        //    long now = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

        //    if (coolDowns.ContainsKey(key))
        //    {
        //        coolDowns[key] = now;
        //    } else
        //    {
        //        coolDowns.Add(key, now);
        //    }
            
        //}

        //private bool PauseKeyPressed()
        //{
        //    return (Keyboard.GetState().IsKeyDown(PAUSE_KEY) && CheckCoolDown(PAUSE_KEY)) 
        //        || (Keyboard.GetState().IsKeyDown(PAUSE_KEY_2) && CheckCoolDown(PAUSE_KEY_2))
        //        ;
        //}
    }
}
