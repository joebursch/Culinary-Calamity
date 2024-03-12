using Items;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

namespace Inventory
{
    /// <summary>
    /// Manages weapon display panel in inventory menu
    /// </summary>
    public class InvWeaponPanel : MonoBehaviour
    {

        private Image _weaponImage;
        [SerializeField] private TextMeshProUGUI _weaponName;

        /// <summary>
        /// Set weapon sprite and name
        /// </summary>
        /// <param name="weapon"></param>
        public void SetWeapon(Weapon weapon)
        {
            if (_weaponImage == null)
            {
                _weaponImage = gameObject.GetComponent<Image>();
            }
            _weaponImage.sprite = weapon.GetSprite();
            _weaponName.text = weapon.GetName();
        }
    }
}