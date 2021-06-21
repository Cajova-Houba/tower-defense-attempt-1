using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;
using TowerDefenseAttempt1.org.valesz.towerdefatt.UI;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Configuration;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Controls
{
    /// <summary>
    /// Contains code for handling mouse input.
    /// </summary>
    public class MouseHandler
    {
        /// <summary>
        /// Sets to true when the left mouse button is presed (used to track click: press-release).
        /// </summary>
        bool leftMouseClick = false;

        public void HandleMouse(Map gameMap, float sidePanelX, GameOverPanel gameOverPanel)
        {
            MouseState state = Mouse.GetState();

            // beginning the mouse click
            if (state.LeftButton == ButtonState.Pressed)
            {
                leftMouseClick = true;
            }

            // releasing the mouse = end of the click
            else if (state.LeftButton == ButtonState.Released && leftMouseClick)
            {
                if (!gameMap.GameLost())
                {
                    // click ended on the side panel
                    // attempting to select/deselect tower in shop
                    if (state.X >= sidePanelX)
                    {

                        if (gameMap.SelectedMapTower != null)
                        {
                            // click ended in the side panel but tower on the map is selected
                            gameMap.DeselectMapTower();
                        }

                        gameMap.SelectTowerFromShop(state.X, state.Y);
                    }

                    // click ended on the map
                    // either attempting to select/deselect tower on the map
                    // or place a new one from the shop
                    else
                    {
                        if (gameMap.SelectedShopTower == null)
                        {
                            gameMap.SelectTowerOnTheMap(state.X, state.Y);
                        }
                        else
                        {
                            gameMap.BuyTower(gameMap.SelectedShopTower.Clone(state.X - gameMap.SelectedShopTower.Center.X, state.Y - gameMap.SelectedShopTower.Center.Y));
                        }
                    }
                }
                else if (gameOverPanel.RetryButtonClicked(state.X, state.Y))
                {
                    ConfigUtils.StartNewMapWithDefaultConfiguration(gameMap);
                }



                leftMouseClick = false;
            }
        }
    }
}
