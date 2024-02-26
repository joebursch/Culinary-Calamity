using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Saving
{
    public class GameSaveState
    {
        // TODO: represent this in a way that can be JSON serialized or make a customer JSON serializer/deserializer
        private Dictionary<string, string> _savedObjects;
        public int SaveId { get; set; }

        public GameSaveState()
        {
            SaveId = (int)System.DateTimeOffset.Now.ToUnixTimeSeconds();
        }

        private GameSaveState(int saveId, Dictionary<string, string> savedObjects)
        {
            _savedObjects = savedObjects;
            SaveId = saveId;
        }

        public string GetSaveData(string objectIdentifier)
        {
            try
            {
                return _savedObjects[objectIdentifier];
            }
            catch { return null; }
        }

        public void AddSaveData(string objectIdentifier, string objectSaveData)
        {
            _savedObjects[objectIdentifier] = objectSaveData;
        }

        public string SerializeSaveState()
        {
            return JsonConvert.SerializeObject(_savedObjects);
        }

        public static GameSaveState DeserializeSaveState(int saveId, string serializedSaveState)
        {
            Dictionary<string, string> savedObjects = JsonConvert.DeserializeObject<Dictionary<string, string>>(serializedSaveState);
            return new GameSaveState(saveId, savedObjects);
        }
    }
}