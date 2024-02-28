using System.Collections.Generic;
using Newtonsoft.Json;

namespace Saving
{
    /// <summary>
    /// Class for storing save information. One instance corresponds to one playthrough.
    /// </summary>
    public class GameSaveState
    {
        private Dictionary<string, string> _savedObjects; // object id -> object json
        public int SaveId { get; set; } // unique, used for naming save files

        /// <summary>
        /// Should only be used for completely new playthroughs.
        /// </summary>
        public GameSaveState()
        {
            SaveId = (int)System.DateTimeOffset.Now.ToUnixTimeSeconds(); // for a new save generate a unique id based on Unix time
        }

        /// <summary>
        /// Should only be used for existing playthroughs. Intentionally private.
        /// Accessed through the static DeserializeSaveState method
        /// </summary>
        /// <param name="saveId">Unique ID corresponding to existing save state</param>
        /// <param name="savedObjects">Information on saved objects. Format: object id -> object json</param>
        private GameSaveState(int saveId, Dictionary<string, string> savedObjects)
        {
            _savedObjects = savedObjects;
            SaveId = saveId;
        }

        /// <summary>
        /// Query for saved data for a particular object id. Returns null if no match found.
        /// </summary>
        /// <param name="objectIdentifier">Name of object to query for</param>
        /// <returns></returns>
        public string GetSaveData(string objectIdentifier)
        {
            try
            {
                return _savedObjects[objectIdentifier];
            }
            catch { return null; }
        }

        /// <summary>
        /// Store an object to be saved.
        /// </summary>
        /// <param name="objectIdentifier">Name of object to save info for (key)</param>
        /// <param name="objectSaveData">Data to save as a JSON serialized Dict: string -> string</param>
        public void AddSaveData(string objectIdentifier, string objectSaveData)
        {
            if (_savedObjects != null)
            {
                _savedObjects[objectIdentifier] = objectSaveData;
            }
            else
            {
                _savedObjects = new()
                {
                    { objectIdentifier, objectSaveData }
                };
            }

        }

        /// <summary>
        /// Return serialized string of savedObjects
        /// </summary>
        /// <returns></returns>
        public string SerializeSaveState()
        {
            return JsonConvert.SerializeObject(_savedObjects);
        }

        /// <summary>
        /// Instantiate GameSaveState instance from JSON serialized savedObjects dict
        /// </summary>
        /// <param name="saveId">Unique id of save</param>
        /// <param name="serializedSaveState">serialized saved objects</param>
        /// <returns>GameSaveState</returns>
        public static GameSaveState DeserializeSaveState(int saveId, string serializedSaveState)
        {
            Dictionary<string, string> savedObjects = JsonConvert.DeserializeObject<Dictionary<string, string>>(serializedSaveState);
            return new GameSaveState(saveId, savedObjects);
        }
    }
}