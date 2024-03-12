using TMPro;
using UnityEngine;

namespace Inventory
{
    /// <summary>
    /// Manages the current gold display on the inventory menu
    /// </summary>
    public class InvGoldPanel : MonoBehaviour
    {
        private readonly string _prefix = "Gold: ";

        [SerializeField] private TextMeshProUGUI _goldAmtField;

        /// <summary>
        /// Sets gold amount in text field
        /// </summary>
        /// <param name="amt"></param>
        public void SetGold(int amt)
        {
            _goldAmtField.text = _prefix + amt.ToString();
        }
    }
}