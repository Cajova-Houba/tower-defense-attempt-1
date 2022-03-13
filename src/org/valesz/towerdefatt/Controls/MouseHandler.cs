using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;
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

        //public void HandleMouse(GameState gameState, Map gameMap, float sidePanelX, GameOverPanel gameOverPanel, ControlsPanel controlsPanel)
        //{
        //    MouseState state = Mouse.GetState();

        //    // beginning the mouse click
        //    if (state.LeftButton == ButtonState.Pressed)
        //    {
        //        leftMouseClick = true;
        //    }

        //    // releasing the mouse = end of the click
        //    else if (state.LeftButton == ButtonState.Released && leftMouseClick)
        //    {
        //        if (!gameMap.GameLost())
        //        {
        //            if (gameState.IsPaused())
        //            {
        //                // is pause => only handle resume button click and nothing else
        //                if (controlsPanel.IsResumeButtonClick(state.X, state.Y))
        //                {
        //                    gameState.UnPause();
        //                }
        //            } else
        //            {
        //                // click ended on the side panel
        //                // attempting to select/deselect tower in shop
        //                if (state.X >= sidePanelX)
        //                {

        //                    if (gameMap.SelectedItem != null)
        //                    {
        //                        // click ended in the side panel but tower on the map is selected
        //                        gameMap.DeselectMapItems();
        //                    }

        //                    gameMap.Shop.SelectFromShop(state.X, state.Y);
        //                }

        //                // click ended on the map
        //                // either attempting to select/deselect tower on the map
        //                // or place a new one from the shop
        //                else
        //                {
        //                    if (gameMap.Shop.SelectedShopItem == null)
        //                    {
        //                        gameMap.SelectItemOnTheMap(state.X, state.Y);
        //                    }
        //                    else
        //                    {
        //                        gameMap.BuySelectedItem(state.X, state.Y);
        //                    }
        //                }

        //            }

        //        }
        //        else if (gameOverPanel.RetryButtonClicked(state.X, state.Y))
        //        {
        //            ConfigUtils.StartNewMapWithDefaultConfiguration(gameMap);
        //        }

        //        leftMouseClick = false;
        //    }
        //}
    }
}
