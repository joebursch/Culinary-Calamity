using UnityEngine;
using System.Collections.Generic;
using System;
namespace Dialogue
{
    /// <summary>
    /// Singleton that manages all dialogue in the scene. 
    /// </summary>
    public class DialogueManager : MonoBehaviour
    {
        private static DialogueManager _dialogueManager;
        private bool _dialogueRemaining;
        private Queue<string> _dialogueLines = new();
        void Awake()
        {
            if (_dialogueManager == null)
            {
                _dialogueManager = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        /// <summary>
        /// Returns a reference to the DialogueManager Singleton
        /// </summary>
        /// <returns>DialogueManager</returns>
        public static DialogueManager GetDialogueManager()
        {
            return _dialogueManager;
        }
        /// <summary>
        /// Prepares a dialogue queue
        /// </summary>
        /// <param name="dialogue">TextAsset to use for dialogue</param>
        public void InitializeDialogue(TextAsset dialogue)
        {
            _dialogueRemaining = true;
            string[] tempLines = dialogue.ToString().Split("\n");
            foreach (string line in tempLines)
            {
                _dialogueLines.Enqueue(line);
            }
            AdvanceDialogue();
        }
        /// <summary>
        /// Sends the next line of dialogue to the canvas manager
        /// </summary>
        public void AdvanceDialogue()
        {
            DialogueCanvasManager.GetDialogueCanvasManager().UpdateDisplay(GetNextLine());
        }
        /// <summary>
        /// Get the next line of dialogue from the queue
        /// </summary>
        /// <returns>line of dialogue</returns>
        private string GetNextLine()
        {
            _dialogueRemaining = _dialogueLines.TryDequeue(out string nextLine);
            return nextLine;
        }
        /// <summary>
        /// Method checks if the character still has dialogue lines. 
        /// </summary>
        /// <returns>Boolean</returns>
        public bool IsDialogueInProgress()
        {
            return _dialogueRemaining;
        }
    }
}