using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshPro dialogueDisplayText;
    private List<String> dialogueQueue;
    private bool isDialogueDisplayed;
}