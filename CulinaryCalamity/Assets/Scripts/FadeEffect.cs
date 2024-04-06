using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the fade-to-black and fade-from-black effects for a UI Image.
/// </summary>
public class FadeEffect : MonoBehaviour
{
    /// <summary>
    /// The Image component used for the fade effect.
    /// </summary>
    public Image fadeImage;

    /// <summary>
    /// Initiates a fade to black effect over a specified duration.
    /// </summary>
    /// <param name="duration">The duration of the fade in seconds.</param>
    public void FadeToBlack(float duration)
    {
        StartCoroutine(FadeToBlackCoroutine(duration));
    }

    /// <summary>
    /// Initiates a fade from black effect over a specified duration.
    /// </summary>
    /// <param name="duration">The duration of the fade in seconds.</param>
    public void FadeFromBlack(float duration)
    {
        StartCoroutine(FadeFromBlackCoroutine(duration));
    }

    /// <summary>
    /// Coroutine that gradually changes the alpha of the fadeImage to 1 (black) over the specified duration.
    /// </summary>
    /// <param name="duration">The duration of the fade in seconds.</param>
    /// <returns>IEnumerator for coroutine.</returns>
    private IEnumerator FadeToBlackCoroutine(float duration)
    {
        float alpha = fadeImage.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / duration)
        {
            Color newColor = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, Mathf.Lerp(alpha, 1, t));
            fadeImage.color = newColor;
            yield return null;
        }
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1);
    }

    /// <summary>
    /// Coroutine that gradually changes the alpha of the fadeImage to 0 (transparent) over the specified duration.
    /// </summary>
    /// <param name="duration">The duration of the fade in seconds.</param>
    /// <returns>IEnumerator for coroutine.</returns>
    private IEnumerator FadeFromBlackCoroutine(float duration)
    {
        float alpha = fadeImage.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / duration)
        {
            Color newColor = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, Mathf.Lerp(alpha, 0, t));
            fadeImage.color = newColor;
            yield return null;
        }
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0);
    }

}
