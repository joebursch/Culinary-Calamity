using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    // enable/disable to toggle visibility of dialogue box
    [SerializeField] private Canvas dialogueDisplayCanvas;
    // can set text for dialogue box using text attribute
    [SerializeField] private TextMeshPro dialogueDisplayText;
    // can load series of dialogue prompts by appending to queue
    private List<String> dialogueQueue;
}