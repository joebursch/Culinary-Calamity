using System.Collections.Generic;
using Newtonsoft.Json;

namespace Saving
{
    public class GameSaveState
    {
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