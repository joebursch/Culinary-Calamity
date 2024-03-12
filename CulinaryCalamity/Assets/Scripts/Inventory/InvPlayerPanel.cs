using UnityEngine.UI;
using TMPro;
using UnityEngine;


namespace Inventory
{
    /// <summary>
    /// Manages player name and sprite display in inventory menu
    /// </summary>
    public class InvPlayerPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameField;
        private Image _playerImage; // image cannot be serialized

        /// <summary>
        /// Sets name in text field
        /// </summary>
        /// <param name="name"></param>
        public void SetName(string name)
        {
            _nameField.text = name;
        }

        /// <summary>
        /// Sets player sprite in image field
        /// </summary>
        /// <param name="sprite"></param>
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