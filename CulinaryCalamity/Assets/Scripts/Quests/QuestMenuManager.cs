using System;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    public class QuestMenuManager : MonoBehaviour
    {
        public event EventHandler QuestMenuClose;
        private List<Quest> _questList;
        [SerializeField] private QuestListPanel _questListPanel;

        public void ToggleQuestMenu()
        {
            if (!gameObject.activeSelf)
            {
                _questListPanel.RefreshDisplay();
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

        public void SetQuestList(List<Quest> questList)
        {
            _questListPanel.SetQuestList(questList);
        }
    }
}