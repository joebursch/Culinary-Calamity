using TMPro;
using UnityEngine;

namespace Inventory
{
    public class InvGoldPanel : MonoBehaviour
    {
        private readonly string _prefix = "Gold: ";

        [SerializeField] private TextMeshProUGUI _goldAmtField;

        public void SetGold(int amt)
        {
            _goldAmtField.text = _prefix + amt.ToString();
        }
    }
}