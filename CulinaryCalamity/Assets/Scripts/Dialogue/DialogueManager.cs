using UnityEngine;
using System.Collections.Generic;
using System;
namespace Dialogue
{
    public class DialogueManager
    {
        private bool canSpeak = true;
        private bool newConversation = true;
        private Queue<string> _dialogueLines;

        /// <summary>
        /// Prepares a dialogue queue
        /// </summary>
        /// <param name="dialogue">TextAsset to use for dialogue</param>
        public void InitializeDialogue(TextAsset dialogue)
        {
            _dialogueLines = new();
            string[] tempLines = dialogue.ToString().Split("\n");
            foreach (string line in tempLines)
            {
                _dialogueLines.Enqueue(line);
            }
        }
        /// <summary>
        /// Plays the next line of dialogue.
        /// </summary>
        public void PlayLine()
        {
            if (newConversation)
            {
                DialogueCanvasManager.GetDialogueCanvasManager().ActivateDisplay();
                newConversation = false;
            }
            DisplayLine(GetNextLine());
        }
        /// <summary>
        /// Sends the current line of dialogue to the Dialogue Canvas Manager.
        /// </summary>
        /// <param name="dialogue">current line of dialogue</param>
        public void DisplayLine(string dialogue)
        {
            DialogueCanvasManager.GetDialogueCanvasManager().UpdateDisplay(dialogue);
        }
        /// <summary>
        /// Get the next line of dialogue from the queue
        /// </summary>
        /// <returns>line of dialogue</returns>
        private string GetNextLine()
        {
            canSpeak = _dialogueLines.TryDequeue(out string nextLine);
            if (!canSpeak)
            {
                DialogueCanvasManager.GetDialogueCanvasManager().DeactivateDisplay();
            }
            return nextLine;
        }
        /// <summary>
        /// Method checks if the character still has dialogue lines. 
        /// </summary>
        /// <returns>Boolean</returns>
        public bool StillSpeaking()
        {
            return canSpeak;
        }
    }
}