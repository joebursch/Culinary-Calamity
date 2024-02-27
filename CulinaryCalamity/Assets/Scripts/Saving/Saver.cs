using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Saving
{
    /// <summary>
    /// Static class - handles reading/writing/tracking save files
    /// </summary>
    public static class Saver
    {
        private static readonly string _savePathFormat = Application.persistentDataPath + "/{0}_save.json";

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
        public static List<string> ListSaves()
        {
            List<string> saveList = (List<string>)Directory.EnumerateFiles(Application.persistentDataPath + '/', "*_save.json");
            for (int i = 0; i < saveList.Count; i++)
            {
                saveList[i] = saveList[i].Split("_")[0];
            }
            return saveList;
        }
    }
}