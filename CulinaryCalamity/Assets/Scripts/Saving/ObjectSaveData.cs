using Newtonsoft.Json;
using System.Collections.Generic;

namespace Saving
{
    /// <summary>
    /// Only certain attributes of an object need/should be saved. 
    /// Objects that want to save information package that information in the ObjectSaveData Class
    /// This class handles storing, serializing, and deserializing that information.
    /// </summary>
    public class ObjectSaveData
    {
        public Dictionary<string, string> SaveData { get; set; } // property/attribute/etc. -> value

        /// <summary>
        /// Add new key/value pair(s) to the SaveData for this object
        /// </summary>
        /// <param name="newSaveData"></param>
        public void UpdateSaveData(Dictionary<string, string> newSaveData)
        {
            if (SaveData != null)
            {
                foreach (string key in newSaveData.Keys)
                {
                    SaveData[key] = newSaveData[key];
                }
            }
            else
            {
                SaveData = newSaveData;
            }
        }

        /// <summary>
        /// return json serialized savedata
        /// </summary>
        /// <returns></returns>
        public virtual string Serialize()
        {
            return JsonConvert.SerializeObject(SaveData);
        }

        /// <summary>
        /// Instance method. Create a new ObjectSaveData and use this method to populate it with SaveData based on the provided json.
        /// </summary>
        /// <param name="serializedData">JSON serialized string-string dictionary</param>
        public virtual void Deserialize(string serializedData)
        {
            SaveData = JsonConvert.DeserializeObject<Dictionary<string, string>>(serializedData);
        }
    }
}