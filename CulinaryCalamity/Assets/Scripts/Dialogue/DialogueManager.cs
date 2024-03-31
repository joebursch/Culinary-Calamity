using UnityEngine;
using System.Collections.Generic;
using System;
namespace Dialogue
{
    public class DialogueManager
    {
        private bool canSpeak = true;
        private Queue<string> _dialogueLines;
        public event EventHandler<DialogueEvent> UpdateDisplay;
        public DialogueManager()
        {
            UpdateDisplay += DialogueCanvasManager.GetDialogueCanvasManager().UpdateDisplay;
        }
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
            DisplayLine(GetNextLine());
        }

        private string GetNextLine()
        {
            canSpeak = _dialogueLines.TryDequeue(out string nextLine);
            if (!canSpeak)
            {
                /* TURN OFF CANVAS? */
            }
            return nextLine;
        }

        private void DisplayLine(string dialogueLine)
        {
            // Debug.Log(dialogueLine);
            OnDisplayLine(new DialogueEvent(dialogueLine));
        }
        protected virtual void OnDisplayLine(DialogueEvent e)
        {
            UpdateDisplay?.Invoke(this, e);
        }

        public bool StillSpeaking()
        {
            return canSpeak;
        }
    }
}