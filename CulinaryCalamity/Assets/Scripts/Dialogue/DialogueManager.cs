using UnityEngine;
using System.Collections.Generic;

public class DialogueManager
{
    private bool canSpeak = true;
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
        // TURN ON CANVAS
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
        Debug.Log(dialogueLine);
    }

    public bool StillSpeaking()
    {
        return canSpeak;
    }

    private void TurnOnDisplay()
    {

    }

    private void TurnOffDisplay()
    {

    }
}