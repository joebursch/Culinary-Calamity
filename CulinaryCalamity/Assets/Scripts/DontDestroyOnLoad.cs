using UnityEngine;

/// <summary>
/// Standard do not destroy on load scripts
/// </summary>
public class DontDestroyOnLoad : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
