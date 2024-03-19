using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    private static MusicController instance;

    private AudioSource audioSource;

    public AudioClip sharedMusicClip;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary>
    /// Method to handle scene changes.
    /// </summary>
    /// <param name="scene">The loaded scene.</param>
    /// <param name="mode">The mode in which the scene was loaded.</param>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Restaurant" || scene.name == "Kitchen")
        {
            audioSource.clip = sharedMusicClip;
            audioSource.Play();
        }
    }
}
