using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Core.GameShop
{
    /// <summary>
    /// Object representing shop player can use to buy towers and obstacles.
    /// </summary>
    public class Shop
    {

        /// <summary>
        /// Item selected in shop by player. Initialized to null.
        /// </summary>

        public IShopItem SelectedShopItem { get; private set; }

        /// <summary>
        /// Towers available through all the game.
        /// </summary>
        public List<ITower> AvailableTowers
        {
            get;
            private set;
        }

        /// <summary>
        /// Obstacles available for buying.
        /// </summary>
        public List<IObstacle> AvailableObstacles
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates shop with items available for buying.
        /// </summary>
        /// <param name="availableTowers">Towers player can buy.</param>
        /// <param name="availableObstacles">Obstacles player can buy.</param>
        public Shop(List<ITower> availableTowers, List<IObstacle> availableObstacles)
        {
            AvailableTowers = availableTowers;
            AvailableObstacles = availableObstacles;
            DeselectAll();
        }

        public IEnumerable<string> AllItemTextures()
        {
            List<string> allTextures = new List<string>();
            AvailableTowers.ForEach((tower) => allTextures.AddRange(tower.AllTextures));
            AvailableObstacles.ForEach((obstacle) => allTextures.AddRange(obstacle.AllTextures));
            return allTextures;
        }

        /// <summary>
        /// Deselects all selected entities in shop (if any).
        /// </summary>
        public void DeselectAll()
        {
            SelectedShopItem = null;
            foreach (ITower t in AvailableTowers)
            {
                t.Selected = false;
            }
            foreach (IShopItem shopItem in AvailableObstacles)
            {
                shopItem.Selected = false;
            }
        }


        /// <summary>
        /// Clones tower currently selected in the shop.
        /// </summary>
        /// <param name="x">Where to clone it.</param>
        /// <param name="y">Where to clone it.</param>
        /// <returns>Clonned tower.</returns>
        public IShopItem CloneSelectedItem(float x, float y)
        {
            return SelectedShopItem.Clone(x - SelectedShopItem.Center.X, y - SelectedShopItem.Center.Y);
        }

        /// <summary>
        /// Deselects all previously selected items in the shop and then selects available item 
        /// from shop on the given coordinates. If the item on the given coordinates is already
        /// selected, this method just deselects it.
        /// </summary>
        public void SelectFromShop(float x, float y)
        {
            foreach (ITower availableTower in AvailableTowers)
            {
                if (IsClickedOn(availableTower, x, y))
                {
                    HandleItemSelection(availableTower);
                    return;
                }
            }

            foreach (IShopItem availableObstacle in AvailableObstacles)
            {
                if (IsClickedOn(availableObstacle, x, y))
                {
                    HandleItemSelection(availableObstacle);
                    return;
                }
            }

            // no item lies on given coordinates => deselect
            DeselectAll();
        }

        /// <summary>
        /// Handles selection of a shop item. If the shop item was previously selected, it will be 
        /// deselected.
        /// </summary>
        /// <param name="selectedItem">Selected shop item.</param>
        private void HandleItemSelection(IShopItem selectedItem)
        {
            // item already selected => deselect
            if (selectedItem.Selected)
            {
                DeselectAll();
            }
            else
            {
                // deselect others and select the current one
                DeselectAll();
                SelectedShopItem = selectedItem;
                selectedItem.Selected = true;
            }

        }

        /// <summary>
        /// Checks whether the click corrdinates match given shop item.
        /// </summary>
        /// <param name="shopItem">Shop item</param>
        /// <param name="clickX">Click X</param>
        /// <param name="clickY">Click Y</param>
        /// <returns></returns>
        private bool IsClickedOn(IShopItem shopItem, float clickX, float clickY)
        {
            return shopItem.CheckColision(clickX, clickY);
        }
    }
}
