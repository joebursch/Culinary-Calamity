using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

namespace Saving
{
    public static class Saver
    {
        private static readonly string _savePathFormat = Application.persistentDataPath + "/{0}_save.json";

        public static void WriteSaveData(GameSaveState saveState)
        {
            string saveJson = saveState.SerializeSaveState();
            string filepath = string.Format(_savePathFormat, saveState.SaveId);
            using StreamWriter writer = new(filepath);
            writer.Write(saveJson, true);
        }

        public static GameSaveState ReadSaveData(string saveId)
        {
            string filepath = string.Format(_savePathFormat, saveId);
            using StreamReader reader = new(filepath);
            string saveJson = reader.ReadToEnd();
            return GameSaveState.DeserializeSaveState(saveJson);
        }

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