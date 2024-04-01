using System;
using UnityEngine;

namespace Quests
{
    public class QuestMenuManager : MonoBehaviour
    {
        public event EventHandler QuestMenuClose;

        public void ToggleQuestMenu()
        {
            if (!gameObject.activeSelf)
            {
                // refresh quest list
            }
            gameObject.SetActive(!gameObject.activeSelf);
        }

        protected virtual void OnQuestMenuClose(EventArgs e)
        {
            QuestMenuClose?.Invoke(this, e);
        }

        public void CloseQuestMenu()
        {
            OnQuestMenuClose(EventArgs.Empty);
        }
    }
}