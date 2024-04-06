using System;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    /// <summary>
    /// Logic for the Quest menu for player to view active quests
    /// </summary>
    public class QuestMenuManager : MonoBehaviour
    {
        public event EventHandler QuestMenuClose;
        private List<Quest> _questList;
        [SerializeField] private QuestListPanel _questListPanel;

        /// <summary>
        /// Activate or Deactivate quest menu
        /// </summary>
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

        /// <summary>
        /// Close the quest menu - used by quest menu X button
        /// </summary>
        public void CloseQuestMenu()
        {
            OnQuestMenuClose(EventArgs.Empty);
        }

        /// <summary>
        /// Set the list of quests to display in the quest list panel
        /// </summary>
        /// <param name="questList">List(Quest), list of quests to display</param>
        public void SetQuestList(List<Quest> questList)
        {
            _questListPanel.SetQuestList(questList);
        }
    }
}