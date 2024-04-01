using UnityEngine;
using System;
using TMPro;
namespace Dialogue
{
    /// <summary>
    /// Singleton that handles displaying and updating dialogue canvas. 
    /// </summary>
    public class DialogueCanvasManager : MonoBehaviour
    {
        private static DialogueCanvasManager _dialogueCanvasManager;
        [SerializeField] private GameObject _dialogueDisplayCanvas;
        [SerializeField] private TMP_Text _dialogueDisplayText;
        private bool _displayActive = false;
        public event EventHandler DisplayActivated;
        public event EventHandler DisplayDeactivated;

        void Awake()
        {
            if (_dialogueCanvasManager == null)
            {
                _dialogueCanvasManager = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        /// <summary>
        /// Returns reference to Singleton DialogueCanvasManager
        /// </summary>
        /// <returns>DialogueCanvasManager</returns>
        public static DialogueCanvasManager GetDialogueCanvasManager()
        {
            return _dialogueCanvasManager;
        }
        /// <summary>
        /// Sets text of display canvas
        /// </summary>
        /// <param name="dialogue">Line of dialogue to display</param>
        public void UpdateDisplay(string dialogue)
        {
            if (!DialogueManager.GetDialogueManager().IsDialogueInProgress())
            {
                DeactivateDisplay();
                _displayActive = false;
            }
            else if (!_displayActive)
            {
                ActivateDisplay();
                _displayActive = true;
            }
            _dialogueDisplayText.text = dialogue;
        }
        /// <summary>
        /// Turn on Dialogue Display
        /// </summary>
        private void ActivateDisplay()
        {
            _dialogueDisplayCanvas.SetActive(true);
            OnDisplayActivated(EventArgs.Empty);
        }
        /// <summary>
        /// Turn off Dialogue Display
        /// </summary>
        private void DeactivateDisplay()
        {
            _dialogueDisplayCanvas.SetActive(false);
            OnDisplayDeactivated(EventArgs.Empty);
        }
        /// <summary>
        /// Alerts player that the dialogue display is active
        /// </summary>
        /// <param name="e">Event Arguments</param>
        protected virtual void OnDisplayActivated(EventArgs e)
        {
            DisplayActivated?.Invoke(this, e);
        }
        /// <summary>
        /// Alerts player that the dialogue display is no longer active.
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected virtual void OnDisplayDeactivated(EventArgs e)
        {
            DisplayDeactivated?.Invoke(this, e);
        }
    }
}
