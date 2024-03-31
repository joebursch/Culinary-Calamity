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
            Debug.Log("Updating Display! " + dialogue);
        }

        public void ActivateDisplay()
        {
            Debug.Log("Turning on Display");
        }

        public void DeactivateDisplay()
        {
            Debug.Log("Turning off Display");
        }
    }
}
