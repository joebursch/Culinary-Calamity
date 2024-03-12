using UnityEngine.UI;
using TMPro;
using UnityEngine;


namespace Inventory
{
    public class InvPlayerPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameField;
        private Image _playerImage; // image cannot be serialized

        public void SetName(string name)
        {
            _nameField.text = name;
        }

        public void SetSprite(Sprite sprite)
        {
            if (_playerImage == null)
            {
                _playerImage = gameObject.GetComponent<Image>();
            }
            _playerImage.sprite = sprite;
        }
    }
}