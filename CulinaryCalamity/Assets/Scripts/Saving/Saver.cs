using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using Unity.Loading;

namespace Saving
{
    /// <summary>
    /// Static class - handles reading/writing/tracking save files
    /// </summary>
    public static class Saver
    {
        private static readonly string _savePathFormat = Application.persistentDataPath + "/{0}_save.json";
        private static string _saveIndexFilepath = Application.persistentDataPath + "/saveIndex.json";

        /// <summary>
        /// Write a save file.
        /// </summary>
        /// <param name="saveState">Must include a saveId</param>
        public static void WriteSaveData(GameSaveState saveState)
        {
            string saveJson = saveState.SerializeSaveState();
            string filepath = string.Format(_savePathFormat, saveState.SaveId);
            Debug.Log(saveJson);
            using StreamWriter writer = new(filepath);
            writer.Write(saveJson);
        }

        /// <summary>
        /// Read a save file corresponding to the provided id
        /// </summary>
        /// <param name="saveId">Must correspond to the id of an existing save file</param>
        /// <returns></returns>
        public static GameSaveState ReadSaveData(int saveId)
        {
            string filepath = string.Format(_savePathFormat, saveId);
            using StreamReader reader = new(filepath);
            string saveJson = reader.ReadToEnd();
            return GameSaveState.DeserializeSaveState(saveId, saveJson);
        }

        /// <summary>
        /// Used to list available saves on the device without reading each save.
        /// </summary>
        /// <returns>List of saves in no particular order</returns>
        public static Dictionary<string, string> ListSaves()
        {
            string contents;
            try
            {
                using StreamReader reader = new(_saveIndexFilepath);
                contents = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(contents);
            }
            catch (FileNotFoundException)
            {
                return new();
            }
        }

        /// <summary>
        /// Adds a new save to the index
        /// </summary>
        /// <param name="saveId">id of save to add</param>
        /// <param name="playerName">name of player in the save</param>
        public static void UpdateSaveIndex(int saveId, string playerName)
        {
            Dictionary<string, string> saveIndexJson;
            try
            {
                using StreamReader reader = new(_saveIndexFilepath);
                string contents = reader.ReadToEnd();
                saveIndexJson = JsonConvert.DeserializeObject<Dictionary<string, string>>(contents);
            }
            catch (FileNotFoundException)
            {
                saveIndexJson = new();
            }
            saveIndexJson[saveId.ToString()] = playerName;
            using StreamWriter writer = new(_saveIndexFilepath);
            writer.Write(JsonConvert.SerializeObject(saveIndexJson));
        }
    }
}