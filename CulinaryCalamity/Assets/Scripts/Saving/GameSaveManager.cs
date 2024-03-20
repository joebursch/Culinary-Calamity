using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Saving
{
    /// <summary>
    /// "Interface" for saving, loading, etc. game state.
    /// Singleton. Follows a kind of Pub/Sub using C# events. This is the "publisher" and any objects that want to save data are "subscribers"
    /// </summary>
    public class GameSaveManager : MonoBehaviour
    {
        public event EventHandler Save;
        public event EventHandler Load;

        private static GameSaveManager _gameSaveManager;
        private GameSaveState _currentSaveState;
        private bool _needsToLoad;

        /// <summary>
        /// Unity awake method. Enforces Singleton pattern.
        /// </summary>
        void Awake()
        {
            if (_gameSaveManager == null)
            {
                _gameSaveManager = this;
                // Enables saving/loading between scenes
                SceneManager.sceneLoaded += OnSceneLoaded;
                SceneManager.sceneUnloaded += OnSceneUnloaded;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Built-in Update method. Used to load game between scenes
        /// </summary>
        void Update()
        {
            // Load needs to be done in update to allow subscribers time to subscribe in awake/start
            // This will occur during the first frame
            if (_needsToLoad && _currentSaveState != null)
            {
                LoadGame(_currentSaveState.SaveId);
                _needsToLoad = false;
            }
        }

        #region Loading
        /// <summary>
        /// When a new scene is loaded, this sets needsToLoad so that we publish a load event once the scene has finished loading
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="mode"></param>
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (_currentSaveState != null)
            {
                _needsToLoad = true;
            }
        }

        /// <summary>
        /// Load a save from a file based on the saves unique id
        /// </summary>
        /// <param name="saveId">Unique save id - must correspond to a save file on the disk</param>
        public void LoadGame(int saveId)
        {
            _currentSaveState = Saver.ReadSaveData(saveId); // load data before emitting load event
            OnLoad(EventArgs.Empty);
        }

        /// <summary>
        /// Emits Load event - prompts subscribers to load their save data
        /// </summary>
        /// <param name="e">EventArgs.Empty</param>
        protected virtual void OnLoad(EventArgs e)
        {
            Load?.Invoke(this, e);
        }
        #endregion

        #region Saving
        /// <summary>
        /// Saves game when a scene is unloaded
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="mode"></param>
        void OnSceneUnloaded(Scene scene)
        {
            if (scene.name != "StartScreen")
            {
                SaveGame();
            }
        }

        /// <summary>
        /// Used to save the state of the game and write to a file
        /// </summary>
        public void SaveGame()
        {
            OnSave(EventArgs.Empty); // EventArgs.Empty is a required parameter for EventHandler delegates
            Saver.WriteSaveData(_currentSaveState); // save after emitting save event and letting subscribers react
        }

        /// <summary>
        /// Emits Save event - prompts subscribers to update their save data
        /// </summary>
        /// <param name="e">EventArgs.Empty</param>
        protected virtual void OnSave(EventArgs e)
        {
            Save?.Invoke(this, e);
        }


        #endregion

        /// <summary>
        /// Used to manually set the saveState for new playthroughs that cannot be loaded.
        /// </summary>
        /// <param name="saveState">GameSaveState corresponding to current playthrough.</param>
        public void SetSaveState(GameSaveState saveState)
        {
            _currentSaveState = saveState;
        }

        /// <summary>
        /// Subscribers use this to update the current save state with their current information.
        /// </summary>
        /// <param name="objectIdentifier">Name corresponding to object w/ data being saved</param>
        /// <param name="newData">ObjectSaveData representing the stuff the object wants to save</param>
        public void UpdateObjectSaveData(string objectIdentifier, ObjectSaveData newData)
        {
            string newDataJson = newData.Serialize();
            _currentSaveState.AddSaveData(objectIdentifier, newDataJson);
        }

        /// <summary>
        /// Subscribers use this to Load save data / update their current state
        /// </summary>
        /// <param name="objectIdentifier">Name corresponding to object loading</param>
        /// <returns>ObjectSaveData reprsenting saved attributes of an object</returns>
        public ObjectSaveData GetObjectSaveData(string objectIdentifier)
        {
            ObjectSaveData saveData = new();
            string jsonString = _currentSaveState.GetSaveData(objectIdentifier);
            if (jsonString != null) { saveData.Deserialize(jsonString); }
            return saveData;
        }

        /// <summary>
        /// Get save file information for displaying in save selection menu
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetSaveFiles()
        {
            return Saver.ListSaves();
        }

        /// <summary>
        /// Static getter matching singleton pattern.
        /// </summary>
        /// <returns></returns>
        public static GameSaveManager GetGameSaveManager()
        {
            return _gameSaveManager;
        }
    }
}