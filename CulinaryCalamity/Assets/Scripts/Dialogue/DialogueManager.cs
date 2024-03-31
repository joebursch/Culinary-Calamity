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

        public void InitializeDialogue(TextAsset dialogue)
        {
            _dialogueLines = new();
            string[] tempLines = dialogue.ToString().Split("\n");
            foreach (string line in tempLines)
            {
                _dialogueLines.Enqueue(line);
            }
        }

        public void PlayLine()
        {
            if (newConversation)
            {
                DialogueCanvasManager.GetDialogueCanvasManager().ActivateDisplay();
                newConversation = false;
            }
            DisplayLine(GetNextLine());
        }

        public void DisplayLine(string dialogue)
        {
            DialogueCanvasManager.GetDialogueCanvasManager().UpdateDisplay(dialogue);
        }

        private string GetNextLine()
        {
            canSpeak = _dialogueLines.TryDequeue(out string nextLine);
            if (!canSpeak)
            {
                DialogueCanvasManager.GetDialogueCanvasManager().DeactivateDisplay();
            }
            return nextLine;
        }
        public bool StillSpeaking()
        {
            return canSpeak;
        }
    }
}