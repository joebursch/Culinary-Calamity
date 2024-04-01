using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
namespace Dialogue
{
    public class DialogueCanvasManager : MonoBehaviour
    {
        private static DialogueCanvasManager _dialogueCanvasManager;
        [SerializeField] private GameObject _dialogueDisplayCanvas;
        [SerializeField] private TMP_Text _dialogueDisplayText;

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

        public static DialogueCanvasManager GetDialogueCanvasManager()
        {
            return _dialogueCanvasManager;
        }

        public void UpdateDisplay(string dialogue)
        {
            _dialogueDisplayText.text = dialogue;
        }

        public void ActivateDisplay()
        {
            _dialogueDisplayCanvas.SetActive(true);
            OnDisplayActivated(EventArgs.Empty);
        }

        public void DeactivateDisplay()
        {
            _dialogueDisplayCanvas.SetActive(false);
            OnDisplayDeactivated(EventArgs.Empty);
        }

        protected virtual void OnDisplayActivated(EventArgs e)
        {
            DisplayActivated?.Invoke(this, e);
        }

        protected virtual void OnDisplayDeactivated(EventArgs e)
        {
            DisplayDeactivated?.Invoke(this, e);
        }
    }
}
