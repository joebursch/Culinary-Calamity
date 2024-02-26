using System;
using UnityEngine;

namespace Saving
{
    public class GameSaveManager : MonoBehaviour
    {
        public event EventHandler Save;
        public event EventHandler Load;

        private static GameSaveManager _gameSaveManager;
        private GameSaveState _currentSaveState;

        void Awake()
        {
            if (_gameSaveManager == null)
            {
                _gameSaveManager = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SaveGame()
        {
            OnSave(EventArgs.Empty);
            Saver.WriteSaveData(_currentSaveState);
        }

        public void LoadGame(int saveId)
        {
            _currentSaveState = Saver.ReadSaveData(saveId);
            OnLoad(EventArgs.Empty);
        }

        protected virtual void OnSave(EventArgs e)
        {
            Save?.Invoke(this, e);
        }

        protected virtual void OnLoad(EventArgs e)
        {
            Load?.Invoke(this, e);
        }

        public void UpdateObjectSaveData(string objectIdentifier, ObjectSaveData newData)
        {
            string newDataJson = newData.Serialize();
            _currentSaveState.AddSaveData(objectIdentifier, newDataJson);
        }

        public ObjectSaveData GetObjectSaveData(string objectIdentifier)
        {
            ObjectSaveData saveData = new();
            saveData.Deserialize(_currentSaveState.GetSaveData(objectIdentifier));
            return saveData;
        }

        public static GameSaveManager GetGameSaveManager()
        {
            return _gameSaveManager;
        }
    }
}