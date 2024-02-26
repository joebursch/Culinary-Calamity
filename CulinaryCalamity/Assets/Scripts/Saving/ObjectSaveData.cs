using System.Collections.Generic;
using Newtonsoft.Json;

namespace Saving
{
    public class ObjectSaveData
    {
        public Dictionary<string, string> SaveData { get; set; }

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

        public virtual string Serialize()
        {
            return JsonConvert.SerializeObject(SaveData);
        }

        public virtual void Deserialize(string serializedData)
        {
            SaveData = JsonConvert.DeserializeObject<Dictionary<string, string>>(serializedData);
        }
    }
}