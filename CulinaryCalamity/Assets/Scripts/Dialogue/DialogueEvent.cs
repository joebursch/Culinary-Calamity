using System;
namespace Dialogue
{
    public class DialogueEvent : EventArgs
    {
        public DialogueEvent(string dialogue)
        {
            Dialogue = dialogue;
        }

        public string Dialogue { get; set; }
    }
}
