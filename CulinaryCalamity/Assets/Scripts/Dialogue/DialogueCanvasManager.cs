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

        public void UpdateDisplay(object sender, DialogueEvent e)
        {
            Debug.Log("Updating Display! " + e.Dialogue);
        }
    }
}
