using Saving;
using UnityEngine;

namespace Furniture
{
    public class Bed : MonoBehaviour, InteractableObject
    {
        public void Interact()
        {
            Debug.Log("Game Saving in Progress");
            GameSaveManager.GetGameSaveManager().SaveGame();
        }
    }
}
