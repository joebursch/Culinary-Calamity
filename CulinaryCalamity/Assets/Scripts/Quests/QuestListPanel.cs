using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Quests
{
    /// <summary>
    /// Manages UI panel on Quest menu contaiing the list of quests
    /// </summary>
    public class QuestListPanel : MonoBehaviour
    {
        private List<Quest> _questList;
        [SerializeField] private GameObject _questTilePrefab;
        private List<GameObject> _questTiles;
        private float _panelHeight;
        private float _panelWidth;
        private float _tilePrefabHeight;
        private float _tilePrefabWidth;

        private void Awake()
        {
            _panelWidth = gameObject.GetComponent<RectTransform>().rect.width;
            _panelHeight = gameObject.GetComponent<RectTransform>().rect.height;
            _tilePrefabWidth = _questTilePrefab.GetComponent<RectTransform>().rect.width;
            _tilePrefabHeight = _questTilePrefab.GetComponent<RectTransform>().rect.height;
        }

        /// <summary>
        /// Sets quest list to track
        /// </summary>
        /// <param name="questList">List(Quest)</param>
        public void SetQuestList(List<Quest> questList)
        {
            _questList = questList;
        }

        /// <summary>
        /// Updates display of quest list
        /// </summary>
        public void RefreshDisplay()
        {
            ResetQuestTiles();
            BuildQuestTiles();
            PlaceQuestTiles();
        }

        private void ResetQuestTiles()
        {
            if (_questTiles == null)
            {
                _questTiles = new();
                return;
            }
            foreach (GameObject tile in _questTiles)
            {
                Destroy(tile);
            }
        }

        private void BuildQuestTiles()
        {
            _questTiles = new();
            foreach (Quest quest in _questList)
            {
                // create tile and get references to components
                GameObject questTile = Instantiate(_questTilePrefab, gameObject.transform);
                questTile.SetActive(false);
                questTile.transform.GetComponentInChildren<TextMeshProUGUI>().text = quest.GetTitle();

                _questTiles.Add(questTile);
            }
        }

        private void PlaceQuestTiles()
        {
            // tiles are created at 0,0 (center) by default, so we have to shift their position
            float xRange = _panelWidth - _tilePrefabWidth; // since positions are centered, remove half from both end
            float yRange = _panelHeight - _tilePrefabHeight;
            float xPos = 0;
            float yPos = yRange / 2;
            for (int idx = 0; idx < _questTiles.Count; idx++)
            {
                _questTiles[idx].GetComponent<RectTransform>().localPosition += new Vector3(xPos, yPos, 0);
                _questTiles[idx].SetActive(true);
                yPos -= _tilePrefabHeight;

            }
        }
    }
}