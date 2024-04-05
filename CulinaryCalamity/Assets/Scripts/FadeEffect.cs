using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    public Image fadeImage;

    public void FadeToBlack(float duration)
    {
        StartCoroutine(FadeToBlackCoroutine(duration));
    }

    public void FadeFromBlack(float duration)
    {
        StartCoroutine(FadeFromBlackCoroutine(duration));
    }

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
