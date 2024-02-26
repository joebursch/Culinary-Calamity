using System.Collections.Generic;
using UnityEngine;

namespace Saving
{
    public class GameSaveState
    {
        // TODO: represent this in a way that can be JSON serialized or make a customer JSON serializer/deserializer
        private Dictionary<string, ObjectSaveData> _savedObjects;
        public int SaveId { get; set; }

        public ObjectSaveData GetSaveData(string objectIdentifier)
        {
            try
            {
                return _savedObjects[objectIdentifier];
            }
            catch { return null; }
        }

        public void AddSaveData(string objectIdentifier, ObjectSaveData objectSaveData)
        {
            _savedObjects[objectIdentifier] = objectSaveData;
        }

        public string SerializeSaveState()
        {
            return JsonUtility.ToJson(_savedObjects);
        }

        public static GameSaveState DeserializeSaveState(string serializedSaveState)
        {
            return JsonUtility.FromJson<GameSaveState>(serializedSaveState);
        }
    }
}