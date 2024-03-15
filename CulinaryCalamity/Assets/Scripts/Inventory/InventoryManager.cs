using UnityEngine;
using System;
using Items;

namespace Inventory
{
    /// <summary>
    /// 'interface' for working with the inventory display menu
    /// </summary>
    public class InventoryManager : MonoBehaviour
    {
        // events
        public event EventHandler InventoryClose;
        // Components
        [SerializeField] private InvContentsPanel _contentsPanel;
        [SerializeField] private InvGoldPanel _goldPanel;
        [SerializeField] private InvPlayerPanel _playerPanel;
        [SerializeField] private InvWeaponPanel _weaponPanel;

        /// <summary>
        /// Set player name
        /// </summary>
        /// <param name="name">string, player name</param>
        public void SetPlayerName(string name)
        {
            _playerPanel.SetName(name);
        }

        /// <summary>
        /// Set player gold amount
        /// </summary>
        /// <param name="gold">int, players current gold</param>
        public void SetGold(int gold)
        {
            _goldPanel.SetGold(gold);
        }

        /// <summary>
        /// Set player sprite
        /// </summary>
        /// <param name="sprite">Sprite, a player's sprite</param>
        public void SetSprite(Sprite sprite)
        {
            _playerPanel.SetSprite(sprite);
        }

        /// <summary>
        /// Set player weapon
        /// </summary>
        /// <param name="weapon">Weapon, a player's weapon</param>
        public void SetWeapon(Weapon weapon)
        {
            _weaponPanel.SetWeapon(weapon);
        }

        /// <summary>
        /// Set inventory object
        /// </summary>
        /// <param name="inv">Inventory to track</param>
        public void SetInventory(PlayerInventory inv)
        {
            _contentsPanel.SetInventory(inv);
        }

        /// <summary>
        /// activates/deactivates the inventory menu. Ensures display is updated before activating
        /// </summary>
        public void ToggleInventory()
        {
            if (!gameObject.activeSelf)
            {
                _contentsPanel.RefreshDisplay();
            }
            gameObject.SetActive(!gameObject.activeSelf);
        }

        /// <summary>
        /// Emits CloseInventory event - prompts subscribers to take necessary actions.
        /// This allows the player class to respond to inventory closes initiated through the UI rather than keyboard/controllers
        /// </summary>
        /// <param name="e">EventArgs.Empty</param>
        protected virtual void OnInventoryClose(EventArgs e)
        {
            InventoryClose?.Invoke(this, e);
        }

        /// <summary>
        /// Linked to 'X' button on inventory menu, calls OnInventoryClose 
        /// </summary>
        public void CloseInventory()
        {
            OnInventoryClose(EventArgs.Empty);
        }
    }
}