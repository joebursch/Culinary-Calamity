using System.Collections.Generic;
using UnityEngine;


public class Minigame : MonoBehaviour
{
    private List<object> minigameData = new List<object>();

    private int score;
    private float rewardMultiplier;

    public void AddMinigameData(Recipe recipe, AudioClip audioFile, float timingInformation)
    {
        minigameData.Add(recipe);
        minigameData.Add(audioFile);
        minigameData.Add(timingInformation);
    }

}