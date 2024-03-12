using Items;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

namespace Inventory
{
    public class InvWeaponPanel : MonoBehaviour
    {

        private Image _weaponImage;
        [SerializeField] private TextMeshProUGUI _weaponName;

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