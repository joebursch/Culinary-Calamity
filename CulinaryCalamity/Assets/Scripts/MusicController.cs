using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Controls the background music based on the current scene.
/// </summary>
public class MusicController : MonoBehaviour
{
    private static MusicController instance;
    private AudioSource audioSource;
    private AudioClip currentMusicClip;
    private float fadeDuration = 1.5f;
    private float targetVolume = 1.0f;

    // Dictionary to map scene names to music clips
    private Dictionary<string, AudioClip> sceneMusicMap = new Dictionary<string, AudioClip>();

    // Music clips for different scenes
    public AudioClip startScreenMusic;
    public AudioClip homeMusic;
    public AudioClip restaurantMusic;
    public AudioClip kitchenMusic;
    public AudioClip dungeonMusic;
    public AudioClip forestMusic;
    public AudioClip townMusic;
    public AudioClip MiniGameMusic;

    /// <summary>
    /// Initializes the MusicController as a singleton instance and subscribes it to scene loading events.
    /// </summary>
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        SceneManager.sceneLoaded += OnSceneLoaded;

        // Populate the sceneMusicMap
        sceneMusicMap.Add("StartScreen", startScreenMusic);
        sceneMusicMap.Add("Home", homeMusic);
        sceneMusicMap.Add("Restaurant", restaurantMusic);
        sceneMusicMap.Add("Kitchen", kitchenMusic);
        sceneMusicMap.Add("Dungeon1", dungeonMusic);
        sceneMusicMap.Add("Forest", forestMusic);
        sceneMusicMap.Add("Town", townMusic);
        sceneMusicMap.Add("MiniGame", MiniGameMusic);
    }

    /// <summary>
    /// Handles scene loading event and updates the music accordingly.
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioClip nextClip = null;

        // Check if the scene name exists in the dictionary
        if (sceneMusicMap.ContainsKey(scene.name))
        {
            nextClip = sceneMusicMap[scene.name];
        }

        // If a different music clip is found, fade out the current one and play the new one
        if (nextClip != null && nextClip != currentMusicClip)
        {
            StartCoroutine(FadeOutMusic(nextClip));
        }
    }

    /// <summary>
    /// Fades out the current music clip and starts playing the new one.
    /// </summary>
    /// <param name="nextClip"></param>
    /// <returns>An IEnumerator for coroutine handling.</returns>
    IEnumerator FadeOutMusic(AudioClip nextClip)
    {
        float startVolume = audioSource.volume;
        float startTime = Time.time;

        // Fade out the current music
        while (Time.time < startTime + fadeDuration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, (Time.time - startTime) / fadeDuration);
            yield return null;
        }

        audioSource.Stop();
        yield return StartCoroutine(FadeInNewMusic(nextClip));
    }

    /// <summary>
    /// Fades in the new music clip.
    /// </summary>
    /// <param name="nextClip">The AudioClip to fade in.</param>
    /// <returns>An IEnumerator for coroutine handling.</returns>
    IEnumerator FadeInNewMusic(AudioClip nextClip)
    {
        // Start playing the new music
        currentMusicClip = nextClip;
        audioSource.clip = nextClip;
        audioSource.Play();

        float startTime = Time.time;

        // Fade in the new music
        while (Time.time < startTime + fadeDuration)
        {
            audioSource.volume = Mathf.Lerp(0, targetVolume, (Time.time - startTime) / fadeDuration);
            yield return null;
        }
        audioSource.volume = targetVolume;
    }


}
